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
    public UnityEvent boost1 = new UnityEvent();
    public UnityEvent accessory1, accessory2, accessory3, accessory4, accessory5 = new UnityEvent();

    //global variables
    public GameObject playerCat;
    // need variable for which town scene is loaded

    //ingredients info
    public int numIngred1; //placeholder name
    public int numIngred2; //placeholder name
    public int numChocolate;
    public int numCocoa;
    public int numRye;

    //coin info
    public int numGoldCoins;
    public int numSilverCoins;
    public int numBronzeCoins;

    //upgrade info
    public string boostsOwned;
    public string accessoriesOwned;
    public string ticketsOwned;

    //Bread info
    public int numDinnerRoll;
    public int numCroissant;
    public int numPumpernickel;

    //location info
    public enum lastScene {
        //should be updated to make sure it includes any scenes we may go to
        Egypt,
        HomeTown,
        Forest,
        KneadingGame,
    }
    public Vector3 lastCoords;

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
    
    public float moveSpeed = 3;

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
        boost1.AddListener(ApplyBootiesUpgrade);
        accessory1.AddListener(StrawHatUpgrade);
        accessory2.AddListener(TopHatUpgrade);
        accessory3.AddListener(BeretHatUpgrade);
        accessory4.AddListener(CowboyHatUpgrade);
        accessory5.AddListener(ChefHatUpgrade);
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

        numIngred1 = 0;
        numIngred2 = 0;
        numChocolate = 0;
        numCocoa = 0;
        numRye = 0;

        numDinnerRoll = 0;
        numCroissant = 0;
        numPumpernickel = 0;

        boostsOwned = "fffff";
        accessoriesOwned = "fffff";
        ticketsOwned = "ff";

        //THE COORDINATES BELOW DETERMINE DEFAULT COORDINATES
        lastCoords = new Vector3(-0.1499996f, -0.01020604f, 42.2f);

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
        moveSpeed = 6;
        Debug.Log("afterUpgrade (in scene) cat speed: " + moveSpeed);

    }

    // Call this function to change the number of ingredients
    public void giveIngredients()
    {
    }

    // Call this function to change the number of coins owned
    public void giveCoins(int gold, int silver, int bronze)
    {
        numGoldCoins += gold;
        numSilverCoins += silver;
        numBronzeCoins += bronze;
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
    
    // Add Hats to Inventory
    public void StrawHatUpgrade()
    {
        // Add Straw Hat to Inventory
    }

    public void TopHatUpgrade()
    {
        // Add Top Hat to Inventory
    }

    public void BeretHatUpgrade()
    {
        // Add Beret to Inventory
    }

    public void CowboyHatUpgrade()
    {
        // Add Cowboy Hat to Inventory
    }

    public void ChefHatUpgrade()
    {
        // Add Chef's Hat to Inventory
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
