using System;
using System.Collections.Generic;
using App.Common.Data.Runtime.Deserializer;
using App.Common.Utilities.Utility.Runtime;
using UnityEngine;
using UnityEngine.Networking;

namespace App.Common.Web.External
{
    public class WebRequestManager : IWebRequestManager
    {
        private readonly IJsonDeserializer m_JsonDeserializer;
        private readonly RequestIDGenerator m_IDGenerator;
        
        private readonly Dictionary<long, UnityWebRequest> m_Requests = new();

        public WebRequestManager(IJsonDeserializer jsonDeserializer)
        {
            m_JsonDeserializer = jsonDeserializer;
            
            m_IDGenerator = new RequestIDGenerator();
        }

        public long SendGet(string url, Action<UnityWebRequest> onComplete)
        {
            var request = UnityWebRequest.Get(url);
            request.timeout = 10;
            return StartRequest(request, onComplete);
        }

        public long SendGet<T>(string url, Action<Optional<T>> onComplete) where T : class
        {
            var id = SendGet(url, (request) =>
            {
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"WebRequestManager: Failed to get data from {url}, error: {request.error}");
                    onComplete?.Invoke(Optional<T>.Fail());
                    return;
                }

                var result = m_JsonDeserializer.Deserialize<T>(request.downloadHandler.text);
                if (!result.HasValue)
                {
                    Debug.LogError("WebRequestManager: Failed to deserialize response");
                    onComplete?.Invoke(Optional<T>.Fail());
                    return;
                }
                    
                onComplete?.Invoke(result);
            });

            return id;
        }

        public long SendGet<T>(Uri uri, Action<Optional<T>> onComplete) where T : class
        {
            return SendGet(uri.AbsoluteUri, onComplete);
        }

        public long GetSprite(string url, Action<Optional<Sprite>> onComplete)
        {
            var id = GetTexture(url, (result) =>
            {
                if (!result.HasValue)
                {
                    onComplete?.Invoke(Optional<Sprite>.Fail());
                    return;
                }

                var texture = result.Value;
                var sprite = Sprite.Create(
                    texture,
                    new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f)
                );
                
                onComplete?.Invoke(Optional<Sprite>.Success(sprite));
            });

            return id;
        }

        public long GetTexture(string url, Action<Optional<Texture2D>> onComplete)
        {
            var request = UnityWebRequestTexture.GetTexture(url);
            request.timeout = 10;
            return StartRequest(request, (_) =>
            {
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"WebRequestManager: Failed to download texture from {url}, error: {request.error}");
                    onComplete?.Invoke(Optional<Texture2D>.Fail());
                    return;
                }
                
                var texture = DownloadHandlerTexture.GetContent(request);
                if (texture == null)
                {
                    Debug.LogError($"WebRequestManager: Downloaded texture is null from {url}");
                    onComplete?.Invoke(Optional<Texture2D>.Fail());
                }
                
                onComplete?.Invoke(Optional<Texture2D>.Success(texture));
            });
        }

        private long StartRequest(UnityWebRequest request, Action<UnityWebRequest> onComplete)
        {
            var id = m_IDGenerator.GetNextID();
            var operation = request.SendWebRequest();
            m_Requests[id] = request;
            
            operation.completed += (_) => 
            {
                try
                {
                    onComplete?.Invoke(request);
                }
                catch (Exception e)
                {
                    Debug.LogError($"WebRequestManager: Error while processing request completion: {e}");
                }
                finally
                {
                    m_Requests.Remove(id);
                    request.Dispose();
                }
            };
            
            return id;
        }

        public bool IsRequestActive(long id)
        {
            if (!m_Requests.TryGetValue(id, out var request))
            {
                return false;
            }
            
            return !request.isDone;
        }

        public void CancelAll()
        {
            foreach (var key in new List<long>(m_Requests.Keys))
            {
                Cancel(key);
            }
        }

        public void Cancel(long id)
        {
            if (m_Requests.TryGetValue(id, out var request))
            {
                request.Abort();
                m_Requests.Remove(id);
            }
        }
    }
}