using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenScript : MonoBehaviour
{
    public GameManager GM;
    public GameObject creditsPanel;

    public void continueGame()
    {
        GM = GameObject.FindObjectOfType<GameManager>();
        switch (GM.lastScene)
        {
            case (GameManager.travelDestination.Egypt):
                GM.lastScene = GM.currScene;
                GM.currScene = GameManager.travelDestination.Egypt;
                GM.lastCoords = new Vector3(-532.916992f, 16.6599998f, 632.41803f);
                SceneManager.LoadScene("Egypt");
                break;
            case (GameManager.travelDestination.Forest):
                GM.lastScene = GM.currScene;
                GM.currScene = GameManager.travelDestination.Forest;
                GM.lastCoords = new Vector3(428.04776f, -0.0199999511f, 381.833923f);
                SceneManager.LoadScene("Forest");
                break;
            case (GameManager.travelDestination.NewHomeTown):
                GM.lastScene = GM.currScene;
                GM.currScene = GameManager.travelDestination.NewHomeTown;
                GM.lastCoords = new Vector3(459.420013f, 0.0289999992f, 451.269989f);
                SceneManager.LoadScene("NewHomeTown");
                break;
            default:
                // Same as HomeTown
                GM.lastScene = GM.currScene;
                GM.currScene = GameManager.travelDestination.NewHomeTown;
                GM.lastCoords = new Vector3(459.420013f, 0.0289999992f, 451.269989f);
                SceneManager.LoadScene("NewHomeTown");
                break;
        }
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void showCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void closeCredits()
    {
        creditsPanel.SetActive(false);
    }
}
