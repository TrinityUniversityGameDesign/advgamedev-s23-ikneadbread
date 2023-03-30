using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //add event
    //global variables
    //bread info
    //coin info

    public UnityEvent onMiniGameCube = new UnityEvent();

    private void Awake()
    {
        if (GameObject.FindObjectsOfType<GameManager>().Length > 1) {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }   
}
