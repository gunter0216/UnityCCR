using UnityEngine;

namespace Core.Currency.External
{
    [CreateAssetMenu(fileName = "EnergyConfig", menuName = "Configs/EnergyConfig")]
    public class EnergyCurrencyConfig : ScriptableObject
    {
        [SerializeField] private long m_StartValue;
        
        public long StartValue => m_StartValue;
    }
}