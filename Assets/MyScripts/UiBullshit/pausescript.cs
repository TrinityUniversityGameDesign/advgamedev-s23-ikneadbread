using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausescript : MonoBehaviour
{
    bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isPaused)
        {
            isPaused = !isPaused;
            SceneManager.LoadScene("IntroStoryScene",LoadSceneMode.Additive);
        }
        else if (Input.GetKeyDown(KeyCode.P) && isPaused)
        {
            isPaused = !isPaused;
            SceneManager.UnloadScene("IntroStoryScene");
        }
    }
}
