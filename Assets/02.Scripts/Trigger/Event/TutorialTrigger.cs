using UnityEngine;

public class TutorialTrigger : TriggerBase
{
    protected void Awake()
    {
        base.Awake();
    }
    
    protected void Start()
    {
        base.Start();
    }
    
    
    protected override void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;
        
        base.OnTriggerEnter(other);

        TutorialManager.Instance.StartTutorial();
    }
    
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
