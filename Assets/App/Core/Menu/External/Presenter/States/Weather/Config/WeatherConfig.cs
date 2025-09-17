using UnityEngine;

namespace App.Core.Menu.External.Presenter.States.Weather.Config
{
    [CreateAssetMenu(fileName = "WeatherConfig", menuName = "Configs/WeatherConfig")]
    public class WeatherConfig : ScriptableObject
    {
        [SerializeField] private long m_RequestPeriod;
        
        public long RequestPeriod => m_RequestPeriod;
    }
}