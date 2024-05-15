using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string m_fileName;
    private FileDataHandler m_dataHandler;

    public static DataPersistenceManager Instance { get; private set; }

    private GameData m_gameData;
    private List<IDataPersistence> m_dataPersistenceObjects;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError("Too many " + this + " in scene.");
    }
    private void Start()
    {
        m_dataHandler = new(Application.persistentDataPath, m_fileName);
        m_dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        m_gameData = new();
    }
    public void LoadGame()
    {
        // TODO - load any saved data from file using handler
        m_gameData = m_dataHandler.Load();

        if (m_gameData == null)
        {
            Debug.Log("No data found, initializing defaults...");
            NewGame();
        }

        // push loaded data where it's needed
        foreach (IDataPersistence p in m_dataPersistenceObjects) p.LoadData(m_gameData);
    }
    public void SaveGame()
    {
        // pass data to other scripts
        foreach (IDataPersistence p in m_dataPersistenceObjects) p.SaveData(ref m_gameData);
        // save data to file using handler
        m_dataHandler.Save(m_gameData);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}