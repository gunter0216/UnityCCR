using System;
using Newtonsoft.Json;

namespace App.Core.Menu.External.Presenter.States.Weather.Dto
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