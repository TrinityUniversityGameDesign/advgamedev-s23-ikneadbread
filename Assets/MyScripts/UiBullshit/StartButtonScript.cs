using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class StartButtonScript : MonoBehaviour
{
    public GameManager GM;
    public GameObject continueButton;

    void Start()
    {
        GM = GameObject.Find("globalGM").GetComponent<GameManager>();
        if (PlayerPrefs.GetInt("savedGameExists") == 0)
        {
            continueButton.SetActive(false);
        }
    }

    public void StartGame()
    {
        GM.newGame();
        GM.lastCoords = new Vector3(474.11f, .0068f, 371.8f);//set last coords to where you want to spawn in the next scene
        SceneManager.LoadScene("CopyBecauseImSilly");   
    }

    public void LoadGame()
    {
        GM.loadGame();
    }
}
