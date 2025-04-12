using UnityEngine;

[CreateAssetMenu(fileName = "MessageData", menuName = "Scriptable Objects/MessageData")]
public class MessageData : ScriptableObject
{
    public string messageText;
    public bool isBadMessage;
}
