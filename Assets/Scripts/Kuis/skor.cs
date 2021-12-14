using UnityEngine;
using UnityEngine.UI;

public class skor : MonoBehaviour
{
    private Text scoreText;

    void Start()
    {
        scoreText = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        scoreText.text = PlayerPrefs.GetInt("nilai").ToString();
    }
}
