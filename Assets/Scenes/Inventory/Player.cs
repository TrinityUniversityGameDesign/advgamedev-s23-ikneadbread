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
    public Button checkItem;
    public string[] ing = new string[] { "Flour", "Yeast", "Cocoa Powder", "Rye Flour"};

    //Add this to other scripts that need the GM.inventory + line 21 in Start()
    private GameManager GM;
    
    void Start()
    {
        GM = GameObject.Find("globalGM").GetComponent<GameManager>();
        if (GM.inventory == null)
        {
            GM.inventory = new Inventory();
        }
        
        uiInventory.SetInventory();
        
        //GM.inventory = new Inventory();
        //uiGM.inventory.SetGM.inventory(GM.inventory);
        Button addBtn = addItem.GetComponent<Button>();
        Button delBtn = removeItem.GetComponent<Button>();
        Button check = checkItem.GetComponent<Button>();
        addBtn.onClick.AddListener(AddOnClick);
        delBtn.onClick.AddListener(RemoveOnClick);
        check.onClick.AddListener(Check4Item);
    }
    
    private void AddOnClick()
    {
        //GM.inventory.itemList;
        //Add Random Item + Random Amount
        int itemRan = Random.Range(0, 4);
        int numRan = Random.Range(1, 10);
        if (itemRan == 0) { GM.inventory.AddItem(new Item { itemType = Item.ItemType.Flour, amount = numRan }); }
        if (itemRan == 1) { GM.inventory.AddItem(new Item { itemType = Item.ItemType.Yeast, amount = numRan }); }
        if (itemRan == 2) { GM.inventory.AddItem(new Item { itemType = Item.ItemType.Cocoa_Powder, amount = numRan }); }
        if (itemRan >= 3) { GM.inventory.AddItem(new Item { itemType = Item.ItemType.Rye_Flour, amount = numRan }); }

        Debug.Log("Add Button Pressed");
        Debug.Log("Added " + numRan + " " + ing[itemRan] + "(s)");
    }

    private void RemoveOnClick()
    {
        //Remove Random Item + Random Amount
        int itemRan = Random.Range(0, 4);
        int numRan = Random.Range(1, 10);
        if (itemRan == 0) { GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Flour, amount = numRan }); }
        if (itemRan == 1) { GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Yeast, amount = numRan }); }
        if (itemRan == 2) { GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Cocoa_Powder, amount = numRan }); }
        if (itemRan >= 3) { GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Rye_Flour, amount = numRan }); }

        Debug.Log("Remove Button Pressed");
        Debug.Log("Removed " + numRan + " " + ing[itemRan] + "(s)");
    }

    private void Check4Item()
    {
        int itemRan = Random.Range(0, 4);
        int numRan = Random.Range(1, 20);
        if (itemRan == 0) { GM.inventory.CheckForItem(Item.ItemType.Flour, numRan); }
        if (itemRan == 1) { GM.inventory.CheckForItem(Item.ItemType.Yeast, numRan); }
        if (itemRan == 2) { GM.inventory.CheckForItem(Item.ItemType.Cocoa_Powder, numRan); }
        if (itemRan >= 3) { GM.inventory.CheckForItem(Item.ItemType.Rye_Flour, numRan); }
        
        Debug.Log("Checking Items...");
        Debug.Log("Checking for " + numRan + " " + ing[itemRan] + "(s)");
        Debug.Log("Verdict... " + GM.inventory.isInInventory );
    }
    
    /*
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
        if (itemRan == 0)
        {
            GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Gold_Coin, amount = numRan });
        }

        if (itemRan == 1)
        {
            GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Silver_Coin, amount = numRan });
        }

        if (itemRan == 2)
        {
            GM.inventory.RemoveItem(new Item { itemType = Item.ItemType.Bronze_Coin, amount = numRan });
        }
    }
    */

}
