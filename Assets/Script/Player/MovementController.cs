<<<<<<< HEAD
using System;
=======
>>>>>>> cda1550 (add player movement and input system)
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Player
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 15f;
        //Set serializeField for debugging inputs
        [SerializeField] private Vector2 movementInput;
<<<<<<< HEAD
        private Vector2 lastDirectioninput;
        [SerializeField] private float magnitude;
        
        Rigidbody2D PlayerRb;
        private AnimationController playerAnimationController;

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
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            PlayerRb = GetComponent<Rigidbody2D>();
            playerAnimationController = GetComponent<AnimationController>();
        }

        //call in PlayerController
        public void Move()
        {
            PlayerRb.linearVelocity = movementInput * moveSpeed;
            magnitude = PlayerRb.linearVelocity.magnitude;
            
            //Set last dir to player input
            if (movementInput.x != 0 || movementInput.y != 0)
            {
                lastDirectioninput = movementInput;   
            }
            
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
=======
        
        Rigidbody2D playerRB;
        void Start()
        {
            playerRB = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            playerRB.linearVelocity = movementInput * moveSpeed;
        }

        public void Move(InputAction.CallbackContext context)
>>>>>>> cda1550 (add player movement and input system)
        {
                movementInput = context.ReadValue<Vector2>();
            
        }
<<<<<<< HEAD
=======
        
>>>>>>> cda1550 (add player movement and input system)
    }
}
