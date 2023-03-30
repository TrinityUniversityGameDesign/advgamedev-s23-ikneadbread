using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //add event
    public UnityEvent gameStarted = new UnityEvent();
    public UnityEvent boostBoots = new UnityEvent();
    //global variables
    public GameObject playerCat;
    // need variable for which town scene is loaded

    //bread info

    //coin info
    public int numGoldCoins;
    public int numSilverCoins;
    public int numBronzeCoins;

    //upgrade info
    public string boostsOwned;
    public string accessoriesOwned;
    public string ticketsOwned;

    public UnityEvent onMiniGameCube = new UnityEvent();


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        newGame(); // Temporary until Continuing Game is added
        gameStarted.AddListener(GlobalGameStart);
        boostBoots.AddListener(ApplyBootiesUpgrade);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void newGame()
    {
        numGoldCoins = 0;
        numSilverCoins = 0;
        numBronzeCoins = 0;

        boostsOwned = "fffff";
        accessoriesOwned = "fffff";
        ticketsOwned = "fff";
        PlayerPrefs.SetString("boostsOwned", boostsOwned);
        PlayerPrefs.SetString("accessoriesOwned", accessoriesOwned);
        PlayerPrefs.SetString("ticketsOwned", ticketsOwned);
    }

    public void GlobalGameStart()
    {

    }

    public void ApplyBootiesUpgrade()
    {
        playerCat.GetComponent<PlayerController>().movementSpeed = 6;
    }
}
