using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private BGMSoundType bgmType;
   

    [HideInInspector] public PlayerController player;
    private int score = 0;

    public string PlayerName { get; private set; }

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();

        SoundManager.Instance.Play(bgmType);
    }

    public void SetPlayerName(string name)
    {
        PlayerName = name;
    }

    public void HandlePlayerDeath()
    {
        if (BirdManager.Instance.FlockCount() > 0)
        {
            BirdManager.Instance.LoseBird();
        }
        else
        {
            Time.timeScale = 0;
            UIManager.Instance.OpenUI(UICategory.Dead);
            BirdManager.Instance.gameSceneUI.SetActive(false);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"���� ����: {score}");
        // UI ������Ʈ
    }
}
