using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class KneadGameManager : MonoBehaviour
{
    public GameObject dough;
    public GameObject lumpManager;
    public GameObject lumpPrefab;

    public GameObject meter;
    public GameObject startMenu;
    public GameObject winMenu;
    public GameObject flourIcon;
    public GameObject mittensIcon;
    public TMP_Text numLoafText;
    public TMP_Text numBreadText;
    public TMP_Text numNewCoinsText;
    public TMP_Text numTotalCoinsText;
    public Timer timer;

    // Bread Selection Screen
    public GameObject selectScreen;
    public GameObject[] breadTypes;
    public TMP_Text[] ingredientsText;
    private int selectedBread = 0;
    private int[,] ingredientsNeeded = new int[3, 4];

    private GameObject catPaw1;
    private GameObject catPaw2;

    private int numLumps;
    private int loavesKneaded = 0;
    private bool flourUpgradeOwned;

    public GameManager GM;

    //wen code
    public bool gameStarted = false;
    public bool gameEnded = false;
    public bool foldGame = false;

    public GameObject foldMenu;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindObjectOfType<GameManager>();
        numLumps = 40;
        meter.GetComponent<Slider>().maxValue = numLumps;
        lumpManager.GetComponent<LumpManager>().numLumps = numLumps;
        winMenu.SetActive(false);
        startMenu.SetActive(true);
        selectScreen.SetActive(false);

        //wen code
        foldMenu.SetActive(false);

        catPaw1 = GameObject.Find("catpaw1");
        catPaw2 = GameObject.Find("catpaw2");

        // Apply Flour Upgrade
        if (GM.boostsOwned.Substring(1, 1) == "t")
        {
            flourUpgradeOwned = true;
            flourIcon.SetActive(true);
        }

        // Apply Mittens Upgrade
        if (GM.boostsOwned.Substring(2, 1) == "t")
        {
            lumpManager.GetComponent<LumpManager>().changeSquishPerClick(-0.2f);
            mittensIcon.SetActive(true);
        }

        // Roll = 2 flour, 1 yeast
        // Croissant = 2 flour, 1 yeast, 1 cocoa
        // Pumpernickel = 1 flour, 1 yeast, 1 cocoa, 1 rye
        ingredientsNeeded = new int[,] { { 2,1,0,0 }, { 2,1,1,0 }, { 1,1,1,1 }  };
    }

    // Update is called once per frame
    void Update()
    {
        meter.GetComponent<Slider>().value = numLumps - lumpManager.GetComponent<LumpManager>().getLumpsRemaining();
    }

    public void StartGame()
    {
        if (canBakeBread())
        {
            selectScreen.SetActive(false);

            // Changing num lumps currently does nothing
            /*switch (selectedBread)
            {
                case 0:
                    numLumps = 40;
                    break;
                case 1:
                    numLumps = 50;
                    break;
                case 2:
                    numLumps = 60;
                    break;
            }*/

            //starts the kneading game
            gameStarted = true;
            GameManager.kneadTutorial = true;
        }
        else Debug.Log("Not Enough Ingredients!");
    }

    public void WinGame()
    {
        gameEnded = true;
        //Debug.Log("You Win");
        loavesKneaded++;
        winMenu.SetActive(true);
        if (loavesKneaded == 1)
        {
            numLoafText.text = "1 Loaf Kneaded!";
        }
        else numLoafText.text = loavesKneaded + " Loaves Kneaded!";

        string breadName = "";
        int breadsOwned = 0;
        switch (selectedBread)
        {
            case 0:
                breadName = "Dinner Roll";
                GM.numDinnerRoll++;
                breadsOwned = GM.numDinnerRoll;
                GM.inventory.RemoveItemNCnt(Item.ItemType.Flour, 2);
                GM.inventory.RemoveItemNCnt(Item.ItemType.Yeast, 1);
                break;
            case 1:
                breadName = "Croissant";
                GM.numCroissant++;
                breadsOwned = GM.numCroissant;
                GM.inventory.RemoveItemNCnt(Item.ItemType.Flour, 2);
                GM.inventory.RemoveItemNCnt(Item.ItemType.Yeast, 1);
                GM.inventory.RemoveItemNCnt(Item.ItemType.Cocoa_Powder, 1);
                break;
            case 2:
                breadName = "Pumpernickel";
                GM.numPumpernickel++;
                breadsOwned = GM.numPumpernickel;
                GM.inventory.RemoveItemNCnt(Item.ItemType.Flour, 1);
                GM.inventory.RemoveItemNCnt(Item.ItemType.Yeast, 1);
                GM.inventory.RemoveItemNCnt(Item.ItemType.Cocoa_Powder, 1);
                GM.inventory.RemoveItemNCnt(Item.ItemType.Rye_Flour, 1);
                break;
        }
        string plural = "s";
        if (breadsOwned == 1)
            plural = "";
        numBreadText.text = breadsOwned + " " + breadName + plural + " Owned!";

        string spaces = "     ";
        int seconds = Mathf.FloorToInt(timer.timeRemaining);
        int silverCoins = 625 / seconds;
        int bronzeCoins = 1000 / seconds;
        if (flourUpgradeOwned)
        {
            silverCoins = (int) (silverCoins * 1.2f);
            bronzeCoins = (int) (bronzeCoins * 1.2f);
        }
        numNewCoinsText.text = "0 G     " + silverCoins + " S" + spaces + bronzeCoins + " B";
        GM.giveCoins(0, silverCoins, bronzeCoins);
        int totalGold = GM.numGoldCoins;
        int totalSilver = GM.numSilverCoins;
        int totalBronze = GM.numBronzeCoins;
        numTotalCoinsText.text = totalGold + " G" + spaces + totalSilver + " S" + spaces + totalBronze + " B";

        GameManager.kneadTutorial = false;
    }

    public void Restart()
    {
        if (canBakeBread())
        {
            winMenu.SetActive(false);
            lumpManager.GetComponent<LumpManager>().NewLoaf();
            catPaw1.transform.position = new Vector3(catPaw1.transform.position.x, 1.19f, catPaw1.transform.position.z);
            catPaw2.transform.position = new Vector3(catPaw2.transform.position.x, 1.19f, catPaw2.transform.position.z);
        }
        else Debug.Log("Not Enough Ingredients!");
    }

    public void BackToTown()
    {
        SceneManager.LoadScene(GM.townToReturn());
    }

    public int getLoavesKneaded()
    {
        return loavesKneaded;
    }

    public void LeftButton()
    {
        GameObject currentButton = breadTypes[selectedBread];
        if (selectedBread == 0)
            selectedBread = breadTypes.Length - 1;
        else selectedBread--;
        GameObject newButton = breadTypes[selectedBread];
        currentButton.SetActive(false);
        newButton.SetActive(true);

        // Ingredient Display
        for (int i = 0; i < ingredientsText.Length; i++)
        {
            ingredientsText[i].text = ingredientsNeeded[selectedBread, i].ToString();
        }
    }

    public void RightButton()
    {
        GameObject currentButton = breadTypes[selectedBread];
        if (selectedBread == breadTypes.Length - 1)
            selectedBread = 0;
        else selectedBread++;
        GameObject newButton = breadTypes[selectedBread];
        currentButton.SetActive(false);
        newButton.SetActive(true);

        // Ingredient Display
        for (int i = 0; i < ingredientsText.Length; i++)
        {
            ingredientsText[i].text = ingredientsNeeded[selectedBread, i].ToString();
        }
    }

    public void openSelectScreen()
    {
        startMenu.SetActive(false);
        selectScreen.SetActive(true);
    }

    public bool canBakeBread()
    {
        switch (selectedBread)
        {
            case 0: 
                return (GM.inventory.CheckForItem(Item.ItemType.Flour, 2) && GM.inventory.CheckForItem(Item.ItemType.Yeast, 1));
            case 1: 
                return (GM.inventory.CheckForItem(Item.ItemType.Flour, 2) && GM.inventory.CheckForItem(Item.ItemType.Yeast, 1)
                    && GM.inventory.CheckForItem(Item.ItemType.Cocoa_Powder, 1));
            case 2: 
                return (GM.inventory.CheckForItem(Item.ItemType.Flour, 1) && GM.inventory.CheckForItem(Item.ItemType.Yeast, 1)
                    && GM.inventory.CheckForItem(Item.ItemType.Cocoa_Powder, 1) 
                    && GM.inventory.CheckForItem(Item.ItemType.Rye_Flour, 1));
        }

        return false;
    }

    // For testing the Kneading Minigame, remove for full release
    public void DebugKneading()
    {
        GM.inventory.AddItemNCnt(Item.ItemType.Flour, 9);
        GM.inventory.AddItemNCnt(Item.ItemType.Yeast, 9);
        GM.inventory.AddItemNCnt(Item.ItemType.Cocoa_Powder, 9);
        GM.inventory.AddItemNCnt(Item.ItemType.Rye_Flour, 9);
    }
}
