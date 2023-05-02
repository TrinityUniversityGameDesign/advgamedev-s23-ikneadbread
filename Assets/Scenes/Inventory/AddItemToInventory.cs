using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemToInventory : MonoBehaviour
{
    private GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("globalGM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GM.inventory.AddItemNCnt(Item.ItemType.Yeast, 1);
            Debug.Log("Adding yeast");
            Debug.Log(GM.inventory.GetItemList());
        }
        
    }
}
