using System;
using Newtonsoft.Json;

namespace App.Menu.UI.External.Presenter.Dto
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class FactsDataDto
    {
        [JsonProperty("id")]
        private string m_Id;
        
        [JsonProperty("attributes")]
        private FactsAttributesDto m_Attributes;
        
        public string Id => m_Id;
        public FactsAttributesDto Attributes => m_Attributes;
    }
}