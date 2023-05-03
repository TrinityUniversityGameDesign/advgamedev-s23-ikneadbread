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
        GM = GameObject.Find("globalGM").GetComponent<GameManager>();
        GM.lastCoords = new Vector3(474.11f, 0.8f, 370);
        GM.currScene = GameManager.travelDestination.NewHomeTown;
        SceneManager.LoadScene("NewHomeTown");
    }
}
