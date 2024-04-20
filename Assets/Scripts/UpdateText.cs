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
        GameManager.Instance.PlayerData.OnPlasticCleaned.AddListener(UpdatePlasticCleanedText);
        GameManager.Instance.OnCleanersCreated.AddListener(UpdateStoreTexts);
        GameManager.Instance.OnDataWiped.AddListener(UpdateAllTexts);
        UpdatePlasticCleanedText();
        UpdateStoreTexts();
    }
    private void UpdateAllTexts()
    {
        UpdatePlasticCleanedText();
        UpdateStoreTexts();
    }
    private void UpdatePlasticCleanedText()
    {
        Debug.Log("Updating total plastic cleaned text");
        m_plasticCleanedText.text = m_prefix + GameManager.Instance.PlayerData.PlasticCleaned.ToString();
    }
    private void UpdateStoreTexts()
    {
        Debug.Log("Updating store texts");
        int bact = 0;
        int enzy = 0;
        int myco = 0;
        int phyt = 0;
        foreach (PassiveCleaner_SO cleaner in GameManager.Instance.PlayerData.PassiveCleaners)
        {
            switch (cleaner.Type)
            {
                case CleanerType.BACTERIA:
                    bact++;
                    break;
                case CleanerType.ENZYME:
                    enzy++;
                    break;
                case CleanerType.MYCO:
                    myco++;
                    break;
                case CleanerType.PHYTO:
                    phyt++;
                    break;
            }
        }
        m_bactText.text = bact.ToString();
        m_enzyText.text = enzy.ToString();
        m_mycoText.text = myco.ToString();
        m_phytText.text = phyt.ToString();
    }
}