using UnityEngine;

namespace Script.Player
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator playerAnim;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void SetAnimParamFloat(float input, string animParam)
        {
            playerAnim.SetFloat(animParam, input);
        }
    }
}
