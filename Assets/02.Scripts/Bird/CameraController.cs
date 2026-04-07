using TMPro;
using UnityEngine;
using UnityEngine.Splines;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SplineContainer spline;  
    [SerializeField] private PlayerController player;  
    [Header("Settings")]
    [SerializeField] private Vector3 localOffset = new Vector3(0, 5f, -12f);
    [SerializeField] private float followSmoothness = 5f;
    void FixedUpdate()
    {
        if (spline == null || player == null) return;

        float t = player.GetDistanceAlongSpline();

        Vector3 splinePos = (Vector3)spline.EvaluatePosition(t);
        Vector3 splineForward = ((Vector3)spline.EvaluateTangent(t)).normalized;
        Vector3 splineRight = Vector3.Cross(Vector3.up, splineForward).normalized;
        Vector3 splineUp = Vector3.Cross(splineForward, splineRight);

        Vector3 worldOffset = splineRight * localOffset.x
                            + splineUp * localOffset.y
                            + splineForward * localOffset.z;


        Vector3 targetPos = splinePos + worldOffset;
        targetPos.y = player.transform.position.y + localOffset.y;

        transform.position = Vector3.Lerp(transform.position, targetPos, followSmoothness * Time.deltaTime);

        transform.LookAt(player.transform.position + splineForward * 5f, Vector3.up);
    }
}
