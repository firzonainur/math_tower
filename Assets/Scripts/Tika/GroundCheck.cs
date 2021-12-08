using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Rigidbody2D rigid;
    public GameObject player;

    void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            player1 sc = player.GetComponent<player1>();
            sc.jump = false;
        }
    }
}