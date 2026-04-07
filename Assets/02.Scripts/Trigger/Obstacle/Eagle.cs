using System;
using UnityEngine;

public class Eagle : Obstacle
{
    [SerializeField] private float acceleration = 50f; // 가속도
    [SerializeField] private float maxSpeed = 300f;    // 최대 속도
    private float currentSpeed = 0f;                  // 현재 속도
    private bool isActivated = false;
    void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (!isActivated) return;
        
        // 속도를 가속
        currentSpeed += acceleration * Time.deltaTime;

        // 최대 속도 제한
        currentSpeed = Mathf.Min(currentSpeed, maxSpeed);

        // 전방으로 이동
        transform.position += -(transform.right * currentSpeed * Time.deltaTime);
    }

    public override void Trigger()
    {
        isActivated = true;
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
