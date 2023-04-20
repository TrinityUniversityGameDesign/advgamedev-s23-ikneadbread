using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class KneadGameManager : MonoBehaviour
{
    public GameObject dough;
    public GameObject lumpManager;
    public GameObject lumpPrefab;

    public GameObject meter;
    public GameObject startMenu;
    public GameObject winMenu;
    public TMP_Text numLoafText;
    public TMP_Text numBreadText;
    public TMP_Text numNewCoinsText;
    public TMP_Text numTotalCoinsText;

    // Bread Selection Screen
    public GameObject selectScreen;
    public GameObject[] breadTypes;
    private int selectedBread = 0;

    private GameObject catPaw1;
    private GameObject catPaw2;

    private int numLumps;
    private int loavesKneaded = 0;

    public GameManager GM;

    //wen code
    public bool gameStarted = false;
    public bool gameEnded = false;
    public bool foldGame = false;


    public GameObject foldMenu;


    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindObjectOfType<GameManager>();
        numLumps = 40;
        meter.GetComponent<Slider>().maxValue = numLumps;
        lumpManager.GetComponent<LumpManager>().numLumps = numLumps;
        winMenu.SetActive(false);
        startMenu.SetActive(true);
        selectScreen.SetActive(false);

        //wen code
        foldMenu.SetActive(false);

        catPaw1 = GameObject.Find("catpaw1");
        catPaw2 = GameObject.Find("catpaw2");
    }

    // Update is called once per frame
    void Update()
    {
        meter.GetComponent<Slider>().value = numLumps - lumpManager.GetComponent<LumpManager>().getLumpsRemaining();
    }

    public void StartGame()
    {
        selectScreen.SetActive(false);

        // Changing num lumps currently does nothing
        /*switch (selectedBread)
        {
            case 0:
                numLumps = 40;
                break;
            case 1:
                numLumps = 50;
                break;
            case 2:
                numLumps = 60;
                break;
        }*/

        //starts the kneading game
        gameStarted = true;
        GameManager.kneadTutorial = true;
    }

    public void WinGame()
    {
        gameEnded = true;
        //Debug.Log("You Win");
        loavesKneaded++;
        winMenu.SetActive(true);
        if (loavesKneaded == 1)
        {
            numLoafText.text = "1 Loaf Kneaded!";
        }
        else numLoafText.text = loavesKneaded + " Loaves Kneaded!";

        string breadName = "";
        int breadsOwned = 0;
        switch (selectedBread)
        {
            case 0:
                breadName = "Dinner Roll";
                GM.numDinnerRoll++;
                breadsOwned = GM.numDinnerRoll;
                break;
            case 1:
                breadName = "Croissant";
                GM.numCroissant++;
                breadsOwned = GM.numCroissant;
                break;
            case 2:
                breadName = "Pumpernickel";
                GM.numPumpernickel++;
                breadsOwned = GM.numPumpernickel;
                break;
        }

        string plural = "s";
        if (breadsOwned == 1)
            plural = "";

        numBreadText.text = breadsOwned + " " + breadName + plural + " Owned!";

        GameManager.kneadTutorial = false;
    }

    public void Restart()
    {
        winMenu.SetActive(false);
        lumpManager.GetComponent<LumpManager>().NewLoaf();
        catPaw1.transform.position = new Vector3(catPaw1.transform.position.x, 1.19f, catPaw1.transform.position.z);
        catPaw2.transform.position = new Vector3(catPaw2.transform.position.x, 1.19f, catPaw2.transform.position.z);
    }

    public void BackToTown()
    {
        SceneManager.LoadScene("CityTime");
    }

    public int getLoavesKneaded()
    {
        return loavesKneaded;
    }

    public void LeftButton()
    {
        GameObject currentButton = breadTypes[selectedBread];
        if (selectedBread == 0)
            selectedBread = breadTypes.Length - 1;
        else selectedBread--;
        GameObject newButton = breadTypes[selectedBread];
        currentButton.SetActive(false);
        newButton.SetActive(true);
    }

    public void RightButton()
    {
        GameObject currentButton = breadTypes[selectedBread];
        if (selectedBread == breadTypes.Length - 1)
            selectedBread = 0;
        else selectedBread++;
        GameObject newButton = breadTypes[selectedBread];
        currentButton.SetActive(false);
        newButton.SetActive(true);
    }

    public void openSelectScreen()
    {
        startMenu.SetActive(false);
        selectScreen.SetActive(true);
    }
}
