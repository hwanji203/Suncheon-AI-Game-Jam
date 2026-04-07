using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("Scale")]
    public float scaleMultiplier = 1.1f;

    [Header("Duration (seconds)")]
    public float enterDuration = 0.12f;
    public float exitDuration = 0.12f;
    public float downDuration = 0.08f; // 버튼 눌렀을 때 복귀하는 시간

    [Header("Easing")]
    public AnimationCurve easing;

    [Header("Timing")]
    public bool useUnscaledTime = true;

    private Vector3 _originalScale;
    private Coroutine _scaleRoutine;

    private void Awake()
    {
        _originalScale = transform.localScale;

        if (easing == null || easing.length == 0)
            easing = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 target = _originalScale * scaleMultiplier;
        StartScale(target, enterDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartScale(_originalScale, exitDuration);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 마우스를 누르는 순간 원래 크기로 복귀
        StartScale(_originalScale, downDuration);
    }

    private void StartScale(Vector3 targetScale, float duration)
    {
        if (_scaleRoutine != null)
            StopCoroutine(_scaleRoutine);

        _scaleRoutine = StartCoroutine(CoScale(targetScale, duration));
    }

    private IEnumerator CoScale(Vector3 targetScale, float duration)
    {
        Vector3 start = transform.localScale;
        if (duration <= 0f)
        {
            transform.localScale = targetScale;
            yield break;
        }

        float t = 0f;
        while (t < 1f)
        {
            float dt = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            t += dt / duration;
            float curve = easing.Evaluate(Mathf.Clamp01(t));
            transform.localScale = Vector3.LerpUnclamped(start, targetScale, curve);
            yield return null;
        }

        transform.localScale = targetScale;
        _scaleRoutine = null;
    }

    private void OnDisable()
    {
        if (_scaleRoutine != null)
        {
            StopCoroutine(_scaleRoutine);
            _scaleRoutine = null;
        }
        transform.localScale = _originalScale;
    }
}
