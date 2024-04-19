using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [field: SerializeField] public PlayerData_SO PlayerData { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError("Too many GameManagers in the scene!");
    }
    public void Clean(float plastic) // Add the plastic to the total
    {
        PlayerData.PlasticCleaned += plastic;
    }
    public void WipeSaveData() // Go through and reset all the values to their default values
    {
        PlayerData.PlasticCleaned = 0;
    }
}