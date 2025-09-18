using App.Core.Utility.External;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace App.Menu.UI.External.View.Dogs
{
    public class BreedInfoWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_BreedNameText;
        [SerializeField] private TMP_Text m_DescriptionText;
        [SerializeField] private TMP_Text m_DescriptionText2;
        [SerializeField] private Button m_OkButton;
        [SerializeField] private SwitchStateByHeight m_SwitchStateByHeight;

        public SwitchStateByHeight SwitchStateByHeight => m_SwitchStateByHeight;
        
        public void SetActive(bool status)
        {
            gameObject.SetActive(status);
        }
        
        public void SetNameText(string text)
        {
            m_BreedNameText.text = text;
        }
        
        public void SetDescriptionText(string text)
        {
            m_DescriptionText.text = text;
            m_DescriptionText2.text = text;
        }
        
        public void SetOkButtonClickCallback(UnityAction action)
        {
            m_OkButton.onClick.RemoveAllListeners();
            m_OkButton.onClick.AddListener(action);
        }
    }
}