using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Leaderboard : MonoBehaviour
{
    [System.Serializable]
    private class HighscoreEntry
    {
        public int time;
        public string name;
    }
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }
    private List<Transform> highscoreEntryTransformList = new List<Transform>();

    [Header("UI References")]
    public Transform entryContainer;
    public Transform entryTemplate;
    public int maxrank = 10;
    [SerializeField] private GameObject _messageBanner;

    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "LeaderboardData.json");

        if (entryTemplate != null)
        {
            entryTemplate.gameObject.SetActive(false);
        }

        LoadSaveLeaderboard();

        //CreateTestLeaderboard();
    }

    //public void CreateTestLeaderboard()
    //{
    //    Highscores highscores = new Highscores();
    //    highscores.highscoreEntryList = new List<HighscoreEntry>()
    //    {
    //        new HighscoreEntry { time = 500, name = "Andrea" },
    //        new HighscoreEntry { time = 300, name = "Marco" },
    //        new HighscoreEntry { time = 100, name = "Luca" },
    //        new HighscoreEntry { time = 50, name = "Giulia" },
    //        new HighscoreEntry { time = 20, name = "Sara" },
    //        new HighscoreEntry { time = 200, name = "Paolo" },
    //        new HighscoreEntry { time = 150, name = "Anna" },
    //        new HighscoreEntry { time = 120, name = "Franco" },
    //        new HighscoreEntry { time = 100, name = "Elena" },
    //        new HighscoreEntry { time = 1000, name = "Michele" },
    //        new HighscoreEntry { time = 800, name = "Tizio" },
    //        new HighscoreEntry { time = 80, name = "Caio" },
    //        new HighscoreEntry { time = 10, name = "Sempronio" },
    //        new HighscoreEntry { time = 100, name = "Giovannino" }
    //    };

    //    SaveHighscoresToFile(highscores);        
    //    LoadSaveLeaderboard();
    //    Debug.Log("Leaderboard di prova creata.");
    //}

    private void ClearLeaderboardUI()
    {
        foreach (Transform entry in highscoreEntryTransformList)
        {
            Destroy(entry.gameObject);
        }
        highscoreEntryTransformList.Clear();
    }

    public void LoadSaveLeaderboard()
    {
        ClearLeaderboardUI();

        Highscores highscores = LoadHighscoresFromFile();

        if (highscores == null || highscores.highscoreEntryList == null || highscores.highscoreEntryList.Count == 0)
        {
            Debug.LogWarning("TABELLA DEI PUNTEGGI VUOTA !!!!");
            _messageBanner.SetActive(true);
            // highscores = CreateDefaultHighscores();
            // SaveHighscoresToFile(highscores);
        }
        else
        {
            Debug.LogWarning("TABELLA DEI PUNTEGGI è presente quindi la ordino !!!!");
            SortHighscores(highscores.highscoreEntryList);
            for (int i = 0; i < highscores.highscoreEntryList.Count && i < maxrank; i++)
            {
                if (highscores.highscoreEntryList[i] != null)
                {
                    CreateHighscoreEntryTransform(highscores.highscoreEntryList[i], entryContainer, highscoreEntryTransformList);
                }
            }
        }
    }

    public void AddHighscoreEntry(int time, string playerName)
    {
        Highscores highscores = LoadHighscoresFromFile();

        if (highscores == null || highscores.highscoreEntryList == null)
        {
            //highscores = CreateDefaultHighscores();
        }
        else
        {
            HighscoreEntry newEntry = new HighscoreEntry
            {
                time = time,
                name = playerName
            };

            highscores.highscoreEntryList.Add(newEntry);

            SortHighscores(highscores.highscoreEntryList);
            SaveHighscoresToFile(highscores);
            LoadSaveLeaderboard();

            Debug.Log("Nuovo punteggio aggiunto: " + playerName + " - " + time);
        }
    }

    private Highscores LoadHighscoresFromFile()
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.Log("LoadSaveLeaderboard: il file non esiste !!!");
            return null;
        }

        try
        {
            string json = File.ReadAllText(saveFilePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                Debug.LogWarning("LoadSaveLeaderboard: file vuoto !!!");
                return null;
            }

            Highscores highscores = JsonUtility.FromJson<Highscores>(json);

            if (highscores == null || highscores.highscoreEntryList == null)
            {
                Debug.LogWarning("LoadSaveLeaderboard: JSON non valido.");
                return null;
            }

            return highscores;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Errore nel caricamento leaderboard: " + e.Message);
            return null;
        }
    }

    private void SaveHighscoresToFile(Highscores highscores)
    {
        try
        {
            string json = JsonUtility.ToJson(highscores, true);
            File.WriteAllText(saveFilePath, json);
            Debug.Log("Leaderboard salvata in: " + saveFilePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Errore nel salvataggio leaderboard: " + e.Message);
        }
    }

    private Highscores CreateDefaultHighscores()
    {
        Highscores highscores = new Highscores();
        highscores.highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry { time = 0, name = "Player" }
        };
        return highscores;
    }

    private void SortHighscores(List<HighscoreEntry> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = i + 1; j < list.Count; j++)
            {
                if (list[j] != null && list[i] != null)
                {
                    if (list[j].time > list[i].time)
                    {
                        HighscoreEntry temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 40f;

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);

        entryTransform.gameObject.SetActive(true);

        Transform pos = entryTransform.Find("background/posText");
        Transform time = entryTransform.Find("background/timeText");
        Transform name = entryTransform.Find("background/nameText");
        Transform background = entryTransform.Find("background");

        TMP_Text posText = pos.GetComponent<TMP_Text>();
        TMP_Text timeText = time.GetComponent<TMP_Text>();
        TMP_Text nameText = name.GetComponent<TMP_Text>();

        int rank = transformList.Count + 1;
        posText.text = rank + ".";

        timeText.text = highscoreEntry.time.ToString();
        nameText.text = highscoreEntry.name;

        if (rank == 1) // il numero uno ha il testo in verde ....
        {
            posText.color = Color.green;
            timeText.color = Color.green;
            nameText.color = Color.green;
        }

        transformList.Add(entryTransform);
    }


    public void HideMessageBanner()
    {
        if (_messageBanner)_messageBanner.SetActive(false);
    }

}