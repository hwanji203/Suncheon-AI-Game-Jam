using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoSingleton<FadeManager>
{
    [SerializeField] private float fadeDuration = 2.0f;

    private Canvas canvas;
    private Image fadeImage;

    void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        fadeImage = GetComponentInChildren<Image>();
    }
    
    void Start()
    {
        canvas.worldCamera = Camera.main;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }
    public void FadeOut(string scene)
    {
        StartCoroutine(FadeOutCoroutine(scene));
    }

    private IEnumerator FadeInCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = 1f - Mathf.Clamp01(elapsed / fadeDuration); // 1 → 0으로 감소
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(0f); // 완전히 투명하게
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration); // 0 → 1로 증가
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(1f); // 완전히 불투명 (검은 화면)
    }

    private IEnumerator FadeOutCoroutine(string scene)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration); // 0 → 1로 증가
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(1f); // 완전히 불투명 (검은 화면)
        SceneManager.LoadScene(scene);
    }

    private void SetAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = alpha;
            fadeImage.color = c;
        }
    }
}
