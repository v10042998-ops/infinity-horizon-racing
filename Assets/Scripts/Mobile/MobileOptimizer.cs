using UnityEngine;

public class MobileOptimizer : MonoBehaviour
{
    [SerializeField] private bool autoOptimizeForMobile = true;
    [SerializeField] private float targetFramerate = 30f;
    [SerializeField] private int maxDrawCalls = 100;
    
    private int qualityLevel;
    
    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (autoOptimizeForMobile)
            {
                OptimizeForMobile();
            }
        }
    }
    
    private void OptimizeForMobile()
    {
        // Set frame rate
        Application.targetFrameRate = (int)targetFramerate;
        
        // Lower quality settings
        QualitySettings.SetQualityLevel(0, true);
        
        // Disable expensive features
        QualitySettings.shadowDistance = 50f;
        QualitySettings.vSyncCount = 0; // Disable vsync for consistent frame rate
        
        // Optimize physics
        Physics.defaultSolverIterations = 6;
        Physics.defaultSolverVelocityIterations = 1;
        
        Debug.Log("Mobile optimization applied");
    }
    
    private void Update()
    {
        // Monitor performance and adjust dynamically
        if (Application.platform == RuntimePlatform.Android)
        {
            AdaptiveQuality();
        }
    }
    
    private void AdaptiveQuality()
    {
        float currentFPS = 1f / Time.deltaTime;
        
        // Lower quality if FPS drops below threshold
        if (currentFPS < targetFramerate - 5f)
        {
            if (QualitySettings.GetQualityLevel() > 0)
            {
                QualitySettings.DecreaseLevel();
                Debug.Log("Quality decreased due to low FPS");
            }
        }
        // Increase quality if FPS is stable
        else if (currentFPS > targetFramerate + 10f)
        {
            if (QualitySettings.GetQualityLevel() < QualitySettings.names.Length - 1)
            {
                QualitySettings.IncreaseLevel();
                Debug.Log("Quality increased - stable FPS");
            }
        }
    }
    
    public void SetTargetFramerate(float fps)
    {
        targetFramerate = Mathf.Max(20f, Mathf.Min(fps, 60f));
        Application.targetFrameRate = (int)targetFramerate;
    }
}
