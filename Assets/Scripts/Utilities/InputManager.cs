using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    
    // Input mappings
    private float horizontalInput = 0f;
    private float verticalInput = 0f;
    private bool brakeInput = false;
    private bool handbrakeInput = false;
    private bool pauseInput = false;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    private void Update()
    {
        ReadInputs();
    }
    
    private void ReadInputs()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            ReadAndroidInputs();
        }
        else
        {
            ReadPCInputs();
        }
    }
    
    private void ReadPCInputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        brakeInput = Input.GetKey(KeyCode.Space);
        handbrakeInput = Input.GetKey(KeyCode.E);
        pauseInput = Input.GetKeyDown(KeyCode.Escape);
    }
    
    private void ReadAndroidInputs()
    {
        // Touch-based input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float screenX = touch.position.x / Screen.width;
            horizontalInput = (screenX * 2f) - 1f;
            horizontalInput = Mathf.Clamp(horizontalInput, -1f, 1f);
        }
        
        verticalInput = 0.8f; // Default acceleration
        brakeInput = Input.GetKey(KeyCode.B);
        handbrakeInput = Input.GetKey(KeyCode.H);
        pauseInput = Input.GetKeyDown(KeyCode.Escape);
    }
    
    public float GetHorizontalInput() => horizontalInput;
    public float GetVerticalInput() => verticalInput;
    public bool GetBrakeInput() => brakeInput;
    public bool GetHandbrakeInput() => handbrakeInput;
    public bool GetPauseInput() => pauseInput;
}
