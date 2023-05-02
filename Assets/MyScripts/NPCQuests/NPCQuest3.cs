using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class NPCQuest3 : MonoBehaviour
{
    public GameManager GM;
    public GameObject player;

    //yarn stuff
    public DialogueRunner dialogueRunner;
    public InMemoryVariableStorage vStorage;
    public bool winner = false;
    public bool dialogueOver = false;
    public bool accept = false;

    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        GM = GameObject.Find("globalGM").GetComponent<GameManager>();
        player = GameObject.Find("PlayerCat");
        vStorage = FindObjectOfType<InMemoryVariableStorage>();

        GM.lastCoords = player.transform.position;
        vStorage.SetValue("$numPump", GM.numPumpernickel);
    }

    // Update is called once per frame
    void Update()
    {
        winner = vStorage.TryGetValue("$winnerWinnerChickenDinner", out winner);
        dialogueOver = vStorage.TryGetValue("$dialogueFin", out dialogueOver);
        accept = vStorage.TryGetValue("$quest3Resp", out accept);

        if (accept)
        {
            GM.quest3Accept = true;
        }

        if (dialogueOver)
        {
            Debug.Log(dialogueOver);
            SceneManager.LoadScene("Egypt");
        }

        if (winner)
        {
            SceneManager.LoadScene("WinScene");
        }
    }
}
