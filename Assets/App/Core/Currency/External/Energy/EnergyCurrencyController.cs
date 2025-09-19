using App.Common.AssetSystem.Runtime;
using App.Common.Data.Runtime;
using App.Common.Utilities.Utility.Runtime;
using Core.Currency.Calculator.Runtime;
using UnityEngine;

namespace Core.Currency.External
{
    public class EnergyCurrencyController : IInitSystem
    {
        private readonly IDataManager m_DataManager;
        private readonly IAssetManager m_AssetManager;
        
        private readonly ICurrencyCalculator m_CurrencyCalculator = new CurrencyCalculator();
        
        private EnergyCurrencyData m_Data;
        private EnergyConfigController m_ConfigController;

        public EnergyCurrencyController(IDataManager dataManager, IAssetManager assetManager)
        {
            m_DataManager = dataManager;
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
            if (!m_Data.Initialized)
            {
                m_Data.Value = m_ConfigController.GetStartValue();
                m_Data.Initialized = true;
            }
        }

        public long GetValue()
        {
            return m_Data.Value;
        }

        public bool Add(long value)
        {
            var addResult = m_CurrencyCalculator.Add(m_Data.Value, value);
            if (addResult.Result == CalculationErrors.Success || addResult.Result == CalculationErrors.BiggerThanMax)
            {
                m_Data.Value = addResult.Value;
            } 
            else if (addResult.Result == CalculationErrors.Overflow)
            {
                Debug.LogError("[EnergyCurrencyController] Overflow with add operation");
                m_Data.Value = addResult.Value;
            }
            else
            {
                return false;
            }
            
            var maxValue = m_ConfigController.GetMaxValue();
            m_Data.Value = m_Data.Value > maxValue ? maxValue : m_Data.Value; 
            
            return true;
        }
        
        public bool Spend(long value)
        {
            var subtractValue = m_CurrencyCalculator.Subtract(m_Data.Value, value);
            
            if (subtractValue.Result == CalculationErrors.Success)
            {
                m_Data.Value = subtractValue.Value;
                return true;
            }

            if (subtractValue.Result == CalculationErrors.Overflow)
            {
                Debug.LogError("[EnergyCurrencyController] Overflow with Spend operation");
                return false;
            }
            
            return true;
        }
        
        public bool IsEnough(long value)
        {
            return m_Data.Value >= value;
        }
    }
}