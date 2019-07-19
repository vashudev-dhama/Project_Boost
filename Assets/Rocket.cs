using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody; //to store and access the reference of rigid body
    AudioSource audioSource; //to store and access the reference of audio source
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); // Get me the component of type rigid body by any mean.
        audioSource = GetComponent<AudioSource>(); // Get me the component of type audio source by any mean.
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) // Thrusting can occur while rotating too
        {
            if (audioSource.isPlaying == false) // To avoid layering audio play for continuous space key pressing.
            {
                audioSource.Play();
            }
            rigidBody.AddRelativeForce(Vector3.up); // Apply force relative to rigid body's cordinate system.
                                                    //Vector3.up stands for xyz cordinate system with focus on up i.e., y axis.
        }
        else // To stop playing audio if space is not pressed.
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // control rotation manually
        //Right or left rotation can't occur at same time
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward); // Rotate transform along z axis i.e., Vector3.forward.
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward); // Rotate transform along z axis i.e., Vector3.forward in opposite direction.
        }
        rigidBody.freezeRotation = false; // resume physics control of rotation
    }

    
}
