using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bukaKuis : MonoBehaviour
{
    public GameObject player;
    public GameObject kuis;
    public GameObject puzzleRock;
    private GameObject kuisInstantiate;
    public float rockDestinationY = -16.5f;
    public float removeSpeed = 0f;
    private bool canOpenQuiz = false;

    private IEnumerator removeRockObject(float removeSpeed)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        canOpenQuiz = false;

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
        string idTempSkor = "TempSkor";
        string idNilai = "nilai";
        int nilai = PlayerPrefs.GetInt(idNilai);
        int tambahSkor = PlayerPrefs.GetInt(idTempSkor) + nilai;
        PlayerPrefs.SetInt(idTempSkor, tambahSkor);
        PlayerPrefs.SetInt(idNilai, 0);

        // Debug.Log("Total Skor di PlayerPrefs: " + PlayerPrefs.GetInt(idSkorPlayer));
        // Debug.Log("Nilai di PlayerPrefs: " + PlayerPrefs.GetInt(idNilai));
        // Debug.Log("Nilai: " + nilai);

        if (nilai < 60) Debug.Log("Tidak lulus");
        else StartCoroutine(removeRockObject(removeSpeed));
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
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            canOpenQuiz = false;
        }
    }
}
