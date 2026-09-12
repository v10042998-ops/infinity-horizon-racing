using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetVehicle;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10);
    [SerializeField] private float followDistance = 10f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private bool useThirdPerson = true;
    [SerializeField] private bool useHeadBobbing = true;
    [SerializeField] private float headBobbAmount = 0.1f;
    
    private float originalYOffset;
    private Rigidbody targetRB;
    
    private void Start()
    {
        if (targetVehicle == null)
            targetVehicle = FindObjectOfType<VehicleController>()?.transform;
        
        if (targetVehicle != null)
            targetRB = targetVehicle.GetComponent<Rigidbody>();
        
        originalYOffset = offset.y;
    }
    
    private void LateUpdate()
    {
        if (targetVehicle == null)
            return;
        
        if (useThirdPerson)
            UpdateThirdPersonCamera();
        else
            UpdateFirstPersonCamera();
    }
    
    private void UpdateThirdPersonCamera()
    {
        // Calculate desired position
        Vector3 desiredPosition = targetVehicle.position + targetVehicle.TransformDirection(offset);
        
        // Apply head bobbing based on speed
        if (useHeadBobbing && targetRB != null)
        {
            float speed = targetRB.velocity.magnitude;
            float bobAmount = Mathf.Sin(Time.time * speed * 0.5f) * headBobbAmount;
            desiredPosition.y += bobAmount;
        }
        
        // Smooth camera movement
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);
        
        // Look at vehicle
        transform.LookAt(targetVehicle.position + Vector3.up * 1.5f);
    }
    
    private void UpdateFirstPersonCamera()
    {
        // Position camera at vehicle height
        transform.position = targetVehicle.position + Vector3.up * 2f;
        transform.rotation = targetVehicle.rotation;
    }
    
    public void SetCameraMode(bool thirdPerson)
    {
        useThirdPerson = thirdPerson;
    }
}
