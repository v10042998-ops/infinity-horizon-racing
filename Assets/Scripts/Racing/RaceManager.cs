using UnityEngine;
using System.Collections.Generic;

public enum RaceMode
{
    FreeRoam,
    Circuit,
    Sprint,
    TimeTrial,
    Checkpoint,
    Drag,
    Drift,
    Highway,
    OffRoad
}

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance { get; private set; }
    
    [SerializeField] private RaceMode raceMode = RaceMode.FreeRoam;
    [SerializeField] private int lapCount = 3;
    [SerializeField] private float checkpointRadius = 50f;
    
    private List<Transform> raceCheckpoints = new List<Transform>();
    private VehicleController playerVehicle;
    private int currentLap = 1;
    private float raceTime = 0f;
    private bool raceActive = false;
    private int playerPosition = 1;
    private List<AIOpponent> aiOpponents = new List<AIOpponent>();
    
    public delegate void RaceEvent();
    public event RaceEvent OnRaceStart;
    public event RaceEvent OnRaceEnd;
    public event RaceEvent OnLapComplete;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    private void Start()
    {
        playerVehicle = FindObjectOfType<VehicleController>();
        FindAllCheckpoints();
        
        if (raceMode != RaceMode.FreeRoam)
        {
            StartRace();
        }
    }
    
    private void Update()
    {
        if (raceActive)
        {
            raceTime += Time.deltaTime;
            UpdatePlayerPosition();
            CheckCheckpointCollisions();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartRace();
        }
    }
    
    private void FindAllCheckpoints()
    {
        GameObject[] checkpointObjects = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (GameObject cp in checkpointObjects)
        {
            raceCheckpoints.Add(cp.transform);
        }
        
        // Sort checkpoints by their order in the race
        raceCheckpoints.Sort((a, b) => a.name.CompareTo(b.name));
    }
    
    public void StartRace()
    {
        raceActive = true;
        raceTime = 0f;
        currentLap = 1;
        OnRaceStart?.Invoke();
        Debug.Log("Race started: " + raceMode);
    }
    
    public void EndRace()
    {
        raceActive = false;
        OnRaceEnd?.Invoke();
        Debug.Log("Race ended. Final time: " + raceTime);
    }
    
    private void CheckCheckpointCollisions()
    {
        if (playerVehicle == null || raceCheckpoints.Count == 0)
            return;
        
        foreach (Transform checkpoint in raceCheckpoints)
        {
            float distance = Vector3.Distance(playerVehicle.transform.position, checkpoint.position);
            if (distance < checkpointRadius)
            {
                // Player passed checkpoint
                OnLapComplete?.Invoke();
                currentLap++;
                
                if (currentLap > lapCount)
                {
                    EndRace();
                }
            }
        }
    }
    
    private void UpdatePlayerPosition()
    {
        // Simple position calculation based on checkpoints passed
        playerPosition = 1;
        foreach (AIOpponent ai in aiOpponents)
        {
            // Compare progress
        }
    }
    
    public void RestartRace()
    {
        Debug.Log("Race restarted");
        Time.timeScale = 1f;
        raceActive = false;
        Start();
    }
    
    public float GetRaceTime() => raceTime;
    public int GetCurrentLap() => currentLap;
    public int GetPlayerPosition() => playerPosition;
    public bool IsRaceActive() => raceActive;
}
