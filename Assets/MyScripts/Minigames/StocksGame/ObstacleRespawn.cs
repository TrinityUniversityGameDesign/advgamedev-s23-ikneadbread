using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleRespawn : MonoBehaviour
{
    public GameObject player;
    public int obstacleType;
    private ObstacleSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("ObstacleSpawner").GetComponent<ObstacleSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Culling"))
        {
            Debug.Log("Culling");
            respawn();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<StockLineController>().obstacleHit(obstacleType);
            respawn();
        }
    }

    public void respawn()
    {
        //Debug.Log(Camera.main.transform.position);
        transform.position = new Vector3(
            x: Camera.main.transform.position.x + Random.Range(spawner.minWidth, spawner.maxWidth),
            y: Camera.main.transform.position.y + Random.Range(spawner.minHeight, spawner.maxHeight),
            z: transform.position.z);
    }
}
