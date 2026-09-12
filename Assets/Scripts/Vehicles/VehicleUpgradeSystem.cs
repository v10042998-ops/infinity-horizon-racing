using UnityEngine;

public class VehicleUpgradeSystem : MonoBehaviour
{
    [System.Serializable]
    public class UpgradeStats
    {
        public int level;
        public int cost;
        public float maxSpeedBonus; // Added to max speed
        public float accelerationBonus;
        public float handlingBonus;
    }
    
    [SerializeField] private UpgradeStats[] engineUpgrades;
    [SerializeField] private UpgradeStats[] brakeUpgrades;
    [SerializeField] private UpgradeStats[] suspensionUpgrades;
    [SerializeField] private UpgradeStats[] tireUpgrades;
    
    private VehicleController vehicleController;
    private SaveSystem saveSystem;
    
    private void Start()
    {
        vehicleController = GetComponent<VehicleController>();
        saveSystem = GameManager.Instance.GetSaveSystem();
    }
    
    public bool TryUpgradeEngine(int vehicleId, GameManager gameManager)
    {
        PlayerSaveData data = saveSystem.GetSaveData();
        if (!data.vehicleUpgrades.ContainsKey(vehicleId))
            return false;
        
        VehicleUpgradeData upgrade = data.vehicleUpgrades[vehicleId];
        if (upgrade.engineLevel >= engineUpgrades.Length)
            return false;
        
        int cost = engineUpgrades[upgrade.engineLevel].cost;
        if (data.credits < cost)
            return false;
        
        // Apply upgrade
        upgrade.engineLevel++;
        data.credits -= cost;
        saveSystem.SaveGame();
        
        return true;
    }
    
    public UpgradeStats GetEngineUpgrade(int level)
    {
        if (level >= 0 && level < engineUpgrades.Length)
            return engineUpgrades[level];
        return null;
    }
    
    public float GetTotalMaxSpeedBonus(VehicleUpgradeData upgrades)
    {
        float bonus = 0f;
        bonus += engineUpgrades[upgrades.engineLevel].maxSpeedBonus;
        bonus += tireUpgrades[upgrades.tireLevel].maxSpeedBonus;
        return bonus;
    }
}
