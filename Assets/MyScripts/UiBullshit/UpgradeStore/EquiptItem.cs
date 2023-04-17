using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquiptItem : MonoBehaviour
{
    private GameObject cowboyHat;

    public Vector3 PickPosition;
    public Vector3 PickRotation;


    private GameManager GM;

    private void Start()
    {
        GM = GameObject.FindObjectOfType<GameManager>();
        cowboyHat = GameObject.Find("CowboyHat");
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
        cowboyHat.transform.parent = GameObject.Find("Head").transform;
        cowboyHat.transform.localPosition = PickPosition;
        cowboyHat.transform.localEulerAngles = PickRotation;

    }
}
