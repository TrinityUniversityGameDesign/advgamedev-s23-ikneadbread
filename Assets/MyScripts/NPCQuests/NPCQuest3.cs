using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class NPCQuest3 : MonoBehaviour
{
    public GameManager GM;

    //yarn stuff
    public DialogueRunner dialogueRunner;
    public InMemoryVariableStorage vStorage;
    public bool winner = false;

    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        GM = GameObject.Find("globalGM").GetComponent<GameManager>();
        vStorage = FindObjectOfType<InMemoryVariableStorage>();

        vStorage.SetValue("$numPump", GM.numPumpernickel);
    }

    // Update is called once per frame
    void Update()
    {
        winner = vStorage.TryGetValue("$winnerWinnerChickenDinner", out winner);

        if (winner)
        {
            SceneManager.LoadScene("WinScene");
        }
    }
}
