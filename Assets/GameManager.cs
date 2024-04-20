using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UnityEvent OnCleanersCreated;
    public UnityEvent OnDataWiped;
    [field: SerializeField] public PlayerData_SO PlayerData { get; private set; }
    private float m_bactTimer = 0;
    private float m_enzyTimer = 0;
    private float m_mycoTimer = 0;
    private float m_phytTimer = 0;
    private readonly float[] intervals = new float[4];
    private readonly float[] amounts = new float[4];
    [Header("Bacteria")]
    [SerializeField] Button m_bactButton;
    public float BactCleanAmount;
    public float BactInterval;
    [Header("Enzyme")]
    [SerializeField] Button m_enzyButton;
    public float EnzyCleanAmount;
    public float EnzyInterval;
    [Header("Myco")]
    [SerializeField] Button m_mycoButton;
    public float MycoCleanAmount;
    public float MycoInterval;
    [Header("Phyto")]
    [SerializeField] Button m_phytButton;
    public float PhytCleanAmount;
    public float PhytInterval;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError("Too many GameManagers in the scene!");
        intervals[0] = BactInterval;
        intervals[1] = EnzyInterval;
        intervals[2] = MycoInterval;
        intervals[3] = PhytInterval;
        amounts[0] = BactCleanAmount;
        amounts[1] = EnzyCleanAmount;
        amounts[2] = MycoCleanAmount;
        amounts[3] = PhytCleanAmount;
#if UNITY_EDITOR
        for (int i = 0; i < 4; i++)
        {
            intervals[i] = Random.Range(4, 10);
            amounts[i] = Random.Range(4, 10);
        }
#endif
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
        if (m_bactTimer > BactInterval)
        {
            CleanByType(CleanerType.BACTERIA);
            m_bactTimer = 0;
        }
        if (m_enzyTimer > EnzyInterval)
        {
            CleanByType(CleanerType.ENZYME); 
            m_enzyTimer = 0;
        }
        if (m_mycoTimer > MycoInterval)
        {
            CleanByType(CleanerType.MYCO);
            m_mycoTimer = 0;
        }
        if (m_phytTimer > PhytInterval)
        {
            CleanByType(CleanerType.PHYTO);
            m_phytTimer = 0;
        }
    }
    public void Clean(float plastic) // Add the plastic to the total
    {
        PlayerData.PlasticCleaned += plastic;
    }
    public void CleanByType(CleanerType type)
    {
        float count = 0;
        foreach (PassiveCleaner_SO cleaner in PlayerData.PassiveCleaners)
        {
            if (!cleaner.Type.Equals(type)) continue; // If this isn't the type of cleaner we want to update, continue
            Clean(cleaner.AmountToClean);

            count += cleaner.AmountToClean;
        }
        Debug.Log(type + " cleaned " + count);
    }
    public void WipeSaveData() // Go through and reset all the values to their default values
    {
        PlayerData.PlasticCleaned = 0;
        PlayerData.PassiveCleaners.Clear();
        OnDataWiped.Invoke();
        Debug.Log("Wiped save data");
    }
    public void UpdateCleanerEfficiency(CleanerType type, float newAmount, float newInterval)
    {
        Debug.Log("Updating " + type + "cleaner to clean " + newAmount + " every " + newInterval + " seconds");
        switch (type)
        {
            case CleanerType.BACTERIA:
                BactCleanAmount = newAmount;
                BactInterval = newInterval;
                break;
            case CleanerType.ENZYME:
                EnzyCleanAmount = newAmount;
                EnzyInterval = newInterval;
                break;
            case CleanerType.MYCO:
                MycoCleanAmount = newAmount;
                MycoInterval = newInterval;
                break;
            case CleanerType.PHYTO:
                PhytCleanAmount = newAmount;
                PhytInterval = newInterval;
                break;
        }
        UpdateCleanEfficiencyByType(type, newAmount, newInterval);
    }
    public void CreateNewCleaners(CleanerType type, int quantity = 1)
    {
        Debug.Log("Creating " + quantity + " new " + type + " cleaner(s)");
        for (int i = 0; i < quantity; i++)
        {
            PassiveCleaner_SO newCleaner = ScriptableObject.CreateInstance<PassiveCleaner_SO>();
            switch (newCleaner.Type)
            {
                case CleanerType.BACTERIA:
                    newCleaner.AmountToClean = BactCleanAmount;
                    newCleaner.CleaningInterval = BactInterval;
                    break;
                case CleanerType.ENZYME:
                    newCleaner.AmountToClean = EnzyCleanAmount;
                    newCleaner.CleaningInterval = EnzyInterval;
                    break;
                case CleanerType.MYCO:
                    newCleaner.AmountToClean = MycoCleanAmount;
                    newCleaner.CleaningInterval = MycoInterval;
                    break;
                case CleanerType.PHYTO:
                    newCleaner.AmountToClean = PhytCleanAmount;
                    newCleaner.CleaningInterval = PhytInterval;
                    break;
            }
            PlayerData.PassiveCleaners.Add(newCleaner);
        }
        OnCleanersCreated.Invoke();
    }
    public void UpdateCleanEfficiencyByType(CleanerType type, float newAmount, float newInterval)
    {
        foreach (PassiveCleaner_SO cleaner in PlayerData.PassiveCleaners)
        {
            if (!cleaner.Type.Equals(type)) continue; // If this isn't the type of cleaner we want to update, continue
            cleaner.AmountToClean = newAmount;
            cleaner.CleaningInterval = newInterval;
        }
    }
}