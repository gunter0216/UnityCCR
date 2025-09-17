using System;
using App.Common.Utilities.Utility.Runtime;
using UnityEngine;
using UnityEngine.Networking;

namespace App.Common.Web.External
{
    public interface IWebRequestManager
    {
        long SendGet(string url, Action<UnityWebRequest> onComplete);
        long SendGet<T>(string url, Action<Optional<T>> onComplete) where T : class;
        void CancelAll();
        void Cancel(long id);
        bool IsRequestActive(long id);
        long GetSprite(string url, Action<Optional<Sprite>> onComplete);
        long GetTexture(string url, Action<Optional<Texture2D>> onComplete);
    }
}