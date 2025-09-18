using System;
using System.Collections.Generic;
using App.Common.AssetSystem.Runtime;
using App.Common.Utilities.Pool.Runtime;
using App.Common.Utilities.Utility.Runtime;
using App.Common.Web.External;
using App.Core.Menu.External.Presenter.States.Dogs.Config;
using App.Menu.UI.External.Presenter.Dto;
using App.Menu.UI.External.View.Dogs;
using Core.Animations.External.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Menu.UI.External.Presenter
{
    public class DogsMenuState : IMenuState, IDisposable
    {
        private const string m_Url = "https://dogapi.dog/api/v2/";

        private readonly DogsView m_View;
        private readonly IAssetManager m_AssetManager;
        private readonly IWebRequestManager m_WebRequestManager;

        private RotateAnimation m_RotateAnimation;
        private DogsConfigController m_ConfigController;
        private BreedInfoWindowPresenter m_InfoWindowPresenter;
        private long m_RequestId = -1;

        private ListPool<BreedPresenter> m_BreedsPool;
        private List<BreedPresenter> m_Breeds;
        private BreedPresenter m_BreedWhoseFactsLoading;

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
            m_RotateAnimation = new RotateAnimation();
            m_ConfigController = new DogsConfigController(m_AssetManager);
            m_ConfigController.Initialize();

            m_InfoWindowPresenter = new BreedInfoWindowPresenter(m_View.BreedInfoWindow);
            m_InfoWindowPresenter.Initialize();

            m_BreedsPool = new ListPool<BreedPresenter>(
                CreateBreedPresenter, 
                10,
                getCallback: presenter => presenter.SetActive(true),
                releaseCallback: presenter => presenter.SetActive(false));
            m_Breeds = new List<BreedPresenter>(10);
        }

        private Optional<BreedPresenter> CreateBreedPresenter()
        {
            var view = Object.Instantiate(m_View.BreedViewPrefab, m_View.DogsContent);
            var presenter = new BreedPresenter(view, OnBreedClick);
            presenter.Initialize();
            return Optional<BreedPresenter>.Success(presenter);
        }

        private void OnBreedClick(BreedPresenter breed)
        {
            if (IsRequestActive())
            {
                if (m_BreedWhoseFactsLoading != null && breed == m_BreedWhoseFactsLoading)
                {
                    return;
                }
                
                m_BreedWhoseFactsLoading?.SetLoadDownloadIconActive(false);
                m_BreedWhoseFactsLoading = null;
                CancelRequest();
            }
            
            m_BreedWhoseFactsLoading = breed;
            var url =$"{m_Url}breeds/{breed.Breed.Id}";
            m_RequestId = m_WebRequestManager.SendGet<FactsResponseDto>(url, (response) =>
            {
                OnFactsRequestComplete(breed, response);
            });
            breed.SetLoadDownloadIconActive(true);
        }

        private void OnFactsRequestComplete(BreedPresenter breed, Optional<FactsResponseDto> dto)
        {
            if (m_BreedWhoseFactsLoading != null && breed == m_BreedWhoseFactsLoading)
            {
                m_BreedWhoseFactsLoading?.SetLoadDownloadIconActive(false);
                m_BreedWhoseFactsLoading = null;
            }
            
            if (!dto.HasValue)
            {
                Debug.LogError("DogsMenuState: No data in facts response");
                return;
            }
            
            var facts = dto.Value.Data.Attributes;
            m_View.BreedInfoWindow.SwitchStateByHeight.SetLessState();
            m_InfoWindowPresenter.SetActive(true);
            m_InfoWindowPresenter.SetInfo(facts);
            m_View.BreedInfoWindow.SwitchStateByHeight.UpdateState();
        }

        private bool IsRequestActive()
        {
            return m_RequestId != -1 && m_WebRequestManager.IsRequestActive(m_RequestId);
        }
        
        private void CancelRequest()
        {
            if (IsRequestActive())
            {
                m_WebRequestManager.Cancel(m_RequestId);
                m_RequestId = -1;
            }
        }

        public void Enter()
        {
            m_View.SetActive(true);
            m_View.BreedInfoWindow.SetActive(false);
            m_View.SetScrollerActive(false);
            var loadingIcon = m_View.LoadingIcon;
            loadingIcon.gameObject.SetActive(true);
            m_RotateAnimation.StartAnimation(loadingIcon);
            foreach (var breed in m_Breeds)
            {
                m_BreedsPool.Release(breed);
            }
            
            m_Breeds.Clear();
            SendBreedsRequest();
        }

        private void SendBreedsRequest()
        {
            var url = m_Url + "breeds";
            m_RequestId = m_WebRequestManager.SendGet<BreedsResponseDto>(url, OnBreedsRequestComplete);
        }

        private void OnBreedsRequestComplete(Optional<BreedsResponseDto> dto)
        {
            if (!dto.HasValue)
            {
                Debug.LogError("DogsMenuState: No data in breeds response");
                return;
            }

            m_View.SetScrollerActive(true);
            m_View.LoadingIcon.gameObject.SetActive(false);
            m_RotateAnimation.StopAnimation();
            var breeds = dto.Value.Data;
            var maxBreeds = m_ConfigController.GetMaxBreedsCount();
            for (int i = 0; i < breeds.Count && i < maxBreeds; ++i)
            {
                var breed = breeds[i];
                var presenter = m_BreedsPool.Get();
                if (!presenter.HasValue)
                {
                    break;
                }

                presenter.Value.SetBreed(breed, i);
                presenter.Value.SetAsLastSibling();
                presenter.Value.SetLoadDownloadIconActive(false);
                m_Breeds.Add(presenter.Value);
            }
        }

        public void Exit()
        {
            m_View.SetActive(false);
            CancelRequest();
        }

        public void Dispose()
        {
            CancelRequest();
        }
    }
}