using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class StartButtonScript : MonoBehaviour
{

    public GameManager GM;
    public void StartGame()
    {
        GM = GameObject.Find("globalGM").GetComponent<GameManager>();
        GM.lastCoords = new Vector3(474.1f, 0.006f, 371.8f);//set last coords to where you want to spawn in the next scene
        SceneManager.LoadScene("CopyBecauseImSilly");   
    }
}
