using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Player
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 15f;
        //Set serializeField for debugging inputs
        [SerializeField] private Vector2 movementInput;
        
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
        {
                movementInput = context.ReadValue<Vector2>();
            
        }
        
    }
}
