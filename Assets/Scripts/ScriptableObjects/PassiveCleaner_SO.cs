using UnityEngine;
public class PassiveCleaner_SO : ScriptableObject
{
    public CleanerType Type;
    [Tooltip("Amount to clean on each interval")]
    public float AmountToClean;
    [Tooltip("How many seconds between cleanings")]
    public float CleaningInterval;
}