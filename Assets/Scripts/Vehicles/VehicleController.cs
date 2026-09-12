using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Vehicle Properties")]
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private float acceleration = 25f;
    [SerializeField] private float brakeForce = 30f;
    [SerializeField] private float steerSensitivity = 1.5f;
    [SerializeField] private float dragCoefficient = 0.1f;
    
    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider[] wheelColliders = new WheelCollider[4];
    [SerializeField] private Transform[] wheelMeshes = new Transform[4];
    
    [Header("Physics")]
    [SerializeField] private float mass = 1500f;
    [SerializeField] private Vector3 centerOfMass = Vector3.zero;
    [SerializeField] private float suspensionStiffness = 35000f;
    [SerializeField] private float suspensionDamping = 4500f;
    
    private Rigidbody rb;
    private float currentSpeed = 0f;
    private float currentSteer = 0f;
    private float motorPower = 0f;
    private bool isBraking = false;
    private bool isHandbraking = false;
    private float wheelRPM = 0f;
    
    // Control input
    private float inputAccelerate = 0f;
    private float inputSteer = 0f;
    private float inputBrake = 0f;
    private float inputHandbrake = 0f;
    
    // Engine system
    private float engineRPM = 0f;
    private float engineMaxRPM = 7000f;
    private float engineMinRPM = 800f;
    private float gear = 0f; // 0=reverse, 1=1st, etc.
    private float maxGears = 6f;
    
    // Audio
    private AudioSource engineAudio;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = mass;
        rb.centerOfMass = centerOfMass;
        
        engineAudio = GetComponent<AudioSource>();
        if (engineAudio == null)
            engineAudio = gameObject.AddComponent<AudioSource>();
        
        // Configure wheel colliders
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            WheelFrictionCurve friction = wheelColliders[i].forwardFriction;
            friction.stiffness = 1f;
            wheelColliders[i].forwardFriction = friction;
            
            WheelFrictionCurve sideFriction = wheelColliders[i].sidewaysFriction;
            sideFriction.stiffness = 1f;
            wheelColliders[i].sidewaysFriction = sideFriction;
            
            JointSpring spring = wheelColliders[i].suspension;
            spring.spring = suspensionStiffness;
            spring.damper = suspensionDamping;
            wheelColliders[i].suspension = spring;
        }
    }
    
    private void Update()
    {
        GetInput();
        UpdateEngineAudio();
    }
    
    private void FixedUpdate()
    {
        ApplyMotor();
        ApplyBrake();
        ApplySteering();
        UpdateWheelMeshes();
        UpdateSpeedometer();
    }
    
    private void GetInput()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            // Android touch input
            GetAndroidInput();
        }
        else
        {
            // PC keyboard/controller input
            inputSteer = Input.GetAxis("Horizontal");
            inputAccelerate = Input.GetAxis("Vertical");
            inputBrake = Input.GetKey(KeyCode.Space) ? 1f : 0f;
            inputHandbrake = Input.GetKey(KeyCode.E) ? 1f : 0f;
        }
    }
    
    private void GetAndroidInput()
    {
        // Simple touch input for Android
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            // Normalized screen position (-1 to 1)
            float screenWidth = Screen.width;
            inputSteer = (touch.position.x / screenWidth) * 2f - 1f;
        }
        
        inputAccelerate = 0.8f; // Default acceleration
        inputBrake = Input.GetKey(KeyCode.B) ? 1f : 0f;
        inputHandbrake = Input.GetKey(KeyCode.H) ? 1f : 0f;
    }
    
    private void ApplyMotor()
    {
        currentSpeed = rb.velocity.magnitude;
        
        if (Mathf.Abs(inputAccelerate) > 0.1f)
        {
            motorPower = inputAccelerate * acceleration;
            if (currentSpeed < maxSpeed)
            {
                gear = Mathf.Clamp(currentSpeed / (maxSpeed / maxGears), 1f, maxGears);
                engineRPM = engineMinRPM + (gear / maxGears) * (engineMaxRPM - engineMinRPM);
            }
            else
            {
                engineRPM = engineMaxRPM;
            }
        }
        else
        {
            motorPower = 0f;
            engineRPM = Mathf.Lerp(engineRPM, engineMinRPM, Time.deltaTime * 2f);
        }
        
        // Apply motor torque to wheels
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            if (i >= 2) // Rear wheels
                wheelColliders[i].motorTorque = motorPower;
            else // Front wheels
                wheelColliders[i].motorTorque = motorPower * 0.3f; // Front-wheel drive assist
        }
        
        // Apply drag
        rb.drag = dragCoefficient * currentSpeed * 0.01f;
    }
    
    private void ApplyBrake()
    {
        isBraking = inputBrake > 0.1f;
        float brakeTorque = isBraking ? brakeForce : 0f;
        
        if (isHandbraking)
        {
            // Handbrake only affects rear wheels
            wheelColliders[2].brakeTorque = inputHandbrake * brakeForce * 1.5f;
            wheelColliders[3].brakeTorque = inputHandbrake * brakeForce * 1.5f;
        }
        else
        {
            for (int i = 0; i < wheelColliders.Length; i++)
            {
                wheelColliders[i].brakeTorque = brakeTorque;
            }
        }
        
        isHandbraking = inputHandbrake > 0.1f;
    }
    
    private void ApplySteering()
    {
        currentSteer = Mathf.Clamp(inputSteer * steerSensitivity, -1f, 1f);
        
        for (int i = 0; i < 2; i++) // Front wheels only
        {
            wheelColliders[i].steerAngle = currentSteer * 30f; // Max 30 degrees
        }
    }
    
    private void UpdateWheelMeshes()
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            if (wheelMeshes[i] != null)
            {
                Vector3 position;
                Quaternion rotation;
                wheelColliders[i].GetWorldPose(out position, out rotation);
                wheelMeshes[i].position = position;
                wheelMeshes[i].rotation = rotation;
            }
        }
    }
    
    private void UpdateSpeedometer()
    {
        wheelRPM = currentSpeed * 60f / (2f * Mathf.PI * 0.34f); // Approximate wheel circumference
    }
    
    private void UpdateEngineAudio()
    {
        if (engineAudio != null)
        {
            float pitchFactor = engineRPM / engineMaxRPM;
            engineAudio.pitch = 0.5f + pitchFactor * 1.5f;
            engineAudio.volume = 0.3f + pitchFactor * 0.5f;
        }
    }
    
    public float GetCurrentSpeed() => currentSpeed;
    public float GetEngineRPM() => engineRPM;
    public float GetGear() => gear;
    public bool GetIsBraking() => isBraking;
    public bool GetIsHandbraking() => isHandbraking;
}
