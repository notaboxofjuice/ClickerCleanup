using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObejcts/PlayerData", order = 1)]
public class PlayerData_SO : ScriptableObject
{
    public UnityEvent OnPlasticCleaned;
    private float m_plasticCleaned;
    public float PlasticCleaned
    {
        get { return m_plasticCleaned; }
        set
        {
            m_plasticCleaned = value;
            OnPlasticCleaned.Invoke();
        }
    }
}