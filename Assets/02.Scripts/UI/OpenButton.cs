using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenButton : MonoBehaviour
{
    [SerializeField] private UICategory opneUI;
    [SerializeField] private bool isClose;

    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        if (isClose == true)
        {
            button.onClick.AddListener(() =>
            {
                UIManager.Instance.CloseUI(opneUI);
            });
        }
        else
        {
            button.onClick.AddListener(() =>
            {
                if (opneUI is UICategory.BirdCollection) UIManager.Instance.CloseUI(UICategory.Clear);
                else if (opneUI is UICategory.Rank) UIManager.Instance.CloseUI(UICategory.BirdCollection);
                UIManager.Instance.OpenUI(opneUI);
            });
            
        }
    }


}
