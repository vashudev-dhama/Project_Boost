using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f; // SerializedField allow to change in inspector but not by other scripts ( Public can be change by other scripts as well).
    [SerializeField] float mainThrust = 100f;
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

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) //switch on the basis of tag of the game object, we've collided with.
        {
            case "Friendly":
                // Do nothing
                break;
            case "Finish":
                //switch the scene
                print("Hit finish");
                SceneManager.LoadScene(1);
                break;
            default:
                //kill the player
                SceneManager.LoadScene(0);
                break;
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) // Thrusting can occur while rotating too
        {
            if (audioSource.isPlaying == false) // To avoid layering audio play for continuous space key pressing.
            {
                audioSource.Play();
            }
            rigidBody.AddRelativeForce(Vector3.up * mainThrust); // Apply force relative to rigid body's cordinate system.
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

        
        float rotationThisFrame = rcsThrust * Time.deltaTime; //longer the frame time larger will be the thrust.

        //Right or left rotation can't occur at same time
        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(Vector3.forward * rotationThisFrame); // Rotate transform along z axis i.e., Vector3.forward.
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame); // Rotate transform along z axis i.e., Vector3.forward in opposite direction.
        }
        rigidBody.freezeRotation = false; // resume physics control of rotation
    }

    
}
