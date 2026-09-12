using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Dropdown qualityDropdown;
    [SerializeField] private Toggle vsyncToggle;
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Button backButton;
    
    private AudioManager audioManager;
    
    private void Start()
    {
        audioManager = AudioManager.Instance;
        
        // Setup sliders
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = audioManager.GetMasterVolume();
            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        }
        
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = audioManager.GetMusicVolume();
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }
        
        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = audioManager.GetSFXVolume();
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        }
        
        // Setup quality dropdown
        if (qualityDropdown != null)
        {
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            qualityDropdown.onValueChanged.AddListener(OnQualityChanged);
        }
        
        // Setup VSync toggle
        if (vsyncToggle != null)
        {
            vsyncToggle.isOn = QualitySettings.vSyncCount > 0;
            vsyncToggle.onValueChanged.AddListener(OnVSyncChanged);
        }
        
        // Setup back button
        if (backButton != null)
            backButton.onClick.AddListener(OnBackPressed);
    }
    
    private void OnMasterVolumeChanged(float value)
    {
        audioManager.SetMasterVolume(value);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
    
    private void OnMusicVolumeChanged(float value)
    {
        audioManager.SetMusicVolume(value);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
    
    private void OnSFXVolumeChanged(float value)
    {
        audioManager.SetSFXVolume(value);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
    
    private void OnQualityChanged(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
    }
    
    private void OnVSyncChanged(bool enabled)
    {
        QualitySettings.vSyncCount = enabled ? 1 : 0;
        PlayerPrefs.SetInt("VSync", enabled ? 1 : 0);
    }
    
    private void OnBackPressed()
    {
        gameObject.SetActive(false);
        gameObject.transform.parent.GetComponent<CanvasGroup>().alpha = 1f;
    }
}
