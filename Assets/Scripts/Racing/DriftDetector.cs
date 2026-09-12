using UnityEngine;

public class DriftDetector : MonoBehaviour
{
    [SerializeField] private float driftThreshold = 0.3f; // Sideslip angle threshold
    [SerializeField] private float driftScoreMultiplier = 10f;
    
    private Rigidbody rb;
    private VehicleController vehicleController;
    private bool isDrifting = false;
    private float driftScore = 0f;
    private float driftComboTimer = 0f;
    private float maxComboTime = 2f;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        vehicleController = GetComponent<VehicleController>();
    }
    
    private void FixedUpdate()
    {
        DetectDrift();
    }
    
    private void Update()
    {
        // Decay combo timer
        if (isDrifting)
        {
            driftComboTimer -= Time.deltaTime;
            if (driftComboTimer <= 0f)
            {
                EndDrift();
            }
        }
    }
    
    private void DetectDrift()
    {
        if (rb.velocity.magnitude < 5f) // Too slow to drift
        {
            if (isDrifting)
                EndDrift();
            return;
        }
        
        // Calculate sideslip
        Vector3 forwardVelocity = Vector3.Project(rb.velocity, transform.forward);
        Vector3 sidewaysVelocity = rb.velocity - forwardVelocity;
        
        float sideslipAngle = Vector3.Angle(rb.velocity, transform.forward);
        
        if (sideslipAngle > driftThreshold && vehicleController.GetIsHandbraking())
        {
            if (!isDrifting)
            {
                StartDrift();
            }
            
            // Accumulate drift score
            driftScore += sidewaysVelocity.magnitude * driftScoreMultiplier * Time.deltaTime;
            driftComboTimer = maxComboTime;
        }
        else if (isDrifting)
        {
            EndDrift();
        }
    }
    
    private void StartDrift()
    {
        isDrifting = true;
        driftScore = 0f;
        GameEvents.Instance?.Drifting();
        Debug.Log("Drift started!");
    }
    
    private void EndDrift()
    {
        isDrifting = false;
        Debug.Log($"Drift ended. Score: {driftScore}");
        
        // Award points
        if (driftScore > 100f)
        {
            int creditReward = (int)(driftScore / 10);
            GameManager.Instance.GetSaveSystem().AddCredits(creditReward);
            GameEvents.Instance?.CreditsGained(creditReward);
        }
        
        driftScore = 0f;
    }
    
    public float GetDriftScore() => driftScore;
    public bool IsDrifting() => isDrifting;
}
