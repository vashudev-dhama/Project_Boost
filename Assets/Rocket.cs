using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody; //to store and access the reference of rigid body
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); // Get me the component of type rigid body by any mean.
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space)) // Thrusting can occur while rotating too
        {
            rigidBody.AddRelativeForce(Vector3.up); // Apply force relative to rigid body's cordinate system.
            //Vector3.up stands for xyz cordinate system with focus on up i.e., y axis.
        }
        //Right or left rotation can't occur at same time
        if (Input.GetKey(KeyCode.A))
        {
            print("Rotate Left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            print("Rotate Right");
        }
    }
}
