using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FromInherit : MonoBehaviour
{
    void OnTriggerEnter(Collider collide){
        SceneManager.LoadScene("CityTime");
    }
}
