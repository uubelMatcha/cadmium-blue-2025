using System;
using UnityEngine;
using UnityEngine.Events;


namespace Script.LevelScripts
{
    public class BGMInteractable : MonoBehaviour
    {
        public UnityEvent<string> triggerBGM;
        public string bgmTitle;
        
        //test function for audio switch
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                triggerBGM.Invoke(bgmTitle);
            }
        }
    }
}
