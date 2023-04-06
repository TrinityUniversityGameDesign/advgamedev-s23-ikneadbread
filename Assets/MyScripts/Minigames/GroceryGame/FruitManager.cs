using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager: MonoBehaviour
{
    public GameObject[] fruitPrefabs; // The prefab for the fruit to generate
    public float minSpeed = 1f; // The minimum speed at which the fruit drops
    public float maxSpeed = 2f; // The maximum speed at which the fruit drops
    public float min=2f;
    public float max=3f;


    private float spawnRate = 4f; // The rate at which to spawn new fruits
    private float nextSpawnTime = 2f; // The time at which to spawn the next fruit
    private int fruitCount = 0;

    // Event called when a fruit is caught
    

    void Start()
    {
        this.transform.position = new Vector3(-7f,12.65f,13.71f);

    }

    void Move()
    {
        min=transform.position.x;
        max=transform.position.x+3;
    }

    // void setGameStatus(bool status)
    // {
    //     GameOver = true;
    // }

    void dropFruit()
    {
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnRate;

            // Generate a random speed for the fruit
            float speed = Random.Range(minSpeed, maxSpeed);
            int ran = Random.Range(0,4);

            // Generate a new fruit and set its speed
            GameObject newFruit = Instantiate(fruitPrefabs[ran], transform.position, Quaternion.identity);
            Rigidbody fruitRigidbody = newFruit.GetComponent<Rigidbody>();
            fruitRigidbody.velocity = new Vector2(0, -speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time*2,max-min)+min);
        dropFruit();
    
    }
}

