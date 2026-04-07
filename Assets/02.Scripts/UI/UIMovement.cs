using DG.Tweening;
using System;
using UnityEngine;

public class UIMovement : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] private float dur = 0.5f;     // 이동 시간
    [SerializeField] private float maxPos = 1200;  // 화면 밖으로 밀어낼 기준 값
    private RectTransform rectT;

    public event Action OnEnd;

    private void Awake()
    {
        rectT = GetComponent<RectTransform>();
    }

    public void AppearFromDown(float targetPosY = 0f)
    {
        rectT.anchoredPosition = new Vector2(rectT.anchoredPosition.x, -maxPos);
        rectT.DOAnchorPosY(targetPosY, dur).SetEase(Ease.OutBack, overshoot: 2).SetUpdate(true).OnComplete(() =>
        {
            OnEnd?.Invoke();
            OnEnd = null;
        });

    }

    /// <summary>
    /// 화면 위쪽(+Y)에서 아래로 등장
    /// </summary>
    public void AppearFromUp(float targetPosY = 0f)
    {
        rectT.anchoredPosition = new Vector2(rectT.anchoredPosition.x, maxPos);
        rectT.DOAnchorPosY(targetPosY, dur).SetEase(Ease.OutBack, overshoot: 2).SetUpdate(true).OnComplete(() =>
        {
            OnEnd?.Invoke();
            OnEnd = null;
        });
    }

    /// <summary>
    /// 현재 위치에서 위로 퇴장
    /// </summary>
    public void DisappearToUp()
    {
        float oriPosY = rectT.anchoredPosition.y;
        rectT.DOAnchorPosY(maxPos, dur).SetEase(Ease.InBack, overshoot: 2).SetUpdate(true).OnComplete(() =>
        {
            UIManager.Instance.DoSomething = false;
            rectT.anchoredPosition = new Vector2(rectT.anchoredPosition.x, oriPosY);
            OnEnd?.Invoke();
            OnEnd = null;
        });

    }

    /// <summary>
    /// 현재 위치에서 아래로 퇴장
    /// </summary>
    public void DisappearToDown()
    { 
        float oriPosY = rectT.anchoredPosition.y;
        rectT.DOAnchorPosY(-maxPos, dur).SetEase(Ease.InBack, overshoot: 2).SetUpdate(true).OnComplete(() =>
        {
            UIManager.Instance.DoSomething = false;
            rectT.anchoredPosition = new Vector2(rectT.anchoredPosition.x, oriPosY);
            OnEnd?.Invoke();
            OnEnd = null;
        });


    }
}
