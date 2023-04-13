using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    public GameObject target;
    public bool followTarget = true;

    private Vector3 offsetFromTarget;

    // Start is called before the first frame update
    void Start()
    {
        offsetFromTarget = target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget)
        {
            transform.position = target.transform.position - offsetFromTarget;
        }
    }
}
