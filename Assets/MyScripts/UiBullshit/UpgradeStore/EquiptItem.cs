using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquiptItem : MonoBehaviour
{
    //private GameObject cowboyHat;
    private GameObject BootBL, BootBR, BootFL, BootFR;

    public Vector3 BLPickPosition;
    public Vector3 BLPickRotation;

    public Vector3 BRPickPosition;
    public Vector3 BRPickRotation;

    public Vector3 FLPickPosition;
    public Vector3 FLPickRotation;

    public Vector3 FRPickPosition;
    public Vector3 FRPickRotation;

    private GameManager GM;

    private void Start()
    {
        GM = GameObject.FindObjectOfType<GameManager>();
        //cowboyHat = GameObject.Find("CowboyHat");

        BootBL = GameObject.Find("BootBL");
        BootBR = GameObject.Find("BootBR");
        BootFL = GameObject.Find("BootFL");
        BootFR = GameObject.Find("BootFR");

        BootBL.GetComponent<Renderer>().enabled = false;
        BootBR.GetComponent<Renderer>().enabled = false;
        BootFL.GetComponent<Renderer>().enabled = false;
        BootFR.GetComponent<Renderer>().enabled = false;


    }

    // Update is called once per frame
    void Update()
    {
        if(GM.bootsBought)
        {
            EquiptBoots();
        }
    }

    public void EquiptBoots()
    {
        //apply vector3
        //cowboyHat.transform.parent = GameObject.Find("Head").transform;
        //cowboyHat.transform.localPosition = PickPosition;
        //cowboyHat.transform.localEulerAngles = PickRotation;

        BootBL.GetComponent<Renderer>().enabled = true;
        BootBR.GetComponent<Renderer>().enabled = true;
        BootFL.GetComponent<Renderer>().enabled = true;
        BootFR.GetComponent<Renderer>().enabled = true;


        //BootBL.transform.parent = GameObject.Find("LegBL").transform;
        //BootBL.transform.localPosition = BLPickPosition;
        //BootBL.transform.localEulerAngles = BLPickRotation;


    }
}
