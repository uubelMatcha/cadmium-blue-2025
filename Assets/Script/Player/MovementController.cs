using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Player
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 15f;
        //Set serializeField for debugging inputs
        [SerializeField] private Vector2 movementInput;
        private Vector2 lastDirectioninput;
        [SerializeField] private float magnitude;
        
        Rigidbody2D PlayerRb;
        private AnimationController playerAnimationController;
        private SpriteRenderer playerSpriteRenderer;

        public bool locked = false;

        //get set
        public Vector2 GetLastDirection()
        {
            return lastDirectioninput;
        }

        //Sets the last direction to the movementInput Vector retrieve from Input System event
        public void SetLastDirectionToPlayerInput()
        {
            if (movementInput.x != 0 || movementInput.y != 0)
            {
                lastDirectioninput = movementInput;   
            }
        }

        private void Awake()
        {
            //DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            PlayerRb = GetComponent<Rigidbody2D>();
            playerAnimationController = GetComponent<AnimationController>();
            playerSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        //call in PlayerController
        public void Move()
        {
            if (locked) {
                return;
            }
            
            PlayerRb.linearVelocity = movementInput * moveSpeed;
            magnitude = PlayerRb.linearVelocity.magnitude;
            
            //Set last dir to player input
            if (movementInput.x != 0 || movementInput.y != 0)
            {
                lastDirectioninput = movementInput;
                if (AudioManager.audioManagerInstance)
                {
                    AudioManager.audioManagerInstance.PlayFootsteps(true);
                }
            }
            else
            {
                if (AudioManager.audioManagerInstance)
                {
                    AudioManager.audioManagerInstance.PlayFootsteps(false);
                }
            }
            
            //flip sprite
            playerSpriteRenderer.flipX = lastDirectioninput switch
            {
                {x: > 0, y: 0} => true,
                {x: < 0, y: < 0} => true,
                _ => false
            };
            
            
            //get last position of player for facing idle direction
            playerAnimationController.SetAnimParamFloat(lastDirectioninput.x, "LastX");
            playerAnimationController.SetAnimParamFloat(lastDirectioninput.y, "LastY");
            
            // get the rb magnitude of difference between last and current frame
            playerAnimationController.SetAnimParamFloat(magnitude, "magnitude");
            
            //set player walk/run direction (only one anim)
            playerAnimationController.SetAnimParamFloat(movementInput.x, "DirX");
            playerAnimationController.SetAnimParamFloat(movementInput.y, "DirY");
        }
        
        public void GetMovementInput(InputAction.CallbackContext context)
        {
            movementInput = context.ReadValue<Vector2>();
        }
        
    }
}
