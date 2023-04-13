using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

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

    //yarn variables
    static bool introDone = false;

    public static bool ingTutorial = false;
    public static bool kneadTutorial = false;
    public static bool ovenTutorial = false;
    public static bool dispTutorial = false;
    public static bool stocksTutorial = false;

    static bool ingDone = false;
    static bool kneadDone = false;
    static bool ovenDone = false;
    static bool dispDone = false;
    static bool stocksDone = false;

    public UnityEvent onMiniGameCube = new UnityEvent();

    //Destroys the old GameManager but still contais all the previous data
    //this awake is necessary so we do not have duplicate GameManagers
    private void Awake()
    {
        if (GameObject.FindObjectsOfType<GameManager>().Length > 1) {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        newGame(); // Temporary until Continuing Game is added
        gameStarted.AddListener(GlobalGameStart);
        Debug.Log("within the start with listeners");
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
        Debug.Log("update boots");
        Debug.Log("current cat speed: " + playerCat.GetComponent<PlayerController>().movementSpeed);
        playerCat.GetComponent<PlayerController>().movementSpeed = 6;
        Debug.Log("afterUpgrade (in scene) cat speed: " + playerCat.GetComponent<PlayerController>().movementSpeed);

    }

    // ------------ Yarn functions ------------

    // Introduction Complete
    [YarnFunction("getIntroDone")]
    public static bool GetIntroDone()
    {
        return introDone;
    }

    [YarnCommand("setIntroDone")]
    public static void SetIntroDone(bool val)
    {
        introDone = val;
    }

    // Ingredient Tutorial
    [YarnFunction("getIngTutorial")]
    public static bool GetIngTutorial()
    {
        return ingTutorial;
    }

    [YarnCommand("setIngTutorial")]
    public static void SetIngTutorial(bool val)
    {
        ingTutorial = val;
    }

    [YarnFunction("getIngDone")]
    public static bool GetIngDone()
    {
        return ingDone;
    }

    [YarnCommand("setIngDone")]
    public static void SetIngDone(bool val)
    {
        ingDone = val;
    }

    // Kneading Tutorial
    [YarnFunction("getKneadTutorial")]
    public static bool GetKneadTutorial()
    {
        return kneadTutorial;
    }

    [YarnCommand("setKneadTutorial")]
    public static void SetKneadTutorial(bool val)
    {
        kneadTutorial = val;
    }

    [YarnFunction("getKneadDone")]
    public static bool GetKneadDone()
    {
        return kneadDone;
    }

    [YarnCommand("setKneadDone")]
    public static void SetKneadDone(bool val)
    {
        kneadDone = val;
    }

    // Oven Tutorial
    [YarnFunction("getOvenTutorial")]
    public static bool GetOvenTutorial()
    {
        return ovenTutorial;
    }

    [YarnCommand("setOvenTutorial")]
    public static void SetOvenTutorial(bool val)
    {
        ovenTutorial = val;
    }

    [YarnFunction("getOvenDone")]
    public static bool GetOvenDone()
    {
        return ovenDone;
    }

    [YarnCommand("setOvenDone")]
    public static void SetOvenDone(bool val)
    {
        ovenDone = val;
    }

    // Display Tutorial
    [YarnFunction("getDispTutorial")]
    public static bool GetDispTutorial()
    {
        return dispTutorial;
    }

    [YarnCommand("setDispTutorial")]
    public static void SetDispTutorial(bool val)
    {
        dispTutorial = val;
    }

    [YarnFunction("getDispDone")]
    public static bool GetDispDone()
    {
        return dispDone;
    }

    [YarnCommand("setDispDone")]
    public static void SetDispDone(bool val)
    {
        dispDone = val;
    }

    // Stocks Tutorial
    [YarnFunction("getStocksTutorial")]
    public static bool GetStocksTutorial()
    {
        return stocksTutorial;
    }

    [YarnCommand("setStocksTutorial")]
    public static void SetStocksTutorial(bool val)
    {
        stocksTutorial = val;
    }

    [YarnFunction("getStocksDone")]
    public static bool GetStocksDone()
    {
        return stocksDone;
    }

    [YarnCommand("setStocksDone")]
    public static void SetStocksDone(bool val)
    {
        stocksDone = val;
    }
}
