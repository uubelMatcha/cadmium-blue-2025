using System.Collections;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using Script.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingSystem : MonoBehaviour
{

    public GameObject flavorTextUI;
    public UnityEngine.UI.Image blackScreen;
    public Slider anxietySlider;
    public MessageSystem messageSystem;


    private string[] ghostBoyDialogue = new string[] {
        "GHOST: Hey bro. What are you doing out here?",
        "YOU: I was just kind of… Wandering around. I don’t know. Looking for you?",
        "GHOST: Why were you looking for me?",
        "YOU: Well, you’re gone. I guess I thought maybe I’d find you somewhere…",
        "GHOST: You don’t have to worry about me. You should get back to Mom. She wants to look out for you.",
        "YOU: Yeah… I know… It’s just a lot. It’s hard.",
        "GHOST: You’ll be OK again. I know you will. Say hi to your friends for me :)"
    };


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartEnding() {

        StartCoroutine(GhostBoyDialogue());
    }
    
    private IEnumerator GhostBoyDialogue() {

        // FindFirstObjectByType<MovementController>().enabled = false;
        // FindFirstObjectByType<AnxietySystem>().gameObject.SetActive(false);

        float sliderStartValue = anxietySlider.value;

        //Show text
        int dialogueIndex = 0;
        flavorTextUI.GetComponentInChildren<TextMeshProUGUI>().text = ghostBoyDialogue[dialogueIndex];
        flavorTextUI.SetActive(true);
        dialogueIndex += 1;

        while (dialogueIndex <= ghostBoyDialogue.Count()) {

            if (Input.GetKeyDown(KeyCode.Space)) {

                if (dialogueIndex == ghostBoyDialogue.Count()) {

                    flavorTextUI.SetActive(false);

                }
                else {
                    flavorTextUI.GetComponentInChildren<TextMeshProUGUI>().text = ghostBoyDialogue[dialogueIndex];
                    anxietySlider.value = sliderStartValue * (1 - ((float)dialogueIndex / ghostBoyDialogue.Count()));
                }
                dialogueIndex += 1;
                Debug.Log(dialogueIndex);
            }

            yield return null;

        }

        StartCoroutine(FadeScreenOut());

        
    }

    private IEnumerator FadeScreenOut() {


        float timer = 0f;

        while (timer < 2f) {

            blackScreen.color = new Color(0f,0f,0f, timer / 2f);

            timer += Time.deltaTime;
            yield return null;

        }

        // StartCoroutine(MomTextScene());

    }


    // private IEnumerator MomTextScene() {

    //     messageSystem.SlidePhone(-900f, 0f);

    //     for (int i = 0; i < 2; i++) {





    //     }



    // }

}
