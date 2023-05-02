using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngridiantsManager: MonoBehaviour
{
    public GameManager gm;
    public GameObject[] ingrPrefabs; // The prefab for the fruit to generate
    public GameObject[] specialIngr;
    private float spawnRate = 4f; // The rate at which to spawn new fruits
    private float nextSpawnTime = 2f; // The time at which to spawn the next fruit
    public Vector3 startPosition = new Vector3(-7, 8, 16); // Starting position of movement
    public Vector3 endPosition = new Vector3(-7, 8, 5); // End position of movement
    public float lerpDuration = 2.0f; // Time taken to move from start to end position
    public float speed = 5.0f; 
    private float lerpTimer = 0.0f; // Time elapsed since starting movement
    private bool moveToEnd = true;
    public float minInterval = 1.0f; // Minimum time between drops
    public float maxInterval = 5.0f; // Maximum time between drops

    private float nextDropTime; // Time when the next fruit will be dropped 

    private bool moveRight = true; // Whether the object is moving right

    

    void Start()
    {
        this.transform.position = new Vector3(-7f,12.65f,13.71f);
        gm = GameObject.FindObjectOfType<GameManager>();
        setIngridiants();
        nextDropTime = Time.time + Random.Range(minInterval, maxInterval);

    }

    void setIngridiants()
    {
        GameManager.travelDestination location = gm.lastScene;
        if(location == GameManager.travelDestination.Egypt)
            ingrPrefabs[ingrPrefabs.Length-1] = specialIngr[0];
        if(location == GameManager.travelDestination.Forest)
            ingrPrefabs[ingrPrefabs.Length-1] = specialIngr[1];
    }

    void dropFruit()
    {
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnRate;

            // Generate a random speed for the fruit
            float speed = Random.Range(8, 19);
            int ran = Random.Range(0,8);
           
            // Generate a new fruit and set its speed
            GameObject newIngr = Instantiate(ingrPrefabs[ran], transform.position, Quaternion.identity);
            Rigidbody ingrRigidbody = newIngr.GetComponent<Rigidbody>();
            ingrRigidbody.velocity = new Vector2(0, -speed);
        }
    }


    void Update()
    {       
         if (Time.time >= nextDropTime)
        {
            dropFruit();
            // Set the time for the next fruit drop
            nextDropTime = Time.time + Random.Range(minInterval, maxInterval);
        }
        //dropFruit();
        lerpTimer += Time.deltaTime;
        float lerpValue = lerpTimer / lerpDuration;

        // If the object has reached the end position, reverse direction
        if (lerpValue >= 1.0f)
        {
            moveToEnd = !moveToEnd;
            lerpTimer = 0.0f;
        }

        // Calculate the target position using the lerp value
        Vector3 targetPosition = moveToEnd ? endPosition : startPosition;
        Vector3 currentPosition = moveToEnd ? startPosition : endPosition;
        
        transform.position = Vector3.Lerp(currentPosition, targetPosition, lerpValue);
       // dropFruit();
        // Move the object towards the target position
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);


    }
}

