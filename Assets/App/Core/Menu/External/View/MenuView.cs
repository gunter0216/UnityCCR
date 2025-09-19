using App.Core.Menu.External.View.Clicker;
using App.Core.Menu.External.View.Dogs;
using App.Core.Menu.External.View.Weather;
using UnityEngine;

namespace App.Core.Menu.External.View
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private MenuTabView m_ClickerTab;
        [SerializeField] private MenuTabView m_DogsTab;
        [SerializeField] private MenuTabView m_WeatherTab;
        [SerializeField] private ClickerView m_ClickerView;
        [SerializeField] private DogsView m_DogsView;
        [SerializeField] private WeatherView m_WeatherView;
        
        public MenuTabView ClickerTab => m_ClickerTab;
        public MenuTabView DogsTab => m_DogsTab;
        public MenuTabView WeatherTab => m_WeatherTab;
        public ClickerView ClickerView => m_ClickerView;
        public DogsView DogsView => m_DogsView;
        public WeatherView WeatherView => m_WeatherView;
    }
}