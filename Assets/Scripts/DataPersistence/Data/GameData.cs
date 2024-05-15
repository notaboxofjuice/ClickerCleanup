[System.Serializable]
public class GameData
{
    public float PlasticCleaned;
    public float CarbonCredits;
    public SerializableDictionary<CleanerType, int> Cleaners;
    public SerializableDictionary<CleanerType, float> Intervals;
    public SerializableDictionary<CleanerType, int> BoostCounts;
    public SerializableDictionary<CleanerType, float> BoostPercents;
    // default values
    public GameData()
    {
        PlasticCleaned = 0;
        CarbonCredits = 0;
        Cleaners = new()
        {
            { CleanerType.BACTERIA, 0 },
            { CleanerType.ENZYME, 0 },
            { CleanerType.MYCO, 0 },
            { CleanerType.PHYTO, 0 }
        };
        Intervals = new()
        {
            { CleanerType.BACTERIA, 10 },
            { CleanerType.ENZYME, 10 },
            { CleanerType.MYCO, 10 },
            { CleanerType.PHYTO, 10 }
        };
        BoostCounts = new()
        {
            { CleanerType.BACTERIA, 0 },
            { CleanerType.ENZYME, 0 },
            { CleanerType.MYCO, 0 },
            { CleanerType.PHYTO, 0 }
        };

        BoostPercents = new()
        {
            { CleanerType.BACTERIA, 0 },
            { CleanerType.ENZYME, 0 },
            { CleanerType.MYCO, 0 },
            { CleanerType.PHYTO, 0 }
        };
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