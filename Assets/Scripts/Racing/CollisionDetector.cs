using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private float damageMultiplier = 1f;
    [SerializeField] private ParticleSystem impactParticles;
    
    private VehicleController vehicleController;
    private float vehicleHealth = 100f;
    private float maxHealth = 100f;
    
    private void Start()
    {
        vehicleController = GetComponent<VehicleController>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;
        
        if (impactForce > 5f)
        {
            HandleCollision(collision.contacts[0].point, impactForce);
        }
    }
    
    private void HandleCollision(Vector3 contactPoint, float force)
    {
        float damage = force * damageMultiplier;
        vehicleHealth -= damage;
        
        // Spawn impact particles
        if (impactParticles != null)
        {
            Instantiate(impactParticles, contactPoint, Quaternion.identity);
        }
        
        // Trigger events
        GameEvents.Instance?.Collision(contactPoint);
        GameEvents.Instance?.Crashed();
        
        Debug.Log($"Collision! Damage: {damage}, Health: {vehicleHealth}");
        
        if (vehicleHealth <= 0)
        {
            DestroyVehicle();
        }
    }
    
    private void DestroyVehicle()
    {
        Debug.Log("Vehicle destroyed!");
        // TODO: Show game over screen
    }
    
    public float GetHealthPercent() => vehicleHealth / maxHealth;
    public float GetHealth() => vehicleHealth;
}
