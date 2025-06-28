using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

[System.Serializable]
public class HighScoreData
{
    public List<int> scores = new List<int>();
}

public class HighScoreManager : NghiaMono
{
    public static HighScoreManager Instance { get; private set; }
    private string savePath => Path.Combine(Application.persistentDataPath, "highscores.json");

    private const int maxScores = 5;
    public List<int> HighScores { get; private set; } = new List<int>();

    protected override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScores();
    }

    public void TryAddNewScore(int score)
    {
        HighScores.Add(score);
        HighScores = HighScores.OrderByDescending(s => s).Take(maxScores).ToList();
        SaveScores();
    }

    public void ResetScores()
    {
        HighScores = new List<int>();
        SaveScores();
    }

    private void LoadScores()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);
            HighScores = data.scores.OrderByDescending(s => s).Take(maxScores).ToList();
        }
        else
        {
            HighScores = new List<int>();
        }
    }

    private void SaveScores()
    {
        HighScoreData data = new HighScoreData { scores = HighScores };
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public void PrintTopScores()
    {
        for (int i = 0; i < HighScores.Count; i++)
        {
            Debug.Log($"Top {i + 1}: {HighScores[i]}");
        }
    }
}
