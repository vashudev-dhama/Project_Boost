using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            print("Thrusting");
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
