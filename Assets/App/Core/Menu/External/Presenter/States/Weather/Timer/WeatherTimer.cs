using System;
using App.Core.Menu.External.Presenter.States.Weather.Config;
using UniRx;

namespace App.Menu.UI.External.Presenter.Timer
{
    public class WeatherTimer : IDisposable
    {
        private readonly WeatherConfigController m_ConfigController;
        private event Action m_TimerTickCallback;
        private IDisposable m_Timer;

        public WeatherTimer(WeatherConfigController configController, Action timerTickCallback)
        {
            m_ConfigController = configController;
            m_TimerTickCallback = timerTickCallback;
        }
        
        public void StartTimer()
        {
            StopTimer();

            m_Timer = Observable
                .Interval(TimeSpan.FromSeconds(m_ConfigController.GetRequestInterval()))
                .Subscribe(OnTimerTick);
        }
        
        private void OnTimerTick(long _)
        {
            m_TimerTickCallback?.Invoke();
        }
        
        public void StopTimer()
        {
            if (m_Timer != null)
            {
                m_Timer.Dispose();
                m_Timer = null;
            }
        }

        public void Dispose()
        {
            m_Timer?.Dispose();
        }
    }
}