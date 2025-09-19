using UnityEngine;

namespace App.Menu.UI.External.Presenter
{
    [CreateAssetMenu(fileName = "ClickerConfig", menuName = "Configs/ClickerConfig")]
    public class ClickerConfig : ScriptableObject
    {
        [SerializeField] private long m_AutoCollectionTimerInterval;
        [SerializeField] private long m_AutoCollectionTimerEnergyPrice;
        [SerializeField] private long m_AutoCollectionTimerSoftIncome;
        [Space]
        [SerializeField] private long m_ClickEnergyPrice;
        [SerializeField] private long m_ClickSoftIncome;
        [Space]
        [SerializeField] private long m_EnergyRecoveryInterval;
        [SerializeField] private long m_EnergyRecoveryValue;
        
        public long AutoCollectionTimerInterval => m_AutoCollectionTimerInterval;
        public long AutoCollectionTimerEnergyPrice => m_AutoCollectionTimerEnergyPrice;
        public long AutoCollectionTimerSoftIncome => m_AutoCollectionTimerSoftIncome;
        public long ClickEnergyPrice => m_ClickEnergyPrice;
        public long ClickSoftIncome => m_ClickSoftIncome;
        public long EnergyRecoveryInterval => m_EnergyRecoveryInterval;
        public long EnergyRecoveryValue => m_EnergyRecoveryValue;
    }
}