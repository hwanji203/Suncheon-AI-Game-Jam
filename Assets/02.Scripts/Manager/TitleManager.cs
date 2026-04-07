using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;

    private void Awake()
    {
        InteractableChange(true);
    }

    public void InteractableChange(bool value)
    {
        foreach (Button button in buttons)
        {
            button.interactable = value;
        }
    }

    public void OnStartBtnClicked()
    {

        if (!UIManager.Instance.DoSomething)
        {
            FadeManager.Instance.FadeOut("InputAndCutscene");
        }
    }

    public void OnSettingsBtnClicked()
    {
        if (!UIManager.Instance.DoSomething)
        {
            InteractableChange(false);
        }
    }

    public void OnExitBtnClicked()
    {
        if (!UIManager.Instance.DoSomething)
        {
            InteractableChange(false);
        }
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
