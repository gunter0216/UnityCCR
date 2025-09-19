using UnityEngine;
using UnityEngine.UI;

namespace App.Core.Menu.External.View
{
    public class MenuTabView : MonoBehaviour
    {
        [SerializeField] private float m_NormalHeight;
        [SerializeField] private float m_SelectedHeight;
        [SerializeField] private RectTransform m_RectTransform;
        [SerializeField] private Button m_Button;

        public void SetActiveState(bool status)
        {
            m_RectTransform.sizeDelta = new Vector2(m_RectTransform.sizeDelta.x, status ? m_SelectedHeight : m_NormalHeight);
        }
        
        public void SetButtonClickCallback(UnityEngine.Events.UnityAction callback)
        {
            m_Button.onClick.RemoveAllListeners();
            m_Button.onClick.AddListener(callback);
        }
    }
}