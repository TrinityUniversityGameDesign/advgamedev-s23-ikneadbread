using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScreenScript : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject EndScreen;

   private BasketMovement bm;
   private GameManager gm;

    public TextMeshProUGUI caughtScores;
    public float totalTime = 60f;
    public TextMeshProUGUI timerText;
    private float timeRemaining;
    public bool isRunning = false;

    public bool gameStarted = false;
    public bool gameEnded = false;

    void Start()
    { 
        StartScreen.SetActive(false);
        EndScreen.SetActive(false);
        bm = GameObject.FindObjectOfType<BasketMovement>();
        gm = GameObject.FindObjectOfType<GameManager>();

        gameStarted = true;
    }

    void Update()
    {
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

    public void endGame()
    {
        isRunning = false;
        EndScreen.SetActive(true);
        caughtScores.text = "Chocolate Caught: " + bm.chocCount + "\nRye Caught: " + bm.ryeCount;
    }
}
