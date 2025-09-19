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
        private const int m_RequestTimeout = 10;
        
        private readonly IJsonDeserializer m_JsonDeserializer;
        private readonly RequestIDGenerator m_IDGenerator;
        
        private readonly Queue<RequestData> m_RequestQueue = new();
        private readonly Dictionary<long, RequestData> m_PendingRequests = new();
        
        private bool m_IsProcessingQueue;
        private RequestData m_CurrentRequest;

        public WebRequestManager(IJsonDeserializer jsonDeserializer)
        {
            m_JsonDeserializer = jsonDeserializer;
            m_IDGenerator = new RequestIDGenerator();
        }

        public long SendGet(string url, Action<UnityWebRequest> onComplete)
        {
            var id = m_IDGenerator.GetNextID();
            var requestData = new RequestData
            {
                Id = id,
                RequestFactory = () => 
                {
                    var request = UnityWebRequest.Get(url);
                    request.timeout = m_RequestTimeout;
                    return request;
                },
                OnComplete = onComplete
            };

            EnqueueRequest(requestData);
            return id;
        }

        public long SendGet<T>(Uri uri, Action<Optional<T>> onComplete) where T : class
        {
            return SendGet(uri.AbsoluteUri, onComplete);
        }

        public long SendGet<T>(string url, Action<Optional<T>> onComplete) where T : class
        {
            var id = m_IDGenerator.GetNextID();
            var requestData = new RequestData
            {
                Id = id,
                RequestFactory = () => 
                {
                    var request = UnityWebRequest.Get(url);
                    request.timeout = m_RequestTimeout;
                    return request;
                },
                OnComplete = (request) =>
                {
                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log($"WebRequestManager: Failed to get data from {url}, error: {request.error}");
                        onComplete?.Invoke(Optional<T>.Fail());
                        return;
                    }

                    var result = m_JsonDeserializer.Deserialize<T>(request.downloadHandler.text);
                    if (!result.HasValue)
                    {
                        Debug.Log("WebRequestManager: Failed to deserialize response");
                        onComplete?.Invoke(Optional<T>.Fail());
                        return;
                    }
                        
                    onComplete?.Invoke(result);
                }
            };

            EnqueueRequest(requestData);
            return id;
        }

        public long GetSprite(string url, Action<Optional<Sprite>> onComplete)
        {
            var id = m_IDGenerator.GetNextID();
            var requestData = new RequestData
            {
                Id = id,
                RequestFactory = () => 
                {
                    var request = UnityWebRequestTexture.GetTexture(url);
                    request.timeout = m_RequestTimeout;
                    return request;
                },
                OnComplete = (request) =>
                {
                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log($"WebRequestManager: Failed to download texture from {url}, error: {request.error}");
                        onComplete?.Invoke(Optional<Sprite>.Fail());
                        return;
                    }
                    
                    var texture = DownloadHandlerTexture.GetContent(request);
                    if (texture == null)
                    {
                        Debug.Log($"WebRequestManager: Downloaded texture is null from {url}");
                        onComplete?.Invoke(Optional<Sprite>.Fail());
                        return;
                    }

                    var sprite = Sprite.Create(
                        texture,
                        new Rect(0, 0, texture.width, texture.height),
                        new Vector2(0.5f, 0.5f)
                    );
                    
                    onComplete?.Invoke(Optional<Sprite>.Success(sprite));
                }
            };

            EnqueueRequest(requestData);
            return id;
        }

        public long GetTexture(string url, Action<Optional<Texture2D>> onComplete)
        {
            var id = m_IDGenerator.GetNextID();
            var requestData = new RequestData
            {
                Id = id,
                RequestFactory = () => 
                {
                    var request = UnityWebRequestTexture.GetTexture(url);
                    request.timeout = m_RequestTimeout;
                    return request;
                },
                OnComplete = (request) =>
                {
                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log($"WebRequestManager: Failed to download texture from {url}, error: {request.error}");
                        onComplete?.Invoke(Optional<Texture2D>.Fail());
                        return;
                    }
                    
                    var texture = DownloadHandlerTexture.GetContent(request);
                    if (texture == null)
                    {
                        Debug.Log($"WebRequestManager: Downloaded texture is null from {url}");
                        onComplete?.Invoke(Optional<Texture2D>.Fail());
                        return;
                    }
                    
                    onComplete?.Invoke(Optional<Texture2D>.Success(texture));
                }
            };

            EnqueueRequest(requestData);
            return id;
        }

        private void EnqueueRequest(RequestData requestData)
        {
            m_RequestQueue.Enqueue(requestData);
            m_PendingRequests[requestData.Id] = requestData;
            
            if (!m_IsProcessingQueue)
            {
                ProcessNextRequest();
            }
        }

        private void ProcessNextRequest()
        {
            if (m_RequestQueue.Count == 0)
            {
                m_IsProcessingQueue = false;
                return;
            }

            m_IsProcessingQueue = true;
            var requestData = m_RequestQueue.Dequeue();
            
            if (!m_PendingRequests.ContainsKey(requestData.Id))
            {
                ProcessNextRequest();
                return;
            }

            m_CurrentRequest = requestData;
            
            var request = requestData.RequestFactory();
            var operation = request.SendWebRequest();
            requestData.Request = request;
            
            operation.completed += (_) => 
            {
                try
                {
                    requestData.OnComplete?.Invoke(request);
                }
                catch (Exception e)
                {
                    Debug.LogError($"WebRequestManager: Error while processing request {requestData.Id} completion: {e}");
                }
                finally
                {
                    m_PendingRequests.Remove(requestData.Id);
                    request.Dispose();
                    m_CurrentRequest = null;
                    
                    ProcessNextRequest();
                }
            };
        }

        public bool IsRequestActive(long id)
        {
            return m_PendingRequests.ContainsKey(id);
        }

        public void Cancel(long id)
        {
            if (m_PendingRequests.Remove(id))
            {
                if (m_CurrentRequest?.Id == id && m_CurrentRequest.Request != null)
                {
                    m_CurrentRequest.Request.Abort();
                }
            }
        }

        public void CancelAll()
        {
            var requestIds = new List<long>(m_PendingRequests.Keys);
            foreach (var id in requestIds)
            {
                Cancel(id);
            }
            
            m_RequestQueue.Clear();
        }
    }
}