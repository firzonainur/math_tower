using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preventBackward : MonoBehaviour
{
    public GameObject targetPlayer;
    public float distanceFromPlayer = 9;

    void Start()
    {

    }

    void Update()
    {
        if ((targetPlayer.transform.position.x - transform.position.x) > distanceFromPlayer)
        {
            transform.position = new Vector3(targetPlayer.transform.position.x - distanceFromPlayer, transform.position.y, transform.position.z);
        }
    }
}
