using UnityEngine;

public class Interactable : MonoBehaviour
{


    public GameObject text;
    public InteractableData data;
<<<<<<< HEAD


    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }



=======
    
>>>>>>> 8dc1cd69ad7e60cb9f3c277e61c0d8958ec4a11b
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
