using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1f;
    
    private void Start()
    {
        // Create fade canvas if it doesn't exist
        if (fadeCanvasGroup == null)
        {
            GameObject fadeObj = new GameObject("FadeCanvas");
            Canvas canvas = fadeObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            
            CanvasScaler scaler = fadeObj.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            
            Image image = fadeObj.AddComponent<Image>();
            image.color = Color.black;
            
            fadeCanvasGroup = fadeObj.AddComponent<CanvasGroup>();
            fadeCanvasGroup.alpha = 0f;
        }
    }
    
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneWithFade(sceneName));
    }
    
    private IEnumerator LoadSceneWithFade(string sceneName)
    {
        // Fade out
        yield return StartCoroutine(FadeTo(1f));
        
        // Load scene
        SceneManager.LoadScene(sceneName);
        
        // Wait for scene to load
        yield return new WaitForSeconds(0.5f);
        
        // Fade in
        yield return StartCoroutine(FadeTo(0f));
    }
    
    private IEnumerator FadeTo(float targetAlpha)
    {
        float elapsed = 0f;
        float startAlpha = fadeCanvasGroup.alpha;
        
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            yield return null;
        }
        
        fadeCanvasGroup.alpha = targetAlpha;
    }
}
