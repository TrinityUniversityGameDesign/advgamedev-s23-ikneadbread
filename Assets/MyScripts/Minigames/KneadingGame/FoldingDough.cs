using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldingDough : MonoBehaviour
{

    public GameObject KneadGameManager;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (KneadGameManager.GetComponent<KneadGameManager>().foldGame == true) {
            foldGame();
        }
    }


    public void foldGame()
    {
        //add anim
        anim.SetInteger("pointDown", 1);

        //anim.Play("pointDown");
        //anim.Play("")

        //code for the game


        Debug.Log("folding Moment");
    }
}
