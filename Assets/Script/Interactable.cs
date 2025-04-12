using UnityEngine;

public class Interactable : MonoBehaviour
{


    public GameObject text;

    public InteractableData data;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player") {
            text.SetActive(true);

            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();

            playerInteraction.SetTextData(data.level1);
        }

    }

    private void OnTriggerExit2D(Collider2D other) {


        if (other.tag == "Player") {
            text.SetActive(false);
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();

            playerInteraction.ResetData();

        }

    }
}
