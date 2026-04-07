using System;
using UnityEngine;

public class Reed : Obstacle
{
    void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        base.Start();
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
