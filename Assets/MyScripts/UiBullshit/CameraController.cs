using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public int circleGap;
    public Transform defaultState;
    public Transform slowdownParent;
    public bool isFollowingObj = true;
    public GameObject followingObj;
    public float followDist = 0;

    private Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isFollowingObj)
        {
            float rotationAroundYAxis = Input.GetAxisRaw("Mouse X") * 2; // camera moves horizontally
            float rotationAroundXAxis = -Input.GetAxisRaw("Mouse Y") * 2; // camera moves vertically

            Camera.main.transform.position = followingObj.transform.position;

            Camera.main.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            Camera.main.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <� This is what makes it work!

            Camera.main.transform.Translate(new Vector3(0, 0, -followDist));
            Camera.main.transform.LookAt(followingObj.transform);
        }
        previousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }
}