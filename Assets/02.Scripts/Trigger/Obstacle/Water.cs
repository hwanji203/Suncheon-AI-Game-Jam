using UnityEngine;

public class Water : TriggerBase
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
        if (!other.CompareTag("Player")) return;
        
        BirdManager.Instance.LoseBird();
        GameManager.Instance.player.ResetSpeed(5f);
        
        Vector3 currentPos = GameManager.Instance.player.transform.position;
        currentPos.y += 10.0f;

        GameManager.Instance.player.transform.position = currentPos;
    }
    
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
