using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasketMovement : MonoBehaviour
{
    private int speed = 5;
    private GameManager globalManager;
    private Inventory inventory;
    public int chocCount;
    public int ryeCount;
    public GameObject basket;
    public List<Item> caughtIngrediants;
    public int ingrCount = 0;
    void Start()
    {
        basket = GameObject.Find("Basket");
        globalManager = GameObject.FindObjectOfType<GameManager>();
        //inventory = GameObject.FindObjectOfType<Inventory>();
    }
   
        void OnCollisionEnter(Collision col)
    {
        // Check if collided object is a fruit
        if (col.gameObject.CompareTag("Ingridient"))
        {
            ingrCount++;
            if(col.gameObject.name == "Chocolate(Clone)")
                chocCount++;
            if(col.gameObject.name == "Rye(Clone)")
                ryeCount++;
            Debug.Log("here!");
            Item newIngr = returnItem(col.gameObject.name);
            //nventory.AddItem(newIngr);
            
            

            caughtIngrediants.Add(newIngr);
            Destroy(col.gameObject); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) { 
            basket.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            basket.transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        
    }

    Item returnItem(string ingrName)
    {
        Item newItem = new Item();

        if(ingrName == "Milk(Clone)")
            newItem.itemType = Item.ItemType.Yeast;
        else if(ingrName == "Butter(Clone)")
            newItem.itemType = Item.ItemType.Flour;
        else if(ingrName == "Sugar(Clone)")
            newItem.itemType = Item.ItemType.Rye_Flour;
        else if(ingrName == "Egg(Clone)")
            newItem.itemType = Item.ItemType.Cocoa_Powder;
        else if(ingrName == "Yeast(Clone)")
            newItem.itemType = Item.ItemType.Yeast;
        else if(ingrName == "Salt(Clone)")
            newItem.itemType = Item.ItemType.Rye_Flour;
        else
            newItem.itemType = Item.ItemType.Flour;

        return newItem;
        Debug.Log("reaching");
    }
}

