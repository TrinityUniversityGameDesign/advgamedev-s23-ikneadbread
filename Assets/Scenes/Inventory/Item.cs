using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Flour,
		Rice,
        Salt,
        Sugar,
        Egg,
        Milk,
        Yeast,
        Butter
    }

    public ItemType itemType;
    public int amount;

	public Sprite GetSprite(){
		switch (itemType){
		default:
		case ItemType.Flour:	return ItemAssets.Instance.flourSprite;
		case ItemType.Rice:		return ItemAssets.Instance.riceSprite;
		case ItemType.Salt:		return ItemAssets.Instance.saltSprite;
		case ItemType.Sugar:	return ItemAssets.Instance.sugarSprite;
		case ItemType.Egg:		return ItemAssets.Instance.eggSprite;
		case ItemType.Milk:		return ItemAssets.Instance.milkSprite;
		case ItemType.Yeast:	return ItemAssets.Instance.yeastSprite;
		case ItemType.Butter:	return ItemAssets.Instance.butterSprite;
		}
	}

	public bool IsStackable(){
		switch(itemType){
		default:
		//Stackable Items
		case ItemType.Flour:
		case ItemType.Rice:
		case ItemType.Salt:
		case ItemType.Sugar:
		case ItemType.Egg:
		case ItemType.Milk:
		case ItemType.Yeast:
		case ItemType.Butter:
			return true;
		//Non-Stackable Items
			return false;
		}
	}

}
