using System;
using App.Common.Utilities.Utility.Runtime;
using App.Common.Web.External;
using App.Core.Menu.External.Presenter.States.Dogs.Dto.Breeds;
using App.Core.Menu.External.Presenter.States.Dogs.Dto.Facts;

namespace App.Core.Menu.External.Presenter.States.Dogs
{
    public class BreedRequestService
    {
        private const string m_Url = "https://dogapi.dog/api/v2/";
        
        private readonly IWebRequestManager m_WebRequestManager;
        
        private long m_RequestId = -1;

        public BreedRequestService(IWebRequestManager webRequestManager)
        {
            m_WebRequestManager = webRequestManager;
        }

        public void SendBreedsRequest(Action<Optional<BreedsResponseDto>> callback)
        {
            var url = m_Url + "breeds";
            m_RequestId = m_WebRequestManager.SendGet<BreedsResponseDto>(url, callback);
        }

        public void SendBreedFactsRequest(string id, Action<Optional<FactsResponseDto>> callback)
        {
            var url =$"{m_Url}breeds/{id}";
            m_RequestId = m_WebRequestManager.SendGet<FactsResponseDto>(url, callback);
        }

        public bool IsRequestActive()
        {
            return m_RequestId != -1 && m_WebRequestManager.IsRequestActive(m_RequestId);
        }

        public void CancelRequest()
        {
            if (IsRequestActive())
            {
                m_WebRequestManager.Cancel(m_RequestId);
                m_RequestId = -1;
            }
        }
    }
}