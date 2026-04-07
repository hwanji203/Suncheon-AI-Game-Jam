using UnityEngine;

public class MudSkipper : Obstacle 
{
    private Animator animator;
    void Awake()
    {
        base.Awake();
        
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        base.Start();
        
        // @TODO: 물에서 뛰어오르는 애니메이션 재생
    }

    public override void Trigger()
    {
        base.Trigger();
        
        animator.SetBool("IsTrigger", true);
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;
        
        base.OnTriggerEnter(other);
    }
    
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
