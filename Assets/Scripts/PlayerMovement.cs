using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static readonly int hashMove = Animator.StringToHash("Move");

    [Header("SpeedSetting")]
    public float moveSpeed = 5f;
    public float roateSpeed = 180f;

    private Rigidbody rb;
    private PlayerInput input;
    private Animator animator;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // 회전
        var rotation = Quaternion.Euler(0f, input.Roate * roateSpeed * Time.deltaTime, 0f);
        rb.MoveRotation(rb.rotation * rotation);

        // 이동
        var distance = input.Move * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + distance * transform.forward);

        // 애니메이션
        //animator.SetFloat("Move", input.Move);
        animator.SetFloat(hashMove, input.Move);    // id로 바꿔서 사용하기
    }
}
