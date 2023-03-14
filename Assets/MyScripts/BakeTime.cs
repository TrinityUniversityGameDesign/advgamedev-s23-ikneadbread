using UnityEngine;
using UnityEngine.SceneManagement;

public class BakeTime : MonoBehaviour
{
    public string sceneLoad; // name of the scene to load

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("hit the box" + sceneLoad);
            SceneManager.LoadScene(sceneLoad);
        }
    }
}
