using UnityEngine;

namespace App.Core.Menu.External.Presenter.States.Dogs.Config
{
    [CreateAssetMenu(fileName = "DogsConfig", menuName = "Configs/DogsConfig")]
    public class DogsConfig : ScriptableObject
    {
        [SerializeField] private long m_MaxBreedsCount;
        
        public long MaxBreedsCount => m_MaxBreedsCount;
    }
}