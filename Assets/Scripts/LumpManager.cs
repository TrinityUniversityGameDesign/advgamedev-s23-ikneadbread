using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LumpManager : MonoBehaviour
{
    public GameObject dough;
    public GameObject lumpPrefab;
    public int numLumps = 40;
    private float squishFactor;
    public LayerMask hitMask;

    private GameObject manager;
    private Camera _mainCamera;
    private Renderer _renderer;

    private RaycastHit _hit;

    private GameObject[] lumps;
    private Vector3 startDoughScale;
    private Vector3 doughScale;
    private float minX, maxX, minY, maxY, minZ, maxZ;
    private float minLmpScale, maxLmpScale;

    private int lumpsRemaining;
    private bool fullyKneaded = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("ScriptsObject");
        _mainCamera = Camera.main;
        _renderer = GetComponent<Renderer>();

        lumpsRemaining = numLumps;
        lumps = new GameObject[numLumps];
        for (int i = 0; i < numLumps; i++)
        {
            lumps[i] = Instantiate(lumpPrefab) as GameObject;
            lumps[i].transform.SetParent(transform);
        }
        startDoughScale = dough.transform.localScale;
        doughScale = startDoughScale;
        squishFactor = (startDoughScale.x - 1) / numLumps;

        defineRange();
        minLmpScale = 0.8f;
        maxLmpScale = 1.2f;
        NewLoaf();
    }

    public void NewLoaf()
    {
        fullyKneaded = false;
        lumpsRemaining = numLumps;
        doughScale = startDoughScale;
        if (dough.transform.localScale != startDoughScale)
            dough.transform.localScale = startDoughScale;
        RandomizeBread();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Click");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hit, 3000f, hitMask))
            {
                if (_hit.collider.CompareTag("Lump"))
                {
                    _hit.transform.localScale += new Vector3(0, -0.1f, 0);
                    //Debug.Log(_hit.transform.localScale);
                    Vector3 newScale = _hit.transform.localScale;
                    if (newScale.y < 0.8f)
                    {
                        // Remove Lump and Flatten Dough a bit
                        _hit.transform.position = Vector3.one * -10;
                        lumpsRemaining--;
                        Vector3 flatterScale = new Vector3(doughScale.x - squishFactor, doughScale.y, doughScale.z);
                        doughScale = flatterScale;
                        dough.transform.localScale = flatterScale;
                        //Debug.Log("Remaining Lumps: "+lumpsRemaining);
                        for (int i = 0; i < numLumps; i++)
                        {
                            if (lumps[i].transform.position.y > 0.15)
                            {
                                lumps[i].transform.position += new Vector3(0, -(squishFactor/2), 0);
                            }
                        }
                    }
                }
                else Debug.Log(_hit.collider.tag);
            }
            //else Debug.Log("Nothing");
        }

        if (!fullyKneaded)
        {
            if (lumpsRemaining <= 0)
            {
                fullyKneaded = true;
                manager.GetComponent<KneadGameManager>().WinGame();
            }
        }
    }

    public int getLumpsRemaining()
    {
        return lumpsRemaining;
    }

    // Define Range in which dough lumps can spawn, called from KneadGameManager
    public void defineRange()
    {
        /* Basic Bread Range:
         * 1.05 > z > 0.70: y ~= 0.75
         * 0.70 > z > 0.35: y ~= 1.10
         * 0.35 > z > 0.00: y ~= 1.35
        */
        minX = doughScale.x * -0.75f;
        maxX = doughScale.x * 0.75f;
        minY = doughScale.y * 0.4f;
        maxY = doughScale.y * 0.45f;
        minZ = doughScale.z * -0.35f;
        maxZ = doughScale.z * 0.35f;
    }

    void RandomizeBread()
    {
        for (int i = 0; i < numLumps; i++)
        {
            float rX = Random.Range(minX, maxX);
            float rY = Random.Range(minY, maxY);
            float rZ = Random.Range(minZ, maxZ);

            if (Mathf.Abs(rZ) > 0.7)
            {
                rY -= 0.6f;
            }
            else if (Mathf.Abs(rZ) > 0.35)
            {
                rY -= 0.15f;
            }
            // Randomize Position
            lumps[i].transform.position = new Vector3(rX, rY, rZ);
            // Randomize Scale
            lumps[i].transform.localScale = new Vector3(Random.Range(minLmpScale, maxLmpScale),
                Random.Range(minLmpScale, maxLmpScale), Random.Range(minLmpScale, maxLmpScale));
        }
    }
}
