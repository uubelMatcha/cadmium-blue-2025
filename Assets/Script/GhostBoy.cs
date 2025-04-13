using UnityEngine;

public class GhostBoy : MonoBehaviour
{

    public GameObject text;
    // public InteractableData data;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "PlayerInteractionHitBox") {
            text.SetActive(true);

            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();

            playerInteraction.ghostBoyIsTarget = true;
            // playerInteraction.SetTextData(data.level1);

        }

    }

    private void OnTriggerExit2D(Collider2D other) {


        if (other.tag == "PlayerInteractionHitBox") {
            text.SetActive(false);
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();

            // playerInteraction.ResetData();
            playerInteraction.ghostBoyIsTarget = false;

        }

    }
}
