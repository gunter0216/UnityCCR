using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace App.Core.Menu.External.View.Clicker
{
    public class ClickerView : MonoBehaviour
    {
        [SerializeField] private RectTransform m_ButtonRectTransform;
        [SerializeField] private Button m_ClickerButton;
        [SerializeField] private TMP_Text m_ClickerButtonText;
        [SerializeField] private TMP_Text m_EnergyAmountText;
        [SerializeField] private TMP_Text m_SoftAmountText;
        [SerializeField] private ParticleSystem m_ParticleSystem;
        
        public ParticleSystem ParticleSystem => m_ParticleSystem;
        
        public RectTransform ButtonRectTransform => m_ButtonRectTransform;
        
        public void SetActive(bool status)
        {
            gameObject.SetActive(status);
        }
        
        public void SetClickerButtonCallback(UnityAction callback)
        {
            m_ClickerButton.onClick.RemoveAllListeners();
            m_ClickerButton.onClick.AddListener(callback);
        }
        
        public void SetClickerButtonText(string text)
        {
            m_ClickerButtonText.text = text;
        }
        
        public void SetEnergyAmountText(string text)
        {
            m_EnergyAmountText.text = text;
        }
        
        public void SetSoftAmountText(string text)
        {
            m_SoftAmountText.text = text;
        }
    }
}