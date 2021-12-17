using System.Collections;
using UnityEngine;

public class musuh : MonoBehaviour
{
    private string m_currentAnimation;
    private Animator m_animator;
    public GameObject target;
    public float moveSpeed;
    public float minDistance;
    public float attackDistance;
    public float attackSpeed;
    public float animationSpeed;
    private bool isAttacking;
    private BoxCollider attackCollider;
    private bool moving = false;
    private bool dead = false;

    private void ChangeAnimationState(string animation, float speed)
    {
        if (m_currentAnimation == animation) return;
        var paused = target.GetComponent<player1>().paused;
        var onQuiz = target.GetComponent<player1>().onQuiz;

        if (paused || onQuiz) return;

        m_currentAnimation = animation;
        m_animator.Play(m_currentAnimation);
        m_animator.speed = speed;
    }

    private void Attack()
    {
        var paused = target.GetComponent<player1>().paused;
        var onQuiz = target.GetComponent<player1>().onQuiz;

        if (paused || onQuiz) return;

        isAttacking = true;
        ChangeAnimationState("Attack", attackSpeed);
        GetComponent<AudioSource>().Play();
        
        if (transform.position.x < target.transform.position.x) transform.eulerAngles = new Vector3(0, 90, 0);
        else if (transform.position.x > target.transform.position.x) transform.eulerAngles = new Vector3(0, 270, 0);
    }

    private void Walk()
    {
        var paused = target.GetComponent<player1>().paused;
        var onQuiz = target.GetComponent<player1>().onQuiz;

        if (paused || onQuiz || dead) return;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);

        if (transform.position.x < target.transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (transform.position.x > target.transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void Idle()
    {
        ChangeAnimationState("Idle", animationSpeed);
    }

    public void Dead()
    {
        ChangeAnimationState("Dead", animationSpeed);
        dead = true;
    }

    void Start()
    {
        m_animator = GetComponent<Animator>();
        attackCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        attackCollider.enabled = isAttacking;

        var dist = Vector3.Distance(transform.position, new Vector3(target.transform.position.x, transform.position.y, transform.position.z));

        if (dist <= minDistance && dist > attackDistance && !isAttacking)
        {
            Walk();
        }

        if (dist < attackDistance && !isAttacking)
        {
            Attack();
        }

        if (isAttacking && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            isAttacking = false;
            Idle();
        }

        if (dead) Destroy(gameObject);
    }
}
