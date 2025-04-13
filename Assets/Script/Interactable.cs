using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Script
{
    public class Interactable : MonoBehaviour
    {


<<<<<<< HEAD
    public GameObject text;
    public InteractableData data;
    
    private void OnTriggerEnter2D(Collider2D other) {
=======
        public GameObject text;
        public InteractableData data;

        private void OnTriggerEnter2D(Collider2D other) {
>>>>>>> be297cb08596f912e5e89f958eb8d481fb92325a

            if (other.tag == "PlayerInteractionHitBox") {
                text.SetActive(true);

                PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();

<<<<<<< HEAD
=======
                AudioManager.audioManagerInstance.PlaySoundEffect(data.sfx.musicTitle);
>>>>>>> be297cb08596f912e5e89f958eb8d481fb92325a
            
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
<<<<<<< HEAD
            else if (anxiety <= 0.66f) {
                playerInteraction.SetTextData(data.level2);
            }
            else {
                playerInteraction.SetTextData(data.level3);
            }

            playerInteraction.sfx = data.sfx.musicTitle;
        }

    }

    private void OnTriggerExit2D(Collider2D other) {


        if (other.tag == "PlayerInteractionHitBox") {
            text.SetActive(false);
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();

            playerInteraction.ResetData();
=======
>>>>>>> be297cb08596f912e5e89f958eb8d481fb92325a

        }

        private void OnTriggerExit2D(Collider2D other) {


            if (other.tag == "PlayerInteractionHitBox") {
                text.SetActive(false);
                PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();

                playerInteraction.ResetData();

            }

        }

        private void Update()
        {
            GameObject other = GameObject.FindGameObjectWithTag("Player");
            //hack
            if ((other.transform.position.y > transform.position.y) && (other.GetComponent<SpriteRenderer>() != null))
            {
                //higher than the ghost boy
                gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = other.GetComponent<SpriteRenderer>().sortingOrder + 1;
            }
            else if(other.transform.position.y <= transform.position.y)
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = other.GetComponent<SpriteRenderer>().sortingOrder - 1;
            }
        }
    }
}
