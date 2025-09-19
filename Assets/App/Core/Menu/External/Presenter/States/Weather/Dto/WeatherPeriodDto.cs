using System;
using Newtonsoft.Json;

namespace App.Core.Menu.External.Presenter.States.Weather.Dto
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class WeatherPeriodDto
    {
        [JsonProperty("name")]
        private string m_Name;
        
        [JsonProperty("icon")]
        private string m_Icon;
        
        [JsonProperty("temperature")]
        private int m_Temperature;
        
        [JsonProperty("temperatureUnit")]
        private string m_TemperatureUnit;
        
        public string Icon => m_Icon;
        public int Temperature => m_Temperature;
        public string TemperatureUnit => m_TemperatureUnit;
        public string Name => m_Name;
    }
}