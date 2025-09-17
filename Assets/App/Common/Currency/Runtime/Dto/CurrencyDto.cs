using System;
using Newtonsoft.Json;

namespace Core.Currency.Runtime.Dto
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class CurrencyDto
    {
        [JsonProperty("key")] private string m_Key;
        [JsonProperty("min")] private long m_MinValue;
        [JsonProperty("max")] private long m_MaxValue;
        [JsonProperty("start")] private int m_StartValue;
        
        public string Key => m_Key;
        public long MinValue => m_MinValue;
        public long MaxValue => m_MaxValue;
        public long StartValue => m_StartValue;
    }
}