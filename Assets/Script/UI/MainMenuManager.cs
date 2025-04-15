using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsMenuButton;
    [SerializeField] private Button exitButton;

    [Header("Next Scene")]
    [SerializeField] private string gameSceneName;
    
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;

#region  Singleton
    public static MainMenuManager Instance { get; private set; }

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
        startButton.onClick.AddListener(StartGame);
        settingsMenuButton.onClick.AddListener(OpenSettingsMenu);
        exitButton.onClick.AddListener(QuitGame);
        
        CloseSettingsMenu();
    }

    private void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
    
    private void OpenSettingsMenu()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    
    //closed in settings manager
    public void CloseSettingsMenu()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
}
