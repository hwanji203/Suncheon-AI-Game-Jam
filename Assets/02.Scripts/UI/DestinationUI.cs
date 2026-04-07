using UnityEngine;
using UnityEngine.Splines;

public class DestinationUI : MonoBehaviour
{
    [Header("Spline & Player")]
    [SerializeField] private SplineContainer spline;
    [SerializeField] private PlayerController player;  

    [Header("t구간 소스")]
    [SerializeField] private bool useSplineEnds = true; 
    [SerializeField] private Transform startWorld;     
    [SerializeField] private Transform goalWorld;      

    [Header("UI (같은 부모 기준)")]
    [SerializeField] private RectTransform container;  
    [SerializeField] private RectTransform marker;      
    [SerializeField] private RectTransform bottomBound; 
    [SerializeField] private RectTransform topBound;   

    [Header("옵션")]
    [SerializeField] private bool useRemaining = false;
    [SerializeField] private float smooth = 12f;

    float tStart = 0f, tGoal = 1f;
    float shownY;

    void Reset()
    {
        container = (RectTransform)transform;
    }

    void Start()
    {
        ComputeStartGoalT();
        if (marker) shownY = marker.anchoredPosition.y;
    }

    void LateUpdate()
    {
        if (!player || !spline || !container || !marker || !bottomBound || !topBound) return;

        float tPlayer = Mathf.Clamp01(player.GetDistanceAlongSpline()); 
        float p = InverseLerpUnclamped(tStart, tGoal, tPlayer);        
        p = Mathf.Clamp01(p);
        if (useRemaining) p = 1f - p;

        float yMin = GetLocalY(bottomBound);
        float yMax = GetLocalY(topBound);
        float targetY = Mathf.Lerp(yMin, yMax, p);

        shownY = Mathf.Lerp(shownY, targetY, Time.unscaledDeltaTime * smooth);
        var pos = marker.anchoredPosition;
        pos.y = shownY;
        marker.anchoredPosition = pos;
    }

    void ComputeStartGoalT(int samples = 256)
    {
        if (!spline) return;

        if (useSplineEnds || (!startWorld && !goalWorld))
        {
            tStart = 0f;
            tGoal = 1f;
        }
        else
        {
            if (startWorld) tStart = FindNearestT(startWorld.position, samples);
            if (goalWorld) tGoal = FindNearestT(goalWorld.position, samples);
        }
    }
    float FindNearestT(Vector3 worldPos, int samples)
    {
        float bestT = 0f, best = float.MaxValue;
        for (int i = 0; i < samples; i++)
        {
            float t = i / (float)(samples - 1);
            Vector3 p = (Vector3)spline.EvaluatePosition(t);
            float d = (p - worldPos).sqrMagnitude;
            if (d < best) { best = d; bestT = t; }
        }
        return bestT;
    }

    float InverseLerpUnclamped(float a, float b, float v)
    {
        if (Mathf.Approximately(a, b)) return 0f;
        return (v - a) / (b - a);
    }
    float GetLocalY(RectTransform rect)
    {
        if (rect.parent == container) return rect.anchoredPosition.y;
        Vector3 local = container.InverseTransformPoint(rect.position);
        return local.y;
    }
}
