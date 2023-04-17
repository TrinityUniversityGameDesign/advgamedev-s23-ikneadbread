using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager GM;

    [SerializeField]
    public float movementSpeed;
    public float jumpForce = 300;
    public float timeBeforeNextJump = 1.2f;
    private float canJump = 0f;
    Animator anim;
    Rigidbody rb;

    private float angleVelocity;

    public Transform cam = null;
    
    void Start()
    {
        Debug.Log("current speed in the CityTime: " + movementSpeed);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        GM = GameObject.Find("globalGM").GetComponent<GameManager>();//find game manager in scene
        this.transform.position = GM.lastCoords;
        

        //Camera.main.GetComponent<CameraController>().AssignTarget(transform);
    }

    void Update()
    {
        ControllPlayer();
    }

    void ControllPlayer()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (movement != Vector3.zero)
        {

            float inputAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            inputAngle += cam.eulerAngles.y;

            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, inputAngle, ref angleVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            movement = Quaternion.Euler(0f, inputAngle, 0f) * Vector3.forward;


            anim.SetInteger("Walk", 1);
        }
        else
        {
            anim.SetInteger("Walk", 0);
        }

        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);

        if (Input.GetButtonDown("Jump") && Time.time > canJump)
        {
            rb.AddForce(0, jumpForce, 0);
            canJump = Time.time + timeBeforeNextJump;
            anim.SetTrigger("jump");
        }
    }
}