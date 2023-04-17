using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenScript : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject EndScreen;
    public float totalTime = 60f;
    public TextMeshProUGUI timerText;


   
    private float timeRemaining;
    public bool isRunning = false;


    void Start()
    { 
        timeRemaining = totalTime;
        EndScreen.SetActive(false);
        StartScreen.SetActive(true);
        StartTimer();

    }

    void Update()
    {
        if (isRunning)
        {
            EndScreen.SetActive(false);
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();

            if (timeRemaining <= 10f)
            {
                timerText.color = Color.red;
            }

            if (timeRemaining <= 0f)
            {
                isRunning = false;                

            }
        } else
        {
            EnableEnd();
        }
 
    }

    public void StartTimer()
    {
        isRunning = true;        
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void DisableStart()
    {
        StartScreen.SetActive(false);
      
    }

    public void EnableEnd()
    {
        EndScreen.SetActive(true);
    }

}
