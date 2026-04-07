using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("움직임 설정")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float acceleration = 0.5f;
    [SerializeField] private GameObject playerRig;
    [SerializeField] private float maxJumpHeight = 5f;
    [SerializeField] private float maxMoveDistance = 7f;
    [Header("Spline 설정")]
    [SerializeField] private SplineContainer spline;       // 경로 참조
    private float distanceAlongSpline = 0f;
    [Header("완료 설정")]
    [SerializeField, Range(0.95f, 1f)] private float completeThresholdT = 0.999f;
    [SerializeField] private bool stopOnComplete = true;
    [SerializeField] private TimerUI timer;
    public float currentForwardSpeed = 0f;
    private Rigidbody rb;
    private bool isDead = false;
    private float horizontalInput;
    private float baseY;
    private float baseX;
    private float targetTiltAngle = 0f;
    private bool completed = false;
    public float HorizontalInput => horizontalInput;
    private float sideOffset = 0f;
    private bool canControl = true;
    public void SetControl(bool value) => canControl = value;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentForwardSpeed = forwardSpeed;

        // 스플라인 첫 위치에서 시작
        distanceAlongSpline = 0f;
        Vector3 startPos = (Vector3)spline.EvaluatePosition(distanceAlongSpline);
        rb.position = startPos;
        transform.position = startPos;

        baseY = startPos.y;
        baseX = startPos.x;
    }

    void Update()
    {
        if (!canControl) return;

        if (isDead)
        {
            horizontalInput = 0;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
            SoundManager.Instance.Play(SFXSoundType.BirdsSwing);
        }
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.X))
        {
            ResetSpeed();
        }
    }

    void FixedUpdate()
    {
        if (isDead || completed) return;
        
        currentForwardSpeed += acceleration * Time.fixedDeltaTime;
        distanceAlongSpline += (currentForwardSpeed * Time.fixedDeltaTime) / spline.CalculateLength();
        distanceAlongSpline = Mathf.Clamp01(distanceAlongSpline);

        // 스플라인 방향
        Vector3 splinePos = (Vector3)spline.EvaluatePosition(distanceAlongSpline);
        Vector3 splineForward = ((Vector3)spline.EvaluateTangent(distanceAlongSpline)).normalized;
        Vector3 splineRight = Vector3.Cross(Vector3.up, splineForward).normalized;
        
        
        // 좌우 오프셋
        sideOffset += horizontalInput * forwardSpeed * Time.fixedDeltaTime;
        sideOffset = Mathf.Clamp(sideOffset, -maxMoveDistance, maxMoveDistance);

        // 목표 위치
        Vector3 targetXZ = splinePos + splineRight * sideOffset;

        // 현재 속도
        Vector3 velocity = rb.linearVelocity;

        Vector3 forwardVel = splineForward * currentForwardSpeed;

        Vector3 sideVel = splineRight * (horizontalInput * forwardSpeed);
        Vector3 finalVel = forwardVel + sideVel + Vector3.up * velocity.y;


        // 최종 속도
        velocity = forwardVel;
        velocity.y = rb.linearVelocity.y; // 점프 물리 유지
        rb.linearVelocity = velocity;

        // 위치 보정
        rb.position = new Vector3(targetXZ.x, rb.position.y, targetXZ.z);
        if (distanceAlongSpline >= completeThresholdT)
        {
            Victory();
        }

        transform.rotation = Quaternion.LookRotation(splineForward, Vector3.up);
    }
    public void ResetSpeed(float limitedSpeed = 5f)
    {
        rb.linearVelocity = Vector3.zero; 
        currentForwardSpeed = limitedSpeed;
    }

    public void Victory()
    {
        completed = true;

        if (stopOnComplete)
        {
            currentForwardSpeed = 0f;
            rb.linearVelocity = Vector3.zero;
        }

        int sec = timer ? timer.StopTimer() : 0; 
        int lifeCount = BirdManager.Instance.TotalCount;
        float finalScore = RankManager.Instance.CalculateScore(lifeCount, sec);
        string playerName = GameManager.Instance.PlayerName;
        RankManager.Instance.AddScore(playerName, (int)finalScore);

        UIManager.Instance.OpenUI(UICategory.Clear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bird"))
        {
            if (BirdManager.Instance.FlockCount() < 4)
            {
                
                var bird = other.GetComponent<FollowingBird>();
                if (bird != null)
                {
                    other.enabled = false;
                    BirdManager.Instance.AddNewBird(other.transform.position, bird.Type);
                    Destroy(other.gameObject);
                }
            }
            BirdManager.Instance.AddCount(1);
            currentForwardSpeed += 2;
            

            currentForwardSpeed += 2;

            string name = other.GetComponent<FollowingBird>().CollectionSO.birdName;
            PlayerPrefs.SetInt(name, PlayerPrefs.GetInt(name, 0) + 1);
            SoundManager.Instance.Play(SFXSoundType.GetBird);
        }
        else if (other.CompareTag("ScoreZone"))
        {
            GameManager.Instance.AddScore(1);
        }
       
    }
    public float GetDistanceAlongSpline()
    {
        return distanceAlongSpline;
    }
    private void Jump()
    {
        if (transform.position.y >= baseY + maxJumpHeight)
            return;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
    }

    public void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector3.zero; 
    }

    public void SetTargetRotation(float angle)
    {
        targetTiltAngle = angle;
    }

    public void ResetRunToStart()
    {
        completed = false;
        distanceAlongSpline = 0f;

        Vector3 startPos = (Vector3)spline.EvaluatePosition(0f);
        rb.position = startPos;
        transform.position = startPos;
        rb.linearVelocity = Vector3.zero;
        currentForwardSpeed = forwardSpeed;
    }
}
