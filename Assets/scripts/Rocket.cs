using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput()
    {
        // On space press
        if (Input.GetKey(KeyCode.Space))
        {
            print("adding force");
            rigidBody.AddRelativeForce(Vector3.up);
        }

        // The user can only go in one direction at a time.
        // For that reason, we're using an else if condition to handle rotation movement.
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        } 
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }
    }
}
