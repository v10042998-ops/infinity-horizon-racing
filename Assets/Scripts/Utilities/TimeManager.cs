using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float gameTime = 0f;
    private float timeScale = 1f;
    private bool isPaused = false;
    
    public delegate void TimeChangedEvent(float newTime);
    public event TimeChangedEvent OnTimeChanged;
    
    private void Update()
    {
        if (!isPaused)
        {
            gameTime += Time.deltaTime * timeScale;
            OnTimeChanged?.Invoke(gameTime);
        }
    }
    
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }
    
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = timeScale;
    }
    
    public void SetTimeScale(float scale)
    {
        timeScale = Mathf.Max(0f, scale);
        if (!isPaused)
            Time.timeScale = timeScale;
    }
    
    public float GetGameTime() => gameTime;
    public bool IsPaused() => isPaused;
    public float GetTimeScale() => timeScale;
}
