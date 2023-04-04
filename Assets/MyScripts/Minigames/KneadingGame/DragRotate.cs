using UnityEngine;

public class DragRotate : MonoBehaviour
{
    private Vector3 dragStartPosition;
    private Quaternion initialRotation;
    private float rotationX;
    private bool isDragging = false;

    void OnMouseDown()
    {
        dragStartPosition = Input.mousePosition;
        initialRotation = transform.rotation;
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            float angle = Mathf.Atan2(mousePosition.y - dragStartPosition.y, mousePosition.x - dragStartPosition.x) * Mathf.Rad2Deg;
            Quaternion newRotation = Quaternion.Euler(0f, angle, 0f) * initialRotation;
            if (Quaternion.Angle(initialRotation, newRotation) >= 360f)
            {
                isDragging = false;
                return;
            }
            transform.rotation = newRotation;
        }
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }
}
