using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquiptItem : MonoBehaviour
{
    //private GameObject cowboyHat;
    private GameObject[] allBoots = new GameObject[4];
    private GameObject[] bakeMittens = new GameObject[2];
    private GameObject[] allHats = new GameObject[5];

    private GameObject BootBL, BootBR, BootFL, BootFR;

    private GameObject Cat;
    private GameObject Flour;
    private GameObject Mittens;


    //hats
    private GameObject StrawHat;
    private GameObject TopHat;
    private GameObject Beret;
    private GameObject CowboyHat;
    private GameObject ChefHat;

    private GameManager GM;
    private UpgradeStoreManager updateStoreManager;
   


    //for shoes and hats to be on the same time
    private bool catShoes = false;
    private bool catHats = false;
    private bool showGatito = false;

    private void Start()
    {
        Cat = GameObject.Find("CatDisplay");
        Flour = GameObject.Find("flour");
        Mittens = GameObject.Find("Mittens");

        //hats
        StrawHat = GameObject.Find("StrawHat");
        TopHat = GameObject.Find("TopHat");
        Beret = GameObject.Find("VikingHelmet");
        CowboyHat = GameObject.Find("CowboyHat");
        ChefHat = GameObject.Find("ChefHat");

        allHats[0] = StrawHat;
        allHats[1] = TopHat;
        allHats[2] = Beret;
        allHats[3] = CowboyHat;
        allHats[4] = ChefHat;

        GM = GameObject.FindObjectOfType<GameManager>();
        updateStoreManager = GameObject.FindObjectOfType<UpgradeStoreManager>();
        //cowboyHat = GameObject.Find("CowboyHat");

        allBoots[0] = GameObject.Find("BootBL");
        allBoots[1] = GameObject.Find("BootBR");
        allBoots[2] = GameObject.Find("BootFL");
        allBoots[3] = GameObject.Find("BootFR");

        bakeMittens[0] = GameObject.Find("Left_Mitten");
        bakeMittens[1] = GameObject.Find("Right_Mitten");

        DeEquiptItems(allHats);
        DeEquiptS(Mittens);


    }

    // Update is called once per frame
    void Update()
    {
        //three cases we want to wear thing
        //1) we click on the object
        //2) we have already purchased the object

        if(updateStoreManager.upgradeNameText.text == "Imported Flour")
        {
            if(showGatito == true)
            {
                DeEquiptS(Cat);
            } else
            {
                //shows flour
                EquiptS(Flour);
                Flour.transform.Rotate(0, 1, 0);
                DeEquiptS(Mittens);
            }
        }

        if(updateStoreManager.upgradeNameText.text == "Kitten Mittens")
        {
            if (showGatito == true)
            {
                DeEquiptS(Flour);
                DeEquiptS(Cat);
            }
            else
            {
                //shows mittens
                EquiptItems(bakeMittens);
                Mittens.transform.Rotate(0, 1, 0);
                DeEquiptS(Flour);

            }
        } 

        if (GM.bootsBought || (updateStoreManager.upgradeNameText.text == "Cat Booties"))
        {
            DeEquiptItems(bakeMittens);
            DeEquiptS(Flour);
            showGatito = true;
            EquiptS(Cat);
            EquiptItems(allBoots);
        } else {
            showGatito = false;
            DeEquiptS(Cat);
            DeEquiptItems(allBoots);
        }

        //hat time
        if (updateStoreManager.upgradeNameText.text == "Straw Hat")
        {
            EquiptS(Cat);
            hatsOnly();
            StrawHat.GetComponent<Renderer>().enabled = true;
            TurnOffMeshRenderers(StrawHat, allHats);
        } else if(updateStoreManager.upgradeNameText.text == "Top Hat")
        {
            EquiptS(Cat);
            hatsOnly();
            TopHat.GetComponent<Renderer>().enabled = true;
            TurnOffMeshRenderers(TopHat, allHats);
        } else if (updateStoreManager.upgradeNameText.text == "Beret")
        {
            EquiptS(Cat);
            hatsOnly();
            Beret.GetComponent<Renderer>().enabled = true;
            TurnOffMeshRenderers(Beret, allHats);
        } else if (updateStoreManager.upgradeNameText.text == "Chef's Hat")
        {
            EquiptS(Cat);
            hatsOnly();
            ChefHat.GetComponent<Renderer>().enabled = true;
            TurnOffMeshRenderers(ChefHat, allHats);
        }
        else if (updateStoreManager.upgradeNameText.text == "Cowboy Hat")
        {
            EquiptS(Cat);
            hatsOnly();
            CowboyHat.GetComponent<Renderer>().enabled = true;
            TurnOffMeshRenderers(CowboyHat, allHats);
        }

        //Top Hat
        //Beret
        //Cowboy Hat
        //Chef's Hat


    }


    void TurnOffMeshRenderers(GameObject obj, GameObject[] objects)
    {
        foreach (GameObject gameObj in objects)
        {
            if (gameObj != obj)
            {
                MeshRenderer meshRenderer = gameObj.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = false;
                }
            }
        }
    }


    public void hatsOnly()
    {
        DeEquiptS(Flour);
        DeEquiptS(Mittens);
    }


    //abstract equiption
    public void EquiptItems(GameObject[] showItems)
    {
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


    //these two are for when there are gameobjects with gameobjects (ex: prefabs)

    public void DeEquiptS(GameObject item)
    {

        foreach (Transform child in item.transform)
        {
            MeshRenderer renderer = child.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        showGatito = false;
    }

    public void EquiptS(GameObject item)
    {

        foreach (Transform child in item.transform)
        {
            MeshRenderer renderer = child.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = true;
            }
        }
        showGatito = true;
    }


}
