using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GrocerySceneFunctions : MonoBehaviour
{
    public void BacktoMain()
    {
        SceneManager.LoadScene("CityTime");
    }
}
