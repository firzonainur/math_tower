using UnityEngine;

public class HpBar : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("tempHP", PlayerPrefs.GetInt("HP"));
    }

    void Update()
    {
        var currentHP = PlayerPrefs.GetInt("tempHP");
        this.gameObject.transform.localScale = new Vector3(currentHP / 100f, 1, 1);
    }
}
