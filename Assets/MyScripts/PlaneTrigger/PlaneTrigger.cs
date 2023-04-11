using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlaneTrigger : MonoBehaviour
{
    public GameObject user;
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;
    public Button ridePlane;


    //public bool breadSale = false;

    public void Start()
    {
        ridePlane.onClick.AddListener(changeScene);
    }

    public void FlyPopUp(string text)
    {
        popUpBox.SetActive(true);
        popUpText.text = text;
        animator.SetTrigger("FlyPop");
    }

    public void changeScene()
    {
        //breadSale = true;
        Debug.Log("changing scenes");
        animator.SetTrigger("FlyDrop");
        SceneManager.LoadScene("Egypt");

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the user's character
        if (other.CompareTag("Player"))
        {
            // Invoke the quad detection event
            FlyPopUp("Fly?");
            Debug.Log("collided");
        }
    }

}
