using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace App.Core.Menu.External.View.Dogs
{
    public class BreedView : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_NumberText;
        [SerializeField] private TMP_Text m_NameText;
        [SerializeField] private Transform m_DownloadIcon;
        [SerializeField] private Button m_Button;

        public Transform DownloadIcon => m_DownloadIcon;
        
        public void SetNumberText(string text)
        {
            m_NumberText.text = text;
        }
        
        public void SetNameText(string text)
        {
            m_NameText.text = text;
        }
        
        public void SetDownloadIconActive(bool status)
        {
            m_DownloadIcon.gameObject.SetActive(status);
        }

        public void SetActive(bool status)
        {
            gameObject.SetActive(status);
        }
        
        public void SetButtonClickCallback(UnityAction action)
        {
            m_Button.onClick.RemoveAllListeners();
            m_Button.onClick.AddListener(action);
        }

        public void SetAsLastSibling()
        {
            transform.SetAsLastSibling();
        }
    }
}