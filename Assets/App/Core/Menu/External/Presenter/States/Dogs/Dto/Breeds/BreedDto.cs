using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace App.Menu.UI.External.Presenter.Dto
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class BreedDto
    {
        [JsonProperty("id")]
        private string m_Id;
        
        [JsonProperty("attributes")]
        private BreedAttributesDto m_Attributes;
        
        public string Id => m_Id;
        public BreedAttributesDto Attributes => m_Attributes;
    }
}