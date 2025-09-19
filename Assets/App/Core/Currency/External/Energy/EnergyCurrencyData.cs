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
        [JsonProperty("initialized")] private bool m_Initialized;
        
        public long Value
        {
            get => m_Value;
            set => m_Value = value;
        }

        public bool Initialized
        {
            get => m_Initialized;
            set => m_Initialized = value;
        }

        public string Name()
        {
            return CurrencyKey;
        }
    }
}