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

    private int numLumps;
    private int loavesKneaded = 0;

    // Start is called before the first frame update
    void Start()
    {
        numLumps = 40;
        meter.GetComponent<Slider>().maxValue = numLumps;
        lumpManager.GetComponent<LumpManager>().numLumps = numLumps;
        winMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        meter.GetComponent<Slider>().value = numLumps - lumpManager.GetComponent<LumpManager>().getLumpsRemaining();
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
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
