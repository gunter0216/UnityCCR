using System;
using Newtonsoft.Json;

namespace App.Core.Menu.External.Presenter.States.Dogs.Dto.Facts
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class FactsResponseDto
    {
        [JsonProperty("data")]
        private FactsDataDto m_Data;
        
        public FactsDataDto Data => m_Data;
    }
}