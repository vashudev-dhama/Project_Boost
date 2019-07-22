using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] // To restrict from adding multiple same scripts to single object.
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(5f, 5f, 5f);
    [SerializeField] float period = 2f;

    float movementFactor;
    Vector3 startingPos; // to keep track of position of game object.

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position; // fetch the position of the game object.
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) { return; } // protect from divide by 0, mathf.epsilon is smallest float possible.
        float cycles = Time.time / period; // calculate cycles which will grow continually from 0;
        const float tau = Mathf.PI * 2f; // as Tau is equal to 2x pie
        float rawSinWave = Mathf.Sin(cycles * tau); //calculate a value between -1 to +1;

        movementFactor = rawSinWave / 2f + 0.5f;
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset; // update the position every time when update() is called.
    }
}
