using System;
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
    
    [Header("UI Elements")]
    [SerializeField] private Slider anxietySlider;
    [SerializeField] private Button ignoreButton;
    
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
    }

// TODO: make it time frame independent?
    private void Update()
    {

        anxietyLevel = anxietySlider.value / 100f;

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
        foreach (ThresholdEvents thresholdEvent in thresholdEvents)
        {
            thresholdEvent.OnAnxietyChanged(anxietySlider.value);
        }
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