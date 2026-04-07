using DG.Tweening;
using System;
using UnityEngine;

public class RankBarAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform iconRect;
    [SerializeField] private float iconStartY;
    [SerializeField] private RectTransform textRect;
    [SerializeField] private float textStartX;

    [SerializeField] private float moveDur;

    private void OnEnable()
    {
        EnableMove();
    }

    private void EnableMove()
    {
        iconRect.anchoredPosition = new Vector2(iconRect.anchoredPosition.x, iconStartY);
        textRect.anchoredPosition = new Vector2(textStartX, textRect.anchoredPosition.y);

        Sequence seq = DOTween.Sequence().SetUpdate(true);
        seq.Join(iconRect.DOAnchorPosY(0, moveDur).SetEase(Ease.OutElastic));
        seq.Join(textRect.DOAnchorPosX(-20, moveDur).SetEase(Ease.OutQuad));
    }
}
