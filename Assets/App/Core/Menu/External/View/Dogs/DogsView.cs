using UnityEngine;

namespace App.Core.Menu.External.View.Dogs
{
    public class DogsView : MonoBehaviour
    {
        [SerializeField] private BreedView m_BreedViewPrefab;
        [SerializeField] private Transform m_DogsContent;
        [SerializeField] private Transform m_LoadingIcon;
        [SerializeField] private BreedInfoWindow m_BreedInfoWindow;
        [SerializeField] private GameObject m_ScrollView;

        public BreedInfoWindow BreedInfoWindow => m_BreedInfoWindow;
        public BreedView BreedViewPrefab => m_BreedViewPrefab;
        public Transform DogsContent => m_DogsContent;
        public Transform LoadingIcon => m_LoadingIcon;
        
        public void SetActive(bool status)
        {
            gameObject.SetActive(status);
        }
        
        public void SetBreedsTableActive(bool status)
        {
            m_ScrollView.SetActive(status);
        }
    }
}