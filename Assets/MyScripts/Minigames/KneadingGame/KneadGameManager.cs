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
    public TMP_Text newCurrencyText;
    public TMP_Text totalCurrencyText;

    private GameObject catPaw1;
    private GameObject catPaw2;

    private int numLumps;
    private int loavesKneaded = 0;



    //wen code
    public bool gameStarted = false;
    public bool foldGame = false;


    public GameObject foldMenu;


    // Start is called before the first frame update
    void Start()
    {
        numLumps = 40;
        meter.GetComponent<Slider>().maxValue = numLumps;
        lumpManager.GetComponent<LumpManager>().numLumps = numLumps;
        winMenu.SetActive(false);
        startMenu.SetActive(true);
        //startMenu.SetActive(false);

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
        startMenu.SetActive(false);

        //wen code
        //foldMenu.SetActive(false);


        //start the folding game
        //foldGame = true;


        //starts the kneading game
        gameStarted = true;
        //gameStarted = false;
    }

    public void WinGame()
    {
        Debug.Log("You Win");
        loavesKneaded++;
        winMenu.SetActive(true);
        if (loavesKneaded == 1)
        {
            numLoafText.text = "1 Loaf Kneaded!";
        }
        else numLoafText.text = loavesKneaded + " Loaves Kneaded!";
        
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
}
