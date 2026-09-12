using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button careerButton;
    [SerializeField] private Button garageButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;
    
    private GameManager gameManager;
    private SaveSystem saveSystem;
    
    private void Start()
    {
        gameManager = GameManager.Instance;
        saveSystem = gameManager.GetSaveSystem();
        
        playButton.onClick.AddListener(OnPlayPressed);
        careerButton.onClick.AddListener(OnCareerPressed);
        garageButton.onClick.AddListener(OnGaragePressed);
        settingsButton.onClick.AddListener(OnSettingsPressed);
        quitButton.onClick.AddListener(OnQuitPressed);
    }
    
    private void OnPlayPressed()
    {
        Debug.Log("Play button pressed - starting free roam");
        gameManager.LoadScene("Racing");
    }
    
    private void OnCareerPressed()
    {
        Debug.Log("Career button pressed");
        gameManager.LoadScene("Career");
    }
    
    private void OnGaragePressed()
    {
        Debug.Log("Garage button pressed");
        gameManager.LoadScene("Garage");
    }
    
    private void OnSettingsPressed()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    
    private void OnQuitPressed()
    {
        gameManager.QuitGame();
    }
}
