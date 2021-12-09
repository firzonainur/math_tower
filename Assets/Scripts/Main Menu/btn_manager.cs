using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class btn_manager : MonoBehaviour
{
    public void Start()
    {
        PlayerPrefs.SetInt("skor", 0);
    }

    public void Load_Scene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
