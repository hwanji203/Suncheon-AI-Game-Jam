using UnityEngine;

public class TestCharacter : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    void Start()
    {
        FadeManager.Instance.FadeOut();
        RankManager.Instance.AddScore("Meoyoung", 100);
        
        RankManager.Instance.PrintAllRanking();
    }

    void Update()
    {
        // A, D 키 입력 확인
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }
}
