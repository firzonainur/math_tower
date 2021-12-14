using UnityEngine;

public class reset : MonoBehaviour
{
    public void OnMouseDown()
    {
        PlayerPrefs.SetInt("nilai", 0);
    }
}
