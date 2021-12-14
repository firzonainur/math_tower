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

    public bool paused = false;
    public bool onQuiz = false;
    public bool readOpening = false;
    public bool readEnding = false;
    public bool onEndingPosition = false;
    public GameObject openingDialog;
    public GameObject endingDialog;

    private void ChangeAnimationState(string animation, float speed)
    {
        if (m_currentAnimation == animation) return;
        if (paused || onQuiz) return;

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
        if (paused || onQuiz) return;

        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        rigid.velocity = new Vector3(horizontalMove * Time.fixedDeltaTime, rigid.velocity.y, rigid.velocity.z);
    }

    private void RotatePlayer()
    {
        if (paused || onQuiz) return;

        if (horizontalMove > 0) gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
        else if (horizontalMove < 0) gameObject.transform.eulerAngles = new Vector3(0, -90, 0);
    }

    private void Jump()
    {
        if (paused || onQuiz) return;

        rigid.velocity = new Vector3(rigid.velocity.x, jumpAmount * Time.fixedDeltaTime, rigid.velocity.z);
    }

    private bool OnGround()
    {
        float extraHeightText = .01f;
        bool grounded = Physics.Raycast(capsuleCollider.bounds.center, Vector3.down, capsuleCollider.bounds.extents.y + extraHeightText);
        Debug.DrawRay(capsuleCollider.bounds.center, Vector3.down * (capsuleCollider.bounds.extents.y + extraHeightText));

        return grounded;
    }

    private void OpenDialogue(GameObject dialogObject)
    {
        paused = true;
        dialogObject.SetActive(true);
    }

    void Awake()
    {
        m_animator = gameObject.GetComponent<Animator>();
        rigid = gameObject.GetComponent<Rigidbody>();
        rigid.freezeRotation = true;
        capsuleCollider = gameObject.GetComponent<CapsuleCollider>();

        PlayerPrefs.SetInt("TempSkor", 0);
        PlayerPrefs.SetInt("nilai", 0);
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

        if (Input.GetKeyDown(KeyCode.Alpha0) && !onQuiz)
        {
            paused = !paused;

            if (paused) Time.timeScale = 0;
            else Time.timeScale = 1;
        }

        if (OnGround() && !readOpening)
        {
            OpenDialogue(openingDialog);
            readOpening = true;
        }

        if (onEndingPosition && !readEnding)
        {
            OpenDialogue(endingDialog);
            readEnding = false;
        }
    }

    void FixedUpdate()
    {
        if (OnGround()) jump = false;
        else jump = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "EndingPos")
        {
            onEndingPosition = true;
        }
    }
}
