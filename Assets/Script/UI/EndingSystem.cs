using System.Collections;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using Script.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingSystem : MonoBehaviour
{

    public GameObject flavorTextUI;
    public UnityEngine.UI.Image blackScreen;
    public Slider anxietySlider;
    public MessageSystem messageSystem;

    [SerializeField] private TextMeshProUGUI  messageText;
    [SerializeField] private UnityEngine.UI.Image profilePictureImage;
    [SerializeField] private TextMeshProUGUI characterName;

    private string[] textsToMom = new string[] {
        "YOU: Hey mom. Sorry I just went for a walk and got a little lost… I’m coming right back. And yeah maybe we can talk about stuff.",
        "YOU: Yay pinecone jelly sandwiches :’)"
    };
    public Sprite momProfile;

    private string[] ghostBoyDialogue = new string[] {
        "GHOST: Hey bro. What are you doing out here?",
        "YOU: I was just kind of… Wandering around. I don’t know. Looking for you?",
        "GHOST: Why were you looking for me?",
        "YOU: Well, you’re gone. I guess I thought maybe I’d find you somewhere…",
        "GHOST: You don’t have to worry about me. You should get back to Mom. She wants to look out for you.",
        "YOU: Yeah… I know… It’s just a lot. It’s hard.",
        "GHOST: You’ll be OK again. I know you will. Say hi to your friends for me :)"
    };



    public void StartEnding() {

        StartCoroutine(GhostBoyDialogue());
    }
    
    private IEnumerator GhostBoyDialogue() {

        FindFirstObjectByType<MovementController>().locked = true;

        if (AudioManager.audioManagerInstance)
        {
            AudioManager.audioManagerInstance.ChangeBGM("Ghost");
        }
        
        float sliderStartValue = anxietySlider.value;
        float sliderCurrentValue = sliderStartValue;

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
                    float targetValue = sliderStartValue * (1 - ((float)dialogueIndex / ghostBoyDialogue.Count()));
                    StartCoroutine(LerpAnxiety(sliderCurrentValue, targetValue));
                    sliderCurrentValue = targetValue;
                }
                dialogueIndex += 1;
                // Debug.Log(dialogueIndex);
            }
            yield return null;
        }
        StartCoroutine(FadeScreenOut());  
    }


    private IEnumerator LerpAnxiety(float start, float end) {

        float timer = 0f;

        while (timer < 0.5f) {
            anxietySlider.value = Mathf.SmoothStep(start, end, timer / 0.5f);

            timer += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeScreenOut() {
        float timer = 0f;
        while (timer < 2f) {

            blackScreen.color = new Color(0f,0f,0f, timer / 2f);

            timer += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(MomTextScene());
    }


    private IEnumerator MomTextScene()
    {
        if (AudioManager.audioManagerInstance)
        {
            AudioManager.audioManagerInstance.ChangeBGM("Pinecone_Jelly");
        }

        messageText.text = textsToMom[0];
        characterName.text = "Mom";
        profilePictureImage.sprite = momProfile;

        StartCoroutine(messageSystem.SlidePhone(-900f, 0f));

        int i = 0;

        while (i < 2) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (i == 0) {
                    messageText.text = textsToMom[1];
                }
                if (i == 1) {
                    StartCoroutine(messageSystem.SlidePhone(0f,-1000f));
                }
                i += 1;
            }
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("CreditsScene");
    }

}
