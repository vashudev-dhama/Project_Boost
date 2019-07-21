using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f; // SerializedField allow to change in inspector but not by other scripts ( Public can be change by other scripts as well).
    [SerializeField] float mainThrust = 100f;

    [SerializeField] AudioClip mainEngine; // No need to have a default audio clip. And it'll be used to keep track of audio clip to be played.
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticle; // No need to have a default particle system. And it'll be used to keep track of particle system to be applied.
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem deathParticle;

    Rigidbody rigidBody; //to store and access the reference of rigid body
    AudioSource audioSource; //to store and access the reference of audio source

    enum State {Alive, Dead, Transcending }; // to keep track of current state of player.
    State state = State.Alive;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); // Get me the component of type rigid body by any mean.
        audioSource = GetComponent<AudioSource>(); // Get me the component of type audio source by any mean.
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Alive) // control during scene switching
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
        
    }

    // Called every time a collision occurs
    void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive) // control during scene switching
        {
            return;
        }
        switch (collision.gameObject.tag) //switch on the basis of tag of the game object, we've collided with.
        {
            case "Friendly":
                // Do nothing
                break;
            case "Finish":
                //switch the scene
                StartSuccessSequence();
                break;
            default:
                //kill the player
                StartDeathSequence();
                break;
        }
    }


    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        successParticle.Play();
        audioSource.PlayOneShot(success);
        Invoke("LoadNextScreen", 1f); // LoadNextScreen method will invoke after 1 sec.
    }

    private void StartDeathSequence()
    {
        state = State.Dead;
        audioSource.Stop();
        deathParticle.Play();
        audioSource.PlayOneShot(death);
        Invoke("LoadFirstScreen", 1f);
    }

    private void LoadFirstScreen()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScreen()
    {
        SceneManager.LoadScene(1);
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space)) // Thrusting can occur while rotating too
        {
            ApplyThrust();
        }
        else // To stop playing audio and particle effect if space is not pressed.
        {
            audioSource.Stop();
            mainEngineParticle.Stop();
        }
        
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust); // Apply force relative to rigid body's cordinate system.
                                                             //Vector3.up stands for xyz cordinate system with focus on up i.e., y axis.
        if (audioSource.isPlaying == false) // To avoid layering audio play for continuous space key pressing.
        {
            audioSource.PlayOneShot(mainEngine); // To play the clip posses by mainEngine.
        }
        mainEngineParticle.Play(); // start the particle effect dedicated for thrust
    }

    private void RespondToRotateInput()
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
