using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum BirdType
{
    crane,
    yellow,
    green,
    dovy,
    phoenix
}
public class FollowingBird : MonoBehaviour
{
    [SerializeField] private BirdType birdType;   // 프리팹마다 직접 지정
    [field: SerializeField] public CollectionSO CollectionSO { get; private set; }
    public BirdType Type => birdType;

    [SerializeField] private float followSmoothness = 6f; // 클수록 빨리 붙음
    [SerializeField] private float tiltSpeed = 8f;

    private Transform target;
    private Vector3 localOffset;   
    private bool isFollowing = false;
    private float targetTiltAngle = 0f;

    public void Setup(Transform player, Vector3 formationLocalOffset, bool snapImmediately = true)
    {
        target = player;
        localOffset = formationLocalOffset;
        isFollowing = true;

        if (snapImmediately)
        {
            Vector3 worldOffset = player.right * localOffset.x
                                + Vector3.up * localOffset.y
                                + player.forward * localOffset.z;
            transform.position = player.position + worldOffset;
            transform.rotation = Quaternion.LookRotation(player.forward, Vector3.up);
        }

        BirdUIManager.Instance.UpdateBirdUI(birdType);
    }

    public void SetTargetRotation(float angle) => targetTiltAngle = angle;

    void FixedUpdate()
    {
        if (!isFollowing || target == null) return;

        Vector3 worldOffset = target.right * localOffset.x
                            + Vector3.up * localOffset.y
                            + target.forward * localOffset.z;

        Vector3 desired = target.position + worldOffset;

        float k = 1f - Mathf.Exp(-followSmoothness * Time.fixedDeltaTime);
        transform.position = Vector3.Lerp(transform.position, desired, k);

        Quaternion face = Quaternion.LookRotation(target.forward, Vector3.up);
        Quaternion tilt = Quaternion.Euler(0f, targetTiltAngle, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, face * tilt, tiltSpeed * Time.fixedDeltaTime);
    }

    public void BreakAway()
    {
        isFollowing = false;
        if (TryGetComponent<Collider>(out var col))
        {
            col.enabled = false;
            StartCoroutine(EnableColliderNextFrame(col));
        }
        if (!TryGetComponent<Rigidbody>(out var rb))
            rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = true;
        Destroy(gameObject, 3f);
    }
    private IEnumerator EnableColliderNextFrame(Collider col)
    {
        yield return new WaitForSeconds(1f);
        if (col != null) col.enabled = true;
    }
}
