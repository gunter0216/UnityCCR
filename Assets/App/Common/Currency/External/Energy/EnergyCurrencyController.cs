using App.Common.AssetSystem.Runtime;
using App.Common.Data.Runtime;
using App.Common.Events.External;
using App.Common.Utilities.Utility.Runtime;
using UnityEngine;

namespace Core.Currency.External
{
    public class EnergyCurrencyController : IInitSystem
    {
        private readonly IDataManager m_DataManager;
        private readonly EventManager m_EventManager;
        private readonly IAssetManager m_AssetManager;
        
        private EnergyCurrencyData m_Data;
        private EnergyConfigController m_ConfigController;

        public EnergyCurrencyController(IDataManager dataManager, EventManager eventManager, IAssetManager assetManager)
        {
            m_DataManager = dataManager;
            m_EventManager = eventManager;
            m_AssetManager = assetManager;
        }

        public void Init()
        {
            m_ConfigController = new EnergyConfigController(m_AssetManager);
            m_ConfigController.Initialize();
            
            var data = m_DataManager.GetData<EnergyCurrencyData>(EnergyCurrencyData.CurrencyKey);
            if (!data.HasValue)
            {
                Debug.LogError("[EnergyCurrencyController] unexpected error: m_EnergyCurrencyData is not exist");
                return;
            }
            
            m_Data = data.Value;
            m_Data.Value = m_ConfigController.GetStartValue();
        }

        public long GetValue()
        {
            return m_Data.Value;
        }

        public bool Add(long value)
        {
            m_Data.Value += value;
            var maxValue = m_ConfigController.GetMaxValue();
            m_Data.Value = m_Data.Value > maxValue ? maxValue : m_Data.Value; 
            
            return true;
        }
        
        public bool Spend(long value)
        {
            m_Data.Value -= value;
            
            return true;
        }
        
        public bool IsEnough(long value)
        {
            return m_Data.Value >= value;
        }
    }
}