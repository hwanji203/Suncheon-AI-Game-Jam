using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.BoolParameter;

public class BirdUIManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private RectTransform backGroundPanel; 
    [SerializeField] private TextMeshProUGUI birdNameText;
    [SerializeField] private TextMeshProUGUI birdExplainText;
    [SerializeField] private Image birdPicture;  

    [Header("Bird Sprites")]
    [SerializeField] private Sprite craneSprite;
    [SerializeField] private Sprite yellowSprite;
    [SerializeField] private Sprite greenSprite;
    [SerializeField] private Sprite dovySprite;
    [SerializeField] private Sprite phoenixSprite;

    [Header("Animation Settings")]
    [SerializeField] private float slideDuration = 0.5f;
    [SerializeField] private float offsetX = -500f;

    private Vector2 originalPos;
    public static BirdUIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        originalPos = backGroundPanel.anchoredPosition;
        backGroundPanel.anchoredPosition = new Vector2(offsetX, originalPos.y);
    }

    public void UpdateBirdUI(BirdType type)
    {
        switch (type)
        {
            case BirdType.crane:
                birdNameText.text = "두루미";
                birdExplainText.text = "두루미는 장수와 평화를 상징합니다.";
                birdPicture.sprite = craneSprite;
                break;

            case BirdType.yellow:
                birdNameText.text = "솔새속";
                birdExplainText.text = "솔새는 희망과 따뜻함을 전해줍니다.";
                birdPicture.sprite = yellowSprite;
                break;
            case BirdType.green:
                birdNameText.text = "멋쟁이새";
                birdExplainText.text = "멋쟁이새는 고운 빛깔로 사랑과 품격을 상징합니다.";
                birdPicture.sprite = yellowSprite;
                break;

            case BirdType.dovy:
                birdNameText.text = "케찰";
                birdExplainText.text = "케찰은 자유와 생명을 상징하는 신성한 새입니다.";
                birdPicture.sprite = dovySprite;
                break;
            
            case BirdType.phoenix:
                birdNameText.text = "봉황";
                birdExplainText.text = "봉황은 부활과 영원한 생명을 의미합니다.";
                birdPicture.sprite = phoenixSprite;
                break;
        }

        backGroundPanel.anchoredPosition = new Vector2(offsetX, originalPos.y);

        Sequence seq = DOTween.Sequence();
        seq.Append(backGroundPanel.DOAnchorPos(originalPos, 0.5f).SetEase(Ease.OutCubic)); // 등장
        seq.AppendInterval(slideDuration); // 대기
        seq.Append(backGroundPanel.DOAnchorPos(new Vector2(offsetX, originalPos.y), 0.5f).SetEase(Ease.InCubic)); // 퇴장
    }
}
