using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class npcMoving : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    public float speed;
    public int startingPoint;
    public Transform[] points;
    private bool hasHit;

    private int i;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
        i = startingPoint;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f) {
            i++;
            if (i == points.Length) {
                i = 0;
            }
        }
        anim.SetInteger("Walk", 1);
        transform.LookAt(points[i].position);
        transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }
}