using System;
using App.Common.AssetSystem.Runtime;
using App.Common.Web.External;
using App.Core.Menu.External.Animations;
using App.Core.Menu.External.Presenter.States.Dogs.Config;
using App.Core.Menu.External.View.Dogs;

namespace App.Core.Menu.External.Presenter.States.Dogs
{
    public class DogsMenuState : IMenuState, IDisposable
    {
        private readonly DogsView m_View;
        private readonly IAssetManager m_AssetManager;
        private readonly IWebRequestManager m_WebRequestManager;

        private RotateAnimation m_RotateAnimation;
        private DogsConfigController m_ConfigController;
        private BreedInfoWindowPresenter m_InfoWindowPresenter;
        
        private BreedTablePresenter m_TablePresenter;
        private BreedRequestService m_RequestService;

        public DogsMenuState(
            DogsView view, 
            IAssetManager assetManager, 
            IWebRequestManager webRequestManager)
        {
            m_View = view;
            m_AssetManager = assetManager;
            m_WebRequestManager = webRequestManager;
        }
        
        public void Initialize()
        {
            m_RequestService = new BreedRequestService(m_WebRequestManager);
            
            m_RotateAnimation = new RotateAnimation();
            
            m_ConfigController = new DogsConfigController(m_AssetManager);
            m_ConfigController.Initialize();

            m_InfoWindowPresenter = new BreedInfoWindowPresenter(m_View.BreedInfoWindow);
            m_InfoWindowPresenter.Initialize();

            m_TablePresenter = new BreedTablePresenter(
                m_View, 
                m_ConfigController, 
                m_RequestService,
                m_InfoWindowPresenter,
                OnTableUpdated);
            m_TablePresenter.Initialize();
        }

        public void Enter()
        {
            m_View.SetActive(true);
            m_View.SetBreedsTableActive(false);

            ActivateLoadingIcon();
            
            m_InfoWindowPresenter.SetActive(false);

            m_TablePresenter.ClearTable();
            m_TablePresenter.UpdateTable();
        }

        private void OnTableUpdated()
        {
            DeactivateLoadingIcon();
        }

        private void DeactivateLoadingIcon()
        {
            m_RotateAnimation.StopAnimation();
            m_View.LoadingIcon.gameObject.SetActive(false);
        }

        private void ActivateLoadingIcon()
        {
            var loadingIcon = m_View.LoadingIcon;
            loadingIcon.gameObject.SetActive(true);
            m_RotateAnimation.StartAnimation(loadingIcon);
        }

        public void Exit()
        {
            m_View.SetActive(false);
            CancelRequest();
        }

        private void CancelRequest()
        {
            m_RequestService.CancelRequest();
        }

        public void Dispose()
        {
            CancelRequest();
        }
    }
}