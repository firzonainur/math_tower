using UnityEngine;
using UnityEngine.UI;

public class changeVolume : MonoBehaviour
{
    public GameObject volumePercent;

    public void Change()
    {
        var value = GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("Volume", value);
        var percent = PlayerPrefs.GetFloat("Volume", 1) * 100;
        volumePercent.GetComponent<Text>().text = (int)percent + "%";
    }

    void Start()
    {
        var percent = PlayerPrefs.GetFloat("Volume", 1) * 100;
        volumePercent.GetComponent<Text>().text = percent + "%";
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("Volume", 1);
    }

    void Update()
    {
        
    }
}
