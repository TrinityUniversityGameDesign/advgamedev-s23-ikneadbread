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
        GM.lastCoords = new Vector3(3.501f, 0f, 3.504f);//set last coords to where you want to spawn in the next scene
        SceneManager.LoadScene("InheritStore");   
    }
}
