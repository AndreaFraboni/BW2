using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HighScoreTable : MonoBehaviour
{
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [Header("UI References")]
    public Transform entryContainer;
    public Transform entryTemplate;
    public int maxrank = 10;

    private List<Transform> highscoreEntryTransformList = new List<Transform>();
    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "LeaderboardData.json");

        if (entryTemplate != null)
        {
            entryTemplate.gameObject.SetActive(false);
        }

        //LoadSaveLeaderboard();

        CreateTestLeaderboard();
    }

    public void CreateTestLeaderboard()
    {
        Highscores highscores = new Highscores();
        highscores.highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry { score = 5000, name = "Andrea" },
            new HighscoreEntry { score = 4900, name = "Marco" },
            new HighscoreEntry { score = 3000, name = "Luca" },
            new HighscoreEntry { score = 2500, name = "Giulia" },
            new HighscoreEntry { score = 2000, name = "Sara" },
            new HighscoreEntry { score = 2700, name = "Paolo" },
            new HighscoreEntry { score = 2400, name = "Anna" },
            new HighscoreEntry { score = 2100, name = "Franco" },
            new HighscoreEntry { score = 1800, name = "Elena" },
            new HighscoreEntry { score = 1500, name = "Michele" },
            new HighscoreEntry { score = 1000, name = "Tizio" },
            new HighscoreEntry { score = 800, name = "Caio" },
            new HighscoreEntry { score = 1800, name = "Sempronio" },
            new HighscoreEntry { score = 1800, name = "Giovannino" }
        };

        SaveHighscoresToFile(highscores);        
        LoadSaveLeaderboard();
        Debug.Log("Leaderboard di prova creata.");
    }

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
            highscores = CreateDefaultHighscores();
            SaveHighscoresToFile(highscores);
        }

        SortHighscores(highscores.highscoreEntryList);

        for (int i = 0; i < highscores.highscoreEntryList.Count && i < maxrank; i++)
        {
            if (highscores.highscoreEntryList[i] != null)
            {
                CreateHighscoreEntryTransform(highscores.highscoreEntryList[i], entryContainer, highscoreEntryTransformList);
            }
        }
    }

    public void AddHighscoreEntry(int score, string playerName)
    {
        Highscores highscores = LoadHighscoresFromFile();

        if (highscores == null || highscores.highscoreEntryList == null)
        {
            highscores = CreateDefaultHighscores();
        }

        HighscoreEntry newEntry = new HighscoreEntry
        {
            score = score,
            name = playerName
        };

        highscores.highscoreEntryList.Add(newEntry);

        SortHighscores(highscores.highscoreEntryList);
        SaveHighscoresToFile(highscores);
        LoadSaveLeaderboard();

        Debug.Log("Nuovo punteggio aggiunto: " + playerName + " - " + score);
    }

    private Highscores LoadHighscoresFromFile()
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.Log("LoadSaveLeaderboard: file non esiste.");
            return null;
        }

        try
        {
            string json = File.ReadAllText(saveFilePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                Debug.LogWarning("LoadSaveLeaderboard: file vuoto.");
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
            new HighscoreEntry { score = 0, name = "Player" }
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
                    if (list[j].score > list[i].score)
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
        Transform score = entryTransform.Find("background/scoreText");
        Transform name = entryTransform.Find("background/nameText");
        Transform background = entryTransform.Find("background");

        TMP_Text posText = pos.GetComponent<TMP_Text>();
        TMP_Text scoreText = score.GetComponent<TMP_Text>();
        TMP_Text nameText = name.GetComponent<TMP_Text>();

        int rank = transformList.Count + 1;
        posText.text = rank + ".";

        scoreText.text = highscoreEntry.score.ToString();
        nameText.text = highscoreEntry.name;

        if (rank == 1) // il numero uno ha il testo in verde ....
        {
            posText.color = Color.green;
            scoreText.color = Color.green;
            nameText.color = Color.green;
        }

        transformList.Add(entryTransform);
    }

}