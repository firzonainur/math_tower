using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogReader : MonoBehaviour
{
    public Sprite[] backgrounds;
    public Image kotakDialog;
    public SpriteRenderer background;
    public Text characterName;
    public SpriteRenderer expressions;
    public Text message;
    public Dialogue story;
    public Sprite[] expressionSprites;
    public AudioSource backSound;
    public AudioSource soundEffect;
    public float fadeTime;
    private int currentIndex = 0;
    private int maxIndex = 0;
    private bool isFading = false;

    private void setExpressions(string id)
    {
        if (id == "neutral") expressions.sprite = expressionSprites[0];
        else if (id == "reading") expressions.sprite = expressionSprites[1];
        else if (id == "surprised") expressions.sprite = expressionSprites[2];
        else if (id == "thinking") expressions.sprite = expressionSprites[3];
        else if (id == "angry") expressions.sprite = expressionSprites[4];
        else if (id == "sad") expressions.sprite = expressionSprites[5];
        else if (id == "") expressions.sprite = null;
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

    void Start()
    {
        maxIndex = story.messages.Length - 1;
        backSound.clip = story.backsound;
        backSound.Play();
        readMessage(currentIndex);
    }

    void Update()
    {
        if (currentIndex < maxIndex)
        {
            if (Input.GetKeyUp(KeyCode.Space) && !isFading)
            {
                currentIndex += 1;
                readMessage(currentIndex);
            }
        }

        if (currentIndex == maxIndex)
        {
            backSound.Stop();
        }
    }
}
