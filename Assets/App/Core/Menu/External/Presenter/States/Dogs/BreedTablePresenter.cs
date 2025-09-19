using System;
using System.Collections.Generic;
using App.Common.Utilities.Pool.Runtime;
using App.Common.Utilities.Utility.Runtime;
using App.Core.Menu.External.Presenter.States.Dogs.Config;
using App.Core.Menu.External.Presenter.States.Dogs.Dto.Breeds;
using App.Core.Menu.External.Presenter.States.Dogs.Dto.Facts;
using App.Core.Menu.External.View.Dogs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Core.Menu.External.Presenter.States.Dogs
{
    public class BreedTablePresenter
    {
        private readonly DogsView m_View;
        private readonly DogsConfigController m_ConfigController;
        private readonly BreedRequestService m_RequestService;
        private readonly BreedInfoWindowPresenter m_InfoWindowPresenter;
        private event Action m_OnTableUpdated;

        private List<BreedPresenter> m_Breeds;
        private ListPool<BreedPresenter> m_BreedsPool;
        private BreedPresenter m_BreedWhoseFactsLoading;

        public BreedTablePresenter(
            DogsView view, 
            DogsConfigController configController,
            BreedRequestService requestService,
            BreedInfoWindowPresenter infoWindowPresenter,
            Action onTableUpdated)
        {
            m_View = view;
            m_ConfigController = configController;
            m_RequestService = requestService;
            m_InfoWindowPresenter = infoWindowPresenter;
            m_OnTableUpdated = onTableUpdated;
        }
        
        public void Initialize()
        {
            var capacity = (int)m_ConfigController.GetMaxBreedsCount();
            m_BreedsPool = new ListPool<BreedPresenter>(
                CreateBreedPresenter, 
                capacity,
                getCallback: presenter => presenter.SetActive(true),
                releaseCallback: presenter => presenter.SetActive(false));
            m_Breeds = new List<BreedPresenter>(capacity);
        }
        
        private Optional<BreedPresenter> CreateBreedPresenter()
        {
            var view = Object.Instantiate(m_View.BreedViewPrefab, m_View.DogsContent);
            var presenter = new BreedPresenter(view, OnBreedClick);
            presenter.Initialize();
            return Optional<BreedPresenter>.Success(presenter);
        }

        public void ClearTable()
        {
            foreach (var breed in m_Breeds)
            {
                m_BreedsPool.Release(breed);
            }
            
            m_Breeds.Clear();
        }

        public void UpdateTable()
        {
            SendBreedsRequest();
        }
        
        private void SendBreedsRequest()
        {
            m_RequestService.SendBreedsRequest(OnBreedsRequestComplete);
        }
        
        private void OnBreedsRequestComplete(Optional<BreedsResponseDto> dto)
        {
            if (!dto.HasValue)
            {
                Debug.Log("DogsMenuState: No data in breeds response");
                return;
            }

            m_View.SetBreedsTableActive(true);
            
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
                m_Breeds.Add(presenter.Value);
            }
            
            m_OnTableUpdated?.Invoke();
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
                m_RequestService.CancelRequest();
            }
            
            m_BreedWhoseFactsLoading = breed;
            m_RequestService.SendBreedFactsRequest(breed.Breed.Id, (response) =>
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
                Debug.Log("DogsMenuState: No data in facts response");
                return;
            }
            
            var facts = dto.Value.Data.Attributes;
            m_InfoWindowPresenter.OpenWindow(facts);
        }
        
        private bool IsRequestActive()
        {
            return m_RequestService.IsRequestActive();
        }
    }
}