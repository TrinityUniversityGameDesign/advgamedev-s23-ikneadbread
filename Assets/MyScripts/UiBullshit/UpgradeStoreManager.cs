using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private string activeCategory = "b";
    private GameObject upgradeInCart;
    private GameObject catDisplay;

    // Start is called before the first frame update
    void Start()
    {
        catDisplay = GameObject.Find("CatDisplay");
        upgradeInCart = GameObject.Find("Boost (1)");
        upgradeNameText.text = boostNameDesc[0];
        upgradeDescText.text = boostNameDesc[1];
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
                activeCategory = "b";
                break;
            case 1:
                accessoriesPanel.SetActive(true);
                activeCategory = "a";
                break;
            case 2:
                ticketsPanel.SetActive(true);
                activeCategory = "t";
                break;
        }
    }

    public void selectUpgrade(GameObject upgrade)
    {
        upgradeInCart = upgrade;
        int index = upgrade.name.IndexOf('(') + 1;
        int upgradeNum = (int) char.GetNumericValue(upgrade.name[index]);
        upgradeNum = (upgradeNum - 1) * 2;

        switch (activeCategory)
        {
            case "b":
                upgradeNameText.text = boostNameDesc[upgradeNum];
                upgradeDescText.text = boostNameDesc[upgradeNum + 1];
                break;
            case "a":
                upgradeNameText.text = accessoryNameDesc[upgradeNum];
                upgradeDescText.text = accessoryNameDesc[upgradeNum + 1];
                break;
            case "t":
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
    }
}
