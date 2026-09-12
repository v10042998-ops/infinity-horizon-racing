using UnityEngine;

[CreateAssetMenu(fileName = "GraphicsSettings", menuName = "Racing/Graphics Settings")]
public class GraphicsSettingsConfig : ScriptableObject
{
    [System.Serializable]
    public class QualityPreset
    {
        public string presetName;
        public int shadowDistance = 100;
        public int textureQuality = 0; // 0=Full, 1=Half, 2=Quarter
        public bool enableSSAO = true;
        public bool enableMotionBlur = true;
        public bool enableBloom = true;
        public int antiAliasing = 2; // 0=Off, 2=2x, 4=4x, 8=8x
        public float LODBias = 1f;
        public bool enableVsync = true;
        public int targetFramerate = 60;
    }
    
    [SerializeField] private QualityPreset[] qualityPresets;
    [SerializeField] private bool enableDynamicResolution = false;
    [SerializeField] private float minResolutionScale = 0.5f;
    [SerializeField] private float maxResolutionScale = 1f;
    
    public QualityPreset GetPreset(int index)
    {
        if (index >= 0 && index < qualityPresets.Length)
            return qualityPresets[index];
        return qualityPresets[qualityPresets.Length - 1];
    }
    
    public int GetPresetCount() => qualityPresets.Length;
    public QualityPreset[] GetAllPresets() => qualityPresets;
}
