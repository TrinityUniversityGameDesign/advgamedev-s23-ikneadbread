using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StockLineController : MonoBehaviour
{
    public GameObject startButton;
    public GameObject bottomLine;

    private Rigidbody rb;

    private float speed = 2;
    private bool started = false;
    private float startingValue = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bottomLine.GetComponent<Slider>().value = startingValue;
    }

    public void StartMoving()
    {
        rb.velocity = new Vector2(speed, speed);
        started = true;
        startButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            float vertical = Input.GetAxis("Vertical");
            if (vertical > 0)
                rb.velocity = new Vector2(speed, speed);
            else if (vertical < 0)
                rb.velocity = new Vector2(speed, -speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            other.gameObject.GetComponent<ObstacleRespawn>().respawn();
            Debug.Log("Obstacle Hit!");
        }
    }
}
