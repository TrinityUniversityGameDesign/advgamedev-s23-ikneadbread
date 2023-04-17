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
            if (gameObject.transform.childCount == 1) //look for ONE child component (that child should be where you want to spawn outside of scene)
            {
                GM.lastCoords = gameObject.transform.GetChild(0).transform.position;
                //this will break if there are several child components and only one is the respawn coord
            }
            Debug.Log("hit the box" + sceneLoad);
            SceneManager.LoadScene(sceneLoad);
        }
    }

    public void switchScenes()
    {
        SceneManager.LoadScene(sceneLoad);
    }
}
