using UnityEngine;

[CreateAssetMenu(fileName = "ControlSettings", menuName = "Racing/Control Settings")]
public class ControlSettingsConfig : ScriptableObject
{
    [System.Serializable]
    public class ControlScheme
    {
        public string schemeName;
        public float steerSensitivity = 1.5f;
        public float accelerationSensitivity = 1f;
        public float brakeSensitivity = 1f;
        public bool invertY = false;
        public bool enableControllerVibration = true;
        public float vibrationIntensity = 1f;
    }
    
    [SerializeField] private ControlScheme pcScheme;
    [SerializeField] private ControlScheme androidScheme;
    [SerializeField] private ControlScheme controllerScheme;
    
    public ControlScheme GetPCScheme() => pcScheme;
    public ControlScheme GetAndroidScheme() => androidScheme;
    public ControlScheme GetControllerScheme() => controllerScheme;
    
    public ControlScheme GetCurrentScheme()
    {
        if (Application.platform == RuntimePlatform.Android)
            return androidScheme;
        else
            return pcScheme; // Could detect controller
    }
}
