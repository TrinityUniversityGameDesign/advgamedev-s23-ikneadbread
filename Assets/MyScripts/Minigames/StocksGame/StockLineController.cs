using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StockLineController : MonoBehaviour
{
    public GameObject startButton;
    public GameObject profitBar;
    public GameObject lossBar;
    public GameObject endScreen;
    public TMP_Text endText;
    public StocksTimer timer;

    public bool gameStarted = false;
    public bool gameEnded = false;

    private Rigidbody rb;

    private float speed = 2;
    private bool growing = false;
    private float startingValue = 20;
    private float growthRate = 0.25f;
    private float shrinkRate = -0.25f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        profitBar.GetComponent<Slider>().value = startingValue;
    }

    public void StartMoving()
    {
        rb.velocity = new Vector2(speed, speed);
        profitBar.GetComponent<Slider>().value = startingValue;
        gameStarted = true;
        growing = true;
        startButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted && !gameEnded)
        {
            float vertical = Input.GetAxis("Vertical");
            if (vertical > 0)
            {
                growing = true;
                rb.velocity = new Vector2(speed, speed);
            }
            else if (vertical < 0)
            {
                growing = false;
                rb.velocity = new Vector2(speed, -speed);
            }

            if (lossBar.GetComponent<Slider>().value == 100)
            {
                Debug.Log("You Lose");
                endGame();
            }
        }
    }
    
    void FixedUpdate()
    {
        if (gameStarted && !gameEnded)
        {
            if (growing)
            {
                addStockValue(growthRate);
            }
            else
            {
                addStockValue(shrinkRate);
            }
        }
    }

    // Called from StocksTimer
    public void endGame()
    {
        gameEnded = true;
        endScreen.SetActive(true);
        rb.velocity = Vector2.zero;
        if (lossBar.GetComponent<Slider>().value > 0)
        {
            endText.text = "Stock Sold at a Loss :(";
            // Reward less currency and ingredients
        }
        else
        {
            // Reward currency and ingredients
        }
    }

    public void addStockValue(float value)
    {
        if (lossBar.GetComponent<Slider>().value <= 0)
            profitBar.GetComponent<Slider>().value += value;
        if (profitBar.GetComponent<Slider>().value <= 0)
            lossBar.GetComponent<Slider>().value -= value;
    }

    public void obstacleHit(int obstacleType)
    {
        switch (obstacleType)
        {
            case 1:
                addStockValue(-10);
                break;
            case 2:
                shrinkRate -= 0.25f;
                break;
            case 3:
                break;
        }
        Debug.Log("Obstacle Hit!");
    }

    public void Restart()
    {
        endScreen.SetActive(false);
        gameEnded = false;
        timer.timeRemaining = 20;
        timer.timeIsRunning = true;
        lossBar.GetComponent<Slider>().value = 0;
        profitBar.GetComponent<Slider>().value = startingValue;
        StartMoving();
    }

    public void BackToTown()
    {
        SceneManager.LoadScene("CityTime");
    }
}
