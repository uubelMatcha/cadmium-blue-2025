using UnityEngine;
using Script.Player;
using TMPro;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{


    public MovementController movementController;
    public GameObject flavorTextUI;
    public float yOffset = 0f;
    public float scaleFactor = 2f;

    public float textDuration = 5f;

    private Vector2 lastDirectionInput;

    public string currentData = null;
    public string sfx = null;

    public bool ghostBoyIsTarget = false;
    private bool endingInProgress = false;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        lastDirectionInput = movementController.GetLastDirection();
        transform.localPosition = new Vector3(lastDirectionInput.x * scaleFactor, lastDirectionInput.y * scaleFactor + yOffset, 0f);

    }


    public void SetTextData(string interactableData) {

        currentData = interactableData;

    }

    public void ResetData() {

        currentData = null;
        sfx = null;

    }

    public void DisplayText() {

        // Debug.Log("E was pressed");


        if (ghostBoyIsTarget && !endingInProgress) {
            // Disable movement
          
            FindFirstObjectByType<EndingSystem>().StartEnding();
            endingInProgress = true;

        }


        if (currentData is null) {
            // Debug.Log("Data is null");
            return;
        }
        else if (flavorTextUI.activeInHierarchy == true) {
            // Debug.Log("Data is already enabled");
            return;
        }
        else {
            // Debug.Log("Displaying Text");
            AudioManager.audioManagerInstance.PlaySoundEffect(sfx);

            flavorTextUI.GetComponentInChildren<TextMeshProUGUI>().text = currentData;
            flavorTextUI.SetActive(true);
            StartCoroutine(HideText());
        }
    }

    private IEnumerator HideText() {

        float timer = 0f;

        while (timer <= textDuration) {

            timer += Time.deltaTime;
            yield return null;
        }

        flavorTextUI.SetActive(false);

    }



}
