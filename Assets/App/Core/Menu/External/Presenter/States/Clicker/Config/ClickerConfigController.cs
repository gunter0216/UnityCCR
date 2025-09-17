using App.Common.AssetSystem.Runtime;
using Core.Currency.External;
using UnityEngine;

namespace App.Menu.UI.External.Presenter
{
    public class ClickerConfigController
    {
        private const string m_AssetKey = "ClickerConfig";

        private readonly IAssetManager m_AssetManager;

        private ClickerConfig m_Config;

        public ClickerConfigController(IAssetManager assetManager)
        {
            m_AssetManager = assetManager;
        }

        public bool Initialize()
        {
            var config = m_AssetManager.LoadSync<ClickerConfig>(new StringKeyEvaluator(m_AssetKey));
            if (!config.HasValue)
            {
                Debug.LogError("[ClickerConfigController] In method Initialize, error load ClickerConfig.");
                return false;
            }

            m_Config = config.Value;

            return true;
        }
        
        public long GetAutoCollectionTimerPeriod()
        {
            return m_Config.AutoCollectionTimerPeriod;
        }
        
        public long GetAutoCollectionTimerEnergyPrice()
        {
            return m_Config.AutoCollectionTimerEnergyPrice;
        }
        
        public long GetAutoCollectionTimerSoftIncome()
        {
            return m_Config.AutoCollectionTimerSoftIncome;
        }
        
        public long GetClickEnergyPrice()
        {
            return m_Config.ClickEnergyPrice;
        }
        
        public long GetClickSoftIncome()
        {
            return m_Config.ClickSoftIncome;
        }
        
        public long GetEnergyRecoveryPeriod()
        {
            return m_Config.EnergyRecoveryPeriod;
        }
        
        public long GetEnergyRecoveryValue()
        {
            return m_Config.EnergyRecoveryValue;
        }
    }
}