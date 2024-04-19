using UnityEngine;
using TMPro;
public class UpdateText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private string m_prefix;
    private void Awake()
    {
        GameManager.Instance.PlayerData.OnPlasticCleaned.AddListener(UpdateTextValue);
        m_prefix += " ";
    }
    private void UpdateTextValue()
    {
        m_text.text = m_prefix + GameManager.Instance.PlayerData.PlasticCleaned.ToString();
    }
}