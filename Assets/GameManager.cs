using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class GameManager : MonoBehaviour, IDataPersistence
{
    public static GameManager Instance { get; private set; }
    private float m_bactIntervalTracker = 0;
    private string BactDisplayTime => Mathf.FloorToInt(Intervals[CleanerType.BACTERIA] - m_bactIntervalTracker).ToString() + "s";
    private float m_enzyIntervalTracker = 0;
    private string EnzyDisplayTime => Mathf.FloorToInt(Intervals[CleanerType.ENZYME] - m_enzyIntervalTracker).ToString() + "s";
    private float m_mycoIntervalTracker = 0;
    private string MycoDisplayTime => Mathf.FloorToInt(Intervals[CleanerType.MYCO] - m_mycoIntervalTracker).ToString() + "s";
    private float m_phytIntervalTracker = 0;
    private string PhytDisplayTime => Mathf.FloorToInt(Intervals[CleanerType.PHYTO] - m_phytIntervalTracker).ToString() + "s";
    [Header("UI")]
    [SerializeField] TextMeshProUGUI m_bactTimeText;
    [SerializeField] TextMeshProUGUI m_enzyTimeText;
    [SerializeField] TextMeshProUGUI m_mycoTimeText;
    [SerializeField] TextMeshProUGUI m_phytTimeText;
    [Header("Dictionaries")]
    public SerializableDictionary<CleanerType, int> Cleaners;
    public SerializableDictionary<CleanerType, float> CleanerCosts;
    public SerializableDictionary<CleanerType, float> Intervals;
    public SerializableDictionary<CleanerType, int> BoostCounts;
    public SerializableDictionary<CleanerType, float> BoostPercents;
    public SerializableDictionary<CleanerType, float> BoostCosts;
    [Header("Buttons")]
    [SerializeField] Button m_bactButton;
    [SerializeField] Button m_enzyButton;
    [SerializeField] Button m_mycoButton;
    [SerializeField] Button m_phytButton;
    [Header("Data Stuff")]
    public UnityEvent OnPlasticChanged;
    public UnityEvent OnCreditsChanged;
    public UnityEvent OnCleanersChanged;
    [SerializeField] private float m_plasticCleaned;
    [SerializeField] private float m_carbonCredits;
    public float PlasticCleaned
    {
        get { return m_plasticCleaned; }
        set
        {
            m_plasticCleaned = value;
            OnPlasticChanged.Invoke();
        }
    }
    public float CarbonCredits
    {
        get { return m_carbonCredits; }
        set
        {
            m_carbonCredits = value;
            OnCreditsChanged.Invoke();
        }
    }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError("Too many GameManagers in the scene!");
    }
    private void Start()
    {
        m_bactButton.onClick.AddListener(() => CreateNewCleaners(CleanerType.BACTERIA));
        m_enzyButton.onClick.AddListener(() => CreateNewCleaners(CleanerType.ENZYME));
        m_mycoButton.onClick.AddListener(() => CreateNewCleaners(CleanerType.MYCO));
        m_phytButton.onClick.AddListener(() => CreateNewCleaners(CleanerType.PHYTO));
    }
    private void Update()
    {
        // get how much time has passed
        float time = Time.deltaTime;
        // increment timers
        if (Cleaners[CleanerType.BACTERIA] > 0)
        {
            m_bactIntervalTracker += time;
            m_bactTimeText.text = BactDisplayTime;
        }
        if (Cleaners[CleanerType.ENZYME] > 0)
        {
            m_enzyIntervalTracker += time;
            m_enzyTimeText.text = EnzyDisplayTime;
        }
        if (Cleaners[CleanerType.MYCO] > 0)
        {
            m_mycoIntervalTracker += time;
            m_mycoTimeText.text = MycoDisplayTime;
        }
        if (Cleaners[CleanerType.MYCO] > 0)
        {
            m_phytIntervalTracker += time;
            m_phytTimeText.text = PhytDisplayTime;
        }
        // do any cleaning
        if (Intervals.ContainsKey(CleanerType.BACTERIA) && m_bactIntervalTracker > Intervals[CleanerType.BACTERIA])
        {
            CleanByType(CleanerType.BACTERIA);
            m_bactIntervalTracker = 0;
        }
        if (Intervals.ContainsKey(CleanerType.ENZYME) && m_enzyIntervalTracker > Intervals[CleanerType.ENZYME])
        {
            CleanByType(CleanerType.ENZYME); 
            m_enzyIntervalTracker = 0;
        }
        if (Intervals.ContainsKey(CleanerType.MYCO) && m_mycoIntervalTracker > Intervals[CleanerType.MYCO])
        {
            CleanByType(CleanerType.MYCO);
            m_mycoIntervalTracker = 0;
        }
        if (Intervals.ContainsKey(CleanerType.PHYTO) && m_phytIntervalTracker > Intervals[CleanerType.PHYTO])
        {
            CleanByType(CleanerType.PHYTO);
            m_phytIntervalTracker = 0;
        }
    }
    public void Clean(float plastic) // Add the plastic to the total
    {
        PlasticCleaned += plastic;
        if (PlasticCleaned % 5 == 0) CarbonCredits++; // gain a credit every 5 plastics
    }
    public void CleanByType(CleanerType type)
    {
        Cleaners.TryGetValue(type, out int cleanerCount);
        BoostCounts.TryGetValue(type, out int boostCount);
        BoostPercents.TryGetValue(type, out float boostPercent);
        int plastic = Mathf.RoundToInt(cleanerCount * (1 + boostCount * boostPercent));
        Clean(plastic);
        Debug.Log(type + " cleaned " + plastic);
    }
    public void CreateNewCleaners(CleanerType type, int quantity = 1)
    {
        if (CarbonCredits < CleanerCosts[type]) return;
        CarbonCredits -= CleanerCosts[type];
        CleanerCosts[type] *= 1.5f;
        Debug.Log("Creating " + quantity + " new " + type + " cleaner(s)");
        Cleaners[type] += quantity;
        OnCleanersChanged.Invoke();
    }
    public void LoadData(GameData data)
    {
        PlasticCleaned = data.PlasticCleaned;
        CarbonCredits = data.CarbonCredits;
        Cleaners = data.Cleaners;
        CleanerCosts = data.CleanerCosts;
        Intervals = data.Intervals;
        BoostCounts = data.BoostCounts;
        BoostPercents = data.BoostPercents;
        BoostCosts = data.BoostCosts;
    }
    public void SaveData(ref GameData data)
    {
        data.PlasticCleaned = PlasticCleaned;
        data.CarbonCredits = CarbonCredits;
        data.Cleaners = Cleaners;
        data.CleanerCosts = CleanerCosts;
        data.Intervals = Intervals;
        data.BoostCounts = BoostCounts;
        data.BoostPercents = BoostPercents;
        data.BoostCosts = BoostCosts;
    }
}