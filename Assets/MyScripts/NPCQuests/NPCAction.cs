using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCAction : MonoBehaviour
{
    Animator anim;
    public Transform player;

    // Speed at which the enemy will move
    public float moveSpeed = 5f;

    // Distance at which the enemy will start following the player
    public float followDistance = 50f;

    // Distance at which the enemy will stop following the player
    public float stopDistance = 2f;

    // Distance at which the enemy will stop moving
    public float playerRadius = 5f;

    public CanvasGroup uiElement;

    //button interaction
    public string sceneName;
    public Button uiButton;


    private GameManager GM;

    void Start(){
        GM = GameObject.Find("globalGM").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        uiButton.onClick.AddListener(LoadScene);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        firstDialogue();
    }

    void LoadScene()
    {
        GM.lastCoords = player.transform.position;
        SceneManager.LoadScene(sceneName);
    }

    void firstDialogue()
    {
        if(GM.homesceneTalked == false)
        {
            // Calculate the distance between the enemy and the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // If the player is within the follow distance and outside the player's radius, move towards them
            if (distanceToPlayer < followDistance && distanceToPlayer > playerRadius)
            {
                // Calculate the direction to the player
                Vector3 directionToPlayer = (player.position - transform.position).normalized;

                // If the enemy is farther away than the stop distance, move towards the player
                if (distanceToPlayer > stopDistance)
                {

                    //Debug.Log("o lord he walking");
                    anim.SetInteger("Walk", 1);
                    transform.position += directionToPlayer * moveSpeed * Time.deltaTime;
                }


                if (distanceToPlayer < followDistance && distanceToPlayer > stopDistance && distanceToPlayer > playerRadius)
                {
                    anim.SetInteger("Walk", 0);

                    // Calculate the direction to the player
                    directionToPlayer = (player.position - transform.position).normalized;

                    // Move towards the player
                    transform.position += directionToPlayer * moveSpeed * Time.deltaTime;

                    //Debug.Log("stops walking");
                }
            }
        } else {
            questPopUp(true);
        }
    }

    void questPopUp(bool qBool)
    {
        uiElement.gameObject.SetActive(qBool);
        uiButton.gameObject.SetActive(qBool);
    }
}
