using TMPro;
using UnityEngine;

public class BirdCountUI : MonoBehaviour
{
    [SerializeField] private TMP_Text birdCount;

    void OnEnable()
    {
        if (BirdManager.Instance == null) return;

        BirdManager.Instance.OnTotalCountChanged += UpdateText;
        // 초기 표시
        UpdateText(BirdManager.Instance.TotalCount);
    }

    void OnDisable()
    {
        if (BirdManager.Instance == null) return;
        BirdManager.Instance.OnTotalCountChanged -= UpdateText;
    }

    private void UpdateText(int current)
    {
        if (birdCount) birdCount.text = current.ToString();
    }
}
