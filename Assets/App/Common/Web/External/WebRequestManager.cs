using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace App.Common.Web.External
{
    public class WebRequestManager : IWebRequestManager
    {
        private readonly RequestIDGenerator m_IDGenerator = new RequestIDGenerator();
        
        private readonly Dictionary<long, UnityWebRequest> m_Requests = new();
        
        public long SendGet(string url, Action<UnityWebRequest> onComplete)
        {
            var request = UnityWebRequest.Get(url);
            request.timeout = 10;
            return StartRequest(request, onComplete);
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