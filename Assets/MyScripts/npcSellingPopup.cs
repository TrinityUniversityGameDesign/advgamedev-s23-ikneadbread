using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class npcSellingPopup : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;
    public Button acceptBreadBtn;


    public bool breadSale = false;


    public void Start()
    {
        acceptBreadBtn.onClick.AddListener(sellingBread);
    }

    public void PopUp(string text)
    {
        popUpBox.SetActive(true);
        popUpText.text = text;
        animator.SetTrigger("Pop");
    }

    public void sellingBread()
    {
        //breadSale = true;
        Debug.Log("clicked");
        FindObjectOfType<CameraControls>().bakeryCamera();
        animator.SetTrigger("Drop");
        Debug.Log("should start the selling portion");
    }

}
