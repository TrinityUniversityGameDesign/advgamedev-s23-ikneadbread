using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownSelect : MonoBehaviour
{

public void FlyTown() {
    SceneManager.LoadScene("NewHomeTown");
}

public void FlyEgypt() {
    SceneManager.LoadScene("Egypt");
}

public void FlyForest() {
        SceneManager.LoadScene("Forest");
}

}
