using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    // Public
    public float rotateSpeed = 175f;
    public float thrustSpeed = 50f;

    // Serial
    [Serializable]
    private int fuel = 100;

    // Private
    private AudioSource engine;
    private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        engine = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject)
        {
            // On space press, thrust.
            Thrust();
            // On A/D press, rotate.
            Rotate();
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag) {
            case "Death":
                Destroy(gameObject);
                print("U dead");
                break;
            default:
                print("U good");
                break;
        }
    }

    private void Rotate()
    {
        // Freeze the rotation. This allows us to have greater control.
        rigidBody.freezeRotation = true;

        float rotationSpeed = rotateSpeed * Time.deltaTime;

        // The user can only go in one direction at a time.
        // For that reason, we're using an else if condition to handle rotation movement.
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }
        // Resume rotation
        rigidBody.freezeRotation = false;
    }

    // Invoked in the Update method, handles the thrusting motion of the rocket.
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float thrustForce = thrustSpeed * Time.deltaTime;
            rigidBody.AddRelativeForce(Vector3.up * thrustForce);
            if (!engine.isPlaying)
            {
                engine.Play();
            }
        }
        else
        {
            engine.Stop();
        }
    }
}
