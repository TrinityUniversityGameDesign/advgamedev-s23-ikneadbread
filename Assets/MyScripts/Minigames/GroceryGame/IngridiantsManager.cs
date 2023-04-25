using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngridiantsManager: MonoBehaviour
{
    private GameManager gm;
    public GameObject[] ingrPrefabs; // The prefab for the fruit to generate
    public GameObject[] specialIngr;
    public float minSpeed = 1f; // The 2inimum speed at which the fruit drops
    public float maxSpeed = 2f; // The maximum speed at which the fruit drops
    private float min=0f;
    private float max=10f;

    private float spawnRate = 4f; // The rate at which to spawn new fruits
    private float nextSpawnTime = 2f; // The time at which to spawn the next fruit

    

    void Start()
    {
        //setIngridiants();
        this.transform.position = new Vector3(-7f,12.65f,13.71f);
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    // void setIngridiants()
    // {
      

    //     if(gm.lastScene.GetType() == GameManager.lastScene.Egypt)
    //         ingrPrefabs[ingrPrefabs.Length] = specialIngr[0];
    //     if(gm.lastScene == 1)
    //         ingrPrefabs[ingrPrefabs.Length] = specialIngr[1];
    //     else
    //         ingrPrefabs[ingrPrefabs.Length] = specialIngr[2];

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
            GameObject newIngr = Instantiate(ingrPrefabs[ran], transform.position, Quaternion.identity);
            Rigidbody ingrRigidbody = newIngr.GetComponent<Rigidbody>();
            ingrRigidbody.velocity = new Vector2(0, -speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time*2,max-min)+min);
        dropFruit();
    }
}


