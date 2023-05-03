using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private GameManager GM;

    public void openBible()
    {
        SceneManager.LoadScene("Bible");
    }


    private void Awake()
    {
        GM = GameObject.Find("globalGM").GetComponent<GameManager>();
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
        //GM.numGoldCoins 
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if(child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        // x: -205, y: 80 (Slot 1)
        float x = 0f;
        float y = 0f;
        float x_itemSlotCellSize = 135f;
        float y_itemSlotCellSize = -140f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            
            //Item
            itemSlotRectTransform.anchoredPosition = new Vector2(x * x_itemSlotCellSize, y * y_itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Ingredient").GetComponent<Image>();
            image.sprite = item.GetSprite();
            
            //Amount
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("Amount").GetComponent<TextMeshProUGUI>();
            uiText.SetText(item.amount.ToString());
            
            x++;
            if (x > 3)
            {
                x = 0f;
                y++;
            }
        }
    }
    
    
    
}
