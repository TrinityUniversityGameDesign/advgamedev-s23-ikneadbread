using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraFader : MonoBehaviour
{
    public float fadeDuration = 1.0f;  // The duration of the fade in seconds
    public Image fadeImage;  // The UI image used to fade the screen

    private bool fadingIn = false;  // Whether the camera is currently fading in
    private bool fadingOut = false;  // Whether the camera is currently fading out

    void Start()
    {
        fadeImage.gameObject.SetActive(true);  // Enable the fade image
        fadeImage.color = Color.clear;  // Make the fade image completely transparent
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  // Replace KeyCode.Space with the button you want to use
        {
            if (!fadingIn && !fadingOut)  // If the camera is not currently fading
            {
                StartCoroutine(FadeIn());  // Start fading the camera in
            }
            else if (fadingIn)  // If the camera is currently fading in
            {
                StopAllCoroutines();  // Stop the fade-in coroutine
                StartCoroutine(FadeOut());  // Start fading the camera out
            }
            else if (fadingOut)  // If the camera is currently fading out
            {
                StopAllCoroutines();  // Stop the fade-out coroutine
                StartCoroutine(FadeIn());  // Start fading the camera in
            }
        }
    }

    IEnumerator FadeIn()
    {
        fadingIn = true;
        fadeImage.gameObject.SetActive(true);  // Enable the fade image
        fadeImage.CrossFadeAlpha(1.0f, fadeDuration, false);  // Fade the image to fully opaque
        yield return new WaitForSeconds(fadeDuration);  // Wait for the fade to complete
        fadeImage.gameObject.SetActive(false);  // Disable the fade image
        fadingIn = false;
    }

    IEnumerator FadeOut()
    {
        fadingOut = true;
        fadeImage.gameObject.SetActive(true);  // Enable the fade image
        fadeImage.CrossFadeAlpha(0.0f, fadeDuration, false);  // Fade the image to fully transparent
        yield return new WaitForSeconds(fadeDuration);  // Wait for the fade to complete
        fadeImage.gameObject.SetActive(false);  // Disable the fade image
        fadingOut = false;
    }
}
