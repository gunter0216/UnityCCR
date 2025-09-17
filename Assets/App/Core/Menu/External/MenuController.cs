using System;
using App.Common.AssetSystem.Runtime;
using App.Common.Data.Runtime;
using App.Common.SceneControllers.Runtime;
using App.Common.Utilities.Utility.Runtime;
using App.Core.Menu.External.Presenter;
using App.Game.Canvases.External;
using Core.Currency.External;
using UnityEngine;

namespace App.Menu.UI.External
{
    public class MenuController : IInitSystem, IDisposable
    {
        private readonly MainCanvas m_MainCanvas;
        private readonly IAssetManager m_AssetManager;
        private readonly IDataManager m_DataManager;
        private readonly ISceneManager m_SceneManager;
        private readonly SoftCurrencyController m_SoftCurrencyController;
        private readonly EnergyCurrencyController m_EnergyCurrencyController;
        
        private MenuPresenter m_Presenter;

        public MenuController(
            MainCanvas mainCanvas, 
            IAssetManager assetManager, 
            IDataManager dataManager, 
            ISceneManager sceneManager, 
            SoftCurrencyController softCurrencyController, 
            EnergyCurrencyController energyCurrencyController)
        {
            m_MainCanvas = mainCanvas;
            m_AssetManager = assetManager;
            m_DataManager = dataManager;
            m_SceneManager = sceneManager;
            m_SoftCurrencyController = softCurrencyController;
            m_EnergyCurrencyController = energyCurrencyController;
        }

        public void Init()
        {
            m_Presenter = new MenuPresenter(
                m_AssetManager, 
                m_MainCanvas,
                m_SoftCurrencyController,
                m_EnergyCurrencyController);
            if (!m_Presenter.Initialize())
            {
                Debug.LogError($"Cant initialize");
            }
        }

        public void Dispose()
        {
            m_Presenter?.Dispose();
        }
    }
}