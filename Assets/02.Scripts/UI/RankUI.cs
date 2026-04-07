using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : AbstractUI
{
    [SerializeField] private GameObject rankBar;
    [SerializeField] private Transform content;

    [SerializeField] private RankBarListSO barListSO;

    [SerializeField] private float delay;

    public IEnumerator OpenRankUI()
    {
        if (RankManager.Instance.rankList.ranks.Count == 0)
        {
            Debug.Log("랭킹이 비어있습니다.");
            StopCoroutine(OpenRankUI());
        }
        else
        {
            for (int i = 0; i < RankManager.Instance.rankList.ranks.Count; i++)
            {
                bool isDefault = i >= barListSO.list.Count - 1;
                RankBarSO barSO = barListSO.list[isDefault ? barListSO.list.Count - 1 : i];

                RankData data = RankManager.Instance.rankList.ranks[i];
                GameObject rank = Instantiate(rankBar, content);

                //바 텍스트 조정
                TextMeshProUGUI tmp = rank.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                tmp.fontSize = barSO.fontSize;
                tmp.text = $"{i + 1}위: {data.playerName} - {data.score}";

                //바 사이즈 조정
                RectTransform rect = rank.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, barSO.yValue);

                //아이콘 또는 텍스트 조정
                Transform iconTransform = rank.transform.GetChild(0).transform;

                if (isDefault)
                {
                    TextMeshProUGUI text = iconTransform.GetComponentInChildren<TextMeshProUGUI>();
                    for (int j = 0; j < iconTransform.childCount; j++)
                    {
                        iconTransform.GetChild(j).gameObject.SetActive(false);
                    }
                    text.gameObject.SetActive(true);
                    text.text = (i + 1).ToString();
                }
                else
                {
                    Image icon = iconTransform.GetComponentInChildren<Image>();
                    for (int j = 0; j < iconTransform.childCount; j++)
                    {
                        iconTransform.GetChild(j).gameObject.SetActive(false);
                    }
                    icon.gameObject.SetActive(true);
                    icon.sprite = barSO.icon;
                    icon.GetComponent<RectTransform>().sizeDelta = new Vector2(barSO.iconSize, barSO.iconSize);
                }

                yield return new WaitForSeconds(delay);
            }
        }
    }

    public override void OnUIOpen()
    {
        base.OnUIOpen();
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        StartCoroutine(OpenRankUI());
    }
}
