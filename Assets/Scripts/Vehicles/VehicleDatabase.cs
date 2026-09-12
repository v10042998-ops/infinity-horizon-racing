using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "VehicleDatabase", menuName = "Racing/Vehicle Database")]
public class VehicleDatabase : ScriptableObject
{
    [System.Serializable]
    public class VehicleData
    {
        public int id;
        public string name;
        public string description;
        public GameObject prefab;
        public Sprite thumbnail;
        [Range(0, 5)] public int rarity; // 0=Common, 5=Legendary
        public int baseCost;
        public float maxSpeed = 250f;
        public float acceleration = 100f;
        public float handling = 0.8f;
        public float acceleration_stat = 8f;
        public float speed_stat = 8f;
        public float handling_stat = 7f;
        public float drift_stat = 6f;
    }
    
    [SerializeField] private List<VehicleData> vehicles = new List<VehicleData>();
    
    public VehicleData GetVehicleById(int id)
    {
        return vehicles.Find(v => v.id == id);
    }
    
    public List<VehicleData> GetAllVehicles() => vehicles;
    
    public VehicleData GetVehicleByName(string name)
    {
        return vehicles.Find(v => v.name == name);
    }
}
