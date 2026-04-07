using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class RankData
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class RankList
{
    public List<RankData> ranks = new List<RankData>();
}

public class RankManager : MonoSingleton<RankManager>
{
    private string filePath;
    public RankList rankList = new RankList();
    private const int maxRanking = 10;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "ranking.json");
        LoadRanking();
    }

    public void AddScore(string name, int score)
    {
        rankList.ranks.Add(new RankData { playerName = name, score = score });
        rankList.ranks.Sort((a, b) => b.score.CompareTo(a.score));
        if (rankList.ranks.Count > maxRanking)
            rankList.ranks.RemoveAt(rankList.ranks.Count - 1);
        SaveRanking();
    }

    private void SaveRanking()
    {
        string json = JsonUtility.ToJson(rankList, true);
        File.WriteAllText(filePath, json);
    }

    private void LoadRanking()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            rankList = JsonUtility.FromJson<RankList>(json);
        }
    }
    
    public void PrintAllRanking()
    {
        if (rankList.ranks.Count == 0)
        {
            Debug.Log("랭킹이 비어있습니다.");
            return;
        }

        Debug.Log("=== 현재 랭킹 ===");
        for (int i = 0; i < rankList.ranks.Count; i++)
        {
            RankData data = rankList.ranks[i];
            Debug.Log($"{i + 1}위: {data.playerName} - {data.score}");
        }
    }

    public float CalculateScore(int lifeCount, float clearTime)
    {   
        float lifeScore = ((float)lifeCount / 8) * 50f;
        float timeRatio = Mathf.Clamp01(1f - (clearTime / 300));
        float timeScore = timeRatio * 50f;

        float finalScore = lifeScore + timeScore;
        return Mathf.Round(finalScore * 100f) / 100f;
    }   
}
