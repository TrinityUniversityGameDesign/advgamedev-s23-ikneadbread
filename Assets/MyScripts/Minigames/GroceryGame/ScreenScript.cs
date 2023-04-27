using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScreenScript : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject EndScreen;

   private BasketMovement bm;

    public TextMeshProUGUI caughtScores;
    public float totalTime = 60f;
    public TextMeshProUGUI timerText;
    private float timeRemaining;
    public bool isRunning = false;


    void Start()
    { 
        
        timeRemaining = totalTime;
        StartTimer();
        StartScreen.SetActive(false);
        EndScreen.SetActive(false);
        bm = GameObject.FindObjectOfType<BasketMovement>();
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
                EndScreen.SetActive(true); 
                caughtScores.text = "Chocolate Caught: " +  bm.chocCount + "\nRye Caught: " + bm.ryeCount;          

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




}
