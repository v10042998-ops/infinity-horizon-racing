using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private float timeScale = 1f;
    private bool isPaused = false;
    private SaveSystem saveSystem;
    private AudioManager audioManager;
    
    public delegate void PauseStateChanged(bool isPaused);
    public event PauseStateChanged OnPauseStateChanged;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        saveSystem = GetComponent<SaveSystem>();
        audioManager = GetComponent<AudioManager>();
        
        if (saveSystem == null)
            gameObject.AddComponent<SaveSystem>();
        if (audioManager == null)
            gameObject.AddComponent<AudioManager>();
    }
    
    private void Start()
    {
        Time.timeScale = timeScale;
        LoadGameSettings();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : timeScale;
        OnPauseStateChanged?.Invoke(isPaused);
        
        if (isPaused)
            audioManager?.PauseAllAudio();
        else
            audioManager?.ResumeAllAudio();
    }
    
    public void SetTimeScale(float scale)
    {
        timeScale = Mathf.Clamp01(scale);
        if (!isPaused)
            Time.timeScale = timeScale;
    }
    
    public bool IsPaused => isPaused;
    
    private void LoadGameSettings()
    {
        // Load graphics settings
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityLevel", 2));
        Screen.SetResolution(
            PlayerPrefs.GetInt("ScreenWidth", 1920),
            PlayerPrefs.GetInt("ScreenHeight", 1080),
            PlayerPrefs.GetInt("Fullscreen", 0) == 1
        );
    }
    
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
    public SaveSystem GetSaveSystem() => saveSystem;
    public AudioManager GetAudioManager() => audioManager;
}
