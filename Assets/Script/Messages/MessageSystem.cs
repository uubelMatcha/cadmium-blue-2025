using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// After X seconds, a message appears.
// When that message is closed, a timer starts again and opens the next message after X more seconds. Repeat.
public class MessageSystem : MonoBehaviour
{
#region  Singleton
    public static MessageSystem Instance { get; private set; }

    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
#endregion

    [Header("Design Variables")]
    [SerializeField] private int secondsBetweenMessages = 5;
    [SerializeField] private List<MessageData> messages;
    
    private MessageData curMessage;
    private int curMessageIndex = 0;
    
    // Currently only one message at a time
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI  messageText;
    [SerializeField] private GameObject messagePanel;
    
    public delegate void OnMessagePopUp(bool badMessage);
    public OnMessagePopUp onMessageOpen;
    
    public delegate void OnMessageClose();
    public OnMessageClose onMessageClose;
    
    public delegate void OnMessageCloseDelayPassed(MessageData messageData);
    public OnMessageCloseDelayPassed onMessageCloseDelayPassed;
    
    private void Start()
    {
        messagePanel.SetActive(false);
        onMessageClose += StartNextPopUpTimer;
        StartCoroutine(OpenPopUpAfterSeconds());
    }

    private void StartNextPopUpTimer()
    {
        StartCoroutine(OpenPopUpAfterSeconds());
    }

    private IEnumerator OpenPopUpAfterSeconds()
    {
        InitializeNextPopUp();
        yield return new WaitForSecondsRealtime(secondsBetweenMessages);
        OpenNextPopUp();
        
        float delay = 0;
        MessageData messageCheck = curMessage;
        while (delay < curMessage.dismissTime )
        {
            if(messageCheck != curMessage)
                yield break;
            delay += Time.deltaTime;
            yield return null;
        }

        messageText.text = curMessage.messageText;
        onMessageCloseDelayPassed.Invoke(curMessage);
    }

    private void OpenNextPopUp()
    {
        messagePanel.SetActive(true);
        onMessageOpen.Invoke(curMessage.ticksAnxiety);
    }
    
    private void InitializeNextPopUp()
    {
        if (messages.Count > curMessageIndex)
        {
            curMessage = messages[curMessageIndex];
            curMessageIndex++;
            messageText.text = curMessage.cutoffText;
        }
    }
    
    private void HidePopUp()
    {
        messagePanel.SetActive(false);
        onMessageClose.Invoke();
    }

    public static void ClosePopUp()
    {
        Instance.HidePopUp();
    }
}