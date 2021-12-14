using UnityEngine;

public class jawab : MonoBehaviour
{
    private GameObject feed_benar, feed_salah, feedback;

    void Start()
    {
        GameObject parent = transform.parent.gameObject;
        feedback = parent.transform.Find("feedback").gameObject;

        feed_benar = feedback.transform.Find("benar").gameObject;

        feed_salah = feedback.transform.Find("salah").gameObject;
    }

    public void jawaban(bool jawab)
    {
        if (jawab)
        {
            feed_benar.SetActive(false);
            feed_benar.SetActive(true);
            int nilai = PlayerPrefs.GetInt("nilai") + 20;
            PlayerPrefs.SetInt("nilai", nilai);
        }
        else
        {
            feed_salah.SetActive(false);
            feed_salah.SetActive(true);
        }
        gameObject.SetActive(false);
        transform.parent.GetChild(gameObject.transform.GetSiblingIndex() + 1).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
