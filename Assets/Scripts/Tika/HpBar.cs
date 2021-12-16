using UnityEngine;

public class HpBar : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        var currentHP = PlayerPrefs.GetInt("Nama_HP");
        this.gameObject.transform.localScale = new Vector3(currentHP / 100f, 1, 1);
    }
}
