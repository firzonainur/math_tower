using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class bukaKuisMusuh : MonoBehaviour
{
    public GameObject player;
    public GameObject kuis;
    public GameObject musuh;
    private GameObject kuisInstantiate;
    public bool canOpenQuiz = false;

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

        if (nilai < 60)
        {
            Debug.Log("Tidak lulus");
            PlayerPrefs.SetInt(idTempSkor, tambahSkor - nilai);
        }
        else
        {
            musuh.GetComponent<musuh>().Dead();
            GameObject hintUI = GameObject.Find("hint").gameObject;
            hintUI.GetComponent<Text>().enabled = false;
            Destroy(gameObject);
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
