using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;



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
    public bool gainedCroissant;
    public bool alreadySeen;

    public CanvasGroup RecipeCanvas;


    public bool talkFinished; //this is for accepting quest
    public bool deniedTalk; //this is for refusing quests

    public bool questResponse;


    //public float dinRolls;

    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        zoomDistance = maxZoom;
        initialPosition = transform.position;

        GM = GameObject.Find("globalGM").GetComponent<GameManager>();
        talkFinished = false;

        vStorage = FindObjectOfType<InMemoryVariableStorage>();

        //for the dinner roll 
        vStorage.SetValue("$numDinnerRoll", GM.numDinnerRoll);

        //RecipeCanvas.alpha = 0;
        //RecipeCanvas.interactable = false;
        //RecipeCanvas.blocksRaycasts = false;
    }

    void Update()
    {
        talkFinished = vStorage.TryGetValue("$acceptFinished", out talkFinished);
        deniedTalk = vStorage.TryGetValue("$noBread", out deniedTalk);
        gainedCroissant = vStorage.TryGetValue("$croissantRec", out gainedCroissant);

        //This is for accepting the quest
        if (talkFinished == true)
        {
            GM.homesceneTalked = true;
            Debug.Log("within the talkFinished");
            SceneManager.LoadScene("NewHomeTown");
        }
        //this is for denying the quest
        if(deniedTalk == true)
        {
            SceneManager.LoadScene("NewHomeTown");
        }

        //this if when you finished the quest
        if(gainedCroissant)
        {
            GM.homesceneTalked = true;
            Debug.Log("finished quest 1!");


            Debug.Log("gold before: " + GM.numGoldCoins);
            GM.numGoldCoins += 10;
            Debug.Log("gold after: " + GM.numGoldCoins);


            Debug.Log("rolls before: " + GM.numDinnerRoll);
            GM.numDinnerRoll -= 1;
            Debug.Log("rolls after: " + GM.numDinnerRoll);


            SceneManager.LoadScene("NewHomeTown");
        }

        //this is fancy camera things
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