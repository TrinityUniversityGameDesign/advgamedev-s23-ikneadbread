using UnityEngine;
using UnityEngine.SceneManagement;

public class BakeTime : MonoBehaviour
{
    public string sceneLoad; // name of the scene to load

    private GameManager GM;

    public void Start()
    {
        GM = GameObject.Find("globalGM").GetComponent<GameManager>();//find game manager in scene
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sceneLoad == "cityTime")
                GM.lastCoords = GM.planePos;
            if (gameObject.transform.childCount >= 1) //has a child component
            {
                GM.lastCoords = transform.Find("respawn").transform.position;
                //if it has respawn gameObject, respawn there
            }
            Debug.Log("hit the box" + sceneLoad);
            switchScenes();
        }
    }

    public void switchScenes()
    {
        if (sceneLoad == "LastScene")
        {
            SceneManager.LoadScene(GM.townToReturn());
        }
        GM.lastScene = GM.currScene;
        SceneManager.LoadScene(sceneLoad);
    }
}
