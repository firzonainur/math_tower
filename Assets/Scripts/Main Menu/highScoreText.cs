using UnityEngine;
using UnityEngine.UI;

public class highScoreText : MonoBehaviour
{
    int skor;
    void Start()
    {
        skor = PlayerPrefs.GetInt("HighScore");
    }

    void Update()
    {
        this.gameObject.GetComponent<Text>().text = "HIGH SCORE: " + skor;
    }
}
