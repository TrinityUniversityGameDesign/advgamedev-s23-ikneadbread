using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;


public class QuestCamera : MonoBehaviour
{
    public Transform target;         // The object the camera should follow
    public float zoomSpeed = 10f;    // The speed at which the camera should zoom
    public float maxZoom = 5f;       // The maximum zoom distance
    public float minZoom = 2f;       // The minimum zoom distance
    public bool isZoomed = false;    // A boolean to indicate if the camera should zoom in or not

    private float zoomDistance;      // The current zoom distance
    private Vector3 initialPosition; // The initial position of the camera


    public GameManager GM;


    //yarn stuff
    public DialogueRunner dialogueRunner;
    public InMemoryVariableStorage vStorage;
    public bool talkFinished = false;
    public TownSelect townSelect;


    //bools for yarn

    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        zoomDistance = maxZoom;
        initialPosition = transform.position;

        GM = GameObject.Find("globalGM").GetComponent<GameManager>();
        townSelect = GameObject.Find("townSelect").GetComponent<TownSelect>();


        vStorage = FindObjectOfType<InMemoryVariableStorage>();
    }

    void Update()
    {
        //movingCamera();
        talkFinished = vStorage.TryGetValue("$acceptFinished", out talkFinished);
        Debug.Log("talkFinished: " + talkFinished);

        if(talkFinished == true)
        {
            Debug.Log("within the talkFinished");
            townSelect.FlyTown();
            //GM.townToReturn();


        }

        if (vStorage.TryGetValue("$cameraShift", out isZoomed))
        {
            if (isZoomed)
            {
                zoomDistance = Mathf.MoveTowards(zoomDistance, minZoom, zoomSpeed * Time.deltaTime);
            }
            else
            {
                zoomDistance = Mathf.MoveTowards(zoomDistance, maxZoom, zoomSpeed * Time.deltaTime);
            }

            // Update the camera position to match the target position and zoom distance
            if (isZoomed)
            {
                transform.position = target.position - (transform.forward * zoomDistance);
            }
            else
            {
                transform.position = initialPosition;
            }
        }
 
    }
}