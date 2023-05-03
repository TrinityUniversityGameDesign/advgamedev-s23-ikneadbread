using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;



public class QuestCamera2 : MonoBehaviour
{
   
    public GameManager GM;


    //yarn stuff
    public DialogueRunner dialogueRunner;
    public InMemoryVariableStorage vStorage;


    public bool croissantTalk; //this is for accepting quest
    public bool deniedCTalk; //this is for refusing quests
    public bool gainedPump; //this is for gained pump recipe 

    public bool questResponse;



    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        GM = GameObject.Find("globalGM").GetComponent<GameManager>();
        croissantTalk = false;

        vStorage = FindObjectOfType<InMemoryVariableStorage>();

        //for the dinner roll 
        vStorage.SetValue("$numDinnerRoll", GM.numDinnerRoll);
    }

    void Update()
    {
        croissantTalk = vStorage.TryGetValue("$acceptCroissantFinished", out croissantTalk);
        deniedCTalk = vStorage.TryGetValue("$noCroissantBread", out deniedCTalk);
        gainedPump = vStorage.TryGetValue("$pumpRec", out gainedPump);

        //This is for accepting the quest
        if (croissantTalk == true)
        {
            GM.homesceneTalked = true;
            Debug.Log("within the talkFinished");
            SceneManager.LoadScene("Forest");
        }
        //this is for denying the quest
        if (deniedCTalk == true)
        {
            SceneManager.LoadScene("Forest");
        }

        //this if when you finished the quest
        if (gainedPump)
        {
            GM.forestTalked = true;
            Debug.Log("finished quest 2!");


            Debug.Log("crois before: " + GM.numCroissant);
            GM.numCroissant -= 1;
            Debug.Log("crois after: " + GM.numCroissant);


            SceneManager.LoadScene("Forest");
        }
    }
}