using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Lights")]
    [SerializeField] private Light sunLight;   
    [SerializeField] private Light moonLight;  
    
    [SerializeField] private Material daySky;
    
    [Header("Cycle Settings")]
    [SerializeField] private float cycleDuration = 150f;
    [SerializeField] private float startTimeOfDay = 0f;
    
    private float timeOfDay = 0f;

    void Start()
    {
        timeOfDay = startTimeOfDay; 
        RenderSettings.skybox = daySky;
    }
    
    private void Update()
    {
        timeOfDay += Time.deltaTime / cycleDuration;
        if (timeOfDay > 1f) timeOfDay -= 1f;

        ApplyCycle();
    }

    private void ApplyCycle()
    {
        Color dayColor = Color.white; 
        Color eveningColor = new Color(1f, 0.6f, 0.3f); 
        Color nightColor = new Color(0.2f, 0.2f, 0.5f);
        
        if (timeOfDay < 0.33f) 
        {
            float t = timeOfDay / 0.33f;
            sunLight.intensity = Mathf.Lerp(1.2f, 0.6f, t);
            sunLight.color = Color.Lerp(dayColor, eveningColor, t); 
            daySky.SetColor("_Tint", Color.Lerp(dayColor, eveningColor, t));

            moonLight.enabled = false;
        }
        else if (timeOfDay < 0.66f)
        {
            float t = (timeOfDay - 0.33f) / 0.33f;
            sunLight.intensity = Mathf.Lerp(0.6f, 0.2f, t);
            sunLight.color = Color.Lerp(eveningColor, nightColor, t);
            daySky.SetColor("_Tint", Color.Lerp(eveningColor, nightColor, t));
            
            moonLight.enabled = false;
        }
        else 
        {
            float t = (timeOfDay - 0.66f) / 0.34f;
            sunLight.intensity = Mathf.Lerp(0.2f, 1.2f, t);
            sunLight.color = Color.Lerp(nightColor, dayColor, t); 
            daySky.SetColor("_Tint", Color.Lerp(nightColor, dayColor, t));
            
            moonLight.enabled = true;
            moonLight.intensity = 0.4f;
            moonLight.color = new Color(0.6f, 0.7f, 1f);
        }

        DynamicGI.UpdateEnvironment();
    }
} 