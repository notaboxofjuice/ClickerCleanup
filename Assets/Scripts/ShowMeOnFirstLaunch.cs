using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMeOnFirstLaunch : MonoBehaviour, IDataPersistence
{
    private bool m_isFirstLaunch;
    public void LoadData(GameData data)
    {
        m_isFirstLaunch = data.IsFirstLaunch;
        gameObject.SetActive(m_isFirstLaunch);
        m_isFirstLaunch = false;
    }
    public void SaveData(ref GameData data)
    {
        data.IsFirstLaunch = m_isFirstLaunch;
    }
}