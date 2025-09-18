using System;
using Newtonsoft.Json;

namespace App.Menu.UI.External.Presenter.Dto
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