using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;
    public Button addItem;
    public Button removeItem;
    public string[] ing = new string[] { "Flour", "Rice", "Salt", "Sugar", "Egg", "Milk", "Yeast", "Butter" };

    void Start()
    {
        //inventory = new Inventory();
        //uiInventory.SetInventory(inventory);
        Button addBtn = addItem.GetComponent<Button>();
        Button delBtn = removeItem.GetComponent<Button>();
        addBtn.onClick.AddListener(AddOnClick);
        delBtn.onClick.AddListener(RemoveOnClick);
    }
    
    private void Awake()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }

    private void AddOnClick()
    {
        //Add Random Item + Random Amount
        int itemRan = Random.Range(0, 8);
        int numRan = Random.Range(1, 10);
        if (itemRan == 0) { inventory.AddItem(new Item { itemType = Item.ItemType.Flour, amount = numRan }); }
        if (itemRan == 1) { inventory.AddItem(new Item { itemType = Item.ItemType.Rice, amount = numRan }); }
        if (itemRan == 2) { inventory.AddItem(new Item { itemType = Item.ItemType.Salt, amount = numRan }); }
        if (itemRan == 3) { inventory.AddItem(new Item { itemType = Item.ItemType.Sugar, amount = numRan }); }
        if (itemRan == 4) { inventory.AddItem(new Item { itemType = Item.ItemType.Egg, amount = numRan }); }
        if (itemRan == 5) { inventory.AddItem(new Item { itemType = Item.ItemType.Milk, amount = numRan }); }
        if (itemRan == 6) { inventory.AddItem(new Item { itemType = Item.ItemType.Yeast, amount = numRan }); }
        if (itemRan >= 7) { inventory.AddItem(new Item { itemType = Item.ItemType.Butter, amount = numRan }); }

        Debug.Log("Add Button Pressed");
        Debug.Log("Added " + numRan + " " + ing[itemRan] + "(s)");
    }

    private void RemoveOnClick()
    {
        /*
        //Remove Last Item
        int len = inventory.itemList.Count - 1;
        inventory.itemList.RemoveAt(len);
        Debug.Log("Remove Button Pressed");
        Debug.Log("Total: " + inventory.itemList.Count);
        */
        
        //Remove Random Item + Random Amount
        int itemRan = Random.Range(0, 8);
        int numRan = Random.Range(1, 10);
        if (itemRan == 0) { inventory.RemoveItem(new Item { itemType = Item.ItemType.Flour, amount = numRan }); }
        if (itemRan == 1) { inventory.RemoveItem(new Item { itemType = Item.ItemType.Rice, amount = numRan }); }
        if (itemRan == 2) { inventory.RemoveItem(new Item { itemType = Item.ItemType.Salt, amount = numRan }); }
        if (itemRan == 3) { inventory.RemoveItem(new Item { itemType = Item.ItemType.Sugar, amount = numRan }); }
        if (itemRan == 4) { inventory.RemoveItem(new Item { itemType = Item.ItemType.Egg, amount = numRan }); }
        if (itemRan == 5) { inventory.RemoveItem(new Item { itemType = Item.ItemType.Milk, amount = numRan }); }
        if (itemRan == 6) { inventory.RemoveItem(new Item { itemType = Item.ItemType.Yeast, amount = numRan }); }
        if (itemRan >= 7) { inventory.RemoveItem(new Item { itemType = Item.ItemType.Butter, amount = numRan }); }

        Debug.Log("Remove Button Pressed");
        Debug.Log("Removed " + numRan + " " + ing[itemRan] + "(s)");
    }

}
