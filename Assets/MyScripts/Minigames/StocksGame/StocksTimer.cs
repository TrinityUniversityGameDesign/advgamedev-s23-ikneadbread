using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StocksTimer : MonoBehaviour
{
    public float timeRemaining = 20;
    public bool timeIsRunning = true;
    public TMP_Text timeText;
    public StockLineController controller;

    // Start is called before the first frame update
    void Start()
    {
        timeIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.gameStarted == true)
        {
            if (timeIsRunning)
            {
                if (timeRemaining >= 0)
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
                else controller.endGame();
            }
            if (controller.gameEnded == true)
            {
                timeIsRunning = false;
                DisplayTime(timeRemaining);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:0} : {1:00}", minutes, seconds);
    }
}
