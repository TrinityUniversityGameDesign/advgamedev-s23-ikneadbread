using UnityEngine;

public class CameraControls : MonoBehaviour
{

    //for events
    private GameManager globalManager;
    private npcSellingPopup npcSale;

    //for the correct first person camera
    public float sensX, sensY;
    private float xRotation, yRotation;

    [SerializeField] private Transform player;
    [SerializeField] private float distance = 3.0f;
    [SerializeField] private float height = 3.0f;
    [SerializeField] private float rotationDamping = 3.0f;

    private bool isFirstPersonView = false;

    //newFirst
    private void Start()
    {

        npcSale = GameObject.FindObjectOfType<npcSellingPopup>();
        globalManager = GameObject.FindObjectOfType<GameManager>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        globalManager.onMiniGameCube.AddListener(checkFunction);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isFirstPersonView = !isFirstPersonView;
        }
    }


    //FixedUpdate vs LateUpdate
    private void FixedUpdate()
    {
        if (!isFirstPersonView)
        {
            //newFirstPerson Camera
            //float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            //float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensX;


            //yRotation += mouseX;

            //xRotation -= mouseY;
            //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            //player.rotation = Quaternion.Euler(0, yRotation, 0);


            // Third person view
            float currentAngle = transform.eulerAngles.y;
            float desiredAngle = player.eulerAngles.y;
            float angle = Mathf.LerpAngle(currentAngle, desiredAngle, rotationDamping * Time.deltaTime);

            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            transform.position = player.position - (rotation * Vector3.forward * distance);
            transform.position = new Vector3(transform.position.x, player.position.y + height, transform.position.z);

            transform.LookAt(player);
        }
        else
        {
            // First person view
            transform.position = player.position + new Vector3(1, height, 0);
            transform.rotation = player.rotation;
        }

    }

    public void bakeryCamera()
    {
        Debug.Log("within the bakery camera");
        distance = 10.0f;  // The distance between the camera and the player
        height = 5.0f;  // The height of the camera above the player
        rotationDamping = 3.0f;  // The speed at which the camera rotates

        //float currentAngle = transform.eulerAngles.y;
        //float desiredAngle = player.eulerAngles.y;
        //float angle = Mathf.LerpAngle(currentAngle, desiredAngle, rotationDamping * Time.deltaTime);

        //Quaternion rotation = Quaternion.Euler(0, angle, 0);
        //transform.position = player.position - (rotation * Vector3.forward * distance);
        //transform.position = new Vector3(transform.position.x, player.position.y + height, transform.position.z);

        //transform.LookAt(player);


        float desiredAngle = transform.eulerAngles.y + 88f;
        float desiredHeight = transform.position.y + (height * 0.5f);
        Vector3 desiredPosition = transform.position - Quaternion.Euler(0, desiredAngle, 0) * Vector3.forward * distance + Vector3.up * desiredHeight;

        // Smoothly move the camera to the desired position and rotation
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationDamping);
        transform.LookAt(transform.position);
    }

    public void SwitchView()
    {
        isFirstPersonView = !isFirstPersonView;
    }

    public void checkFunction()
    {
        Debug.Log("Maketime access? ");
    }
}