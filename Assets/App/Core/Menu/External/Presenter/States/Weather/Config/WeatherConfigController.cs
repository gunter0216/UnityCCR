using App.Common.AssetSystem.Runtime;
using UnityEngine;

namespace App.Core.Menu.External.Presenter.States.Weather.Config
{
    public class WeatherConfigController
    {
        private const string m_AssetKey = "WeatherConfig";

        private readonly IAssetManager m_AssetManager;

        private WeatherConfig m_Config;

        public WeatherConfigController(IAssetManager assetManager)
        {
            m_AssetManager = assetManager;
        }

        public bool Initialize()
        {
            var config = m_AssetManager.LoadSync<WeatherConfig>(new StringKeyEvaluator(m_AssetKey));
            if (!config.HasValue)
            {
                Debug.LogError("[WeatherConfigController] In method Initialize, error load WeatherConfig.");
                return false;
            }

            m_Config = config.Value;

            return true;
        }
     
        public long GetRequestPeriod()
        {
            return m_Config.RequestPeriod;
        }
    }
}