using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private CinemachineCamera cineCamera;
    private CinemachineBasicMultiChannelPerlin noise;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    void Awake()
    {
        // 이 스크립트가 붙어있는 오브젝트에서 CinemachineCamera 가져오기
        cineCamera = GetComponent<CinemachineCamera>();

        // Noise Component 가져오기
        noise = cineCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
    }
    
    public void ShakeCamera(float intensity, float time)
    {
        if (noise == null) return;

        noise.AmplitudeGain = intensity;
        noise.FrequencyGain = intensity;

        startingIntensity = intensity;
        shakeTimer = time;
        shakeTimerTotal = time;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            float normalized = shakeTimer / shakeTimerTotal;

            // 흔들림이 점점 줄어들도록 보간
            noise.AmplitudeGain = Mathf.Lerp(0f, startingIntensity, normalized);
            noise.FrequencyGain = Mathf.Lerp(0f, startingIntensity, normalized);
        }
    }
}
