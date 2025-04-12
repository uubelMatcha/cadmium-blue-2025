using System;
using UnityEngine;

namespace Script.Player
{
    public class PlayerController : MonoBehaviour
    {
        private MovementController MovementController;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            MovementController = GetComponent<MovementController>();
        }

        private void FixedUpdate()
        {
            MovementController.Move(); // Start is called once before the first execution of Update after the MonoBehaviour is created
        }
        

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
