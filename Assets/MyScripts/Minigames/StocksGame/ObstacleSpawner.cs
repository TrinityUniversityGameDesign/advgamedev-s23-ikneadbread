using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab1;
    public GameObject obstaclePrefab2;
    public GameObject obstaclePrefab3;
    public GameObject player;
    public int numberOfObstacles = 20;

    // Area we want obstacles to spawn in
    public float minWidth = -2f;
    public float maxWidth = 15f;
    public float minHeight = -10f;
    public float maxHeight = 10f;

    private GameObject[] obstacles;

    // Start is called before the first frame update
    void Start()
    {
        obstacles = new GameObject[numberOfObstacles*3];
        for (int i = 0; i < numberOfObstacles*3; i+=3)
        {
            obstacles[i] = Instantiate(obstaclePrefab1) as GameObject;
            obstacles[i].transform.position = new Vector3(Random.Range(minWidth, maxWidth),
                Random.Range(minHeight, maxHeight), -0.1f);
            obstacles[i].transform.SetParent(transform);
            obstacles[i].GetComponent<ObstacleRespawn>().player = player;

            obstacles[i + 1] = Instantiate(obstaclePrefab2) as GameObject;
            obstacles[i + 1].transform.position = new Vector3(Random.Range(minWidth, maxWidth),
                Random.Range(minHeight, maxHeight), -0.1f);
            obstacles[i + 1].transform.SetParent(transform);
            obstacles[i + 1].GetComponent<ObstacleRespawn>().player = player;

            obstacles[i + 2] = Instantiate(obstaclePrefab3) as GameObject;
            obstacles[i + 2].transform.position = new Vector3(Random.Range(minWidth, maxWidth),
                Random.Range(minHeight, maxHeight), -0.1f);
            obstacles[i + 2].transform.SetParent(transform);
            obstacles[i + 2].GetComponent<ObstacleRespawn>().player = player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
