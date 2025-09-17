using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Currency.Runtime.Dto
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class CurrenciesDto
    {
        [JsonProperty("currencies")] 
        private CurrencyDto[] m_Currencies;
        
        public IReadOnlyList<CurrencyDto> Currencies => m_Currencies;
    }
}