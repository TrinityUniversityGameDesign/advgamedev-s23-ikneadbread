using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distance = 3.0f;
    [SerializeField] private float height = 3.0f;
    [SerializeField] private float rotationDamping = 3.0f;

    private bool isFirstPersonView = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            isFirstPersonView = !isFirstPersonView;
        }
    }

    private void LateUpdate()
    {
        if (!isFirstPersonView)
        {
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
            transform.position = player.position + new Vector3(0, height, 0);
            transform.rotation = player.rotation;
        }
    }

    public void SwitchView()
    {
        isFirstPersonView = !isFirstPersonView;
    }
}