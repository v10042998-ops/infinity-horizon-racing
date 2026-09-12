using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Text speedometerText;
    [SerializeField] private Text rpmText;
    [SerializeField] private Text gearText;
    [SerializeField] private Text lapText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text positionText;
    [SerializeField] private Image speedBar;
    [SerializeField] private Image rpmBar;
    
    private VehicleController vehicleController;
    private RaceManager raceManager;
    private float maxSpeedDisplay = 300f;
    
    private void Start()
    {
        vehicleController = FindObjectOfType<VehicleController>();
        raceManager = RaceManager.Instance;
        
        if (raceManager != null)
        {
            raceManager.OnRaceStart += OnRaceStart;
            raceManager.OnRaceEnd += OnRaceEnd;
        }
    }
    
    private void OnDestroy()
    {
        if (raceManager != null)
        {
            raceManager.OnRaceStart -= OnRaceStart;
            raceManager.OnRaceEnd -= OnRaceEnd;
        }
    }
    
    private void Update()
    {
        if (vehicleController != null)
        {
            UpdateSpeedometerUI();
            UpdateRPMUI();
            UpdateGearUI();
        }
        
        if (raceManager != null)
        {
            UpdateRaceUI();
        }
    }
    
    private void UpdateSpeedometerUI()
    {
        float speed = vehicleController.GetCurrentSpeed();
        int speedKMH = (int)(speed * 3.6f); // m/s to km/h
        
        if (speedometerText != null)
            speedometerText.text = speedKMH.ToString("D3");
        
        if (speedBar != null)
        {
            float fillAmount = Mathf.Clamp01(speed / maxSpeedDisplay);
            speedBar.fillAmount = fillAmount;
        }
    }
    
    private void UpdateRPMUI()
    {
        float rpm = vehicleController.GetEngineRPM();
        
        if (rpmText != null)
            rpmText.text = rpm.ToString("F0");
        
        if (rpmBar != null)
        {
            float fillAmount = rpm / 7000f; // Max RPM
            rpmBar.fillAmount = Mathf.Clamp01(fillAmount);
        }
    }
    
    private void UpdateGearUI()
    {
        int gear = (int)vehicleController.GetGear();
        
        if (gearText != null)
        {
            if (gear == 0)
                gearText.text = "R";
            else if (gear == 1)
                gearText.text = "N";
            else
                gearText.text = (gear - 1).ToString();
        }
    }
    
    private void UpdateRaceUI()
    {
        if (raceManager.IsRaceActive())
        {
            if (lapText != null)
                lapText.text = $"Lap: {raceManager.GetCurrentLap()}";
            
            if (timerText != null)
            {
                float time = raceManager.GetRaceTime();
                int minutes = (int)(time / 60);
                int seconds = (int)(time % 60);
                int milliseconds = (int)((time - Mathf.Floor(time)) * 1000);
                timerText.text = $"{minutes:D2}:{seconds:D2}.{milliseconds:D3}";
            }
            
            if (positionText != null)
                positionText.text = $"Position: {raceManager.GetPlayerPosition()}";
        }
    }
    
    private void OnRaceStart()
    {
        gameObject.SetActive(true);
    }
    
    private void OnRaceEnd()
    {
        // Show results screen
    }
}
