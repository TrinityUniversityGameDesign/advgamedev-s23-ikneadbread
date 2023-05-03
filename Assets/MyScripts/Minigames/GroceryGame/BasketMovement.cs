using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasketMovement : MonoBehaviour
{
    private int speed = 8;
    private GameManager globalManager;
    public GameObject basket;
    public List<Item> caughtIngrediants;
    public int ingrCount = 0;
    public GameObject bagIcon;
    [SerializeField] private AudioSource collectSoundEffect;

    void Start()
    {
        basket = GameObject.Find("Basket");
        globalManager = GameObject.FindObjectOfType<GameManager>();

        //inventory = GameObject.FindObjectOfType<Inventory>();

        // Apply Tote Bag Upgrade
        if (globalManager.boostsOwned.Substring(3, 1) == "t")
        {
            basket.transform.localScale = new Vector3(basket.transform.localScale.x, basket.transform.localScale.y,
                basket.transform.localScale.z * 2);
            bagIcon.SetActive(true);
        }
    }
   
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ingridient"))
        {
            ingrCount++;
            Destroy(col.gameObject);
            collectSoundEffect.Play();
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

    /*Item returnItem(string ingrName)
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
    }*/
}

