using UnityEngine;

public class ClearUI : AbstractUI
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
        uiMove.AppearFromDown(GetComponent<RectTransform>().anchoredPosition.y);
        uiMove.OnEnd += base.OnUIOpen;
    }

    public override void OnUIClose()
    {
        UIManager.Instance.DoSomething = false;
    }
}
