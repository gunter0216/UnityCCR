using System;
using App.Common.AssetSystem.Runtime;
using App.Common.Audio.External;
using App.Common.Web.External;
using App.Core.Canvases.External;
using App.Core.Currency.External.Energy;
using App.Core.Currency.External.Soft;
using App.Core.Menu.External.Animations;
using App.Core.Menu.External.Presenter.Fabric;
using App.Core.Menu.External.Presenter.States;
using App.Core.Menu.External.Presenter.States.Clicker;
using App.Core.Menu.External.Presenter.States.Dogs;
using App.Core.Menu.External.Presenter.States.Weather;
using App.Core.Menu.External.View;
using UnityEngine;

namespace App.Core.Menu.External.Presenter
{
    public class MenuPresenter : IDisposable
    {
        private readonly IAssetManager m_AssetManager;
        private readonly ICanvas m_Canvas;
        private readonly SoftCurrencyController m_SoftCurrencyController;
        private readonly EnergyCurrencyController m_EnergyCurrencyController;
        private readonly ISoundManager m_SoundManager;
        private readonly IWebRequestManager m_WebRequestManager;

        private MenuView m_View;
        
        private MenuTabPresenter m_ClickerTabPresenter;
        private MenuTabPresenter m_WeatherTabPresenter;
        private MenuTabPresenter m_DogsTabPresenter;
        
        private ClickerMenuState m_ClickerMenuState;
        private WeatherMenuState m_WeatherMenuState;
        private DogsMenuState m_DogsMenuState;
        
        private IMenuState m_CurrentState;
        private MenuTabPresenter m_CurrentTabPresenter;
        
        private SoftAccrualAnimation m_SoftAccrualAnimation;

        public MenuPresenter(
            IAssetManager assetManager,
            ICanvas canvas,
            SoftCurrencyController softCurrencyController,
            EnergyCurrencyController energyCurrencyController,
            ISoundManager soundManager, 
            IWebRequestManager webRequestManager)
        {
            m_AssetManager = assetManager;
            m_Canvas = canvas;
            m_SoftCurrencyController = softCurrencyController;
            m_EnergyCurrencyController = energyCurrencyController;
            m_SoundManager = soundManager;
            m_WebRequestManager = webRequestManager;
        }

        public bool Initialize()
        {
            if (!CreateView())
            {
                return false;
            }

            m_SoftAccrualAnimation = new SoftAccrualAnimation(m_AssetManager, m_Canvas);
            m_SoftAccrualAnimation.Initialize();

            m_ClickerMenuState = new ClickerMenuState(
                m_View.ClickerView, 
                m_SoftAccrualAnimation,
                m_SoftCurrencyController,
                m_EnergyCurrencyController,
                m_AssetManager,
                m_SoundManager);
            m_WeatherMenuState = new WeatherMenuState(
                m_View.WeatherView, 
                m_AssetManager,
                m_WebRequestManager);
            m_DogsMenuState = new DogsMenuState(
                m_View.DogsView,
                m_AssetManager,
                m_WebRequestManager);
            
            m_ClickerMenuState.Initialize();
            m_WeatherMenuState.Initialize();
            m_DogsMenuState.Initialize();

            m_ClickerTabPresenter = new MenuTabPresenter(
                m_View.ClickerTab,
                m_ClickerMenuState,
                OnTabClick);
            
            m_WeatherTabPresenter = new MenuTabPresenter(
                m_View.WeatherTab,
                m_WeatherMenuState,
                OnTabClick);
            
            m_DogsTabPresenter = new MenuTabPresenter(
                m_View.DogsTab,
                m_DogsMenuState,
                OnTabClick);
            
            m_ClickerMenuState.Enter();
            m_ClickerTabPresenter.Enter();
            
            m_CurrentState = m_ClickerMenuState;
            m_CurrentTabPresenter = m_ClickerTabPresenter;

            return true;
        }

        private bool CreateView()
        {
            var menuViewCreator = new MenuViewCreator(m_AssetManager, m_Canvas);
            var view = menuViewCreator.Create();
            if (!view.HasValue)
            {
                Debug.LogError("Cant create MenuView");
                return false;
            }
            
            m_View = view.Value;
            return true;
        }
        
        private void OnTabClick(MenuTabPresenter tabPresenter)
        {
            if (m_CurrentState == tabPresenter.State)
            {
                return;
            }
            
            m_CurrentState?.Exit();
            m_CurrentTabPresenter?.Exit();
            
            m_CurrentState = tabPresenter.State;
            m_CurrentTabPresenter = tabPresenter;
            
            m_CurrentState.Enter();
            m_CurrentTabPresenter.Enter();
        }

        public void Dispose()
        {
            m_SoftAccrualAnimation?.Dispose();
            m_ClickerMenuState?.Dispose();
            m_WeatherMenuState?.Dispose();
            m_DogsMenuState?.Dispose();
        }
    }
}