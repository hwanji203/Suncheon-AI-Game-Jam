using UnityEngine;

public class DeadUI : AbstractUI
{
    private UIMovement uiMove;

    protected override void Awake()
    {
        base.Awake();
        uiMove = GetComponent<UIMovement>();
    }

    public override void OnUIOpen()
    {
        gameObject.SetActive(true);
        uiMove.AppearFromUp(GetComponent<RectTransform>().anchoredPosition.y);
        uiMove.OnEnd += base.OnUIOpen;
    }

    public override void OnUIClose()
    {
        uiMove.DisappearToDown();
        uiMove.OnEnd += base.OnUIClose;
    }
}
