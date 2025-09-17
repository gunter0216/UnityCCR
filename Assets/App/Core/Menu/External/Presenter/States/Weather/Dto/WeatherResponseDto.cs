using System;
using Newtonsoft.Json;

namespace App.Menu.UI.External.Presenter.Dto
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class WeatherResponseDto
    {
        [JsonProperty("properties")]
        private WeatherPropertiesDto m_Properties;
        
        public WeatherPropertiesDto Properties => m_Properties;
    }
}