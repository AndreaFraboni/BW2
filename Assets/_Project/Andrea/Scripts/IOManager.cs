using System.IO;
using UnityEngine;
using static IOManager;

public class IOManager : MonoBehaviour
{
    private string _saveFile;

    [System.Serializable]
    public class PlayerData
    {
        public string Name;
        public float MasterVolume;
        public float MusicVolume;
        public float SFXVolume;
    }
    PlayerData mPlayerData = new PlayerData();

    private void Awake()
    {
        _saveFile = Path.Combine(Application.persistentDataPath, "GameData.sav");
    }

    public void SaveDataFile()
    {
        try
        {
            string jsonwritingText = JsonUtility.ToJson(mPlayerData);
            File.WriteAllText(_saveFile, jsonwritingText);
            Debug.Log("File di salvataggio scritto in: " + _saveFile);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Errore nel salvataggio del Player data: " + e.Message);
        }
    }

    public void LoadDataFile()
    {
        if (!File.Exists(_saveFile))
        {
            Debug.Log("Loading problem: file di salvataggio non esiste.");
            return;
        }

        try
        {
            string jsonloadingtext = File.ReadAllText(_saveFile);

            if (string.IsNullOrWhiteSpace(jsonloadingtext))
            {
                Debug.LogWarning("file di salavtaggio è file vuoto ????");
                return;
            }

            mPlayerData = JsonUtility.FromJson<PlayerData>(jsonloadingtext);

            if (mPlayerData == null)
            {
                Debug.LogWarning("problema con il file di salvataggio : JSON non valido.");
                return;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("il FILE di salvataggio NON ESISTE e quindi ne creo uno con dati di default !!!");
            mPlayerData.Name = "Player";
            mPlayerData.MasterVolume = 1.0f;
            mPlayerData.MusicVolume = 1.0f;
            mPlayerData.SFXVolume = 1.0f;
            string jsonwritingText = JsonUtility.ToJson(mPlayerData);
            File.WriteAllText(_saveFile, jsonwritingText);
            return;
        }
    }

}