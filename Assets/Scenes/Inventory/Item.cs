using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Flour,
        Yeast,
        Cocoa_Powder,
		Rye_Flour
    }

    public ItemType itemType;
    public int amount;

	public Sprite GetSprite(){
		switch (itemType){
		default:
		case ItemType.Flour:	    return ItemAssets.Instance.flourSprite;
		case ItemType.Yeast:	    return ItemAssets.Instance.yeastSprite;
		case ItemType.Cocoa_Powder:	return ItemAssets.Instance.cocoaSprite;
		case ItemType.Rye_Flour:	return ItemAssets.Instance.ryeSprite;
		}
	}

	public bool IsStackable(){
		switch(itemType){
		default:
		//Stackable Items
		case ItemType.Flour:
		case ItemType.Yeast:
		case ItemType.Cocoa_Powder:
		case ItemType.Rye_Flour:
			return true;
		//Non-Stackable Items
			return false;
		}
	}

}
