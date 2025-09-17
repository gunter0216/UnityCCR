using App.Common.AssetSystem.Runtime;
using UnityEngine;

namespace Core.Currency.External
{
    public class EnergyConfigController
    {
        private const string m_AssetKey = "EnergyConfig";
        
        private readonly IAssetManager m_AssetManager;
        
        private EnergyCurrencyConfig m_Config;

        public EnergyConfigController(IAssetManager assetManager)
        {
            m_AssetManager = assetManager;
        }
        
        public bool Initialize()
        {
            var config = m_AssetManager.LoadSync<EnergyCurrencyConfig>(new StringKeyEvaluator(m_AssetKey));
            if (!config.HasValue)
            {
                Debug.LogError("[EnergyConfigController] In method Initialize, error load EnergyConfig.");
                return false;
            }
            
            m_Config = config.Value;

            return true;
        }
        
        public long GetStartValue()
        {
            return m_Config.StartValue;
        }
    }
}