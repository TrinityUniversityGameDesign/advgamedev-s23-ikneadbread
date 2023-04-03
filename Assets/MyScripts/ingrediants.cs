using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingrediants : MonoBehaviour
{
    public string type { get; set; }
    public int amount { get; set; }
    public ingrediants (string type, int amount)
    {
        this.type = type;
        this.amount = amount;
    }
}
