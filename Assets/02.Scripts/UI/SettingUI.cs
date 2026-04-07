using UnityEngine;

public class SettingUI : AbstractUI
{
    private UIMovement uiMove;

    protected override void Awake()
    {
        base.Awake();
        uiMove = GetComponent<UIMovement>();
    }

    public override void OnUIOpen()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        uiMove.AppearFromUp(GetComponent<RectTransform>().anchoredPosition.y);
        uiMove.OnEnd += base.OnUIOpen;
    }

    public override void OnUIClose()
    {
        Time.timeScale = 1;
        uiMove.DisappearToUp();
        uiMove.OnEnd += base.OnUIClose;
    }
}
