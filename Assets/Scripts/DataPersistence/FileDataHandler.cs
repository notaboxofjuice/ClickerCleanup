using System;
using System.IO;
using UnityEngine;
public class FileDataHandler
{
    private string m_dataDirPath = "";
    private string m_dataFileName = "";
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        m_dataDirPath = dataDirPath;
        m_dataFileName = dataFileName;
    }
    public GameData Load()
    {
        // use combine to account for different OS
        string fullPath = Path.Combine(m_dataDirPath, m_dataFileName);

        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // load serialized data from file
                string dataToLoad = "";
                using FileStream stream = new(fullPath, FileMode.Open);
                using StreamReader reader = new(stream);
                dataToLoad = reader.ReadToEnd();

                // deserialize
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }
    public void Save(GameData data)
    {
        // use combine to account for different OS
        string fullPath = Path.Combine(m_dataDirPath, m_dataFileName);
        try
        {
            // create the directory we're saving to if it doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialize C# data to JSON
            string dataToStore = JsonUtility.ToJson(data, true);

            // write
            using FileStream stream = new(fullPath, FileMode.Create);
            using StreamWriter writer = new(stream);
            writer.Write(dataToStore);
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }
}