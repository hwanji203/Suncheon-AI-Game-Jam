using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CutsceneUI : AbstractUI
{
    [SerializeField] private Image background;
    [SerializeField] private Image cutsceneImage;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CutSceneListSO cutsceneData;

    private IEnumerator PlayCutscene()
    { 
        background.enabled = true;
        background.color = new Color(0, 0, 0, 0);
        background.DOFade(1f, 1f); 
        
        cutsceneImage.enabled = false;
        text.enabled = false;
        yield return new WaitForSeconds(1f);

        cutsceneImage.enabled = true;
        text.enabled = true;

        foreach (var element in cutsceneData.list)
        {
            // 이미지 교체 + 페이드 인
            cutsceneImage.sprite = element.image;
            cutsceneImage.color = new Color(1, 1, 1, 0);
            cutsceneImage.DOFade(1f, 0.5f);

            // 텍스트 초기화
            text.text = "";
            text.alpha = 1f;

            // 타자 치기 효과 시작
            yield return StartCoroutine(TypeText(element.text, element.duration * 0.1f));

            // 남은 30% 동안 유지
            yield return new WaitForSeconds(element.duration * 0.5f);

            // 다음 컷으로 넘어가기 전 페이드 아웃
            cutsceneImage.DOFade(0f, 0.5f);
            text.DOFade(0f, 0.5f);
            yield return new WaitForSeconds(0.5f);

            // 다음을 위해 알파 초기화
            text.alpha = 1f;
        }

        OnUIClose();
        SceneManager.LoadScene("InGame");
    }

    private IEnumerator TypeText(string fullText, float typingDuration)
    {
        text.text = fullText;
        text.maxVisibleCharacters = 0;

        // 글자당 대기 시간
        float delay = typingDuration / fullText.Length;

        for (int i = 0; i < fullText.Length; i++)
        {
            text.maxVisibleCharacters = i + 1;
            yield return new WaitForSeconds(delay);
        }

        Debug.Log("컷신 텍스트 표시 완료");
    }

    public override void OnUIOpen()
    {
        base.OnUIOpen();
        StartCoroutine(PlayCutscene());
    }

    public override void OnUIClose()
    {
        Time.timeScale = 1;
        base.OnUIClose();
        UIManager.Instance.DoSomething = false;
    }
}
