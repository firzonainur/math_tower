using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class findRespawn : MonoBehaviour
{
    private GameObject closest;
    private float oldDistance = 9999;
    private bool findClosest = false;

    public float minimumY;

    IEnumerator find()
    {
        PlayerPrefs.SetInt("HP", PlayerPrefs.GetInt("HP") - 25);
        Debug.Log("HP: " + PlayerPrefs.GetInt("HP"));

        if (PlayerPrefs.GetInt("HP") <= 0)
        {
            Debug.Log("Game over");
            PlayerPrefs.SetString("Nama_Level", "Level_1");
            if (PlayerPrefs.GetInt("HighScore", 0) < PlayerPrefs.GetInt("Skor")) PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Skor"));
            SceneManager.LoadScene("main_menu");
        }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "lavaOrWater")
        {
            findClosest = true;
            StartCoroutine(find());
        }
    }
}
