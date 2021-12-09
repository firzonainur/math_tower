using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feedback : MonoBehaviour
{
    public GameObject senyum, soal;
    bool selesai = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void cek()
    {
        for(int i = 0; i<10; i++)
        {
            if (transform.GetChild(i).GetComponent<drag>().on_pos)
            {
                selesai = true;
            }
            else
            {
                selesai = false;
                i = 10;
            }
        }
        if (selesai) {
            senyum.SetActive(true);
            soal.SetActive(false);
            selesai = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!selesai)
        {
            cek();
        }
    }
}
