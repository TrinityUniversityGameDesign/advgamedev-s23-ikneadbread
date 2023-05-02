using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GroceryTimer : MonoBehaviour
{
    public float timeRemaining = 30;
    public bool timeIsRunning = true;
    public TMP_Text timeText;
    public GameObject controller;

    // Start is called before the first frame update
    void Start()
    {
        timeIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.GetComponent<ScreenScript>().gameStarted == true)
        {
            if (timeIsRunning)
            {
                if (timeRemaining <= 10)
                {
                    timeText.color = Color.red;
                }
                if (timeRemaining >= 0)
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
                else controller.GetComponent<ScreenScript>().endGame();
            }
            if (controller.GetComponent<ScreenScript>().gameEnded == true)
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
