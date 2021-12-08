using UnityEngine;

public class player1 : MonoBehaviour
{
    private string m_currentAnimation;
    private Animator m_animator;
    public GameObject collide;
    private Rigidbody2D rigid;

    private float horizontalMove = 0f;

    public bool jump = false;

    private CharacterController controller;

    public float moveSpeed = 10f;
    public float jumpAmount = 10f;
    public float animationSpeed = 1;
    public float jumpAnimationSpeed = 1.5f;

    public Vector3 vel;

    public void ChangeAnimationState(string animation, float speed)
    {
        if (m_currentAnimation == animation) return;

        m_currentAnimation = animation;
        m_animator.Play(m_currentAnimation);
        m_animator.speed = speed;
    }

    void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();
        rigid = collide.GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
    }

    void FixedUpdate()
    {
        vel = rigid.velocity;

        transform.position = rigid.transform.position;

        rigid.transform.position += new Vector3(horizontalMove, 0, 0) * Time.fixedDeltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && !jump)
        {
            rigid.AddForce(new Vector2(0, jumpAmount), ForceMode2D.Impulse);
            ChangeAnimationState("Jump", jumpAnimationSpeed);
            jump = true;
        }

        if (Mathf.Abs(rigid.velocity.y) > 0)
        {
            ChangeAnimationState("Jump", jumpAnimationSpeed);
            jump = true;
        }

        if (horizontalMove > 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
            if (!jump) ChangeAnimationState("Slow Run", animationSpeed);
        }
        else if (horizontalMove < 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0, -90, 0);
            if (!jump) ChangeAnimationState("Slow Run", animationSpeed);
        }

        else if (horizontalMove == 0) {
            if (!jump) ChangeAnimationState("Idle", animationSpeed);
        }
    }
}
