using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeRock : MonoBehaviour
{
    private bool canOpenPuzzle = false;
    public GameObject puzzleRock;
    public float rockDestinationY = -16.5f;
    public float removeSpeed = 0f;

    private IEnumerator removeRockObject(float removeSpeed)
    {
        while (puzzleRock.transform.position.y > rockDestinationY)
        {
            puzzleRock.transform.position += Vector3.down * removeSpeed;
            yield return null;
        }

        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P) && canOpenPuzzle)
        {
            StartCoroutine(removeRockObject(removeSpeed));
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            canOpenPuzzle = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            canOpenPuzzle = false;
        }
    }
}
