using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;
    //private GM.inventory GM.inventory;
    public Button addItem;
    public Button removeItem;
    public string[] ing = new string[] { "Flour", "Rice", "Salt", "Sugar", "Egg", "Milk", "Yeast", "Butter" };

    //Add this to other scripts that need the GM.inventory + line 21 in Start()
    private GameManager GM;
    
    void Start()
    {
        GM = GameObject.Find("globalGM").GetComponent<GameManager>();
        GM.inventory = new Inventory();
        uiInventory.SetInventory(GM.inventory);
        //GM.inventory = new Inventory();
        //uiGM.inventory.SetGM.inventory(GM.inventory);
        Button addBtn = addItem.GetComponent<Button>();
        Button delBtn = removeItem.GetComponent<Button>();
        addBtn.onClick.AddListener(AddOnClick);
        delBtn.onClick.AddListener(RemoveOnClick);
    }

    private void AddOnClick()
    {
        //GM.itemList
        //Add Random Item + Random Amount
        int itemRan = Random.Range(0, 8);
        int numRan = Random.Range(1, 10);
        if (itemRan == 0) { GM.inventory.AddItem(new Item { itemType = Item.ItemType.Flour, amount = numRan }); }
        if (itemRan == 1) { GM.inventory.AddItem(new Item { itemType = Item.ItemType.Rice, amount = numRan }); }
        if (itemRan == 2) { GM.inventory.AddItem(new Item { itemType = Item.ItemType.Salt, amount = numRan }); }
        if (itemRan == 3) { GM.inventory.AddItem(new Item { itemType = Item.ItemType.Sugar, amount = numRan }); }
        if (itemRan == 4) { GM.inventory.AddItem(new Item { itemType = Item.ItemType.Egg, amount = numRan }); }
        if (itemRan == 5) { GM.inventory.AddItem(new Item { itemType = Item.ItemType.Milk, amount = numRan }); }
        if (itemRan == 6) { GM.inventory.AddItem(new Item { itemType = Item.ItemType.Yeast, amount = numRan }); }
        if (itemRan >= 7) { GM.inventory.AddItem(new Item { itemType = Item.ItemType.Butter, amount = numRan }); }

        Debug.Log("Add Button Pressed");
        Debug.Log("Added " + numRan + " " + ing[itemRan] + "(s)");
    }

    private void RemoveOnClick()
    {
        /*
        //Remove Last Item
        int len = GM.inventory.itemList.Count - 1;
        GM.inventory.itemList.RemoveAt(len);
        Debug.Log("Remove Button Pressed");
        Debug.Log("Total: " + GM.inventory.itemList.Count);
        */
        
        //Remove Random Item + Random Amount
        int itemRan = Random.Range(0, 8);
        int numRan = Random.Range(1, 10);
        if (itemRan == 0) { GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Flour, amount = numRan }); }
        if (itemRan == 1) { GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Rice, amount = numRan }); }
        if (itemRan == 2) { GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Salt, amount = numRan }); }
        if (itemRan == 3) { GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Sugar, amount = numRan }); }
        if (itemRan == 4) { GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Egg, amount = numRan }); }
        if (itemRan == 5) { GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Milk, amount = numRan }); }
        if (itemRan == 6) { GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Yeast, amount = numRan }); }
        if (itemRan >= 7) { GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Butter, amount = numRan }); }

        Debug.Log("Remove Button Pressed");
        Debug.Log("Removed " + numRan + " " + ing[itemRan] + "(s)");
    }

    private void GetMoney()
    {
        int itemRan = Random.Range(0, 2);
        int numRan = Random.Range(1, 100);
        if (itemRan == 0) { GM.inventory.AddItem(new Item { itemType = Item.ItemType.Gold_Coin, amount = numRan }); }
        if (itemRan == 1) { GM.inventory.AddItem(new Item { itemType = Item.ItemType.Silver_Coin, amount = numRan }); }
        if (itemRan == 2) { GM.inventory.AddItem(new Item { itemType = Item.ItemType.Bronze_Coin, amount = numRan }); }
    }

    private void RemoveMoney()
    {
        int itemRan = Random.Range(0, 2);
        int numRan = Random.Range(1, 100);
        if (itemRan == 0) { GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Gold_Coin, amount = numRan }); }
        if (itemRan == 1) { GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Silver_Coin, amount = numRan }); }
        if (itemRan == 2) { GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Bronze_Coin, amount = numRan }); }
    }
    
}
