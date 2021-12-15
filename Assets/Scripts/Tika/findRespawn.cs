using System.Collections;
using UnityEngine;

public class findRespawn : MonoBehaviour
{
    private GameObject closest;
    private float oldDistance = 9999;
    private bool findClosest = false;

    public float minimumY;

    IEnumerator find()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Respawn"))
        {
            float dist = Vector3.Distance(this.gameObject.transform.position, g.transform.position);
            
            if (dist < oldDistance)
            {
                closest = g;
                oldDistance = dist;
            }
        }

        if (closest != null)
        {
            Debug.Log("Closest respawn object name: " + closest.name);
            yield return new WaitForSeconds(1f);
            Vector3 current = this.gameObject.transform.position;
            this.gameObject.transform.position = new Vector3(closest.transform.position.x, closest.transform.position.y, current.z);
            oldDistance = 9999;
        }

        findClosest = false;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (this.gameObject.transform.position.y <= minimumY && !findClosest)
        {
            findClosest = true;
            StartCoroutine(find());
        }
    }
}
