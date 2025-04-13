using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
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
    // [SerializeField] private Image ignoreButtonCompletionSlider;


    public float maxIgnoreButtonScale = 1.5f;
    public float ignoreButtonRelaxTime = 0.25f;
    
    private bool isTickingAnxiety = false;
    private int curIgnoreClicks = 0;
    private bool hasMissedDelay = false;

    public float anxietyLevel;

    private PostProcessingBehaviour postProcessingBehaviour;
    
    private void Start()
    {
        ignoreButton.gameObject.SetActive(false);
        MessageSystem.Instance.onMessageOpen += OnMessageOpen;
        MessageSystem.Instance.onMessageClose += ResetSystem;
        MessageSystem.Instance.onMessageCloseDelayPassed += OnMessageCloseDelayPassed;

        postProcessingBehaviour = FindFirstObjectByType<PostProcessingBehaviour>();
        // ignoreButtonCompletionSlider.gameObject.transform.localScale = new Vector3(0f, 1f, 1f);
    }
    
    private void OnMessageOpen(bool isBadMessage)
    {
        // if (isBadMessage == true) {
        isTickingAnxiety = true;
        StartCoroutine(postProcessingBehaviour.HeartBeatEffect());
        // }
        curIgnoreClicks = 0;
        ignoreButton.gameObject.SetActive(true);
    }
    
    private void ResetSystem()
    {
        curIgnoreClicks = 0;
        isTickingAnxiety = false;
        ignoreButton.gameObject.SetActive(false);
        postProcessingBehaviour.panicMode = false;

        ignoreButton.transform.localScale = new Vector3(1f, 1f, 1f);
        // ignoreButtonCompletionSlider.gameObject.transform.localScale = new Vector3(0f, 1f, 1f);


    }

    // TODO: make it time frame independent?
    private void Update()
    {
        // Update exposed anxiety level from 0 to 1 for dependent objects
        anxietyLevel = anxietySlider.value / 100f;

        // checks if the player has clicked ignore enough times
        CheckIgnoreClicks();
        
        if (isTickingAnxiety)
        {
            TickAnxietyUp();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // if (hasMissedDelay)
                // {
                //     MessageSystem.ClosePopUp();
                // }
                // else
                // {
                curIgnoreClicks++;
                // ignoreButtonCompletionSlider.gameObject.transform.localScale = new Vector3(curIgnoreClicks / clicksUntilCloseMessage, 1f, 1f);
                StopCoroutine("PulseIgnoreButton");
                StartCoroutine("PulseIgnoreButton");
                // TickAnxietyDown();
                // }
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

    private IEnumerator PulseIgnoreButton() {

        float timer = 0f;

        while (timer <= ignoreButtonRelaxTime) {

            float scale = Mathf.Lerp(maxIgnoreButtonScale, 1f, timer / ignoreButtonRelaxTime);
            ignoreButton.transform.localScale = new Vector3(scale, scale, 1f);

            timer += Time.deltaTime;
            
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
                // yield break;
            // }
            // else {
            yield return null;
            // }
        }

    }
}