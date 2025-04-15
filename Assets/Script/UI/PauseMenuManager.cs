using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Teleport Debug")]
    [SerializeField] private Transform teleportLocation;
    [SerializeField] private GameObject player;
    
#region  Singleton
    public static PauseMenuManager Instance { get; private set; }

    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
#endregion
    
    private void Update()
    {
        /* remove debug used for behaviour jam
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        
        if(Input.GetKeyDown(KeyCode.T))
        {
            player.transform.position = teleportLocation.position;
        }
        */
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
