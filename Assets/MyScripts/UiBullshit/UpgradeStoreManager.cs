using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UpgradeStoreManager : MonoBehaviour
{
    public GameObject storePage;
    public GameObject boostsPanel;
    public GameObject accessoriesPanel;
    public GameObject ticketsPanel;
    public GameObject confirmPanel;

    public string[] boostNameDesc;
    public int[] boostPrices;
    public string[] accessoryNameDesc;
    public int[] accessoryPrices;
    public string[] ticketNameDesc;
    public int[] ticketPrices;

    public TMP_Dropdown category;
    public TMP_Text upgradeNameText;
    public TMP_Text upgradeDescText;
    public TMP_Text confirmPanelUpgrade;
    public GameObject confirmPurchaseButton;

    public TMP_Text goldDisplay;
    public TMP_Text silverDisplay;
    public TMP_Text bronzeDisplay;

    public GameManager GM;

    private char activeCategory = 'b';
    private GameObject upgradeInCart;
    private int upgradeIndex;
    private int selectedPrice;
    private GameObject catDisplay;

    private string boostsOwned;
    private string accessoriesOwned;
    private string ticketsOwned;


    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindObjectOfType<GameManager>();
        catDisplay = GameObject.Find("CatDisplay");
        upgradeInCart = GameObject.Find("Boost (1)");
        upgradeIndex = 0;
        activeCategory = 'b';
        upgradeNameText.text = boostNameDesc[0];
        upgradeDescText.text = boostNameDesc[1];

        boostsOwned = PlayerPrefs.GetString("boostsOwned");
        accessoriesOwned = PlayerPrefs.GetString("accessoriesOwned");
        ticketsOwned = PlayerPrefs.GetString("ticketsOwned");

        goldDisplay.text = GM.numGoldCoins.ToString();
        silverDisplay.text = GM.numSilverCoins.ToString();
        bronzeDisplay.text = GM.numBronzeCoins.ToString();

        // Deactivate buttons for purchased upgrades
        for (int i = 0; i < 5; i++)
        {
            char isOwned = boostsOwned[i];
            if (isOwned == 't')
                boostsPanel.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = false;
            isOwned = accessoriesOwned[i];
            if (isOwned == 't')
                accessoriesPanel.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = false;
            if (i < ticketsOwned.Length)
            {
                isOwned = ticketsOwned[i];
                if (isOwned == 't')
                    ticketsPanel.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = false;
            }
        }
        Debug.Log(boostsOwned);
        Debug.Log(accessoriesOwned);
        Debug.Log(ticketsOwned);
        Debug.Log("");
    }

    // Update is called once per frame
    void Update()
    {
        catDisplay.transform.Rotate(0, 1, 0);
    }

    public void changeProducts()
    {
        Debug.Log(GM);
        boostsPanel.SetActive(false);
        accessoriesPanel.SetActive(false);
        ticketsPanel.SetActive(false);

        switch (category.value)
        {
            case 0:
                boostsPanel.SetActive(true);
                activeCategory = 'b';
                break;
            case 1:
                accessoriesPanel.SetActive(true);
                activeCategory = 'a';
                break;
            case 2:
                ticketsPanel.SetActive(true);
                activeCategory = 't';
                break;
        }
    }

    public void selectUpgrade(GameObject upgrade)
    {
        upgradeInCart = upgrade;
        int index = upgrade.name.IndexOf('(') + 1;
        upgradeIndex = (int) char.GetNumericValue(upgrade.name[index]) - 1;
        int upgradeNum = (upgradeIndex) * 2;

        switch (activeCategory)
        {
            case 'b':
                upgradeNameText.text = boostNameDesc[upgradeNum];
                upgradeDescText.text = boostNameDesc[upgradeNum + 1];
                selectedPrice = boostPrices[upgradeIndex];
                break;
            case 'a':
                upgradeNameText.text = accessoryNameDesc[upgradeNum];
                upgradeDescText.text = accessoryNameDesc[upgradeNum + 1];
                selectedPrice = accessoryPrices[upgradeIndex];
                break;
            case 't':
                upgradeNameText.text = ticketNameDesc[upgradeNum];
                upgradeDescText.text = ticketNameDesc[upgradeNum + 1];
                selectedPrice = ticketPrices[upgradeIndex];
                break;
        }
    }

    public void openConfirmPanel()
    {
        confirmPanel.SetActive(true);
        catDisplay.SetActive(false);
        if (canBuy())
        {
            confirmPanelUpgrade.text = "Are you sure you want to buy " + upgradeNameText.text + "?";
            confirmPurchaseButton.SetActive(true);
        }
        else
        {
            confirmPanelUpgrade.text = "Not enough money!";
            confirmPurchaseButton.SetActive(false);
        }
    }

    public bool canBuy()
    {
        switch (activeCategory)
        {
            case 'b':
                return (GM.numSilverCoins >= selectedPrice);
            case 'a':
                return (GM.numBronzeCoins >= selectedPrice);
            case 't':
                return (GM.numGoldCoins >= selectedPrice);
        }
        return false;
    }

    public void cancelPurchase()
    {
        confirmPanel.SetActive(false);
        catDisplay.SetActive(true);
    }

    public void purchaseUpgrade()
    {
        confirmPanel.SetActive(false);
        catDisplay.SetActive(true);
        upgradeInCart.GetComponent<Button>().interactable = false;

        // Maybe use events to implement upgrades
        switch (upgradeInCart.name)
        {
            case "Boost (1)":
                GM.boost1.Invoke();
                break;
            case "Accessory (1)":
                GM.accessory1.Invoke();
                break;
            case "Accessory (2)":
                GM.accessory2.Invoke();
                break;
            case "Accessory (3)":
                GM.accessory3.Invoke();
                break;
            case "Accessory (4)":
                GM.accessory4.Invoke();
                break;
            case "Accessory (5)":
                GM.accessory5.Invoke();
                break;
            case "Ticket (1)":
                break;
            case "Ticket (2)":
                break;
        }

        // Mark upgrade as permanently owned
        switch (activeCategory)
        {
            case 'b':
                string updatedBoosts = boostsOwned.Substring(0,upgradeIndex) + 't' + boostsOwned.Substring(upgradeIndex + 1);
                boostsOwned = updatedBoosts;
                PlayerPrefs.SetString("boostsOwned", updatedBoosts);
                GM.boostsOwned = updatedBoosts;
                GM.giveCoins(0, -selectedPrice, 0);
                break;
            case 'a':
                string updatedAccs = accessoriesOwned.Substring(0, upgradeIndex) + 't' + 
                    accessoriesOwned.Substring(upgradeIndex + 1);
                accessoriesOwned = updatedAccs;
                PlayerPrefs.SetString("accessoriesOwned", updatedAccs);
                GM.accessoriesOwned = updatedAccs;
                GM.giveCoins(0, 0, -selectedPrice);
                break;
            case 't':
                string updatedTickets = ticketsOwned.Substring(0, upgradeIndex) + 't' + 
                    ticketsOwned.Substring(upgradeIndex + 1);
                ticketsOwned = updatedTickets;
                PlayerPrefs.SetString("ticketsOwned", updatedTickets);
                if (GM) GM.ticketsOwned = updatedTickets;
                GM.giveCoins(-selectedPrice, 0, 0);
                break;
        }

        goldDisplay.text = GM.numGoldCoins.ToString();
        silverDisplay.text = GM.numSilverCoins.ToString();
        bronzeDisplay.text = GM.numBronzeCoins.ToString();

        Debug.Log(boostsOwned);
        Debug.Log(accessoriesOwned);
        Debug.Log(ticketsOwned);
        Debug.Log("");
    }

    public void backToGame()
    {
        SceneManager.LoadScene("CityTime");
    }
}
