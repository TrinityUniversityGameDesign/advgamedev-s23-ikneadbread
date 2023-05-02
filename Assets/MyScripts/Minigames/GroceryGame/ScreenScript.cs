using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenScript : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject EndScreen;
    public GameObject cocoaIcon;
    public GameObject ryeIcon;

    private BasketMovement bm;
    private GameManager GM;

    private int flourCaught = 0;
    private int yeastCaught = 0;
    private int specialCaught = 0;
    private Inventory inventory;

    public TextMeshProUGUI caughtScores;

    public bool gameStarted = false;
    public bool gameEnded = false;

    void Start()
    { 
        StartScreen.SetActive(true);
        EndScreen.SetActive(false);
        bm = GameObject.FindObjectOfType<BasketMovement>();
        GM = GameObject.FindObjectOfType<GameManager>();
        inventory = GM.inventory;
    }

    void Update()
    {
    }

    public void startTimer()
    {
        gameStarted = true;
        StartScreen.SetActive(false);
    }

    public void endGame()
    {
        gameEnded = true;
        EndScreen.SetActive(true);
        calculateRewards();
        string world = GM.lastScene.ToString();

        // Add World-Specific Ingredients to Inventory, Show Rewards Text
        switch (world)
        {
            case "NewHomeTown":
                inventory.AddItemNCnt(Item.ItemType.Flour, specialCaught);
                caughtScores.text = "Flour Caught: " + (flourCaught + specialCaught) + "\nYeast Caught: " + yeastCaught;
                cocoaIcon.SetActive(false);
                ryeIcon.SetActive(false);
                break;
            case "Forest":
                inventory.AddItemNCnt(Item.ItemType.Cocoa_Powder, specialCaught);
                caughtScores.text = "Flour Caught: " + flourCaught + "\nYeast Caught: " + yeastCaught
                    + "\nCocoa Powder Caught: " + specialCaught;
                cocoaIcon.SetActive(true);
                ryeIcon.SetActive(false);
                break;
            case "Egypt":
                inventory.AddItemNCnt(Item.ItemType.Rye_Flour, specialCaught);
                caughtScores.text = "Flour Caught: " + flourCaught + "\nYeast Caught: " + yeastCaught
                    + "\nRye Flour Caught: " + specialCaught;
                cocoaIcon.SetActive(false);
                ryeIcon.SetActive(true);
                break;
        }
    }

    public void calculateRewards()
    {
        int score = bm.ingrCount;

        int count = 1;
        // Reward Flour, then Yeast, then Special Ingredient
        for (int i = 1; i <= score; i++)
        {
            switch (count)
            {
                case 1:
                    flourCaught++;
                    break;
                case 2:
                    yeastCaught++;
                    break;
                case 3:
                    specialCaught++;
                    break;
            }
            count++;
            if (count > 3) count = 1;
        }

        // Add Basic Ingredients to Inventory
        inventory.AddItemNCnt(Item.ItemType.Flour, flourCaught);
        inventory.AddItemNCnt(Item.ItemType.Yeast, yeastCaught);
    }
}
