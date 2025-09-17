using System;
using App.Common.Data.Runtime;
using Newtonsoft.Json;

namespace Core.Currency.External
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class EnergyCurrencyData : IData
    {
        public const string CurrencyKey = nameof(EnergyCurrencyData);
        
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