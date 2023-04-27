using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownSelect : MonoBehaviour
{
    public GameManager GM;

    private void Start()
    {
        GM = GameObject.Find("globalGM").GetComponent<GameManager>();
    }

    public void FlyTown() {
        Vector3 homeTownPlanePos = new Vector3(474.100006f, 0.00699999928f, 371.799988f);
        GM.lastCoords = homeTownPlanePos;//set last coords to where you want to spawn in the next scene
        GM.lastScene = GM.currScene;
        GM.currScene = GameManager.travelDestination.NewHomeTown;
        SceneManager.LoadScene("NewHomeTown");
    }

    public void FlyEgypt() {
        Vector3 egyptPlanePos = new Vector3(-561.690002f, 16.6599998f, 598.76001f);
        GM.lastCoords = egyptPlanePos;//set last coords to where you want to spawn in the next scene
        GM.lastScene = GM.currScene;
        GM.currScene = GameManager.travelDestination.Egypt;
        SceneManager.LoadScene("Egypt");
    }

    public void FlyForest() {
        Vector3 forestPlanePos = new Vector3(404.670013f, -4.76837158e-06f, 471.670013f);
        GM.lastCoords = forestPlanePos;//set last coords to where you want to spawn in the next scene
        GM.lastScene = GM.currScene;
        GM.currScene = GameManager.travelDestination.Forest;
        SceneManager.LoadScene("Forest");
    }

    public void flyCity()
    {
        Vector3 forestPlanePos = new Vector3(404.670013f, -4.76837158e-06f, 471.670013f);
        GM.lastCoords = GM.planePos;//set last coords to where you want to spawn in the next scene
        GM.lastScene = GM.currScene;
        GM.currScene = GameManager.travelDestination.CityTime;
        SceneManager.LoadScene("CityTime");
    }
}
