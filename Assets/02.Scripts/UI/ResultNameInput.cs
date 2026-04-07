using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using NUnit.Framework.Internal.Commands; // DOTween 사용

public enum WarningText
{
    None,
    NoText,
    TooLong
}

public class ResultNameInput : AbstractUI
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private int maxTextLength;

    private string playerName = string.Empty;
    private Color defaultColor;            
    private RectTransform warningRect;     
    private Vector2 defaultWarningPos;     
    private bool isSelected;

    public string PlayerName() => playerName;
    private void Update()
    {
        // 엔터 입력 처리
        if (Input.GetKeyDown(KeyCode.Return) && !isSelected)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                Warning(WarningText.NoText);
                return;
            }

            if (playerName.Length > maxTextLength)
            {
                Warning(WarningText.TooLong);
                return;
            }

            // 정상 입력이면 저장
            GameManager.Instance.SetPlayerName(playerName);

            // ���� �� ȿ�� ���� (��: ��� ��� �������� ȿ��)
            OnUIClose();
        }
    }

    private void Warning(WarningText type)
    {
        switch (type)
        {
            case WarningText.NoText:
                ShowWarning("이름을 입력해주세요!");
                break;
            case WarningText.TooLong:
                ShowWarning($"이름은 {maxTextLength}자 이하로 입력해주세요!");
                break;
            default:
                warningText.text = string.Empty;
                break;
        }
    }

    private void ShowWarning(string msg)
    {
        Sequence seq = DOTween.Sequence();

        warningText.text = msg;
        warningText.color = Color.red;

        // 애니메이션 시작 전에 위치 초기화
        warningRect.anchoredPosition = defaultWarningPos;

        // 흔들림 애니메이션
        seq.Join(warningRect.DOShakeAnchorPos(0.5f, new Vector2(10f, 0f), 10, 90)
            .OnComplete(() =>
            {
                // 색상과 위치 복귀
                warningText.color = defaultColor;
                warningRect.anchoredPosition = defaultWarningPos;
            }));
        seq.AppendInterval(1);
        seq.Append(warningText.DOFade(0, 0.6f));
    }

    private void UpdatePlayerName(string input)
    {
        playerName = input;
    }

    public override void OnUIOpen()
    {
        UIManager.Instance.DoSomething = false;

        gameObject.SetActive(true);
        GetComponent<UIMovement>().AppearFromDown(GetComponent<RectTransform>().anchoredPosition.y);
        GetComponent<UIMovement>().OnEnd += base.OnUIOpen;

        nameInputField.interactable = true;
        nameInputField.text = string.Empty;
        isSelected = false;

        nameInputField.onValueChanged.AddListener(UpdatePlayerName);
        warningText.text = string.Empty;

        defaultColor = warningText.color;
        warningRect = warningText.GetComponent<RectTransform>();
        defaultWarningPos = warningRect.anchoredPosition;
    }

    public override void OnUIClose()
    {
        Time.timeScale = 1;
        isSelected = true;
        nameInputField.interactable = false;

        UIManager.Instance.DoSomething = false;
        nameInputField.onValueChanged.RemoveListener(UpdatePlayerName);

        UIManager.Instance.OpenUI(UICategory.Cutscene);

        GetComponent<UIMovement>().DisappearToDown();
        GetComponent<UIMovement>().OnEnd += base.OnUIClose;

    }
}
