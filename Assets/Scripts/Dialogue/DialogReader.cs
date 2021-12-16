using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogReader : MonoBehaviour
{
    public GameObject player;
    public Sprite[] backgrounds;
    public Image kotakDialog;
    public Image background;
    public Text characterName;
    public Image expressions;
    public Text message;
    public Dialogue story;
    public Sprite[] expressionSprites;
    public AudioSource backSound;
    public AudioSource soundEffect;
    public float fadeTime;
    private int currentIndex = 0;
    private int maxIndex = 0;
    private bool isFading = false;
    public bool goToNextSceneAfterEnds = false;
    public string nextScene;
    public bool hideDialogueAfterEnds = false;

    private void setExpressions(string id)
    {
        var rect = expressions.transform as RectTransform;

        if (id == "")
        {
            expressions.sprite = null;    
            rect.sizeDelta = new Vector2(0, 0);
        }
        else
        {   
            if (id == "neutral") expressions.sprite = expressionSprites[0];
            else if (id == "reading") expressions.sprite = expressionSprites[1];
            else if (id == "surprised") expressions.sprite = expressionSprites[2];
            else if (id == "thinking") expressions.sprite = expressionSprites[3];
            else if (id == "angry") expressions.sprite = expressionSprites[4];
            else if (id == "sad") expressions.sprite = expressionSprites[5];

            var width = expressions.sprite.rect.width;
            var height = expressions.sprite.rect.height;
            rect.sizeDelta = new Vector2(width / 1.4f, height / 1.4f);
        }
    }

    private IEnumerator fadeIn(float fadeTime, int index)
    {
        hideDialog();
        isFading = true;

        Color tmp = background.color;
        tmp.a = 0f;
        background.color = tmp;
        background.sprite = backgrounds[story.messages[index].background];

        while (tmp.a < 1f)
        {
            tmp.a += Time.deltaTime / fadeTime;
            background.color = tmp;

            if (tmp.a > 1f) tmp.a = 1f;

            yield return null;
        }

        background.color = tmp;
        isFading = false;

        if (story.messages[index].message != "") showDialog();
        fillDialogueUI(story, index);
    }

    private void fillDialogueUI(Dialogue story, int index)
    {
        var current = story.messages[index];

        background.sprite = backgrounds[current.background];
        characterName.text = current.characterName;
        message.text = current.message;
        soundEffect.clip = current.soundEffect;
        soundEffect.Play();
        setExpressions(current.expressions);
    }

    private void hideDialog()
    {
        Color tmp = kotakDialog.color;
        tmp.a = 0f;
        kotakDialog.color = tmp;

        characterName.text = "";
        message.text = "";
    }

    private void showDialog()
    {
        Color tmp = kotakDialog.color;
        tmp.a = 0.9f;
        kotakDialog.color = tmp;
    }

    private void readMessage(int index)
    {
        var current = story.messages[index];

        if (background.sprite != backgrounds[current.background])
        {
            StartCoroutine(fadeIn(fadeTime, index));
        }
        else
        {
            fillDialogueUI(story, index);
        }
    }

    private void GoToNextScene()
    {
        PlayerPrefs.SetInt("Skor", PlayerPrefs.GetInt("Skor") + PlayerPrefs.GetInt("TempSkor"));
        PlayerPrefs.SetString("Level", nextScene);

        PlayerPrefs.SetInt("TempSkor", 0);

        Debug.Log("Skor terbaru: " + PlayerPrefs.GetInt("Skor"));
        Debug.Log("Level selanjutnya: " + PlayerPrefs.GetString("Level"));

        GameObject loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen").gameObject;
        loadingScreen.GetComponent<loading>().StartLoading(nextScene);
    }

    void Awake()
    {
        Time.timeScale = 1;
        maxIndex = story.messages.Length;
        backSound.clip = story.backsound;
        backSound.Play();
        readMessage(currentIndex);
        currentIndex = 1;
    }

    void Update()
    {
        if (currentIndex < maxIndex)
        {
            if (Input.GetKeyUp(KeyCode.Space) && !isFading)
            {
                readMessage(currentIndex);
                currentIndex += 1;
            }
        }

        if (currentIndex == maxIndex)
        {
            expressions.sprite = null;
            var rect = expressions.transform as RectTransform;
            rect.sizeDelta = Vector2.zero;
            backSound.Stop();

            if (goToNextSceneAfterEnds && !hideDialogueAfterEnds)
            {
                if (player != null)
                {
                    player.GetComponent<player1>().paused = true;
                }
                GoToNextScene();
            }
            else if (hideDialogueAfterEnds && !goToNextSceneAfterEnds)
            {
                kotakDialog.transform.parent.gameObject.SetActive(false);
                if (player != null)
                {
                    player.GetComponent<player1>().paused = false;
                }
            }
        }
    }
}
