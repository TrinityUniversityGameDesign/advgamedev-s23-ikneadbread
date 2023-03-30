using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class book : MonoBehaviour
{
    [SerializeField] float pageSpeed = 0.5f;
    [SerializeField] List<Transform> pages;
    int index = -1;
    bool rotate = false;


    public void RotateForward()
    {
        if (rotate == true)
        {
            return;
        }
        index++;
        //in oder to rotate the page forward, you need to set the rotation to 180 degrees around the y axis
        float angle = 180;
        pages[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, true));
    }

    public void RotateBack()
    {
        if (rotate == true)
        {
            return; 
        } 
        //in order to rotate the page back, you need to set the rotation to 0 degrees around the y axis
        float angle = 0;
        pages[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, false));
    }

    IEnumerator Rotate(float angle, bool forward)
    {
        float value = 0f;
        while (true)
        {
            rotate = true; 
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            value += Time.deltaTime * pageSpeed;
            pages[index].rotation = Quaternion.Slerp(pages[index].rotation, targetRotation, value);
            float angle1 = Quaternion.Angle(pages[index].rotation, targetRotation);
            if (angle1 < 0.1f)
            {
                if (forward == false)
                {
                    index--;
                }
                rotate = false;
                break;
            }
            yield return null;
        }
    }
}
