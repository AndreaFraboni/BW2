using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class LoadSaveData : MonoBehaviour
{
    private string _saveFile = Application.persistentDataPath + "/GameData.sav";

    [System.Serializable]
    public class PlayerData
    {
        public string Name;
        public float MasterVolume;
        public float MusicVolume;
        public float SFXVolume;
    }
    PlayerData mPlayerData = new PlayerData();

    [System.Serializable]
    public class HighScore
    {
        public int Score;
        public string Name;
    }
    HighScore mHighScore = new HighScore();

    public void SaveDataFile()
    {
        if (File.Exists(_saveFile))
        {
            Debug.LogWarning("IL FILE di salvataggio ESISTE GIà non posso sovrascrivere !!!");
            return;
        }
        else
        {
            Debug.LogWarning("il FILE di salvataggio NON ESISTE e quindi lo creo !!!");
            string jsonwritingText = JsonUtility.ToJson(mPlayerData);
            File.WriteAllText(_saveFile, jsonwritingText);
        }
    }

    public void LoadDataFile()
    {
        if (File.Exists(_saveFile))
        {
            Debug.LogWarning("IL FILE di salvataggio ESISTE quindi carico i dati !!!");
            string jsonloadingtext = File.ReadAllText(_saveFile);
            mPlayerData = JsonUtility.FromJson<PlayerData>(jsonloadingtext);
        }
        else
        {
            Debug.LogWarning("il FILE di salvataggio NON ESISTE e quindi lo creo con dati di default !!!");
            
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