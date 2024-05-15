using UnityEngine;
public class ResearchUnlockable : MonoBehaviour, IDataPersistence
{
    [SerializeField] CleanerType m_cleanerType;
    [SerializeField] int m_boostCounts;
    [SerializeField] float m_boostPercent;
    public void LoadData(GameData data)
    {
        data.BoostCounts.TryGetValue(m_cleanerType, out m_boostCounts);
        data.BoostPercents.TryGetValue(m_cleanerType, out m_boostPercent);
    }
    public void SaveData(ref GameData data)
    {
        data.BoostCounts[m_cleanerType] = m_boostCounts;
        data.BoostPercents[m_cleanerType] = m_boostPercent;
    }
}