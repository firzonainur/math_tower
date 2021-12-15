using UnityEngine;

public class player1 : MonoBehaviour
{
    private string m_currentAnimation;
    private Animator m_animator;
    private Rigidbody rigid;
    private CapsuleCollider capsuleCollider;

    private float horizontalMove = 0f;

    public bool jump = false;

    public float moveSpeed = 10f;
    public float jumpAmount = 10f;
    public float animationSpeed = 1;
    public float jumpAnimationSpeed = 1.5f;

    private void ChangeAnimationState(string animation, float speed)
    {
        if (m_currentAnimation == animation) return;

        m_currentAnimation = animation;
        m_animator.Play(m_currentAnimation);
        m_animator.speed = speed;
    }

    private void SetPlayerAnimation()
    {
        if (jump) ChangeAnimationState("Jump", jumpAnimationSpeed);
        else
        {
            if (Mathf.Abs(horizontalMove) > 0)
            {
                ChangeAnimationState("Slow Run", animationSpeed);
            }
            else if (horizontalMove == 0)
            {
                ChangeAnimationState("Idle", animationSpeed);
            }
        }
    }

    private void MoveHorizontal()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        rigid.velocity = new Vector3(horizontalMove * Time.fixedDeltaTime, rigid.velocity.y, rigid.velocity.z);
    }

    private void RotatePlayer()
    {
        if (horizontalMove > 0) gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
        else if (horizontalMove < 0) gameObject.transform.eulerAngles = new Vector3(0, -90, 0);
    }

    private void Jump()
    {
        rigid.velocity = new Vector3(rigid.velocity.x, jumpAmount * Time.fixedDeltaTime, rigid.velocity.z);
    }

    private bool OnGround()
    {
        float extraHeightText = .01f;
        bool grounded = Physics.Raycast(capsuleCollider.bounds.center, Vector3.down, capsuleCollider.bounds.extents.y + extraHeightText);
        Debug.DrawRay(capsuleCollider.bounds.center, Vector3.down * (capsuleCollider.bounds.extents.y + extraHeightText));

        return grounded;
    }

    void Awake()
    {
        m_animator = gameObject.GetComponent<Animator>();
        rigid = gameObject.GetComponent<Rigidbody>();
        rigid.freezeRotation = true;
        capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        MoveHorizontal();
        SetPlayerAnimation();
        RotatePlayer();

        if (Input.GetKeyDown(KeyCode.Space) && !jump)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (OnGround()) jump = false;
        else jump = true;
    }
}