using UnityEngine;

public class Event : TriggerBase
{
    // 닿았을 때 스폰되는 프리팹에 대한 변수입니다. ex) 비, 번개
    [SerializeField] private GameObject eventPrefab;
    
    // 이벤트가 출력되는 위치를 나타내는 변수입니다.
    [SerializeField] private Vector3 spawnPoint;
    
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
        
        Vector3 position = spawnPoint + transform.position;
        Quaternion rotation = Quaternion.identity;
        
        Instantiate(eventPrefab, position, rotation);
        Debug.Log($"{eventPrefab.name} 스폰됐습니다 !");
    }
    
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
