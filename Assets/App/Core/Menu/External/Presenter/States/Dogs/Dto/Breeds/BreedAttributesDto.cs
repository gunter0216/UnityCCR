using System;
using Newtonsoft.Json;

namespace App.Menu.UI.External.Presenter.Dto
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class BreedAttributesDto
    {
        [JsonProperty("name")]
        private string m_Name;
        
        public string Name => m_Name;
    }
}