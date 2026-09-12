using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip engineIdleClip;
    [SerializeField] private AudioClip engineDriveClip;
    [SerializeField] private AudioClip tireSkidClip;
    [SerializeField] private AudioClip collisionClip;
    
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    private float masterVolume = 1f;
    private float musicVolume = 0.7f;
    private float sfxVolume = 0.8f;
    private List<AudioSource> pausedSources = new List<AudioSource>();
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    private void Start()
    {
        if (musicSource == null)
            musicSource = gameObject.AddComponent<AudioSource>();
        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();
        
        musicSource.loop = true;
        musicSource.volume = musicVolume * masterVolume;
        sfxSource.volume = sfxVolume * masterVolume;
        
        LoadAudioClips();
    }
    
    private void LoadAudioClips()
    {
        // Load audio from Resources folder
        // This is a placeholder - replace with actual audio loading
        Debug.Log("Audio Manager initialized");
    }
    
    public void PlayMusic(string clipName, bool loop = true)
    {
        if (audioClips.ContainsKey(clipName))
        {
            musicSource.clip = audioClips[clipName];
            musicSource.loop = loop;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music clip not found: " + clipName);
        }
    }
    
    public void PlaySFX(string clipName, float volume = 1f)
    {
        if (audioClips.ContainsKey(clipName))
        {
            sfxSource.PlayOneShot(audioClips[clipName], volume * sfxVolume * masterVolume);
        }
        else
        {
            Debug.LogWarning("SFX clip not found: " + clipName);
        }
    }
    
    public void PlayEngineSound(float rpm, float maxRPM)
    {
        if (musicSource.isPlaying)
        {
            float pitchFactor = rpm / maxRPM;
            musicSource.pitch = 0.5f + pitchFactor * 1.5f;
        }
    }
    
    public void StopMusic()
    {
        musicSource.Stop();
    }
    
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume * masterVolume;
        sfxSource.volume = sfxVolume * masterVolume;
    }
    
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume * masterVolume;
    }
    
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        sfxSource.volume = sfxVolume * masterVolume;
    }
    
    public void PauseAllAudio()
    {
        pausedSources.Clear();
        AudioSource[] allSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allSources)
        {
            if (source.isPlaying)
            {
                source.Pause();
                pausedSources.Add(source);
            }
        }
    }
    
    public void ResumeAllAudio()
    {
        foreach (AudioSource source in pausedSources)
        {
            if (source != null)
                source.Play();
        }
        pausedSources.Clear();
    }
    
    public float GetMasterVolume() => masterVolume;
    public float GetMusicVolume() => musicVolume;
    public float GetSFXVolume() => sfxVolume;
}
