using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AnxietySystem : MonoBehaviour
{
    public enum Comparators
    {
        LessThanOrEqual,
        GreaterThanOrEqual,
    }
    
    [Serializable]
    public struct ThresholdEvents
    {
        public Comparators comparator;
        public float threshold;
        public GameObject onAnxiety;
        public MonoBehaviour onAnxietyComponent;

        public void OnAnxietyChanged(float value)
        {
            
            switch (comparator)
            {
                case Comparators.GreaterThanOrEqual:
                    DoOnGreaterEqual(value);
                    break;
                case Comparators.LessThanOrEqual:
                    DoOnLessOrEqual(value);
                    break;
            }
        }
        
        void DoOnLessOrEqual(float value)
        {
            if (value <= threshold)
            {
                onAnxiety.SetActive(true);
                if(onAnxietyComponent != null)
                    onAnxietyComponent.enabled = true;
            }
            else
            {
                onAnxiety.SetActive(false);
                if(onAnxietyComponent != null)
                    onAnxietyComponent.enabled = false;
            }

        }
        void DoOnGreaterEqual(float value)
        {
            if (  value >= threshold)
            {
                onAnxiety.SetActive(true);
                if(onAnxietyComponent != null)
                    onAnxietyComponent.enabled = true;
            }
            else
            {
                onAnxiety.SetActive(false);
                if(onAnxietyComponent != null)
                    onAnxietyComponent.enabled = false;
            }
        }
    }
    
    [Header("Design Variables")]
    [SerializeField] private int clicksUntilCloseMessage = 10;
    [SerializeField] private float anxietyDamagePerSecond = 1f;
    [SerializeField] private float ignoreHealPerSecond = 1.2f;
    [SerializeField] List<ThresholdEvents> thresholdEvents = new List<ThresholdEvents>();

    public float anxietyLevel;

    
    [Header("UI Elements")]
    [SerializeField] private Slider anxietySlider;
    [SerializeField] private Button ignoreButton;
    [SerializeField] private RectTransform ignoreButtonCompletionMask;


    [SerializeField] private float maxIgnoreButtonScale = 1.5f;
    [SerializeField] private float ignoreButtonRelaxTime = 0.25f;
    
    private bool messageOpen = false;
    private bool isTickingAnxiety = false;
    private int curIgnoreClicks = 0;
    // private bool hasMissedDelay = false;


    private PostProcessingBehaviour postProcessingBehaviour;
    
    private void Start()
    {
        ignoreButton.gameObject.SetActive(false);
        MessageSystem.Instance.onMessageOpen += OnMessageOpen;
        MessageSystem.Instance.onMessageClose += ResetSystem;
        // MessageSystem.Instance.onMessageCloseDelayPassed += OnMessageCloseDelayPassed;

        postProcessingBehaviour = FindFirstObjectByType<PostProcessingBehaviour>();
        ignoreButtonCompletionMask.localScale = new Vector3(0f, 0.2f, 1f);
    }
    
    private void OnMessageOpen(bool isBadMessage)
    {
        if (isBadMessage == true) {
            isTickingAnxiety = true;
            if (AudioManager.audioManagerInstance)
            {
                AudioManager.audioManagerInstance.ChangeBGM("Anxiety");
            }
            StartCoroutine(postProcessingBehaviour.HeartBeatEffect());
            ignoreButton.gameObject.SetActive(true);
        }
        messageOpen = true;
        curIgnoreClicks = 0;
    }
    
    private void ResetSystem()
    {
        curIgnoreClicks = 0;
        isTickingAnxiety = false;
        if (AudioManager.audioManagerInstance)
        {
            AudioManager.audioManagerInstance.ChangeBGM("At_The_Campsite");
        }
        messageOpen = false;
        ignoreButton.gameObject.SetActive(false);
        postProcessingBehaviour.panicMode = false;

        ignoreButton.transform.localScale = new Vector3(1f, 1f, 1f);
        ignoreButtonCompletionMask.localScale = new Vector3(0f, 0.2f, 1f);

    }

// TODO: make it time frame independent?
    private void Update()
    {
        // Update exposed anxiety level from 0 to 1 for dependent objects
        anxietyLevel = anxietySlider.value / 100f;

        // checks if the player has clicked ignore enough times
        CheckIgnoreClicks();
        
        if (messageOpen) {
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
                    // Debug.Log(curIgnoreClicks(float) / clicksUntilCloseMessage(float));
                    ignoreButtonCompletionMask.localScale = new Vector3((float)curIgnoreClicks / clicksUntilCloseMessage, 0.2f, 1f);

                    StopCoroutine("PulseIgnoreButton");
                    StartCoroutine("PulseIgnoreButton");
                    // TickAnxietyDown();
                    // }
                }
            }
            else {

                if (Input.GetKeyDown(KeyCode.Space)) {
                    MessageSystem.ClosePopUp();
                }

            }   
        }
    }

    private void TickAnxietyUp() 
    {
        anxietySlider.value += anxietyDamagePerSecond * Time.deltaTime;
        foreach (ThresholdEvents thresholdEvent in thresholdEvents)
        {
            thresholdEvent.OnAnxietyChanged(anxietySlider.value);
        }
    }

    private void TickAnxietyDown()
    {
        anxietySlider.value -= ignoreHealPerSecond * Time.deltaTime;
    }
    
    // private void OnMessageCloseDelayPassed(MessageData messagedata)
    // {
    //     anxietySlider.value += messagedata.anxietyOnFullShow;
    //     hasMissedDelay = true;
    // }
    
    private void CheckIgnoreClicks()
    {
        if (curIgnoreClicks >= clicksUntilCloseMessage)
        {
            // if (!isTickingAnxiety)
            // {
            //     isTickingAnxiety = false;
            //     curIgnoreClicks = 0;
            //     Debug.LogError("Ignore clicks reached threshold, but anxiety system should be inactive");
            // }

            // if (isTickingAnxiety) {
            //     isTickingAnxiety = false;
            // }

            MessageSystem.ClosePopUp();
        }  
    }

    private IEnumerator PulseIgnoreButton() {

        float timer = 0f;

        while (timer <= ignoreButtonRelaxTime) {

            float scale = Mathf.Lerp(maxIgnoreButtonScale, 1f, timer / ignoreButtonRelaxTime);
            ignoreButton.transform.localScale = new Vector3(scale, scale, 1f);

            timer += Time.deltaTime;

            yield return null;
        }

    }
}