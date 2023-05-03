using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadSelection : MonoBehaviour
{
    private GameObject[] buttonList;
    private int index = 0;
    GameManager GM;
    [SerializeField] GameObject uhohWindow;
    [SerializeField] GameObject exit;

    // Start is called before the first frame update
    void Start()
    {
        uhohWindow.SetActive(false);
        exit.SetActive(false);


        buttonList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            buttonList[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in buttonList)
        {
            go.SetActive(false);
        }

        if (buttonList[0])
            buttonList[0].SetActive(true);
        GM = GameObject.Find("globalGM").GetComponent<GameManager>();
        GM.inventory = new Inventory();

    }

    public void ToggleLeft()
    {
        buttonList[index].SetActive(false);
        if (index > 0)
        {
            index--;

        }

        buttonList[index].SetActive(true);
    }

    public void ToggleRight()
    {
        buttonList[index].SetActive(false);
        if (index != buttonList.Length - 1)
        {
            index++;
        }
        buttonList[index].SetActive(true);

    }

    public void selectBread()
    {
        if (index == 1)
        {
            if (GM.inventory.CheckForItem(Item.ItemType.Flour, 2) &&
                GM.inventory.CheckForItem(Item.ItemType.Yeast, 1))
            {
                GM.inventory.RemoveItemNCnt(Item.ItemType.Flour, 2);
                GM.inventory.RemoveItemNCnt(Item.ItemType.Yeast, 1);
                //go to scene
                Debug.Log("Make Bread");
            }
            else
            {
                Debug.Log("Can't make bread");
            }
        }
        if (index == 2)
        {
            if (GM.inventory.CheckForItem(Item.ItemType.Flour, 2) &&
                GM.inventory.CheckForItem(Item.ItemType.Yeast, 1) &&
                GM.inventory.CheckForItem(Item.ItemType.Cocoa_Powder, 1))
            {
                GM.inventory.RemoveItemNCnt(Item.ItemType.Flour, 2);
                GM.inventory.RemoveItemNCnt(Item.ItemType.Yeast, 1);
                GM.inventory.RemoveItemNCnt(Item.ItemType.Cocoa_Powder, 1);
                Debug.Log("Make Bread");
                //go to scene
            }
            else
            {
                Debug.Log("Can't make bread");
            }
        }
        if (index == 3)
        {
            if (GM.inventory.CheckForItem(Item.ItemType.Flour, 1) &&
                GM.inventory.CheckForItem(Item.ItemType.Rye_Flour, 1) &&
                GM.inventory.CheckForItem(Item.ItemType.Yeast, 1) &&
                GM.inventory.CheckForItem(Item.ItemType.Cocoa_Powder, 1))
            {
                GM.inventory.RemoveItemNCnt(Item.ItemType.Flour, 1);
                GM.inventory.RemoveItemNCnt(Item.ItemType.Rye_Flour, 1);
                GM.inventory.RemoveItemNCnt(Item.ItemType.Yeast, 1);
                GM.inventory.RemoveItemNCnt(Item.ItemType.Cocoa_Powder, 1);
                //go to scene
                Debug.Log("Make Bread");
            }
            else
            {
                Debug.Log("Can't make bread");
            }
        }
        
    }

    public void leavePopup()
    {
        uhohWindow.SetActive(false);
        exit.SetActive(false);

    }


}
