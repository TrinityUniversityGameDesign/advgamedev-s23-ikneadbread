using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public int numberOfObstacles = 10;

    // Area we want obstacles to spawn in
    public float minWidth = -2f;
    public float maxWidth = 15f;
    public float minHeight = -10f;
    public float maxHeight = 10f;

    private GameObject[] obstacles;

    // Start is called before the first frame update
    void Start()
    {
        obstacles = new GameObject[numberOfObstacles];
        for (int i = 0; i < numberOfObstacles; i++)
        {
            obstacles[i] = Instantiate(obstaclePrefab) as GameObject;
            obstacles[i].transform.position = new Vector3(Random.Range(minWidth, maxWidth),
                Random.Range(minHeight, maxHeight), -0.1f);
            obstacles[i].transform.SetParent(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
