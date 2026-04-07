using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BirdPrefabEntry
{
    public BirdType type;
    public GameObject prefab;
}

public class BirdManager : MonoSingleton<BirdManager>
{
    [Header("게임 설정")]
    [SerializeField] private List<BirdPrefabEntry> birdPrefabs;
    [SerializeField] private Transform playerTransform;

    [Header("무리 대형 설정")]
    [SerializeField] private float horizontalSpacing = 1.2f;
    [SerializeField] private float depthSpacing = 1.5f;
    [SerializeField] private float moveTime = 0.1f;

    [Header("회전 설정")]
    [SerializeField] private float maxTiltAngle = 20f;
    [SerializeField] private float rotationDelay = 0.08f;

    [Header("UI 설정")]
    public GameObject gameSceneUI;
    private List<FollowingBird> flock = new List<FollowingBird>();

    private float previousInput = 0f;
    private Coroutine rotationCoroutine;
    public PlayerController controller;
    public event Action<int> OnTotalCountChanged;
    public int totalCount = 0;
    public int TotalCount { get; private set; } = 0;
    public int FlockCount()
    {
        return flock.Count;
    }

    public void AddCount(int count)
    {
        if (count == 0) return;
        TotalCount += count;
        OnTotalCountChanged?.Invoke(TotalCount);
    }

    public void LoseCount()
    {
        TotalCount -=1;
        OnTotalCountChanged?.Invoke(TotalCount);
    }

    public void ResetCount(int count = 0)
    {
        TotalCount = count;
        OnTotalCountChanged?.Invoke(TotalCount);
    }

    void Update()
    {
        if (controller == null) return;
        float currentInput = controller.HorizontalInput;

        if (currentInput != previousInput)
        {
            if (rotationCoroutine != null)
            {
                StopCoroutine(rotationCoroutine);
            }
            rotationCoroutine = StartCoroutine(TriggerFlockRotation(currentInput));
        }
        previousInput = currentInput;
    }
    private IEnumerator TriggerFlockRotation(float currentInput)
    {
        float targetAngle = currentInput * maxTiltAngle;

        controller.SetTargetRotation(targetAngle);

        if (currentInput < 0)
        {
            if (flock.Count > 0 && flock[0] != null)
            {
                flock[0].SetTargetRotation(targetAngle);
            }
            yield return new WaitForSeconds(moveTime);

            if (flock.Count > 2 && flock[2] != null)
            {
                flock[2].SetTargetRotation(targetAngle);
            }
            yield return new WaitForSeconds(moveTime);

            if (flock.Count > 1 && flock[1] != null)
            {
                flock[1].SetTargetRotation(targetAngle);
            }
            yield return new WaitForSeconds(moveTime);

            if (flock.Count > 3 && flock[3] != null)
            {
                flock[3].SetTargetRotation(targetAngle);
            }
        }
        else
        {
            if (flock.Count > 1 && flock[1] != null)
            {
                flock[1].SetTargetRotation(targetAngle);
            }
            yield return new WaitForSeconds(moveTime);

            if (flock.Count > 3 && flock[3] != null)
            {
                flock[3].SetTargetRotation(targetAngle);
            }
            yield return new WaitForSeconds(rotationDelay);

            if (flock.Count > 0 && flock[0] != null)
            {
                flock[0].SetTargetRotation(targetAngle);
            }
            yield return new WaitForSeconds(moveTime);

            if (flock.Count > 2 && flock[2] != null)
            {
                flock[2].SetTargetRotation(targetAngle);
            }
        }

    }
    public void AddNewBird(Vector3 spawnPosition, BirdType type)
    {
        var entry = birdPrefabs.Find(e => e.type == type);
        if (entry == null) { Debug.LogError($"{type} 프리팹 없음"); return; }

        // 어디에 생성해도 상관없음 (곧바로 스냅됨)
        GameObject go = Instantiate(entry.prefab, spawnPosition, Quaternion.identity);
        var fb = go.GetComponent<FollowingBird>();

        // 충돌 잠시 비활성
        if (go.TryGetComponent<Collider>(out var col))
        {
            col.enabled = false;
            StartCoroutine(EnableColliderNextFrame(col));
        }

        int birdIndex = flock.Count + 1;
        float side = (birdIndex % 2 == 0) ? 1f : -1f;      // 좌/우
        int row = (birdIndex + 1) / 2;

        Vector3 localOffset = new Vector3(
            row * horizontalSpacing * side,
            0f,
            -row * depthSpacing
        );

        // 플레이어 트랜스폼이 비었으면 컨트롤러에서 보충
        if (playerTransform == null && controller != null)
            playerTransform = controller.transform;

        fb.Setup(playerTransform, localOffset, snapImmediately: true); // ← 즉시 스냅
        flock.Add(fb);
    }

    public void LoseBird()
    {
        if (flock.Count > 0)
        {
            // 리스트의 마지막 새를 가져옴
            FollowingBird birdToRemove = flock[flock.Count - 1];
            LoseCount();
            flock.Remove(birdToRemove);

            // 새 이탈
            birdToRemove.BreakAway();

        }
        else
        {
            // 만약 따라오는 새가 없다면 플레이어가 죽음
            GameManager.Instance.HandlePlayerDeath();
            // 게임 오버 처리
        }
    }
    private IEnumerator EnableColliderNextFrame(Collider col)
    {
        yield return new WaitForSeconds(1f);
        if (col != null) col.enabled = true;
    }
}
