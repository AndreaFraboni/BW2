using System;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Time;
}

[System.Serializable]
public class AudioSettingData
{
    public float masterVolValue;
    public float musicVolValue;
    public float sfxVolValue;
}
public class IOManager : GenericSingleton<IOManager>
{
    private string _savePlayerFile;
    private string _saveAudioSettingsFile;

    private void Start()
    {
        _savePlayerFile = Application.persistentDataPath + "/GameData.json";
        _saveAudioSettingsFile = Application.persistentDataPath + "/audiosettings.json";
    }

//******************************************************************************************//
//*************************  PLAYER DATA LOAD & SAVE PLAYER DATA ***************************//
//******************************************************************************************//
    public bool LoadPlayerDataFile(ref string playerName, ref int playerScore)
    {
        PlayerData mPlayerData = new PlayerData();

        if (!File.Exists(_savePlayerFile))
        {
            Debug.Log("Player Data Loading problem: file json in lettura non esiste !!!");
            return false;
        }

        try
        {
            string jsonloadingtext = File.ReadAllText(_savePlayerFile);

            if (string.IsNullOrWhiteSpace(jsonloadingtext))
            {
                Debug.LogWarning("file json in lettura è un file vuoto ????");
                return false;
            }

            mPlayerData = JsonUtility.FromJson<PlayerData>(jsonloadingtext);

            if (mPlayerData == null)
            {
                Debug.LogWarning("problema con il file json in lettura : non è valido !!!");
                return false;
            }
            else
            {
                Debug.LogWarning("creo dei dati di default !!!");
                playerName = "Player";
                playerScore = 0;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Errore nella lettura dei dati per un errore : " + e.Message);
            return false;
        }
        return true;
    }

    public bool SavePlayerDataFile(string PlayerName, int Time)
    {
        PlayerData mPlayerData = new PlayerData();

        mPlayerData.Name = PlayerName;
        mPlayerData.Time = Time;

        try
        {
            string jsonwritingText = JsonUtility.ToJson(mPlayerData);
            File.WriteAllText(_savePlayerFile, jsonwritingText);
            Debug.Log("File di salvataggio Player Data scritto in: " + _savePlayerFile);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Errore nel salvataggio del Player data: " + e.Message);
            return false;
        }
    }

//******************************************************************************************//
//********************* AUDIO SETTINGS LOAD & SAVE AUDIO DATA*******************************//
//******************************************************************************************//
    public bool SaveAudioSettings(float masterVol, float musicVol, float sfxVol)
    {
        AudioSettingData mAudioSettings = new AudioSettingData();

        mAudioSettings.masterVolValue = masterVol;
        mAudioSettings.musicVolValue = musicVol;
        mAudioSettings.sfxVolValue = sfxVol;
              
        try
        {
            string json = JsonUtility.ToJson(mAudioSettings);
            File.WriteAllText(_saveAudioSettingsFile, json);
            Debug.Log("File di salvataggio Audio scritto in: " + _saveAudioSettingsFile);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Errore nel salvataggio degli Audio Settings: " + e.Message);
            return false;
        }
    }

    public bool LoadAudioSettings(ref float MasterVol, ref float MusicVol, ref float SFXVol)
    {
        AudioSettingData mAudioSettings = new AudioSettingData();

        if (!File.Exists(_saveAudioSettingsFile))
        {
            Debug.Log("Loading problem: il json file non esiste.");
            return false;
        }

        try
        {
            string json = File.ReadAllText(_saveAudioSettingsFile);

            if (string.IsNullOrWhiteSpace(json))
            {
                Debug.LogWarning("file json è vuoto ????");
                return false;
            }

            mAudioSettings = JsonUtility.FromJson<AudioSettingData>(json);

            if (mAudioSettings == null)
            {
                Debug.LogWarning("problema : file json in lettura non è valido !!!");
                return false;
            }
            else
            {
                Debug.LogWarning("carico i dati dal file alle strutture Audio !!!");
                MasterVol = mAudioSettings.masterVolValue;
                MusicVol = mAudioSettings.musicVolValue;
                SFXVol = mAudioSettings.sfxVolValue;
                return true;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Errore nel caricamento dati per un errore : " + e.Message);
            return false;
        }

    }

}