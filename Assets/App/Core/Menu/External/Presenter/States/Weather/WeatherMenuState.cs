using App.Menu.UI.External.View.Weather;

namespace App.Menu.UI.External.Presenter
{
    public class WeatherMenuState : IMenuState
    {
        private readonly WeatherView m_View;

        public WeatherMenuState(WeatherView view)
        {
            m_View = view;
        }
        
        public void Initialize()
        {
            
        }

        public void Enter()
        {
            m_View.SetActive(true);
        }

        public void Exit()
        {
            m_View.SetActive(false);
        }
    }
}