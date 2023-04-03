using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public float pickupDistance = 2.0f;

    public Camera cam;
    private Rigidbody pickedObject;
    private float pickupTime;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, pickupDistance))
            {
                if (hit.rigidbody != null)
                {
                    pickedObject = hit.rigidbody;
                    pickedObject.isKinematic = true;
                    pickupTime = Time.time;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (pickedObject != null)
            {
                pickedObject.isKinematic = false;
                pickedObject = null;
            }
        }

        if (pickedObject != null)
        {
            Vector3 newPosition = cam.transform.position + cam.transform.forward * pickupDistance;
            float elapsedTime = Time.time - pickupTime;
            float t = Mathf.Clamp01(elapsedTime / 0.5f);
            pickedObject.MovePosition(Vector3.Lerp(pickedObject.position, newPosition, t));
        }
    }
}
