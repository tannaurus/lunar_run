﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocket : MonoBehaviour {

    // Public
    public float rotateSpeed = 175f;
    public float thrustForce = 650f;
    public float fuelBoostForce = 1000f;
    public Rigidbody projectile;

    // UI
    public Slider fuelSlider;
    public Text distanceText;

    // Serial
    [SerializeField]
    private float maxFuel = 1000f;
    private float fuel = 10000f;
    private float baseMass = 1f;

    // Private
    private AudioSource engine;
    private Rigidbody rigidBody;
    private bool canShoot = true;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        engine = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // We have to be constantly adjusting mass in order to get the 
        // correct mass value based on the ever changing altitude of the rocket.
        AdjustMass();           
        // Update the UI
        UpdateUI();
        // If our rocket hasn't blown up, allow the user to control it.
        if (gameObject)
        {
            // On space press, thrust.
            Thrust();
            // On A/D press, rotate.
            Rotate();
            // On F press, fire.
            FireProjectile();
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag) {
            case "Death":
                SceneManager.LoadScene("Level 1");
                break;
            default:
                print("U good");
                break;
        }
    }

    void OnTriggerEnter(Collider collider) {
        print(collider.gameObject);
        switch(collider.gameObject.tag)
        {
            case "Fuel":
                Destroy(collider.gameObject);            
                // Adjust the thrust force we've calculated by the delta time.
                float adjustedBoostForce = fuelBoostForce * Time.deltaTime;
                rigidBody.AddRelativeForce(Vector3.up * adjustedBoostForce);
                fuel = fuel + 100f;
                break;
            default:
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
        // If they are pressing the space key and the rocket still has fuel, give it some gas.
        if (Input.GetKey(KeyCode.Space) && fuel != 0f)
        {
            // Decrement fuel.
            fuel--;
            // Adjust the thrust force we've calculated by the delta time.
            float adjustedThrustForce = thrustForce * Time.deltaTime;
            // Add the adjusted force to our rocket
            rigidBody.AddRelativeForce(Vector3.up * adjustedThrustForce);

            // If the engine noise isn't playing already, play it.
            // This prevents the layering of audio.
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

    private void FireProjectile()
    {
        if (Input.GetKey(KeyCode.F) && canShoot)
        {
            // Instantiate a new projectile
            Rigidbody newProjectile = (Rigidbody) Instantiate(projectile, transform.position, transform.rotation);
            // Set its velocity relative to the rocket's
            newProjectile.velocity = transform.forward * 10f;
            // Add some force to get that bb moving
            newProjectile.AddRelativeForce(Vector3.up * 1500f);
            canShoot = false;
            StartCoroutine(ShootDelay());
        }
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(3);
        canShoot = true;
    }

    private void AdjustMass()
    {

        float normilizedFuelLevel = GetNormilizedFuelLevel();
        // The funciton GetNormilizedAltitude can return a value above 1f.
        // For this use case, we only care if the user is still in the atomosphere.
        // Any number above 1 means they are mass-less.
        float ultraNormilizedAltitude = Mathf.Clamp(GetNormilizedAltitude(), 0f, 1f);
        // Start calculating the mass:
        // As we get closer to leaving the atmosphere, normilizedAltitude will also get closer to 1 (our baseMass value)
        // Meaning the closer we get to the leaving the atmosphere, our mass will drop considerably.
        float mass = baseMass - ultraNormilizedAltitude;
        // We don't want our fuel level to effect our rocket too much.
        // TODO - 
        // mass = mass - normilizedFuelLevel / 10;
        // We always want the rocket to have a little bit of mass.
        mass = Mathf.Clamp(mass, 0.2f, 1f);
        rigidBody.mass = mass;
    }

    private void UpdateUI()
    {
        // The slider expects a normilized value.
        fuelSlider.value = GetNormilizedFuelLevel();
        distanceText.text = "You've traveled " + rigidBody.transform.position.y;
    }

    // Gives us a value between 0 and 1.
    // Where 1 == a full tank && 0 == an empty tank.
    private float GetNormilizedFuelLevel()
    {        
        return fuel / maxFuel;
    }

    // An effort to normilize the rocket's altitude.
    // < 1 == they are still in the atmosphere
    // > 1 == they are outside of the atomosphere
    private float GetNormilizedAltitude()
    {        
        return Mathf.Clamp(rigidBody.transform.position.y / Constants.OUTER_SPACE, 0f, Mathf.Infinity);
    }
}
