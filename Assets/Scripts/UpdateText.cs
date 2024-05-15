using UnityEngine;
using TMPro;
public class UpdateText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_plasticCleanedText;
    [SerializeField] private string m_prefix;
    [SerializeField] private TextMeshProUGUI m_bactText;
    [SerializeField] private TextMeshProUGUI m_enzyText;
    [SerializeField] private TextMeshProUGUI m_mycoText;
    [SerializeField] private TextMeshProUGUI m_phytText;
    private void Start()
    {
        m_prefix += " ";
        GameManager.Instance.OnPlasticChanged.AddListener(UpdatePlasticCleanedText);
        GameManager.Instance.OnCleanersChanged.AddListener(UpdateStoreTexts);
        UpdateAllTexts();
    }
    private void UpdateAllTexts()
    {
        UpdatePlasticCleanedText();
        UpdateStoreTexts();
    }
    private void UpdatePlasticCleanedText()
    {
        Debug.Log("Updating total plastic cleaned text");
        m_plasticCleanedText.text = m_prefix + GameManager.Instance.PlasticCleaned.ToString();
    }
    private void UpdateStoreTexts()
    {
        Debug.Log("Updating store texts");
        if (GameManager.Instance.Cleaners.ContainsKey(CleanerType.BACTERIA)) m_bactText.text = GameManager.Instance.Cleaners[CleanerType.BACTERIA].ToString();
        if (GameManager.Instance.Cleaners.ContainsKey(CleanerType.ENZYME)) m_enzyText.text = GameManager.Instance.Cleaners[CleanerType.ENZYME].ToString();
        if (GameManager.Instance.Cleaners.ContainsKey(CleanerType.MYCO)) m_mycoText.text = GameManager.Instance.Cleaners[CleanerType.MYCO].ToString();
        if (GameManager.Instance.Cleaners.ContainsKey(CleanerType.PHYTO)) m_phytText.text = GameManager.Instance.Cleaners[CleanerType.PHYTO].ToString();
    }
}