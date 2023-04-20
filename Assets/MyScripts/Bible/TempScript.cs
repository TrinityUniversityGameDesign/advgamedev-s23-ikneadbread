using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TempScript : MonoBehaviour
{
    int egg = 0;
    int flour = 0;
    int milk = 0;

    //store ingrediant over sessions   
    public void Setup()
    {
        //EGGS
        if (egg <= PlayerPrefs.GetInt("egg"))
        {
            egg = PlayerPrefs.GetInt("egg");
            Debug.Log("EGG: " + egg);
        }
        else
        {
            PlayerPrefs.SetInt("egg", egg);
            Debug.Log("EGG: " + egg);
        }

        //FLOUR
        if (flour <= PlayerPrefs.GetInt("flour"))
        {
            flour = PlayerPrefs.GetInt("flour");
        }
        else
        {
            PlayerPrefs.SetInt("flour", flour);
        }

        //MILK
        if (milk <= PlayerPrefs.GetInt("milk"))
        {
            milk = PlayerPrefs.GetInt("milk");
        }
        else
        {
            PlayerPrefs.SetInt("milk", milk);
        }
        
    }
}