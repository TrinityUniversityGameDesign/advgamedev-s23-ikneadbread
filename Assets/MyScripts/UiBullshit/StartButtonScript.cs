using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    public void StartGame()
    {
       SceneManager.LoadScene("InheritStore");   
    }
}
