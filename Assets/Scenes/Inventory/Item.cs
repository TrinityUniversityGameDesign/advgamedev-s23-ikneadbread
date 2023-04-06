using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Wheat,
        Salt,
        Sugar,
        Egg,
        Milk,
        Yeast,
        Butter
    }

    public ItemType itemType;
    public int amount;

}
