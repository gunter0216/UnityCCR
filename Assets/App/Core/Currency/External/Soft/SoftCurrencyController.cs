using App.Common.Data.Runtime;
using App.Common.Utilities.Utility.Runtime;
using Core.Currency.Calculator.Runtime;
using UnityEngine;

namespace App.Core.Currency.External.Soft
{
    public class SoftCurrencyController : IInitSystem
    {
        private readonly IDataManager m_DataManager;

        private readonly ICurrencyCalculator m_CurrencyCalculator = new CurrencyCalculator();
        
        private SoftCurrencyData m_Data;

        public SoftCurrencyController(IDataManager dataManager)
        {
            m_DataManager = dataManager;
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
            var addResult = m_CurrencyCalculator.Add(m_Data.Value, value);
            if (addResult.Result == CalculationErrors.Success || addResult.Result == CalculationErrors.BiggerThanMax)
            {
                m_Data.Value = addResult.Value;
                return true;
            } 
            
            if (addResult.Result == CalculationErrors.Overflow)
            {
                Debug.LogError("[SoftCurrencyController] Overflow with add operation");
                m_Data.Value = addResult.Value;
                return true;
            }
            
            return false;
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
                Debug.LogError("[SoftCurrencyController] Overflow with Spend operation");
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