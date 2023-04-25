using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    // Reference to the UI element to fade
    public CanvasGroup uiElement;

    // Duration of the fade effect
    public float fadeDuration = 1f;

    // Delay before starting the fade effect
    public float delay = 1f;

    // Duration of time the UI element will be displayed
    public float displayDuration = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // Set the UI element's alpha to 0
        uiElement.alpha = 0;

        // Start the fade coroutine after the specified delay
        StartCoroutine(FadeCoroutine());
    }

    IEnumerator FadeCoroutine()
    {
        // Wait for the specified delay before starting the fade effect
        yield return new WaitForSeconds(delay);

        // Fade the UI element in
        StartCoroutine(Fade(uiElement, 0f, 1f, fadeDuration));

        // Wait for the fade effect to complete
        yield return new WaitForSeconds(fadeDuration);

        // Wait for the specified display duration
        yield return new WaitForSeconds(displayDuration);

        // Fade the UI element out
        StartCoroutine(Fade(uiElement, 1f, 0f, fadeDuration));
    }

    IEnumerator Fade(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        // Set the UI element's starting alpha
        canvasGroup.alpha = startAlpha;

        // Calculate the rate at which the alpha should change each frame
        float alphaChangePerFrame = (endAlpha - startAlpha) / duration * Time.deltaTime;

        // Loop until the alpha reaches the target value
        while (Mathf.Abs(canvasGroup.alpha - endAlpha) > 0.01f)
        {
            // Update the UI element's alpha
            canvasGroup.alpha += alphaChangePerFrame;

            // Wait for the next frame
            yield return null;
        }

        // Set the UI element's alpha to the target value
        canvasGroup.alpha = endAlpha;
    }
}
