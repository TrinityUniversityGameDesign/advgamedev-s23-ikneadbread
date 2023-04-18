using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsInv : MonoBehaviour
{
    private Dictionary<AnimalsType, int> inventory;

    private void Awake()
    {
        inventory = new Dictionary<AnimalsType, int>();
    }

    public void Add(AnimalsType type, int count = 1)
    {
        if (!inventory.TryGetValue(type, out int current))
        {
            inventory.Add(type, count);
        }
        else
        {
            inventory[type] += count;
        }
    }

    public int Get(AnimalsType type)
    {
        if (inventory.TryGetValue(type, out int current))
        {
            return current;
        }
        else
        {
            throw new KeyNotFoundException();
        }
    }
}
