using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text timerText;    
    [Header("Options")]
    [SerializeField] private bool autoStart = true;  
    [SerializeField] private bool useUnscaledTime = true;

    private float elapsed;      
    private bool isRunning;
    private int finalTimeInt;  

    public float Elapsed => elapsed;         
    public int FinalTimeInt => finalTimeInt; 

    void Awake()
    {
        if (!timerText) TryGetComponent(out timerText);
    }

    void Start()
    {
        ResetTimer();
        if (autoStart) StartTimer();
        UpdateText(elapsed);
    }

    void Update()
    {
        if (!isRunning) return;

        elapsed += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        UpdateText(elapsed);
    }

    private void UpdateText(float t)
    {
        if (timerText) timerText.text = t.ToString("00.0");
    }


    public void StartTimer() => isRunning = true;
    public void PauseTimer() => isRunning = false;

    public int StopTimer(bool floor = true)
    {
        isRunning = false;
        finalTimeInt = floor ? Mathf.FloorToInt(elapsed) : Mathf.RoundToInt(elapsed);
        return finalTimeInt;
    }

    public void ResetTimer()
    {
        isRunning = false;
        elapsed = 0f;
        finalTimeInt = 0;
        UpdateText(elapsed);
    }

    public int GetFinalTimeInt() => finalTimeInt;
}
