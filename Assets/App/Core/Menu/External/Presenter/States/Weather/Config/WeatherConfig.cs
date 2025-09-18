using UnityEngine;

namespace App.Core.Menu.External.Presenter.States.Weather.Config
{
    [CreateAssetMenu(fileName = "WeatherConfig", menuName = "Configs/WeatherConfig")]
    public class WeatherConfig : ScriptableObject
    {
        [SerializeField] private long m_RequestInterval;
        
        public long RequestInterval => m_RequestInterval;
    }
}