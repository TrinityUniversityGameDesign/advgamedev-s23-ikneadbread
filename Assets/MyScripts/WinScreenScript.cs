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
        SceneManager.LoadScene(GM.townToReturn());
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
