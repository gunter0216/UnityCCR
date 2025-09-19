using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Core.Menu.External.View.Weather
{
    public class WeatherView : MonoBehaviour
    {
        [SerializeField] private Image m_WeatherIcon;
        [SerializeField] private TMP_Text m_TemperatureText;
        
        public void SetActive(bool status)
        {
            gameObject.SetActive(status);
        }
        
        public void SetWeatherIcon(Sprite icon)
        {
            m_WeatherIcon.sprite = icon;
        }
        
        public void SetTemperatureText(string text)
        {
            m_TemperatureText.text = text;
        }
    }
}