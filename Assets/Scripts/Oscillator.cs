using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] // To restrict from adding multiple same scripts to single object.
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [Range(0, 1)] [SerializeField] float movementFactor; // 0 for not moved, 1 for fully moved.

    Vector3 startingPos; // to keep track of position of game object.

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position; // fetch the position of the game object.
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
