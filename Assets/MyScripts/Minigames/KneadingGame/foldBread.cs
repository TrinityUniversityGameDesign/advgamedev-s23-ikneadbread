using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class foldBread : MonoBehaviour
{
    private GameObject catPaw1;
    private GameObject catPaw2;

    //needed to access other scripts
    public GameObject KneadGameManager;


    private RaycastHit _hit;
    public LayerMask hitMask;

    [SerializeField] private AudioSource kneadSoundEffect;



    // Start is called before the first frame update
    void Start()
    {
        catPaw1 = GameObject.Find("catpaw1");
        catPaw2 = GameObject.Find("catpaw2");
        Debug.Log("started the game");

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("within the update");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out _hit, 3000f, hitMask);
        movePaws();

        if (KneadGameManager.GetComponent<KneadGameManager>().gameStarted == true)
        {
            //Debug.Log("pressed to start the game");
        }
    }

    public void movePaws()
    {
        if (Input.GetMouseButtonDown(0)) {
            //Debug.Log("within the move paw scripts");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (_hit.transform != null)
            {
                Debug.Log("Hit: " + _hit.transform);

                catPaw1.transform.position = _hit.point;
                catPaw2.transform.position = _hit.point + new Vector3(1f, 0f, 0f);
            }
            kneadSoundEffect.Play();
        }
    }
}
