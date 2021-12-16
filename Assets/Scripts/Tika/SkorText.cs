using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkorText : MonoBehaviour
{
    int skor;
    void Start()
    {
        skor = PlayerPrefs.GetInt("Skor");
    }

    void Update()
    {
        var newSkor = skor + PlayerPrefs.GetInt("TempSkor");
        this.gameObject.GetComponent<Text>().text = "Skor: " + newSkor;
    }
}
