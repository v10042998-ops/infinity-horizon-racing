using UnityEngine;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class PlayerSaveData
{
    public string playerName;
    public int level;
    public int credits;
    public int experience;
    public List<int> unlockedVehicles = new List<int>();
    public Dictionary<int, VehicleUpgradeData> vehicleUpgrades = new Dictionary<int, VehicleUpgradeData>();
    public int worldSeed;
    public float playtimeSeconds;
    public List<RaceRecord> raceRecords = new List<RaceRecord>();
}

[System.Serializable]
public class VehicleUpgradeData
{
    public int engineLevel;
    public int brakeLevel;
    public int suspensionLevel;
    public int tireLevel;
    public int transmissionLevel;
}

[System.Serializable]
public class RaceRecord
{
    public string raceName;
    public string raceMode;
    public float bestTime;
    public int position;
    public int credits;
    public System.DateTime date;
}

public class SaveSystem : MonoBehaviour
{
    private string savePath;
    private const string SAVE_FILE = "savegame.json";
    private PlayerSaveData currentSaveData;
    
    private void Awake()
    {
        #if UNITY_ANDROID
            savePath = Application.persistentDataPath + "/InfinityHorizonRacing/";
        #else
            savePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "/InfinityHorizonRacing/";
        #endif
        
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
    }
    
    public void CreateNewSave(string playerName, int worldSeed)
    {
        currentSaveData = new PlayerSaveData
        {
            playerName = playerName,
            level = 1,
            credits = 100000,
            experience = 0,
            worldSeed = worldSeed,
            playtimeSeconds = 0f
        };
        
        // Unlock first vehicle
        currentSaveData.unlockedVehicles.Add(0);
        SaveGame();
    }
    
    public void SaveGame()
    {
        if (currentSaveData == null)
            return;
        
        string json = JsonUtility.ToJson(currentSaveData, true);
        string filePath = savePath + SAVE_FILE;
        
        File.WriteAllText(filePath, json);
        Debug.Log("Game saved to: " + filePath);
    }
    
    public bool LoadGame()
    {
        string filePath = savePath + SAVE_FILE;
        
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Save file not found: " + filePath);
            return false;
        }
        
        try
        {
            string json = File.ReadAllText(filePath);
            currentSaveData = JsonUtility.FromJson<PlayerSaveData>(json);
            Debug.Log("Game loaded from: " + filePath);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load game: " + e.Message);
            return false;
        }
    }
    
    public void AddCredits(int amount)
    {
        if (currentSaveData != null)
            currentSaveData.credits += amount;
    }
    
    public void AddExperience(int amount)
    {
        if (currentSaveData == null)
            return;
        
        currentSaveData.experience += amount;
        
        // Level up every 1000 XP
        int newLevel = 1 + currentSaveData.experience / 1000;
        if (newLevel > currentSaveData.level)
            currentSaveData.level = newLevel;
    }
    
    public void UnlockVehicle(int vehicleId)
    {
        if (currentSaveData != null && !currentSaveData.unlockedVehicles.Contains(vehicleId))
            currentSaveData.unlockedVehicles.Add(vehicleId);
    }
    
    public void RecordRaceResult(string raceName, string raceMode, float bestTime, int position, int credits)
    {
        if (currentSaveData == null)
            return;
        
        currentSaveData.raceRecords.Add(new RaceRecord
        {
            raceName = raceName,
            raceMode = raceMode,
            bestTime = bestTime,
            position = position,
            credits = credits,
            date = System.DateTime.Now
        });
        
        AddCredits(credits);
    }
    
    public PlayerSaveData GetSaveData() => currentSaveData;
    
    public bool SaveFileExists()
    {
        return File.Exists(savePath + SAVE_FILE);
    }
    
    public void DeleteSave()
    {
        string filePath = savePath + SAVE_FILE;
        if (File.Exists(filePath))
            File.Delete(filePath);
        currentSaveData = null;
    }
}
