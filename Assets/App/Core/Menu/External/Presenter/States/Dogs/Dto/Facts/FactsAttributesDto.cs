using System;
using Newtonsoft.Json;

namespace App.Menu.UI.External.Presenter.Dto
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class FactsAttributesDto
    {
        [JsonProperty("name")]
        private string m_Name;
        
        [JsonProperty("description")]
        private string m_Description;
        
        public string Name => m_Name;
        public string Description => m_Description;
    }
}