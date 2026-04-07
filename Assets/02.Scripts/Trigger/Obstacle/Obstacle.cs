using UnityEngine;

public class Obstacle : TriggerBase
{
    // 카메라 흔들림 강도에 대한 변수입니다.
    [SerializeField] protected float cameraShakeIntensity;
    
    // 제한하는 속도에 대한 변수입니다.
    [SerializeField] protected float limitedIntensity;

    protected void Awake()
    {
        base.Awake();
    }
    
    protected void Start()
    {
        base.Start();
    }

    public virtual void Trigger()
    {
        
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;
        
        base.OnTriggerEnter(other);
        
        Debug.Log("Obstacle Enter");
        
        CameraManager.Instance.ShakeCamera(cameraShakeIntensity, 1.0f);
        BirdManager.Instance.LoseBird();
        GameManager.Instance.player.ResetSpeed(limitedIntensity);

        SoundManager.Instance.Play(SFXSoundType.Hited);
    }
    
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
    
}
