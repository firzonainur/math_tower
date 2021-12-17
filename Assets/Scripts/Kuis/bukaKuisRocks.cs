using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bukaKuisRocks : MonoBehaviour
{
    public GameObject player;
    public GameObject kuis;
    public GameObject[] puzzleRocks;
    private GameObject kuisInstantiate;
    public float rockDestinationY = -16.5f;
    public float removeSpeed = 0f;
    public bool canOpenQuiz = false;

    private IEnumerator removeRockObjects(float removeSpeed)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        canOpenQuiz = false;

        foreach (GameObject puzzleRock in puzzleRocks)
        {
            if (puzzleRock.transform.position.y > rockDestinationY)
            {
                while (puzzleRock.transform.position.y > rockDestinationY)
                {
                    puzzleRock.transform.position += Vector3.down * removeSpeed;
                    yield return null;
                }
            }
            else if (puzzleRock.transform.position.y < rockDestinationY)
            {
                while (puzzleRock.transform.position.y < rockDestinationY)
                {
                    puzzleRock.transform.position += Vector3.up * removeSpeed;
                    yield return null;
                }
            }
            Debug.Log(puzzleRock.name);
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(gameObject);
    }

    public void ExitQuiz()
    {
        player.GetComponent<player1>().onQuiz = false;
        Time.timeScale = 1;
        Destroy(kuisInstantiate);

        CalculateScore();
    }

    private void CalculateScore()
    {
        // Debug.Log("Total Skor di PlayerPrefs: " + PlayerPrefs.GetInt(idSkorPlayer));
        // Debug.Log("Nilai di PlayerPrefs: " + PlayerPrefs.GetInt(idNilai));
        // Debug.Log("Nilai: " + nilai);

        string idTempSkor = "TempSkor";
        string idNilai = "nilai";
        int nilai = PlayerPrefs.GetInt(idNilai);
        int tambahSkor = PlayerPrefs.GetInt(idTempSkor) + nilai;

        if (nilai < 60)
        {
            Debug.Log("Tidak lulus");
            PlayerPrefs.SetInt(idTempSkor, tambahSkor - nilai);
        }
        else
        {
            PlayerPrefs.SetInt(idTempSkor, tambahSkor);
            PlayerPrefs.SetInt(idNilai, 0);
            StartCoroutine(removeRockObjects(removeSpeed));
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P) && canOpenQuiz && !player.GetComponent<player1>().onQuiz)
        {
            player.GetComponent<player1>().onQuiz = true;
            Time.timeScale = 0;

            kuisInstantiate = Instantiate(kuis);
            GameObject hasil = kuisInstantiate.transform.Find("hasil").gameObject;
            GameObject exitButton = hasil.transform.Find("exit").gameObject;
            exitButton.GetComponent<Button>().onClick.AddListener(ExitQuiz);
        }        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            canOpenQuiz = true;
            GameObject hintUI = GameObject.Find("hint").gameObject;
            hintUI.GetComponent<Text>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            canOpenQuiz = false;
            GameObject hintUI = GameObject.Find("hint").gameObject;
            hintUI.GetComponent<Text>().enabled = false;
        }
    }
}
