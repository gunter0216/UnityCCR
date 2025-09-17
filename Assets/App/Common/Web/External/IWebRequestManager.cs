using System;
using UnityEngine.Networking;

namespace App.Common.Web.External
{
    public interface IWebRequestManager
    {
        long SendGet(string url, Action<UnityWebRequest> onComplete);
        void CancelAll();
        void Cancel(long id);
        bool IsRequestActive(long id);
    }
}