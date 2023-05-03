using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{

    //for music
    public AudioClip clickSound; // The sound to play when the mouse is clicked
    public AudioClip backgroundMusic; // The background music to play
    private AudioSource audioSource;
    public float backgroundMusicVolume = 0.5f; // The volume of the background music



    //NPC quest + scene checking
    private string currentSceneName;
    private bool isFirstVisit = true;
    public bool homesceneTalked;

    //FirstQuest =
    public static bool deniedQ;
    public static bool finishQuest1;
    public static bool enoughRolls;

    //Second Quest
    public static bool deniedF;
    public static bool finishQuest2;
    public static bool enoughCroissants;
    public bool forestTalked;




    //popup panel for what scene you are on
    public GameObject scenePanel;

    //quest 3 variables
    public bool quest3Accept;

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


    //bread int number
    public static int questDinnerRoll;

    //Quest info
    public bool townQuestStarted;
    public bool townQuestDone;
    public bool forestQuestStarted;
    public bool forestQuestDone;
    public bool egyptQuestStarted;
    public bool egyptQuestDone;

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

        //music things
        audioSource = GetComponent<AudioSource>();
        backgroundMusicStart();

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


    void backgroundMusicStart()
    {
        // Play the background music
        audioSource.clip = backgroundMusic;
        audioSource.volume = backgroundMusicVolume;
        audioSource.loop = true;
        audioSource.Play();
    }

    void showSceneLabel()
    {
        Debug.Log("scene is here!");
        scenePanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        QuestBools();
        musicRelated();

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

    public void musicRelated()
    {
        //for music
        if (Input.GetMouseButtonDown(0))
        {
            // Play the click sound
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void QuestBools()
    {
        //for the npc Quests
        if (numDinnerRoll >= 1)
        {
            enoughRolls = true;
        }
        if (numCroissant >= 1)
        {
            enoughCroissants = true;
        }
    }

    public void newGame()
    {
        numGoldCoins = 100;
        numSilverCoins = 100;
        numBronzeCoins = 100;

        numDinnerRoll = 2;
        numCroissant = 2;
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

        // Load Quest Status
        townQuestStarted = PlayerPrefs.GetInt("townQuestStarted") == 1;
        townQuestDone = PlayerPrefs.GetInt("townQuestDone") == 1;
        forestQuestStarted = PlayerPrefs.GetInt("forestQuestStarted") == 1;
        forestQuestDone = PlayerPrefs.GetInt("forestQuestDone") == 1;
        egyptQuestStarted = PlayerPrefs.GetInt("egyptQuestStarted") == 1;
        egyptQuestDone = PlayerPrefs.GetInt("egyptQuestDone") == 1;

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

        // Set Quest Status
        if (townQuestStarted) PlayerPrefs.SetInt("townQuestStarted", 1);
        else PlayerPrefs.SetInt("townQuestStarted", 0);

        if (townQuestDone) PlayerPrefs.SetInt("townQuestDone", 1);
        else PlayerPrefs.SetInt("townQuestDone", 0);

        if (forestQuestStarted) PlayerPrefs.SetInt("forestQuestStarted", 1);
        else PlayerPrefs.SetInt("forestQuestStarted", 0);

        if (forestQuestDone) PlayerPrefs.SetInt("forestQuestDone", 1);
        else PlayerPrefs.SetInt("forestQuestDone", 0);

        if (egyptQuestStarted) PlayerPrefs.SetInt("egyptQuestStarted", 1);
        else PlayerPrefs.SetInt("egyptQuestStarted", 0);

        if (egyptQuestDone) PlayerPrefs.SetInt("egyptQuestDone", 1);
        else PlayerPrefs.SetInt("egyptQuestDone", 0);

        // Set Location
        PlayerPrefs.SetString("currentSceneName", lastScene.ToString()); // Save button is in Inventory
        PlayerPrefs.SetFloat("lastX", lastCoords.x);
        if (lastCoords.y < 0.25f)
            PlayerPrefs.SetFloat("lastY", 0.25f);
        else PlayerPrefs.SetFloat("lastY", lastCoords.y);
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

    [YarnFunction("getDinnerRoll")]
    public static bool GetDinnerRoll()
    {
        return enoughRolls;
    }


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

    [YarnFunction("getFinishQuest1")]
    public static bool GetFinishQuest1()
    {
        return finishQuest1;
    }

    [YarnCommand("setFinishQuest1")]
    public static void SetFinishQuest1(bool val)
    {
        finishQuest1 = val;
    }


    //for the second quest

    [YarnFunction("getCroissants")]
    public static bool GetCroissants()
    {
        return enoughCroissants;
    }

    [YarnFunction("getForestDenied")]
    public static bool GetForestDenied()
    {
        return deniedF;
    }

    [YarnCommand("setForestDenied")]
    public static void SetForestDenied(bool val)
    {
        deniedF = val;
    }

    [YarnCommand("setFinishQuest2")]
    public static void SetFinishQuest2(bool val)
    {
        finishQuest2 = val;
    }

    [YarnFunction("getFinishQuest2")]
    public static bool GetFinishQuest2()
    {
        return finishQuest2;
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
}
