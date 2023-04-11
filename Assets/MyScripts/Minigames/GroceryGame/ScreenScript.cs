using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScreenScript : MonoBehaviour
{

    public GameObject StartScreen;
    //public GameObject EndScore;


    void Start()
    {
        StartScreen.SetActive(true);
    }

   public void playGame()
    {
       StartScreen.SetActive(false);
    }


    public void endGame()
    {
        SceneManager.LoadScene("CityTime");
    }


}

