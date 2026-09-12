using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private GameObject settingsPanel;
    
    private GameManager gameManager;
    private bool isPauseMenuOpen = false;
    
    private void Start()
    {
        gameManager = GameManager.Instance;
        
        if (resumeButton != null)
            resumeButton.onClick.AddListener(OnResumePressed);
        if (settingsButton != null)
            settingsButton.onClick.AddListener(OnSettingsPressed);
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(OnMainMenuPressed);
        
        if (gameManager != null)
            gameManager.OnPauseStateChanged += OnPauseStateChanged;
        
        if (pausePanel != null)
            pausePanel.SetActive(false);
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }
    
    private void OnDestroy()
    {
        if (gameManager != null)
            gameManager.OnPauseStateChanged -= OnPauseStateChanged;
    }
    
    private void OnPauseStateChanged(bool paused)
    {
        isPauseMenuOpen = paused;
        if (pausePanel != null)
            pausePanel.SetActive(paused);
    }
    
    private void OnResumePressed()
    {
        gameManager.TogglePause();
    }
    
    private void OnSettingsPressed()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }
    
    private void OnMainMenuPressed()
    {
        Time.timeScale = 1f; // Unpause
        gameManager.LoadScene("MainMenu");
    }
}
