using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rig;
     float moveSpeed = 10f;
     bool useTorque = true;
 
     void Start()
     {
         rig = GetComponent<Rigidbody>();
     }
     public void Control()
     {
         if (useTorque)
         {
             rig.AddTorque(new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")) * moveSpeed);
         }
     }
     void FixedUpdate()
     {
         Control();
     }
}  