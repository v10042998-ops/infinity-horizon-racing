using UnityEngine;
using UnityEngine.SceneManagement;

public class Startup : MonoBehaviour
{
    private void Awake()
    {
        // Initialize game systems
        if (GameManager.Instance == null)
        {
            GameObject gameManagerObj = new GameObject("GameManager");
            gameManagerObj.AddComponent<GameManager>();
            gameManagerObj.AddComponent<SaveSystem>();
            gameManagerObj.AddComponent<AudioManager>();
        }
        
        // Load main menu
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
