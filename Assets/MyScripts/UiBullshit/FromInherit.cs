using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FromInherit : MonoBehaviour
{
    public GameManager GM;

    private void Start()
    {
    }

    void OnTriggerEnter(Collider collide){
        //GM = GameObject.Find("globalGM").GetComponent<GameManager>();
        //GM.lastCoords = GM.planePos;//set last coords to where you want to spawn in the next scene
        SceneManager.LoadScene("NewHomeTown");
    }
}
