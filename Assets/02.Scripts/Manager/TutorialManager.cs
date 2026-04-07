using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct FTutorialInfo
{
    public string text;
    public Sprite sprite;
}

public class TutorialManager : MonoSingleton<TutorialManager>
{
    [SerializeField] private Image canvasImage;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image image;
    [SerializeField] private List<FTutorialInfo> tutorialInfos = new List<FTutorialInfo>();
    [SerializeField] private TimerUI timer;
    private int currentTutorialIndex = 0;
    private Canvas canvas;
    private PlayerController playerController;
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }
    private void Start()
    {
        canvasImage.gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
        playerController = GameManager.Instance.player;
    }

    public void StartTutorial()
    {
        playerController.SetControl(false);
        canvasImage.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
        timer.PauseTimer();
        Time.timeScale = 0f;
        text.text = tutorialInfos[currentTutorialIndex].text;
        image.sprite = tutorialInfos[currentTutorialIndex].sprite;
    }

    public void StopTutorial()
    {
        Debug.Log("클릭");
        playerController.SetControl(true);
        Time.timeScale = 1f;
        currentTutorialIndex++;
        timer.StartTimer();
        canvasImage.gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
    }
}
