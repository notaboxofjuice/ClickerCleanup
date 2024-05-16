using UnityEngine;
using TMPro;
public class UpdateText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_plasticCleanedText;
    [SerializeField] TextMeshProUGUI m_carbonCreditsText;
    [SerializeField] string m_plasticPrefix;
    [SerializeField] string m_creditsPrefix;
    [SerializeField] TextMeshProUGUI m_bactText;
    [SerializeField] TextMeshProUGUI m_bactCostText;
    [SerializeField] TextMeshProUGUI m_enzyText;
    [SerializeField] TextMeshProUGUI m_enzyCostText;
    [SerializeField] TextMeshProUGUI m_mycoText;
    [SerializeField] TextMeshProUGUI m_mycoCostText;
    [SerializeField] TextMeshProUGUI m_phytText;
    [SerializeField] TextMeshProUGUI m_phytCostText;
    private void Start()
    {
        m_plasticPrefix += "\n";
        m_creditsPrefix += "\n$";
        GameManager.Instance.OnPlasticChanged.AddListener(UpdatePlasticCleanedText);
        GameManager.Instance.OnCreditsChanged.AddListener(UpdateCarbonCreditsText);
        GameManager.Instance.OnCleanersChanged.AddListener(UpdateStoreTexts);
        Invoke(nameof(UpdateAllTexts), 2);
    }
    private void UpdateAllTexts()
    {
        UpdatePlasticCleanedText();
        UpdateCarbonCreditsText();
        UpdateStoreTexts();
    }
    private void UpdatePlasticCleanedText()
    {
        Debug.Log("Updating total plastic cleaned text");
        m_plasticCleanedText.text = m_plasticPrefix + GameManager.Instance.PlasticCleaned.ToString("F0");
    }
    private void UpdateCarbonCreditsText()
    {
        Debug.Log("Updating carbon credit text");
        m_carbonCreditsText.text = m_creditsPrefix + GameManager.Instance.CarbonCredits.ToString("F0");
    }
    private void UpdateStoreTexts()
    {
        Debug.Log("Updating store texts");
        m_bactText.text = GameManager.Instance.Cleaners[CleanerType.BACTERIA].ToString();
        m_bactCostText.text = "$" + Mathf.RoundToInt(GameManager.Instance.CleanerCosts[CleanerType.BACTERIA]).ToString();
        m_enzyText.text = GameManager.Instance.Cleaners[CleanerType.ENZYME].ToString();
        m_enzyCostText.text = "$" + Mathf.RoundToInt(GameManager.Instance.CleanerCosts[CleanerType.ENZYME]).ToString();
        m_mycoText.text = GameManager.Instance.Cleaners[CleanerType.MYCO].ToString();
        m_mycoCostText.text = "$" + Mathf.RoundToInt(GameManager.Instance.CleanerCosts[CleanerType.MYCO]).ToString();
        m_phytText.text = GameManager.Instance.Cleaners[CleanerType.PHYTO].ToString();
        m_phytCostText.text = "$" + Mathf.RoundToInt(GameManager.Instance.CleanerCosts[CleanerType.PHYTO]).ToString();
    }
}