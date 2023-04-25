using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenScript : MonoBehaviour
{
    public GameObject StartScreen;
  
    public float totalTime = 60f;
    public TextMeshProUGUI timerText;
    private float timeRemaining;
    public bool isRunning = false;


    void Start()
    { 
        timeRemaining = totalTime;
      
        StartScreen.SetActive(false);
        StartTimer();

    }

    void Update()
    {
        if (isRunning)
        {
            
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


}
