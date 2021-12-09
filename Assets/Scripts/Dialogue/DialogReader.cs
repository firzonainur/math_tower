using UnityEngine;
using UnityEngine.UI;

public class DialogReader : MonoBehaviour
{
    public Sprite[] backgrounds;
    public SpriteRenderer background;
    public Text characterName;
    public SpriteRenderer expressions;
    public Text message;
    public Dialogue story;
    public Sprite[] expressionSprites;
    private int currentIndex = 0;
    private int maxIndex = 0;

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

    private void readMessage(int index)
    {
        var current = story.messages[index];
        background.sprite = backgrounds[current.background];
        characterName.text = current.characterName;
        message.text = current.message;
        setExpressions(current.expressions);
    }

    void Start()
    {
        maxIndex = story.messages.Length - 1;
        readMessage(currentIndex);
    }

    void Update()
    {
        if (currentIndex < maxIndex)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                currentIndex += 1;
                readMessage(currentIndex);
            }
        }
    }
}
