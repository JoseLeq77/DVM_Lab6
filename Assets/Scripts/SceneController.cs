using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeDuration = 0.5f;

    private static SceneController instance;
    
    public static SceneController GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            if (fadePanel != null)
            {
                Color initialColor = fadePanel.color;
                initialColor.a = 0f; 
                fadePanel.color = initialColor;
                fadePanel.gameObject.SetActive(false);
            }
        }
    }

    public void FadeIn(System.Action onComplete = null)
    {
        StartCoroutine(FadeCoroutine(0f, 1f, onComplete));
    }

    public void FadeOut(System.Action onComplete = null)
    {
        StartCoroutine(FadeCoroutine(1f, 0f, onComplete));
    }

    private IEnumerator FadeCoroutine(float startAlpha, float targetAlpha, System.Action onComplete)
    {
        fadePanel.gameObject.SetActive(true);
        
        Color currentColor = fadePanel.color;
        currentColor.a = startAlpha;
        fadePanel.color = currentColor;
        
        float currentTransitionTime = 0f;
        
        while (currentTransitionTime < fadeDuration)
        {
            currentTransitionTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp(currentTransitionTime / fadeDuration, 0f, 1f);
            
            currentColor.a = Mathf.Lerp(startAlpha, targetAlpha, normalizedTime);
            fadePanel.color = currentColor;
            
            yield return null;
        }
        
        currentColor.a = targetAlpha;
        fadePanel.color = currentColor;
        
        if (targetAlpha == 0f)
        {
            fadePanel.gameObject.SetActive(false);
        }
        
        onComplete?.Invoke();
    }
}