using App.Menu.UI.External.View.Dogs;

namespace App.Menu.UI.External.Presenter
{
    public class DogsMenuState : IMenuState
    {
        private readonly DogsView m_View;
        
        public DogsMenuState(DogsView view)
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