using UnityEngine;

public class PerformanceProfiler : MonoBehaviour
{
    [SerializeField] private bool showDebugStats = true;
    
    private float updateInterval = 0.5f;
    private float accum = 0.0f;
    private int frames = 0;
    private float timeleft;
    private float fps = 0.0f;
    private float avgFrameTime = 0.0f;
    
    private GUIStyle style = new GUIStyle();
    
    private void Start()
    {
        timeleft = updateInterval;
        style.normal.textColor = Color.green;
        style.fontSize = 20;
    }
    
    private void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;
        
        if (timeleft <= 0.0)
        {
            fps = accum / frames;
            avgFrameTime = (1000.0f / fps);
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }
    
    private void OnGUI()
    {
        if (!showDebugStats)
            return;
        
        GUI.Label(new Rect(10, 10, 300, 50), 
            $"FPS: {fps:F1}\nFrame Time: {avgFrameTime:F2}ms\nMemory: {System.GC.GetTotalMemory(false) / 1048576}MB",
            style);
    }
    
    public float GetCurrentFPS() => fps;
    public float GetAverageFrameTime() => avgFrameTime;
}
