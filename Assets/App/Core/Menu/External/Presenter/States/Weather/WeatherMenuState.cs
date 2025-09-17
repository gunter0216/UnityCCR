using System;
using App.Common.AssetSystem.Runtime;
using App.Common.Data.Runtime.Deserializer;
using App.Common.Web.External;
using App.Core.Menu.External.Presenter.States.Weather.Config;
using App.Menu.UI.External.Presenter.Timer;
using App.Menu.UI.External.View.Weather;
using UnityEngine;
using UnityEngine.Networking;

namespace App.Menu.UI.External.Presenter
{
    public class WeatherMenuState : IMenuState, IDisposable
    {
        private const string m_API = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";
        
        private readonly IAssetManager m_AssetManager;
        private readonly WeatherView m_View;
        private readonly IWebRequestManager m_WebRequestManager;

        private WeatherConfigController m_ConfigController;
        private WeatherTimer m_Timer;

        public WeatherMenuState(
            WeatherView view, 
            IAssetManager assetManager, 
            IWebRequestManager webRequestManager)
        {
            m_View = view;
            m_AssetManager = assetManager;
            m_WebRequestManager = webRequestManager;
        }
        
        public void Initialize()
        {
            m_ConfigController = new WeatherConfigController(m_AssetManager);
            m_ConfigController.Initialize();
            
            m_Timer = new WeatherTimer(m_ConfigController, OnTimerTick);
        }

        private void OnTimerTick()
        {
            // var request = UnityWebRequest.Get(m_API);
            // request.timeout = 10;
            // var operation = request.SendWebRequest();
            // operation.completed += (_) =>
            // {
            //     T result = default;
            //     try
            //     {
            //         result = m_Serializer.Deserialize<T>(request.downloadHandler.text);
            //     }
            //     catch (Exception e)
            //     {
            //         Debug.LogError($"LiveOpsPresenter: Error while deserializing response: {e}");
            //         Debug.LogError($"downloadHandler.text: {request.downloadHandler.text}");
            //     }
            //     finally
            //     {
            //         if (request?.result != UnityWebRequest.Result.Success)
            //         {
            //             Debug.LogError($"request result: {request.result} + error: {request.error}");
            //         }
            //         
            //         onComplete(request.result == UnityWebRequest.Result.Success && result != null, new ResponseObject<T>(result, tries));
            //         request.Dispose();
            //     }
            // };
            // todo
        }

        public void Enter()
        {
            m_View.SetActive(true);
            m_Timer.StartTimer();
        }

        public void Exit()
        {
            m_View.SetActive(false);
            m_Timer.StopTimer();
        }

        public void Dispose()
        {
            m_Timer?.Dispose();
        }
    }
}