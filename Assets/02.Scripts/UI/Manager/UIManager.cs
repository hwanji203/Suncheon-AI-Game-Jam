using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum UICategory
{
    Setting,
    Rank,
    Dead,
    Cutscene,
    NameInput,
    BirdCollection,
    Clear
}

public class UIManager : MonoSingleton<UIManager>
{
    private Dictionary<UICategory, AbstractUI> uiDictionary = new();
    private List<AbstractUI> uis = new();
    [field: SerializeField] public bool DoSomething { get; set; } = false;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CollectUI();

        SceneManager.activeSceneChanged += (was, now) =>
        {
            if (now.name == "InputAndCutscene")
            {
                OpenUI(UICategory.NameInput);
            }
        };
    }

    private void CollectUI()
    {
        var uiList = FindObjectsByType<AbstractUI>(FindObjectsSortMode.None);
        foreach (var ui in uiList)
        {
            if (!uiDictionary.ContainsKey(ui.MyCategory))
            {
                uis.Add(ui);
                uiDictionary[ui.MyCategory] = ui;
                ui.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && !DoSomething && Time.timeScale != 0)
        {
            OpenUI(UICategory.Setting);
        }
    }

    public void ReStartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        UISetFalse();
    }

    public void LobbyButton()
    {
        Time.timeScale = 1;
        UISetFalse();
        SceneManager.LoadScene("Title");
    }

    private void UISetFalse()
    {
        foreach (AbstractUI ui in uis)
        {
            uiDictionary[ui.MyCategory].gameObject.SetActive(false);
        }

        DoSomething = false;
    }

    public void OpenUI(UICategory uiCate)
    {
        if (uiDictionary.TryGetValue(uiCate, out var ui) && ui != null && !DoSomething)
        {
            Instance.DoSomething = true;
            ui.OnUIOpen();
        }
    }

    public void CloseUI(UICategory uiCate)
    {
        if (true)
        {
            uiDictionary[uiCate].OnUIClose();

            TitleManager title = FindAnyObjectByType<TitleManager>();
            if (title)
            {
                title.InteractableChange(true);
            }
        }
    }
}
