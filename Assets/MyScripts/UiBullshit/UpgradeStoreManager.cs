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
    public string[] accessoryNameDesc;
    public string[] ticketNameDesc;

    public TMP_Dropdown category;
    public TMP_Text upgradeNameText;
    public TMP_Text upgradeDescText;
    public TMP_Text confirmPanelUpgrade;

    private int goldCoins;
    private int silverCoins;
    private int bronzeCoins;

    private char activeCategory = 'b';
    private GameObject upgradeInCart;
    private int upgradeIndex;
    private GameObject catDisplay;

    private GameManager GM;
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

        // Deactivate buttons for purchased upgrades
        for (int i = 0; i < 5; i++)
        {
            char isOwned = boostsOwned[i];
            if (isOwned == 't')
                boostsPanel.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = false;
            isOwned = accessoriesOwned[i];
            if (isOwned == 't')
                accessoriesPanel.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = false;
            if (i < 3)
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
                break;
            case 'a':
                upgradeNameText.text = accessoryNameDesc[upgradeNum];
                upgradeDescText.text = accessoryNameDesc[upgradeNum + 1];
                break;
            case 't':
                upgradeNameText.text = ticketNameDesc[upgradeNum];
                upgradeDescText.text = ticketNameDesc[upgradeNum + 1];
                break;
        }
    }

    public void openConfirmPanel()
    {
        confirmPanel.SetActive(true);
        catDisplay.SetActive(false);
        confirmPanelUpgrade.text = upgradeNameText.text + "?";
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
                if (GM) GM.boostBoots.Invoke();
                break;
        }

        // Mark upgrade as permanently owned
        switch (activeCategory)
        {
            case 'b':
                string updatedBoosts = boostsOwned.Substring(0,upgradeIndex) + 't' + boostsOwned.Substring(upgradeIndex + 1);
                boostsOwned = updatedBoosts;
                PlayerPrefs.SetString("boostsOwned", updatedBoosts);
                if (GM) GM.boostsOwned = updatedBoosts;
                break;
            case 'a':
                string updatedAccs = accessoriesOwned.Substring(0, upgradeIndex) + 't' + 
                    accessoriesOwned.Substring(upgradeIndex + 1);
                accessoriesOwned = updatedAccs;
                PlayerPrefs.SetString("accessoriesOwned", updatedAccs);
                if (GM) GM.accessoriesOwned = updatedAccs;
                break;
            case 't':
                string updatedTickets = ticketsOwned.Substring(0, upgradeIndex) + 't' + 
                    ticketsOwned.Substring(upgradeIndex + 1);
                ticketsOwned = updatedTickets;
                PlayerPrefs.SetString("ticketsOwned", updatedTickets);
                if (GM) GM.ticketsOwned = updatedTickets;
                break;
        }
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
