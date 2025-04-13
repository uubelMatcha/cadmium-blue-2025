using UnityEngine;

public class Interactable : MonoBehaviour
{


    public GameObject text;
    public InteractableData data;

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "PlayerInteractionHitBox") {
            text.SetActive(true);

            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();

            AudioManager.audioManagerInstance.PlaySoundEffect(data.sfx.musicTitle);
            
            float anxiety = FindFirstObjectByType<AnxietySystem>().anxietyLevel;

            if (anxiety <= 0.33f) {
                playerInteraction.SetTextData(data.level1);
            }
            else if (anxiety <= 0.66f) {
                playerInteraction.SetTextData(data.level2);
            }
            else {
                playerInteraction.SetTextData(data.level3);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other) {


        if (other.tag == "PlayerInteractionHitBox") {
            text.SetActive(false);
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();

            playerInteraction.ResetData();

        }

    }
}
