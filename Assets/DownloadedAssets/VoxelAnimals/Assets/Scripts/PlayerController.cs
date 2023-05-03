﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager GM;

    [SerializeField]
    public float movementSpeed;
    public float jumpForce = 300;
    public float timeBeforeNextJump = 1.2f;
    private float canJump = 0f;
    //Animator anim;
    Rigidbody rb;

    private float angleVelocity;

    public Transform cam = null;


    private GameObject Cat;
    private GameObject Mittens;

    //allboots
    private GameObject BootBL, BootBR, BootFL, BootFR;
    private GameObject[] allBoots = new GameObject[4];
    private GameObject[] allMittens = new GameObject[1];

    //hats
    public GameObject StrawHat;
    public GameObject TopHat;
    public GameObject Beret;
    public GameObject CowboyHat;
    public GameObject ChefHat;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GM = GameObject.Find("globalGM").GetComponent<GameManager>();
        movementSpeed = GM.moveSpeed;
        this.transform.position = GM.lastCoords;
        Debug.Log("Last coords: " + GM.lastCoords);
        Debug.Log("boots owned?: " + GM.boostsOwned);


        //boots accessories
        Mittens = GameObject.Find("Mittens");
        allBoots[0] = GameObject.Find("shoe");
        allBoots[1] = GameObject.Find("shoe (1)");
        allBoots[2] = GameObject.Find("shoe (2)");
        allBoots[3] = GameObject.Find("shoe (3)");
        allMittens[0] = GameObject.Find("Mitten_fbx");


        //hats
        StrawHat = GameObject.Find("StrawHat");
        TopHat = GameObject.Find("MagicianHat");
        Beret = GameObject.Find("VikingHelmet");
        CowboyHat = GameObject.Find("CowboyHat");
        ChefHat = GameObject.Find("ChefHat");

        StrawHat.GetComponent<Renderer>().enabled = false;
        TopHat.GetComponent<Renderer>().enabled = false;
        Beret.GetComponent<Renderer>().enabled = false;
        CowboyHat.GetComponent<Renderer>().enabled = false;
        ChefHat.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        ControllPlayer();

        CheckBought();

    }

    void CheckBought()
    {
        //boostsOwned = PlayerPrefs.GetString("boostsOwned");
        //accessoriesOwned = PlayerPrefs.GetString("accessoriesOwned");

        switch (GM.boostsOwned)
        {
            case "tffff":
                //show the boots
                Debug.Log("got boots");
                break;
            case "tftff":
                //show boots and mitten
                Debug.Log("got boots and mittens");
                break;
            case "fftff":
                Debug.Log("just mittens");
                break;
            default:
                Debug.Log("nothing to showcase");
                break;
        }

        switch (GM.accessoriesOwned)
        {
            case "tffff":
                //show the strawhat
                Debug.Log("got starhat");
                this.StrawHat.GetComponent<Renderer>().enabled = true;
                break;
            case "ftfff":
                //show top hat
                Debug.Log("tophat");
                this.TopHat.GetComponent<Renderer>().enabled = true;
                break;
            case "fftff":
                Debug.Log("beret ig");
                this.Beret.GetComponent<Renderer>().enabled = true;
                break;
            case "ffftf":
                Debug.Log("cowboy hat");
                this.CowboyHat.GetComponent<Renderer>().enabled = true;
                break;
            case "fffft":
                Debug.Log("chefhat");
                this.ChefHat.GetComponent<Renderer>().enabled = true;
                break;
            default:
                Debug.Log("nothing to showcase");
                break;
        }

    }

    void ControllPlayer()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (movement != Vector3.zero)
        {

            float inputAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            inputAngle += cam.eulerAngles.y;

            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, inputAngle, ref angleVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            movement = Quaternion.Euler(0f, inputAngle, 0f) * Vector3.forward;


            //anim.SetInteger("Walk", 1);
        }
        else
        {
            //anim.SetInteger("Walk", 0);
        }

        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);

        if (Input.GetButtonDown("Jump") && Time.time > canJump)
        {
            rb.AddForce(0, jumpForce, 0);
            canJump = Time.time + timeBeforeNextJump;
            //anim.SetTrigger("jump");
        }
    }
}