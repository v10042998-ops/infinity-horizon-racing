using UnityEngine;

public class AIOpponent : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 80f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float stoppingDistance = 10f;
    [SerializeField] private float waypointRadius = 5f;
    [SerializeField] private int difficultyLevel = 1; // 1 = Easy, 2 = Normal, 3 = Hard
    
    private Rigidbody rb;
    private VehicleController vehicleController;
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private float currentSpeed = 0f;
    private bool isActive = false;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        vehicleController = GetComponent<VehicleController>();
        FindWaypoints();
    }
    
    private void FixedUpdate()
    {
        if (isActive && waypoints.Length > 0)
        {
            FollowRoute();
        }
    }
    
    private void FindWaypoints()
    {
        GameObject waypointContainer = GameObject.Find("Waypoints");
        if (waypointContainer != null)
        {
            waypoints = waypointContainer.GetComponentsInChildren<Transform>();
        }
    }
    
    private void FollowRoute()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 directionToWaypoint = (targetWaypoint.position - transform.position).normalized;
        
        // Calculate speed based on difficulty and distance to waypoint
        float distanceToWaypoint = Vector3.Distance(transform.position, targetWaypoint.position);
        float targetSpeed = maxSpeed * (difficultyLevel / 3f);
        
        if (distanceToWaypoint < stoppingDistance)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * 2f);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * acceleration * 0.1f);
        }
        
        // Apply movement
        rb.velocity = directionToWaypoint * currentSpeed;
        
        // Rotate towards waypoint
        Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
        
        // Check if reached waypoint
        if (distanceToWaypoint < waypointRadius)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
    
    public void SetActive(bool active)
    {
        isActive = active;
    }
    
    public void SetDifficulty(int difficulty)
    {
        difficultyLevel = Mathf.Clamp(difficulty, 1, 3);
    }
    
    public float GetCurrentSpeed() => currentSpeed;
}
