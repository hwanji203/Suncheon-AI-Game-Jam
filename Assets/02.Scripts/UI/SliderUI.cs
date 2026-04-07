using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class SliderUI : MonoBehaviour
{
    [SerializeField] private SliderValue myValueType;
    [SerializeField] private GameObject changeable;
    [SerializeField] private int maxValue;
    [SerializeField] private AudioClip changeSliderClip;
    [SerializeField] private TextMeshProUGUI value;
    [SerializeField] private Image icon;
    [SerializeField] private Sprite[] sprites;

    private Slider slider;

    private SoundManager soundManager;
    private SaveManager saveManager;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        soundManager = SoundManager.Instance;
        saveManager = SaveManager.Instance;

        Initialized();

    }


    private void ChangeValue(float sliderValue)
    {
        saveManager.SaveValue(myValueType.ToString(), sliderValue / maxValue);
        value.text = sliderValue.ToString();
        if (changeable.TryGetComponent(out IChangeable change))
        {
            change.ChangeValue();
        }
        else
        {
            Debug.Log("조정하려는 스크립트가 IChangeable을 구현해야 합니다.");
        }

        if (sliderValue == 0) { icon.sprite = sprites[0]; }
        else { icon.sprite = sprites[1]; }
    }

    private void Initialized()
    {
        slider.maxValue = maxValue;
        slider.wholeNumbers = true;
        slider.value = PlayerPrefs.GetFloat(myValueType.ToString()) * maxValue;
        value.text = slider.value.ToString();


        slider.onValueChanged.AddListener((sliderValue) =>
        {
            soundManager.Play(SFXSoundType.SliderValueChange);
            ChangeValue(sliderValue);
        });
    }
}
