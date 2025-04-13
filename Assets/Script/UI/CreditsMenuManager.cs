using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsMenuManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button restartButton;

    [Header("Next Scene")]
    [SerializeField] private string titleSceneName;
    
#region  Singleton
    public static CreditsMenuManager Instance { get; private set; }

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
    
    private void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(titleSceneName);
    }
}
