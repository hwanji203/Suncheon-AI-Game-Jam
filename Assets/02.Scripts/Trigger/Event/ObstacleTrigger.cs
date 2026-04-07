using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : TriggerBase
{
    // 짱뚱어에 대한 참조 변수입니다.
    [SerializeField] private List<Obstacle> obstacles;
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

        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].Trigger();
        }
    }
    
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
