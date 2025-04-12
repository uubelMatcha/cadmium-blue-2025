using System;
using UnityEngine;
using UnityEngine.UI;

public class AnxietySystem : MonoBehaviour
{
    [Header("Design Variables")]
    [SerializeField] private int clicksUntilCloseMessage = 10;
    [SerializeField] private float anxietyDamagePerSecond = 1f;
    [SerializeField] private float ignoreHealPerSecond = 1.2f;
    
    [Header("UI Elements")]
    [SerializeField] private Slider anxietySlider;
    [SerializeField] private Button ignoreButton;
    
    private bool isTickingAnxiety = false;
    private int curIgnoreClicks = 0;
    private bool hasMissedDelay = false;
    
    private void Start()
    {
        ignoreButton.gameObject.SetActive(false);
        MessageSystem.Instance.onMessageOpen += OnMessageOpen;
        MessageSystem.Instance.onMessageClose += ResetSystem;
        MessageSystem.Instance.onMessageCloseDelayPassed += OnMessageCloseDelayPassed;
    }
    
    private void OnMessageOpen(bool isBadMessage)
    {
        isTickingAnxiety = true;
        curIgnoreClicks = 0;
        ignoreButton.gameObject.SetActive(true);
    }
    
    private void ResetSystem()
    {
        curIgnoreClicks = 0;
        isTickingAnxiety = false;
        ignoreButton.gameObject.SetActive(false);
    }

    // TODO: make it time frame independent?
    private void Update()
    {
        // checks if the player has clicked ignore enough times
        CheckIgnoreClicks();
        
        if (isTickingAnxiety)
        {
            TickAnxietyUp();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (hasMissedDelay)
                {
                    MessageSystem.ClosePopUp();
                }
                else
                {
                    curIgnoreClicks++;
                    TickAnxietyDown();
                }
            }
        }
    }

    private void TickAnxietyUp()
    {
        anxietySlider.value += anxietyDamagePerSecond * Time.deltaTime;
    }

    private void TickAnxietyDown()
    {
        anxietySlider.value -= ignoreHealPerSecond * Time.deltaTime;
    }
    
    private void OnMessageCloseDelayPassed(MessageData messagedata)
    {
        anxietySlider.value += messagedata.anxietyOnFullShow;
        hasMissedDelay = true;
    }
    
    private void CheckIgnoreClicks()
    {
        if (curIgnoreClicks >= clicksUntilCloseMessage)
        {
            if (!isTickingAnxiety)
            {
                isTickingAnxiety = false;
                curIgnoreClicks = 0;
                Debug.LogError("Ignore clicks reached threshold, but anxiety system should be inactive");
            }

            MessageSystem.ClosePopUp();
        }  
    }
}