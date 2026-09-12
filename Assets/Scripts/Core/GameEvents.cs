using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance { get; private set; }
    
    // Race events
    public event Action OnRaceStarted;
    public event Action OnRaceEnded;
    public event Action OnLapCompleted;
    public event Action<int> OnCheckpointReached;
    public event Action<Vector3> OnCollision;
    
    // Vehicle events
    public event Action<float> OnSpeedChanged;
    public event Action<float> OnRPMChanged;
    public event Action OnDrifting;
    public event Action OnCrashed;
    
    // Game events
    public event Action OnGamePaused;
    public event Action OnGameResumed;
    public event Action<int> OnCreditsGained;
    public event Action<int> OnExperienceGained;
    
    // UI events
    public event Action OnMenuOpened;
    public event Action OnMenuClosed;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void RaceStarted() => OnRaceStarted?.Invoke();
    public void RaceEnded() => OnRaceEnded?.Invoke();
    public void LapCompleted() => OnLapCompleted?.Invoke();
    public void CheckpointReached(int checkpointId) => OnCheckpointReached?.Invoke(checkpointId);
    public void Collision(Vector3 position) => OnCollision?.Invoke(position);
    
    public void SpeedChanged(float speed) => OnSpeedChanged?.Invoke(speed);
    public void RPMChanged(float rpm) => OnRPMChanged?.Invoke(rpm);
    public void Drifting() => OnDrifting?.Invoke();
    public void Crashed() => OnCrashed?.Invoke();
    
    public void GamePaused() => OnGamePaused?.Invoke();
    public void GameResumed() => OnGameResumed?.Invoke();
    public void CreditsGained(int amount) => OnCreditsGained?.Invoke(amount);
    public void ExperienceGained(int amount) => OnExperienceGained?.Invoke(amount);
    
    public void MenuOpened() => OnMenuOpened?.Invoke();
    public void MenuClosed() => OnMenuClosed?.Invoke();
}
