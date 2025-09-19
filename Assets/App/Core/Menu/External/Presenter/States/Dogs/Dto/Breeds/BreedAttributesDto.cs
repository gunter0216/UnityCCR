using System;
using Newtonsoft.Json;

namespace App.Core.Menu.External.Presenter.States.Dogs.Dto.Breeds
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