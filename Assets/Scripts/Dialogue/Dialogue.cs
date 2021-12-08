using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    public Message[] messages;
}

[Serializable]
public class Message
{
    public string characterName;
    public string message;
    public string expressions;
}