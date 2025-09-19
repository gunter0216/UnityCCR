using System;
using App.Common.AssetSystem.Runtime;
using App.Common.Audio.External;
using App.Core.Currency.External.Energy;
using App.Core.Currency.External.Soft;
using App.Core.Menu.External.Animations;
using App.Core.Menu.External.Presenter.States.Clicker.Config;
using App.Core.Menu.External.Presenter.States.Clicker.Timers;
using App.Core.Menu.External.View.Clicker;
using UnityEngine;

namespace App.Core.Menu.External.Presenter.States.Clicker
{
    public class ClickerMenuState : IMenuState, IDisposable
    {
        private readonly ClickerView m_View;
        private readonly ISoftAccrualAnimation m_SoftAccrualAnimation;
        private readonly SoftCurrencyController m_SoftCurrencyController;
        private readonly EnergyCurrencyController m_EnergyCurrencyController;
        private readonly IAssetManager m_AssetManager;
        private readonly ISoundManager m_SoundManager;

        private ClickerConfigController m_ConfigController;

        private ClickerAutoCollectionTimer m_AutoCollectionTimer;
        private ClickerEnergyRecoveryTimer m_EnergyRecoveryTimer;

        public ClickerMenuState(ClickerView view,
            ISoftAccrualAnimation softAccrualAnimation,
            SoftCurrencyController softCurrencyController,
            EnergyCurrencyController energyCurrencyController,
            IAssetManager assetManager, 
            ISoundManager soundManager)
        {
            m_View = view;
            m_SoftAccrualAnimation = softAccrualAnimation;
            m_SoftCurrencyController = softCurrencyController;
            m_EnergyCurrencyController = energyCurrencyController;
            m_AssetManager = assetManager;
            m_SoundManager = soundManager;
        }

        public void Initialize()
        {
            m_ConfigController = new ClickerConfigController(m_AssetManager);
            m_ConfigController.Initialize();
            
            m_View.SetClickerButtonCallback(OnButtonClick);
            
            m_AutoCollectionTimer = new ClickerAutoCollectionTimer(m_ConfigController, OnAutoCollectionTimerTick);
            m_EnergyRecoveryTimer = new ClickerEnergyRecoveryTimer(m_ConfigController, OnEnergyRecoveryTimerTick);
            
            m_EnergyRecoveryTimer.StartTimer();
            
            UpdateSoftView();
            UpdateEnergyView();
        }

        private void OnEnergyRecoveryTimerTick()
        {
            m_EnergyCurrencyController.Add(m_ConfigController.GetEnergyRecoveryValue());
            UpdateEnergyView();
        }

        private void OnAutoCollectionTimerTick()
        {
            var energySpend = m_ConfigController.GetAutoCollectionTimerEnergyPrice();
            var softIncome = m_ConfigController.GetAutoCollectionTimerSoftIncome();
            ProcessButtonClick(energySpend, softIncome);
        }

        private void OnButtonClick()
        {
            var energySpend = m_ConfigController.GetClickEnergyPrice();
            var softIncome = m_ConfigController.GetClickSoftIncome();
            ProcessButtonClick(energySpend, softIncome);
        }

        private void ProcessButtonClick(long energySpend, long softIncome)
        {
            if (!m_EnergyCurrencyController.IsEnough(energySpend)) 
            {
                return;
            }
            
            var parent = m_View.ButtonRectTransform;
            var localPosition = new Vector3(0, 100, 0);
            m_SoundManager.Play("TapSound");
            m_SoftAccrualAnimation.PlayAnimation(localPosition, parent, softIncome);
            m_View.ParticleSystem.Play();
            
            m_SoftCurrencyController.Add(softIncome);
            m_EnergyCurrencyController.Spend(energySpend);
            
            UpdateEnergyView();
            UpdateSoftView();
        }

        private void UpdateSoftView()
        {
            var softAmount = m_SoftCurrencyController.GetValue();
            m_View.SetSoftAmountText(softAmount.ToString());
        }

        private void UpdateEnergyView()
        {
            var energyAmount = m_EnergyCurrencyController.GetValue();
            m_View.SetEnergyAmountText(energyAmount.ToString());
        }

        public void Enter()
        {
            m_View.SetActive(true);
            m_AutoCollectionTimer.StartTimer();
        }

        public void Exit()
        {
            m_View.SetActive(false);
            m_AutoCollectionTimer.StopTimer();
        }

        public void Dispose()
        {
            m_AutoCollectionTimer?.Dispose();
            m_EnergyRecoveryTimer?.Dispose();
        }
    }
}