using UnityEngine;

public class TriggerBase : MonoBehaviour
{
    protected bool isTriggered = false;
    
    protected void Awake()
    {
        
    }
    
    protected void Start()
    {
        
    }
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;
        
        isTriggered = true;
        Debug.Log(this.gameObject.name + "(이)가 " + other.gameObject.name + "(와)과 충돌했습니다.");
    }
    
    protected virtual void OnTriggerExit(Collider other)
    {
        Debug.Log(this.gameObject.name + "(이)가 " + other.gameObject.name + "를 벗어났습니다.");
    }
}
