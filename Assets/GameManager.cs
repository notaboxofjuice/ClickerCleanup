using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class GameManager : MonoBehaviour, IDataPersistence
{
    public static GameManager Instance { get; private set; }
    private float m_bactTimer = 0;
    private float m_enzyTimer = 0;
    private float m_mycoTimer = 0;
    private float m_phytTimer = 0;
    [Header("Dictionaries")]
    public SerializableDictionary<CleanerType, int> Cleaners;
    public SerializableDictionary<CleanerType, float> Intervals;
    public SerializableDictionary<CleanerType, int> BoostCounts;
    public SerializableDictionary<CleanerType, float> BoostPercents;
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
        m_bactTimer += time;
        m_enzyTimer += time;
        m_mycoTimer += time;
        m_phytTimer += time;
        if (Intervals.ContainsKey(CleanerType.BACTERIA) && m_bactTimer > Intervals[CleanerType.BACTERIA])
        {
            CleanByType(CleanerType.BACTERIA);
            m_bactTimer = 0;
        }
        if (Intervals.ContainsKey(CleanerType.ENZYME) && m_enzyTimer > Intervals[CleanerType.ENZYME])
        {
            CleanByType(CleanerType.ENZYME); 
            m_enzyTimer = 0;
        }
        if (Intervals.ContainsKey(CleanerType.MYCO) && m_mycoTimer > Intervals[CleanerType.MYCO])
        {
            CleanByType(CleanerType.MYCO);
            m_mycoTimer = 0;
        }
        if (Intervals.ContainsKey(CleanerType.PHYTO) && m_phytTimer > Intervals[CleanerType.PHYTO])
        {
            CleanByType(CleanerType.PHYTO);
            m_phytTimer = 0;
        }
    }
    public void Clean(float plastic) // Add the plastic to the total
    {
        PlasticCleaned += plastic;
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
        Debug.Log("Creating " + quantity + " new " + type + " cleaner(s)");
        Cleaners[type] += quantity;
        OnCleanersChanged.Invoke();
    }
    public void LoadData(GameData data)
    {
        PlasticCleaned = data.PlasticCleaned;
        CarbonCredits = data.CarbonCredits;
        Cleaners = data.Cleaners;
        Intervals = data.Intervals;
        BoostCounts = data.BoostCounts;
        BoostPercents = data.BoostPercents;
    }
    public void SaveData(ref GameData data)
    {
        data.PlasticCleaned = PlasticCleaned;
        data.CarbonCredits = CarbonCredits;
        data.Cleaners = Cleaners;
        data.Intervals = Intervals;
        data.BoostCounts = BoostCounts;
        data.BoostPercents = BoostPercents;
    }
}