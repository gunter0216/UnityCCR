using System;
using App.Menu.UI.External.Animations;
using App.Menu.UI.External.View.Clicker;
using Core.Currency.External;
using UniRx;
using UnityEngine;

namespace App.Menu.UI.External.Presenter
{
    public class ClickerMenuState : IMenuState, IDisposable
    {
        private readonly ClickerView m_View;
        private readonly ISoftAccrualAnimation m_SoftAccrualAnimation;
        private readonly SoftCurrencyController m_SoftCurrencyController;
        private readonly EnergyCurrencyController m_EnergyCurrencyController;
        
        private IDisposable m_TimerSubscription;

        public ClickerMenuState(
            ClickerView view,
            ISoftAccrualAnimation softAccrualAnimation, 
            SoftCurrencyController softCurrencyController,
            EnergyCurrencyController energyCurrencyController)
        {
            m_View = view;
            m_SoftAccrualAnimation = softAccrualAnimation;
            m_SoftCurrencyController = softCurrencyController;
            m_EnergyCurrencyController = energyCurrencyController;
        }

        public void Initialize()
        {
            m_View.SetClickerButtonCallback(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if (!m_EnergyCurrencyController.IsEnough(1)) 
            {
                return;
            }
            
            var parent = m_View.ButtonRectTransform;
            var localPosition = new Vector3(0, 100, 0);
            m_SoftAccrualAnimation.PlayAnimation(localPosition, parent, 1);
            m_SoftCurrencyController.Add(1);
            m_EnergyCurrencyController.Spend(1);
            
            UpdateEnergyView();
            UpdateSoftView();
        }
        
        private void StartTimer()
        {
            StopTimer();

            m_TimerSubscription = Observable
                .Interval(TimeSpan.FromSeconds(3))
                .Subscribe(OnTimerTick);
        }
        
        private void OnTimerTick(long _)
        {
            // Логика, которая выполняется при каждом тике таймера
            Debug.Log("Таймер сработал!");
        }

        private void StopTimer()
        {
            if (m_TimerSubscription != null)
            {
                m_TimerSubscription.Dispose();
                m_TimerSubscription = null;
            }
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
            StartTimer();
        }

        public void Exit()
        {
            m_View.SetActive(false);
            StopTimer();
        }

        public void Dispose()
        {
            m_TimerSubscription?.Dispose();
        }
    }
}