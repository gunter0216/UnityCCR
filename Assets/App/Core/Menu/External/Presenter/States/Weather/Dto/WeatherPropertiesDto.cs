using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace App.Core.Menu.External.Presenter.States.Weather.Dto
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class WeatherPropertiesDto
    {
        [JsonProperty("periods")]
        private List<WeatherPeriodDto> m_Periods;
        
        public List<WeatherPeriodDto> Periods => m_Periods;
    }
}