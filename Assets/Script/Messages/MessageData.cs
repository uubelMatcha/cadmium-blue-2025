using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MessageData", menuName = "Scriptable Objects/MessageData")]
public class MessageData : ScriptableObject
{
    public string cutoffText;
    public string messageText;
    public bool ticksAnxiety;

    public string characterName;

    public float dismissTime = 2f;
    public float anxietyOnPartialShow = 1f;
    public float anxietyOnFullShow = 1f;
}
