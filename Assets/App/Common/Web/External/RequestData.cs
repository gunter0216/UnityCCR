using System;
using UnityEngine.Networking;

namespace App.Common.Web.External
{
    public class RequestData
    {
        private long m_Id;
        private Func<UnityWebRequest> m_RequestFactory;
        private Action<UnityWebRequest> m_OnComplete;
        private UnityWebRequest m_Request;

        public long Id
        {
            get => m_Id;
            set => m_Id = value;
        }

        public Func<UnityWebRequest> RequestFactory
        {
            get => m_RequestFactory;
            set => m_RequestFactory = value;
        }

        public Action<UnityWebRequest> OnComplete
        {
            get => m_OnComplete;
            set => m_OnComplete = value;
        }

        public UnityWebRequest Request
        {
            get => m_Request;
            set => m_Request = value;
        }
    }
}