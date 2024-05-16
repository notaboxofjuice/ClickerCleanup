using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ResearchMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button m_bactButton;
    [SerializeField] Button m_enzyButton;
    [SerializeField] Button m_mycoButton;
    [SerializeField] Button m_phytButton;
    [Header("Texts")]
    [SerializeField] TextMeshProUGUI m_bactCost;
    [SerializeField] TextMeshProUGUI m_bactCount;
    [SerializeField] TextMeshProUGUI m_enzyCost;
    [SerializeField] TextMeshProUGUI m_enzyCount;
    [SerializeField] TextMeshProUGUI m_mycoCost;
    [SerializeField] TextMeshProUGUI m_mycoCount;
    [SerializeField] TextMeshProUGUI m_phytCost;
    [SerializeField] TextMeshProUGUI m_phytCount;
    private void Start()
    {
        m_bactButton.onClick.AddListener(() => BuyResearch(CleanerType.BACTERIA));
        m_enzyButton.onClick.AddListener(() => BuyResearch(CleanerType.ENZYME));
        m_mycoButton.onClick.AddListener(() => BuyResearch(CleanerType.MYCO));
        m_phytButton.onClick.AddListener(() => BuyResearch(CleanerType.PHYTO));
        UpdateTexts(CleanerType.BACTERIA);
        UpdateTexts(CleanerType.ENZYME);
        UpdateTexts(CleanerType.MYCO);
        UpdateTexts(CleanerType.PHYTO);
    }
    public void BuyResearch(CleanerType type) // Called by buttons
    {
        if (GameManager.Instance.CarbonCredits < Mathf.CeilToInt(GameManager.Instance.BoostCosts[type])) return; // if we don't have enough money, return
        GameManager.Instance.CarbonCredits -= Mathf.RoundToInt(GameManager.Instance.BoostCosts[type]); // deduct credits
        GameManager.Instance.BoostCounts[type]++; // add a new research boost
        if (GameManager.Instance.BoostCounts[type] % 5 == 0) GameManager.Instance.BoostPercents[type] *= 1.1f; // every five researches, improve cleaning power by 10%
        if (GameManager.Instance.BoostCounts[type] % 10 == 0 && GameManager.Instance.Intervals[type] * .9f > 0) GameManager.Instance.Intervals[type] *= .9f; // every 10 researches, reduce cleaning cooldown by 10%
        GameManager.Instance.BoostCosts[type] *= 1.1f; // increase the cost
        Debug.Log(GameManager.Instance.BoostCounts[type] + " " + type + " boosts\nTotal boost: " + GameManager.Instance.BoostCounts[type] * GameManager.Instance.BoostPercents[type]);
        UpdateTexts(type);
    }
    private void UpdateTexts(CleanerType type)
    {
        string count = (100 * (GameManager.Instance.BoostCounts[type] * GameManager.Instance.BoostPercents[type])).ToString("F0") + "%";
        switch (type)
        {
            case CleanerType.BACTERIA:
                m_bactCost.text = "$" + GameManager.Instance.BoostCosts[type].ToString("F0");
                m_bactCount.text = count;
                break;
            case CleanerType.ENZYME:
                m_enzyCost.text = "$" + GameManager.Instance.BoostCosts[type].ToString("F0");
                m_enzyCount.text = count;
                break;
            case CleanerType.MYCO:
                m_mycoCost.text = "$" + GameManager.Instance.BoostCosts[type].ToString("F0");
                m_mycoCount.text = count;
                break;
            case CleanerType.PHYTO:
                m_phytCost.text = "$" + GameManager.Instance.BoostCosts[type].ToString("F0");
                m_phytCount.text = count;
                break;
        }
    }
}