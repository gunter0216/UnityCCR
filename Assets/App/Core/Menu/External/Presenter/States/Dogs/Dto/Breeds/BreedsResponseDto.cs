using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace App.Core.Menu.External.Presenter.States.Dogs.Dto.Breeds
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class BreedsResponseDto
    {
        [JsonProperty("data")]
        private List<BreedDto> m_Data;
        
        public List<BreedDto> Data => m_Data;
    }
}
