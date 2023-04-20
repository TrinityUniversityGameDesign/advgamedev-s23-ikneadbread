using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using static UnityEditor.VersionControl.Asset;

public class StockLineController : MonoBehaviour
{
    public GameObject startButton;
    public GameObject profitBar;
    public GameObject lossBar;
    public GameObject endScreen;
    public TMP_Text endText;
    public TMP_Text numNewCoinsText;
    public TMP_Text numTotalCoinsText;
    public StocksTimer timer;

    public bool gameStarted = false;
    public bool gameEnded = false;

    public GameManager GM;

    private Rigidbody rb;

    private float speed = 2;
    private bool growing = false;
    private float startingValue = 0;
    private float growthRate = 0.25f;
    private float shrinkRate = -0.25f;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
        profitBar.GetComponent<Slider>().value = startingValue;

        // Apply Bank Account Upgrade
        if (GM.boostsOwned.Substring(4) == "t")
        {
            profitBar.GetComponent<Slider>().maxValue = 200;
            lossBar.GetComponent<Slider>().maxValue = 200;
        }
    }

    public void StartMoving()
    {
        Debug.Log(GM.boostsOwned);
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
        string spaces = "       ";
        if (lossBar.GetComponent<Slider>().value > 0)
        {
            endText.text = "Stock Sold at a Loss :(";
            numNewCoinsText.text = "0" + spaces + "0" + spaces + "0";
        }
        else
        {
            // Reward currency
            endText.text = "Successful Cash Out!";
            float profit = profitBar.GetComponent<Slider>().value;
            int goldCoins = (int) profit / 3;
            int silverCoins = (int) profit / 2;
            int bronzeCoins = (int) profit;
            numNewCoinsText.text = goldCoins + spaces + silverCoins + spaces + bronzeCoins;
            GM.giveCoins(goldCoins, silverCoins, bronzeCoins);
        }
        int totalGold = GM.numGoldCoins;
        int totalSilver = GM.numSilverCoins;
        int totalBronze = GM.numBronzeCoins;
        numTotalCoinsText.text = totalGold + spaces + totalSilver + spaces + totalBronze;
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
                addStockValue(-35);
                break;
            case 2:
                shrinkRate -= 0.25f;
                break;
            case 3:
                addStockValue(15);
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
        shrinkRate = -0.25f;
        StartMoving();
    }

    public void BackToTown()
    {
        SceneManager.LoadScene("CityTime");
    }
}
