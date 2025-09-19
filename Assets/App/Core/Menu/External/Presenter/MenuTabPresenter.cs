using System;
using App.Core.Menu.External.Presenter.States;
using App.Core.Menu.External.View;

namespace App.Core.Menu.External.Presenter
{
    public class MenuTabPresenter
    {
        private readonly MenuTabView m_View;
        private readonly IMenuState m_State;
        private event Action<MenuTabPresenter> m_ClickCallback;

        public IMenuState State => m_State;
        
        public MenuTabPresenter(
            MenuTabView view, 
            IMenuState state, 
            Action<MenuTabPresenter> clickCallback)
        {
            m_View = view;
            m_State = state;
            m_ClickCallback = clickCallback;
            
            m_View.SetButtonClickCallback(OnClick);
        }
        
        public void Enter()
        {
            m_View.SetActiveState(true);
        }
        
        public void Exit()
        {
            m_View.SetActiveState(false);
        }

        private void OnClick()
        {
            m_ClickCallback?.Invoke(this);
        }
    }
}