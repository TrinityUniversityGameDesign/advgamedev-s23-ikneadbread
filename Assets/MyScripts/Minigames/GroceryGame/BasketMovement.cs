using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasketMovement : MonoBehaviour
{
    private int speed = 5;
    public GameObject basket;
    private int ingrCount;
    public TextMeshProUGUI text;
    public GameObject EndScreen;
    void Start()
    {
        basket = GameObject.Find("Basket");
        EndScreen.SetActive(false);
    }
   
        void OnCollisionEnter(Collision col)
    {
        // Check if collided object is a fruit
        if (col.gameObject.CompareTag("Ingridient"))
        {
            // Increment fruit count
            ingrCount++;
            text.text = "Score: " + ingrCount;
            // Destroy caught fruit
            Destroy(col.gameObject);
            if(ingrCount==10)
                EndScreen.SetActive(true);
                
        }
    }

    public int getScore()
    {
        return ingrCount;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) { 
            basket.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            basket.transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        
    }
}

