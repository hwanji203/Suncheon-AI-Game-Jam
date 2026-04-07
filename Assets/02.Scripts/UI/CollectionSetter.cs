using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionSetter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI explaneTmp;
    [SerializeField] private TextMeshProUGUI nameTmp;
    [SerializeField] private TextMeshProUGUI meetCountTmp;
    [SerializeField] private Image image;

    [SerializeField] private Sprite noneSprite;

    public void SetCollection(CollectionSO collection)
    {
        int meetCount = PlayerPrefs.GetInt(collection.birdName, 0);

        if (meetCount <= 0)
        {
            nameTmp.text = "???";
            explaneTmp.text = "???";
            meetCountTmp.text = $"같이 이동한 횟수 : {meetCount}";
            image.sprite = noneSprite;
        }
        else
        {
            nameTmp.text = collection.birdName;
            explaneTmp.text = collection.explain;
            meetCountTmp.text = $"같이 이동한 횟수 : {meetCount}";
            image.sprite = collection.birdPicture;
        }
    }
}
