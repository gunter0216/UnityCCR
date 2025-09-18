using System.Collections;
using UnityEngine;

namespace App.Core.Utility.External
{
    public class SwitchStateByHeight : MonoBehaviour
    {
        [SerializeField] private RectTransform m_RectTransform;
        [SerializeField] private GameObject m_LessState;
        [SerializeField] private GameObject m_MoreState;
        [SerializeField] private float m_MaxHeight;

        public void SetLessState()
        {
            m_MoreState.SetActive(false);
            m_LessState.SetActive(true);
        }

        public void SetMoreState()
        {
            m_MoreState.SetActive(true);
            m_LessState.SetActive(false);
        }

        public void UpdateState()
        {
            StartCoroutine(WaitAndUpdate());
        }
        
        private IEnumerator WaitAndUpdate()
        {
            yield return new WaitForEndOfFrame();
            var more = m_RectTransform.sizeDelta.y > m_MaxHeight;
            m_MoreState.SetActive(more);
            m_LessState.SetActive(!more);
        }
    }
}