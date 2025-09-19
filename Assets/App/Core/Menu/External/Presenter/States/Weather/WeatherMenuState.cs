using System;
using App.Common.AssetSystem.Runtime;
using App.Common.Utilities.Utility.Runtime;
using App.Common.Web.External;
using App.Core.Menu.External.Presenter.States.Weather.Config;
using App.Core.Menu.External.Presenter.States.Weather.Dto;
using App.Core.Menu.External.Presenter.States.Weather.Timer;
using App.Core.Menu.External.View.Weather;
using UnityEngine;

namespace App.Core.Menu.External.Presenter.States.Weather
{
    public class WeatherMenuState : IMenuState, IDisposable
    {
        private const string m_Url = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";
        
        private readonly IAssetManager m_AssetManager;
        private readonly WeatherView m_View;
        private readonly IWebRequestManager m_WebRequestManager;

        private WeatherConfigController m_ConfigController;
        private WeatherTimer m_Timer;
        private long m_RequestId = -1;

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

        private bool IsRequestActive()
        {
            return m_RequestId != -1 && m_WebRequestManager.IsRequestActive(m_RequestId);
        }
        
        private void CancelRequest()
        {
            if (IsRequestActive())
            {
                m_WebRequestManager.Cancel(m_RequestId);
                m_RequestId = -1;
            }
        }
        
        private void OnTimerTick()
        {
            if (IsRequestActive())
            {
                return;
            }
            
            SendRequest();
        }

        private void SendRequest()
        {
            m_RequestId = m_WebRequestManager.SendGet<WeatherResponseDto>(m_Url, OnRequestComplete);
        }

        private void OnRequestComplete(Optional<WeatherResponseDto> dto)
        {
            if (!dto.HasValue)
            {
                return;
            }

            var periods = dto.Value?.Properties?.Periods;
            if (periods == null || periods.Count == 0)
            {
                Debug.LogError("WeatherMenuState: No periods in response");
                return;
            }

            var today = periods[0];
            m_View.SetTemperatureText($"{today.Name} - {today.Temperature}{today.TemperatureUnit}");
            
            m_RequestId = m_WebRequestManager.GetSprite(today.Icon, OnIconRequestComplete);
        }

        private void OnIconRequestComplete(Optional<Sprite> sprite)
        {
            if (!sprite.HasValue)
            {
                return;
            }
            
            m_View.SetWeatherIcon(sprite.Value);
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
            CancelRequest();
        }

        public void Dispose()
        {
            m_Timer?.Dispose();
            CancelRequest();
        }
    }
}