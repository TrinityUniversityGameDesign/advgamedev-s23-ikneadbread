using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquiptItem : MonoBehaviour
{
    //private GameObject cowboyHat;
    private GameObject[] allBoots = new GameObject[4];
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
    private UpgradeStoreManager updateStoreManager;


    //for shoes and hats to be on the same time
    private bool catShoes = false;
    private bool catHats = false;

    private void Start()
    {
        GM = GameObject.FindObjectOfType<GameManager>();
        updateStoreManager = GameObject.FindObjectOfType<UpgradeStoreManager>();
        //cowboyHat = GameObject.Find("CowboyHat");

        allBoots[0] = GameObject.Find("BootBL");
        allBoots[1] = GameObject.Find("BootBR");
        allBoots[2] = GameObject.Find("BootFL");
        allBoots[3] = GameObject.Find("BootFR");


        //BootBL.GetComponent<Renderer>().enabled = false;
        //BootBR.GetComponent<Renderer>().enabled = false;
        //BootFL.GetComponent<Renderer>().enabled = false;
        //BootFR.GetComponent<Renderer>().enabled = false;


    }

    // Update is called once per frame
    void Update()
    {
        //three cases we want to wear thing
        //1) we click on the object
        //2) we have already purchased the object


        if (GM.bootsBought || (updateStoreManager.upgradeNameText.text == "Cat Booties"))
        {
            EquiptItems(allBoots);
            //EquiptBoots();
        } else {
            DeEquiptItems(allBoots);
        }
    }


    //boosts



    //abstract equiption
    public void EquiptItems(GameObject[] showItems)
    {
        Debug.Log("lenght: " + showItems.Length);
        for (int i = 0; i < showItems.Length ; i++)
        {
            showItems[i].GetComponent<Renderer>().enabled = true;
        }
    }

    public void DeEquiptItems(GameObject[] hideItems)
    {

        foreach (GameObject obj in hideItems)
        {
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
    }

    public void EquiptBoots()
    {
        catShoes = true;

        BootBL.GetComponent<Renderer>().enabled = true;
        BootBR.GetComponent<Renderer>().enabled = true;
        BootFL.GetComponent<Renderer>().enabled = true;
        BootFR.GetComponent<Renderer>().enabled = true;

    }

    public void DeEquiptBoots()
    {
        catShoes = false;

        BootBL.GetComponent<Renderer>().enabled = false;
        BootBR.GetComponent<Renderer>().enabled = false;
        BootFL.GetComponent<Renderer>().enabled = false;
        BootFR.GetComponent<Renderer>().enabled = false;
    }



    public void EquiptCowboyHat()
    {
        //apply vector3
        //cowboyHat.transform.parent = GameObject.Find("Head").transform;
        //cowboyHat.transform.localPosition = PickPosition;
        //cowboyHat.transform.localEulerAngles = PickRotation;
    }
}
