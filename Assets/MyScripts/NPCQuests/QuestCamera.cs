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


    public DialogueRunner dialogueRunner;

    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        zoomDistance = maxZoom;
        initialPosition = transform.position;

    }


    void Update()
    {
        
        movingCamera();
        
    }

    public void movingCamera()
    {
        // Check if the boolean is true and adjust the zoom distance accordingly
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


//using Yarn;

//// Accessing lastPromptChoice in a C# script

//// Create a DialogueRunner instance
//DialogueRunner dialogueRunner = new DialogueRunner();

//// Start a dialogue
//dialogueRunner.StartDialogue("MyDialogueNode");

//// Get the result of the last prompt
//Value result = dialogueRunner.dialogueUI.GetLastResult();

//// Check if the last prompt choice was "No"
//if (result.AsString == "No")
//{
//    // Do something
//}
