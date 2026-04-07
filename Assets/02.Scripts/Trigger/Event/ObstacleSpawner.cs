using UnityEngine;

public class ObstacleSpawner : TriggerBase
{
    // 닿았을 때 스폰되는 프리팹에 대한 변수입니다. ex) 짱뚱어, 독수리
    [SerializeField] private GameObject obstaclePrefab;
    
    // 장애물이 소환되는 위치에 대한 변수입니다.
    private Vector3 obstacleSpawnPoint;
    protected void Awake()
    {
        base.Awake();
        
        obstacleSpawnPoint = transform.GetChild(0).position;
    }
    
    protected void Start()
    {
        base.Start();
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;
        
        base.OnTriggerEnter(other);
        
        Quaternion rotation = Quaternion.identity;
        
        Instantiate(obstaclePrefab, obstacleSpawnPoint, rotation);
        Debug.Log($"{obstaclePrefab.name} 스폰됐습니다 !");    
    }
    
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
