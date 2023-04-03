using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcControll : MonoBehaviour
{


    public float movementSpeed = 3;
    Animator anim;
    Rigidbody rb;

    //trigger spot
    public Transform quadTile;

    //to get the position of the player so that npc can face player
    public Transform playerCat;


    private float lowRangeY, highRangeY;
    private float lowRangeX, highRangeX;


    //for popup box
    public string popupScript;
    private bool bakeryTrigger = false;

    void Start()
    {
        lowRangeX = quadTile.position.x - .05f;
        highRangeX = quadTile.position.x + .05f;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void walkTowardsBakery()
    {
        var step = movementSpeed * Time.deltaTime; // calculate distance to move
        if (lowRangeX <= transform.position.x && transform.position.x <= highRangeX)
        {
            bakeryTrigger = true;
            anim.SetInteger("Walk", 0);
            transform.LookAt(playerCat);

            //call to trigger popup for bakery scene;
            popupText();

        }
        else
        {
            anim.SetInteger("Walk", 1);
            // Move our position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, quadTile.position, step);
            transform.forward = transform.position;
        }
    }


    private void FixedUpdate()
    {
        walkTowardsBakery();
    }

    public void popupText()
    {
        if (bakeryTrigger == true)
        {
            npcSellingPopup pop = GameObject.FindObjectOfType<npcSellingPopup>();
            pop.PopUp(popupScript);
        }
    }
}