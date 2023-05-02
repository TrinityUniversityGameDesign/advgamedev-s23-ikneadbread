using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //[SerializeField]
    public TownSelect townSelect;


    //NPC quest + scene checking
    private string currentSceneName;
    private bool isFirstVisit = true;
    public bool homesceneTalked;

    //FirstQuest =
    public static bool deniedQ;


    //popup panel for what scene you are on
    public GameObject scenePanel;

    //add event
    public UnityEvent gameStarted = new UnityEvent();
    public UnityEvent boost1 = new UnityEvent();
    public UnityEvent accessory1, accessory2, accessory3, accessory4, accessory5 = new UnityEvent();

    //global variables
    public GameObject playerCat;

    //coin info
    public int numGoldCoins;
    public int numSilverCoins;
    public int numBronzeCoins;

    //Inventory
    public Inventory inventory;

    //upgrade info
    public string boostsOwned;
    public string accessoriesOwned;
    public string ticketsOwned;

    //Bread info
    public int numDinnerRoll;
    public int numCroissant;
    public int numPumpernickel;

    //location info
    public enum travelDestination
    {
        //should be updated to make sure it includes any scenes we may go to
        Egypt,
        NewHomeTown,
        Forest,
        CityTime,
        InheritStore,
        Inventory,
        GroceryGame,
        KneadingGame,
        StocksGame,
        TownSelect,
        UpgradeStore,
        TitleScene,
        WinScene
    }
    
    public Vector3 lastCoords;
    public Vector3 planePos = new Vector3(0.150714532f, -0.020006299f, -27.3976288f);
    public travelDestination lastScene;
    public travelDestination currScene;
    //minor change

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

    public float moveSpeed = 7;
    public UnityEvent onMiniGameCube = new UnityEvent();


    //boolean for values
    public bool bootsBought = false;

    //Destroys the old GameManager but still contais all the previous data
    //this awake is necessary so we do not have duplicate GameManagers
    private void Awake()
    {
        if (GameObject.FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(this.gameObject);
        }

        inventory = new Inventory();
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //newGame(); // Temporary until Continuing Game is added
        gameStarted.AddListener(GlobalGameStart);
        Debug.Log("within the start with listeners");
        boost1.AddListener(ApplyBootiesUpgrade);
        accessory1.AddListener(StrawHatUpgrade);
        accessory2.AddListener(TopHatUpgrade);
        accessory3.AddListener(BeretHatUpgrade);
        accessory4.AddListener(CowboyHatUpgrade);
        accessory5.AddListener(ChefHatUpgrade);

        //for checking what scene you are in
        currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("start, within: " + currentSceneName);
        if (PlayerPrefs.HasKey(currentSceneName))
        {
            // If they have visited before, set isFirstVisit to false
            Debug.Log("has visited");
            //isFirstVisit = false;

            //for brain sake
            //isFirstVisit = true;
            showSceneLabel();
        }
        //else
        //{
        //    Debug.Log("first visit");
        //    // If this is the first time the player is visiting, show the UI panel
        //    scenePanel.SetActive(true);
        //    Debug.Log("should be showng panel");
        //    // Set the PlayerPrefs key for this scene to indicate the player has visited before
        //    PlayerPrefs.SetInt(currentSceneName, 1);
        //    PlayerPrefs.Save();
        //}

    }

    void showSceneLabel()
    {
        Debug.Log("scene is here!");
        scenePanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if (deniedQ)
        {
            Debug.Log("attack justin");
            townSelect.FlyTown();
        }


        if (playerCat == null)
        {
            if (currScene == travelDestination.NewHomeTown || currScene == travelDestination.Forest
                || currScene == travelDestination.Egypt || currScene == travelDestination.CityTime)
            {
                playerCat = GameObject.Find("Cat");
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (currScene == travelDestination.Inventory)
            {
                SceneManager.LoadScene(townToReturn());
            }
            else if (currScene == travelDestination.NewHomeTown || currScene == travelDestination.Forest 
                || currScene == travelDestination.Egypt || currScene == travelDestination.CityTime)
            {
                lastScene = currScene;
                currScene = travelDestination.Inventory;
                lastCoords = playerCat.transform.position;
                //Debug.Log(lastScene);
                SceneManager.LoadScene("Inventory");
            }
        }
    }

    public void newGame()
    {
        numGoldCoins = 0;
        numSilverCoins = 0;
        numBronzeCoins = 0;

        numDinnerRoll = 0;
        numCroissant = 0;
        numPumpernickel = 0;

        boostsOwned = "fffff";
        accessoriesOwned = "fffff";
        ticketsOwned = "ff";

        //THE COORDINATES BELOW DETERMINE DEFAULT COORDINATES
        lastCoords = new Vector3(-0.1499996f, -0.01020604f, 42.2f);

        PlayerPrefs.SetInt("savedGameExists", 0);

        // Reset Coins
        PlayerPrefs.SetInt("numGold", numGoldCoins);
        PlayerPrefs.SetInt("numSilver", numSilverCoins);
        PlayerPrefs.SetInt("numBronze", numBronzeCoins);

        // Reset Breads Owned
        PlayerPrefs.SetInt("numDinnerRoll", numDinnerRoll);
        PlayerPrefs.SetInt("numCroissant", numCroissant);
        PlayerPrefs.SetInt("numPumpernickel", numPumpernickel);

        // Reset Ingredients in Inventory
        PlayerPrefs.SetInt("numFlour", 0);
        PlayerPrefs.SetInt("numYeast", 0);
        PlayerPrefs.SetInt("numCocoa", 0);
        PlayerPrefs.SetInt("numRye", 0);

        // Reset Upgrades
        PlayerPrefs.SetString("boostsOwned", boostsOwned);
        PlayerPrefs.SetString("accessoriesOwned", accessoriesOwned);
        PlayerPrefs.SetString("ticketsOwned", ticketsOwned);

        // Reset Quest Status
        PlayerPrefs.SetInt("townQuestStarted", 0);
        PlayerPrefs.SetInt("townQuestDone", 0);
        PlayerPrefs.SetInt("forestQuestStarted", 0);
        PlayerPrefs.SetInt("forestQuestDone", 0);
        PlayerPrefs.SetInt("egyptQuestStarted", 0);
        PlayerPrefs.SetInt("egyptQuestDone", 0);

        // Reset Location
        PlayerPrefs.SetString("currentSceneName", "NewHomeTown");
        PlayerPrefs.SetFloat("lastX", lastCoords.x);
        PlayerPrefs.SetFloat("lastY", lastCoords.y);
        PlayerPrefs.SetFloat("lastZ", lastCoords.z);
    }

    public void loadGame()
    {
        // Load Coins
        numGoldCoins = PlayerPrefs.GetInt("numGold");
        numSilverCoins = PlayerPrefs.GetInt("numSilver");
        numBronzeCoins = PlayerPrefs.GetInt("numBronze");

        // Load Breads Owned
        numDinnerRoll = PlayerPrefs.GetInt("numDinnerRoll");
        numCroissant = PlayerPrefs.GetInt("numCroissant");
        numPumpernickel = PlayerPrefs.GetInt("numPumpernickel");

        // Load Inventory
        inventory.AddItemNCnt(Item.ItemType.Flour, PlayerPrefs.GetInt("numFlour"));
        inventory.AddItemNCnt(Item.ItemType.Yeast, PlayerPrefs.GetInt("numYeast"));
        inventory.AddItemNCnt(Item.ItemType.Cocoa_Powder, PlayerPrefs.GetInt("numCocoa"));
        inventory.AddItemNCnt(Item.ItemType.Rye_Flour, PlayerPrefs.GetInt("numRye"));

        // Load Upgrades
        boostsOwned = PlayerPrefs.GetString("boostsOwned");
        Debug.Log(boostsOwned);
        if (boostsOwned[0] == 't')
            boost1.Invoke();
        accessoriesOwned = PlayerPrefs.GetString("accessoriesOwned");
        for (int i = 0; i < accessoriesOwned.Length; i++)
        {
            if (accessoriesOwned[i] == 't')
            {
                switch (i)
                {
                    case 0:
                        accessory1.Invoke();
                        break;
                    case 1:
                        accessory2.Invoke();
                        break;
                    case 2:
                        accessory3.Invoke();
                        break;
                    case 3:
                        accessory4.Invoke();
                        break;
                    case 4:
                        accessory5.Invoke();
                        break;
                }
            }
        }
        ticketsOwned = PlayerPrefs.GetString("ticketsOwned");

        // Load Quest Status (Not Done)

        // Load Location
        lastCoords = new Vector3(PlayerPrefs.GetFloat("lastX"), PlayerPrefs.GetFloat("lastY"), PlayerPrefs.GetFloat("lastZ"));

        SceneManager.LoadScene(PlayerPrefs.GetString("currentSceneName"));
    }

    public void saveGame()
    {
        PlayerPrefs.SetInt("savedGameExists", 1);

        // Set Coins
        PlayerPrefs.SetInt("numGold", numGoldCoins);
        PlayerPrefs.SetInt("numSilver", numSilverCoins);
        PlayerPrefs.SetInt("numBronze", numBronzeCoins);

        // Set Breads Owned
        PlayerPrefs.SetInt("numDinnerRoll", numDinnerRoll);
        PlayerPrefs.SetInt("numCroissant", numCroissant);
        PlayerPrefs.SetInt("numPumpernickel", numPumpernickel);

        // Set Ingredients in Inventory
        List<Item> itemList = inventory.GetItemList();
        for (int i = 0; i < itemList.Count; i++)
        {
            Item it = itemList[i];
            switch (it.itemType)
            {
                case (Item.ItemType.Flour):
                    PlayerPrefs.SetInt("numFlour", it.amount);
                    break;
                case (Item.ItemType.Yeast):
                    PlayerPrefs.SetInt("numYeast", it.amount);
                    break;
                case (Item.ItemType.Cocoa_Powder):
                    PlayerPrefs.SetInt("numCocoa", it.amount);
                    break;
                case (Item.ItemType.Rye_Flour):
                    PlayerPrefs.SetInt("numRye", it.amount);
                    break;
            }
        }

        // Set Upgrades
        Debug.Log(boostsOwned);
        PlayerPrefs.SetString("boostsOwned", boostsOwned);
        PlayerPrefs.SetString("accessoriesOwned", accessoriesOwned);
        PlayerPrefs.SetString("ticketsOwned", ticketsOwned);

        // Reset Quest Status (Not Done)

        // Set Location
        PlayerPrefs.SetString("currentSceneName", lastScene.ToString()); // Save button is in Inventory
        PlayerPrefs.SetFloat("lastX", lastCoords.x);
        PlayerPrefs.SetFloat("lastY", lastCoords.y);
        PlayerPrefs.SetFloat("lastZ", lastCoords.z);

        Debug.Log("Game Is Saved");
    }

    public void GlobalGameStart()
    {

    }


    public void ApplyBootiesUpgrade()
    {
        bootsBought = true;
        Debug.Log("update boots");
        moveSpeed = 14;
        //Debug.Log("afterUpgrade (in scene) cat speed: " + moveSpeed);
    }

    // Call this function to change the number of coins owned
    public void giveCoins(int gold, int silver, int bronze)
    {
        numGoldCoins += gold;
        numSilverCoins += silver;
        numBronzeCoins += bronze;
    }

    // Updates lastScene and currScene, returns the name of the scene to load
    public string townToReturn()
    {
        switch (lastScene)
        {
            case (travelDestination.CityTime):
                lastScene = currScene;
                currScene = travelDestination.CityTime;
                //lastCoords = new Vector3(-13.75f, -0.0102060437f, -25.1299992f);
                return "CityTime";
            case (travelDestination.Egypt):
                lastScene = currScene;
                currScene = travelDestination.Egypt;
                //lastCoords = new Vector3(-532.916992f, 16.6599998f, 632.41803f);
                return "Egypt";
            case (travelDestination.Forest):
                lastScene = currScene;
                currScene = travelDestination.Forest;
                //lastCoords = new Vector3(428.04776f, -0.0199999511f, 381.833923f);
                return "Forest";
            case (travelDestination.NewHomeTown):
                lastScene = currScene;
                currScene = travelDestination.NewHomeTown;
                //lastCoords = new Vector3(459.420013f, 0.0289999992f, 451.269989f);
                return "NewHomeTown";
            default:
                // Same as HomeTown
                lastScene = currScene;
                currScene = travelDestination.NewHomeTown;
                //lastCoords = new Vector3(459.420013f, 0.0289999992f, 451.269989f);
                return "NewHomeTown";
        }
    }

    public void loadScene(string sceneLoad)
    {
        lastScene = currScene;
        SceneManager.LoadScene(sceneLoad);
    }

    // ------------ Yarn functions ------------


    //for the first quest

    [YarnFunction("getDeniedQuest")]
    public static bool GetDeniedQuest()
    {
        return deniedQ;
    }


    [YarnCommand("setDeniedQuest")]
    public static void SetDeniedQuest(bool val)
    {
        deniedQ = val;
    }



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
