using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    public List<Item> itemList;
	public event EventHandler OnItemListChanged;

    public Inventory()
    {
        itemList = new List<Item>();
        AddItem(new Item { itemType = Item.ItemType.Egg, amount = 1 });
		//AddItem(new Item { itemType = Item.ItemType.Flour, amount = 1 });
		//AddItem(new Item { itemType = Item.ItemType.Milk, amount = 1 });
        //Debug.Log("Amount of unique Items: " + itemList.Count);
		//Debug.Log("Amount of " + itemList[0].itemType + ":" + itemList[0].amount);

		//Make Dinner Roll Button Pressed
		//for(int i = 0; i < itemList.Length(); i++){
			//if(itemList[i].itemType = "Egg" && itemList[i].Count >= 2){
				//Debug.Log("We have enough Eggs for DR");
			//} else Debug.Log("NO");
		//}
		
    }
	
    public void AddItem(Item item)
    {
		if(item.IsStackable()){
			bool itemAlreadyInInventory = false;
			foreach(Item inventoryItem in itemList){
				if(inventoryItem.itemType == item.itemType){
					inventoryItem.amount += item.amount;
					itemAlreadyInInventory = true;
				}
			}
			if(!itemAlreadyInInventory){
				itemList.Add(item);
			}
		} else {
			itemList.Add(item);
		}
		OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

	public void RemoveItem(Item item)
    {
		if(item.IsStackable()){
			Item itemInInventory = null;
			foreach(Item inventoryItem in itemList){
				if(inventoryItem.itemType == item.itemType){
					inventoryItem.amount -= item.amount;
					itemInInventory = inventoryItem;
				}
			}
			if(itemInInventory != null && itemInInventory.amount <= 0){
				itemList.Remove(itemInInventory);
			}
		} else {
			itemList.Remove(item);
		}
		OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

	//Expose Item List
	public List<Item> GetItemList()
	{
		return itemList;
	}

	public void AddItemNCnt(Item.ItemType type, int count)
	{
		AddItem(new Item { itemType = type, amount = count });
	}
	//This method could be called by saying inventory.AddItemNCnt(Item.ItemType.Salt, 6)

}
