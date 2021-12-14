using System.Collections;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public float movingSpeed;
    public Vector3 startDestination;
    public Vector3 endDestination;

    void Start()
    {
        StartCoroutine(movePlatform(endDestination));
    }

    IEnumerator movePlatform(Vector3 target)
    {
        float time = 0f;
        while (gameObject.transform.position != target)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target, (time / Vector3.Distance(gameObject.transform.position, target)) * movingSpeed);
            time += Time.deltaTime;
            yield return null;
        }
    }

    void Update()
    {
        if (gameObject.transform.position == startDestination) StartCoroutine(movePlatform(endDestination));
        else if (gameObject.transform.position == endDestination) StartCoroutine(movePlatform(startDestination));
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.SetParent(gameObject.transform, true);
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent = null;
        }
    }
}
