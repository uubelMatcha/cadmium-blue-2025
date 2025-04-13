using System;
using UnityEngine;

public class GhostBoy : MonoBehaviour
{

    public GameObject text;
   private SpriteRenderer ghostboySprite;
   

   private void Start()
   {
       ghostboySprite = GetComponent<SpriteRenderer>();
   }

   private void Update()
   {
       GameObject other = GameObject.FindGameObjectWithTag("Player");
       //hack
       if ((other.transform.position.y > transform.position.y) && (other.GetComponent<SpriteRenderer>() != null))
       {
           //higher than the ghost boy
           ghostboySprite.sortingOrder = other.GetComponent<SpriteRenderer>().sortingOrder + 1;
       }
       else if(other.transform.position.y <= transform.position.y)
       {
           ghostboySprite.sortingOrder = other.GetComponent<SpriteRenderer>().sortingOrder - 1;
       }
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
