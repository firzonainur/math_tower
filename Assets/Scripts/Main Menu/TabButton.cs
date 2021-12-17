using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TabGroup tabGroup;
    public Image background;
    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;
    public string nama_scene;

    private void NewGame()
    {
        PlayerPrefs.SetInt("HP", 100);
        PlayerPrefs.SetInt("Skor", 0);
        PlayerPrefs.SetInt("TempSkor", 0);
        PlayerPrefs.SetString("Level", "Prolog");
        SceneManager.LoadScene("Prolog");
    }

    private void Continue()
    {
        if (PlayerPrefs.GetString("Level") == "Win" || PlayerPrefs.GetString("Level") == "")
        {
            NewGame();
            return;
        }

        PlayerPrefs.SetInt("TempSkor", 0);
        SceneManager.LoadScene(PlayerPrefs.GetString("Level"));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
            tabGroup.OnTabSelected(this);

            if (nama_scene == "NewGame")
            {
                NewGame();
            }
            else if (nama_scene == "Continue")
            {
                Continue();
            }
            else if (nama_scene == "Quit")
            {
                Application.Quit();
            }
            else SceneManager.LoadScene(nama_scene);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
        var parent = transform.parent;
        if (parent.transform.parent.gameObject.GetComponent<AudioSource>() != null)
            parent.transform.parent.gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Select()
    {
        if (onTabSelected != null)
        {
            onTabSelected.Invoke();
        }
    }

    public void Deselect()
    {
        if (onTabDeselected != null)
        {
            onTabDeselected.Invoke();
        }
    }
}
