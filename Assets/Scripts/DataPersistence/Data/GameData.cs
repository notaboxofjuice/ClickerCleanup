[System.Serializable]
public class GameData
{
    public bool IsFirstLaunch;
    public float PlasticCleaned;
    public float CarbonCredits;
    public SerializableDictionary<CleanerType, int> Cleaners;
    public SerializableDictionary<CleanerType, float> CleanerCosts;
    public SerializableDictionary<CleanerType, float> Intervals;
    public SerializableDictionary<CleanerType, int> BoostCounts;
    public SerializableDictionary<CleanerType, float> BoostPercents;
    public SerializableDictionary<CleanerType, float> BoostCosts;
    // default values
    public GameData()
    {
        IsFirstLaunch = true;
        PlasticCleaned = 0;
        CarbonCredits = 0;
        Cleaners = new()
        {
            { CleanerType.BACTERIA, 0 },
            { CleanerType.ENZYME, 0 },
            { CleanerType.MYCO, 0 },
            { CleanerType.PHYTO, 0 }
        };
        CleanerCosts = new()
        {
            { CleanerType.BACTERIA, 10},
            { CleanerType.ENZYME, 10},
            { CleanerType.MYCO, 10},
            { CleanerType.PHYTO, 10}
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
            { CleanerType.BACTERIA, .1f },
            { CleanerType.ENZYME, .1f },
            { CleanerType.MYCO, .1f },
            { CleanerType.PHYTO, .1f }
        };
        BoostCosts = new()
        {
            { CleanerType.BACTERIA, 10},
            { CleanerType.ENZYME, 10},
            { CleanerType.MYCO, 10},
            { CleanerType.PHYTO, 10}
        };
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
        data.IsFirstLaunch = IsFirstLaunch;
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