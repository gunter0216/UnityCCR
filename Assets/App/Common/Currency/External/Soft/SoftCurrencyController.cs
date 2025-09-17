using App.Common.AssetSystem.Runtime;
using App.Common.Data.Runtime;
using App.Common.Events.External;
using App.Common.Utilities.Utility.Runtime;
using UnityEngine;

namespace Core.Currency.External
{
    public class SoftCurrencyController : IInitSystem
    {
        private readonly IDataManager m_DataManager;
        private readonly EventManager m_EventManager;
        private readonly IAssetManager m_AssetManager;
        
        private SoftCurrencyData m_Data;

        public SoftCurrencyController(IDataManager dataManager, EventManager eventManager, IAssetManager assetManager)
        {
            m_DataManager = dataManager;
            m_EventManager = eventManager;
            m_AssetManager = assetManager;
        }

        public void Init()
        {
            var data = m_DataManager.GetData<SoftCurrencyData>(SoftCurrencyData.CurrencyKey);
            if (!data.HasValue)
            {
                Debug.LogError("[SoftCurrencyController] unexpected error: m_SoftCurrencyData is not exist");
                return;
            }
            
            m_Data = data.Value;
        }

        public long GetValue()
        {
            return m_Data.Value;
        }

        public bool Add(long value)
        {
            m_Data.Value += value;
            
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