using UnityEngine;
using System.Collections.Generic;

public class WeatherSystem : MonoBehaviour
{
    public enum WeatherType
    {
        Clear,
        Rainy,
        Stormy,
        Snowy,
        Foggy
    }
    
    [SerializeField] private WeatherType currentWeather = WeatherType.Clear;
    [SerializeField] private Light sunLight;
    [SerializeField] private ParticleSystem rainParticles;
    [SerializeField] private ParticleSystem snowParticles;
    [SerializeField] private ParticleSystem stormParticles;
    [SerializeField] private float weatherChangeInterval = 300f; // 5 minutes
    
    private float weatherTimer = 0f;
    private float transitionDuration = 10f;
    private float transitionTimer = 0f;
    private Color dayColor = new Color(1f, 1f, 1f);
    private Color nightColor = new Color(0.3f, 0.3f, 0.5f);
    
    private void Update()
    {
        weatherTimer += Time.deltaTime;
        
        if (weatherTimer >= weatherChangeInterval)
        {
            ChangeWeather((WeatherType)Random.Range(0, 5));
            weatherTimer = 0f;
        }
        
        UpdateDayNightCycle();
    }
    
    public void ChangeWeather(WeatherType newWeather)
    {
        if (newWeather == currentWeather)
            return;
        
        currentWeather = newWeather;
        ApplyWeather();
    }
    
    private void ApplyWeather()
    {
        // Disable all weather effects first
        if (rainParticles) rainParticles.Stop();
        if (snowParticles) snowParticles.Stop();
        if (stormParticles) stormParticles.Stop();
        
        switch (currentWeather)
        {
            case WeatherType.Rainy:
                if (rainParticles) rainParticles.Play();
                break;
            case WeatherType.Snowy:
                if (snowParticles) snowParticles.Play();
                break;
            case WeatherType.Stormy:
                if (stormParticles) stormParticles.Play();
                break;
            case WeatherType.Foggy:
                RenderSettings.fog = true;
                RenderSettings.fogDensity = 0.05f;
                break;
            case WeatherType.Clear:
            default:
                RenderSettings.fog = false;
                break;
        }
        
        Debug.Log($"Weather changed to: {currentWeather}");
    }
    
    private void UpdateDayNightCycle()
    {
        if (sunLight == null)
            return;
        
        // Simple day/night cycle - rotate sun
        float timeOfDay = (System.DateTime.Now.Hour + System.DateTime.Now.Minute / 60f) / 24f;
        float sunRotation = timeOfDay * 360f;
        
        sunLight.transform.eulerAngles = new Vector3(sunRotation - 90f, 0, 0);
        
        // Adjust lighting
        if (timeOfDay > 0.2f && timeOfDay < 0.8f) // Day time
        {
            sunLight.color = dayColor;
            RenderSettings.ambientLight = Color.white * 0.8f;
        }
        else // Night time
        {
            sunLight.color = nightColor;
            RenderSettings.ambientLight = nightColor * 0.3f;
        }
    }
    
    public WeatherType GetCurrentWeather() => currentWeather;
}
