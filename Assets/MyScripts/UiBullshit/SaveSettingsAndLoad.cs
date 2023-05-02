using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSettingsAndLoad : MonoBehaviour
{
    public GameObject player;
    public GameManager manager;
    Vector3 pos;
    int world;
    public void Start(){
        Scene current = SceneManager.GetActiveScene();
        string scenename = current.name;
        if(scenename == "NewHomeTown"){
            world = 0;
        }
        else if(scenename == "CityTime"){
            world = 1;
        }else if(scenename == "Egypt"){
            world = 2;
        }
        else{ //forest scene
            world = 3;
        }
    }

    
    public void LoadSettings(){
        PlayerPrefs.GetInt("worl");
        PlayerPrefs.GetFloat("x");
        PlayerPrefs.GetFloat("y");
        PlayerPrefs.GetFloat("z");

    }
    public void SaveSettings(){
        PlayerPrefs.SetInt("worl",world);
        //PlayerPrefs.SetFloat("x", player.X);
        //PlayerPrefs.SetFloat("y", player.Y);
        //PlayerPrefs.SetFloat("z",player.Z);
        PlayerPrefs.Save();
    }
}
