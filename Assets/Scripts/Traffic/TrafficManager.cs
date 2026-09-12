using UnityEngine;
using System.Collections.Generic;

public class TrafficManager : MonoBehaviour
{
    [SerializeField] private int maxTrafficVehicles = 20;
    [SerializeField] private float spawnDistance = 200f;
    [SerializeField] private float despawnDistance = 300f;
    [SerializeField] private GameObject trafficVehiclePrefab;
    
    private List<GameObject> activeTraffic = new List<GameObject>();
    private Transform player;
    private ObjectPool trafficPool;
    
    private void Start()
    {
        player = FindObjectOfType<VehicleController>()?.transform;
        trafficPool = new ObjectPool(trafficVehiclePrefab, maxTrafficVehicles, transform);
    }
    
    private void Update()
    {
        if (player != null)
        {
            UpdateTraffic();
        }
    }
    
    private void UpdateTraffic()
    {
        // Spawn traffic near player
        if (activeTraffic.Count < maxTrafficVehicles)
        {
            Vector3 spawnPos = player.position + Random.onUnitSphere * spawnDistance;
            spawnPos.y = 0; // On ground
            
            GameObject traffic = trafficPool.Get();
            traffic.transform.position = spawnPos;
            traffic.transform.rotation = Random.rotation;
            activeTraffic.Add(traffic);
        }
        
        // Despawn distant traffic
        for (int i = activeTraffic.Count - 1; i >= 0; i--)
        {
            float distance = Vector3.Distance(activeTraffic[i].transform.position, player.position);
            if (distance > despawnDistance)
            {
                trafficPool.Release(activeTraffic[i]);
                activeTraffic.RemoveAt(i);
            }
        }
    }
}

public class ObjectPool
{
    private Queue<GameObject> pool = new Queue<GameObject>();
    private GameObject prefab;
    private Transform parent;
    private int maxSize;
    
    public ObjectPool(GameObject prefab, int maxSize, Transform parent)
    {
        this.prefab = prefab;
        this.maxSize = maxSize;
        this.parent = parent;
        
        for (int i = 0; i < maxSize; i++)
        {
            GameObject obj = Object.Instantiate(prefab, parent);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    
    public GameObject Get()
    {
        GameObject obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = Object.Instantiate(prefab, parent);
        }
        obj.SetActive(true);
        return obj;
    }
    
    public void Release(GameObject obj)
    {
        obj.SetActive(false);
        if (pool.Count < maxSize)
            pool.Enqueue(obj);
    }
}
