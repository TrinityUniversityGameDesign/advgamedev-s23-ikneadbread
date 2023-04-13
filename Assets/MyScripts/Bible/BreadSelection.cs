using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadSelection : MonoBehaviour
{
    private GameObject[] buttonList;
    private int index;
    [SerializeField] GameObject uhohWindow;
    [SerializeField] GameObject exit;
    

    private void Start()
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
        {
            buttonList[0].SetActive(true);
        }


        //NOT CODE THAT SHOULD REMAIN HERE, JUST FOR TESTING
        PlayerPrefs.SetInt("flour", 7);
        PlayerPrefs.SetInt("milk", 7);
        PlayerPrefs.SetInt("egg", 7);

        ////////////////////////////////////////////////////
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
        if (index != buttonList.Length-1)
        {
            index++;
        }
        buttonList[index].SetActive(true);

    }

    public void ConfirmButton()
    {
        //Oh No You Don't Have Enough Ingrediants to Make This Bread
        //Current Invetory: ... 
        if (index == 1)
        {
            if (PlayerPrefs.GetInt("milk") < 2 || PlayerPrefs.GetInt("flour") < 8)
            {
                uhohWindow.SetActive(true);
                exit.SetActive(true);
            }
        }
        if (index == 2)
        {
            if (PlayerPrefs.GetInt("flour") < 6)
            {
                uhohWindow.SetActive(true);
                exit.SetActive(true);
            }
            else
            {
                Debug.Log("we gucci");
            }
        }
    }

    public void leavePopup()
    {
        uhohWindow.SetActive(false);
        exit.SetActive(false);

    }

}
