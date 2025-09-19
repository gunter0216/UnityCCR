using System;
using App.Common.Data.Runtime;
using Newtonsoft.Json;

namespace App.Core.Currency.External.Soft
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class SoftCurrencyData : IData
    {
        public const string CurrencyKey = nameof(SoftCurrencyData);
        
        [JsonProperty("value")] private long m_Value;
        
        public long Value
        {
            get => m_Value;
            set => m_Value = value;
        }
        
        public string Name()
        {
            return CurrencyKey;
        }
    }
}