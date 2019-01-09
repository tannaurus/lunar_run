using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public GameObject asteroid;
    public GameObject fuel;
    public int asteroidCount = 75;
    public int fuelCount = 15;

	// Use this for initialization
	void Start () {
        Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Spawn ()
    {
        // Spawn asteroids
        for(int i = 0; i < asteroidCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-30f, 30f), Random.Range(20f, Constants.THERMOSPHERE), 0);
            Instantiate(asteroid, position, Random.rotation);
        }
        for(int i = 0; i < fuelCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-30f, 30f), Random.Range(20f, Constants.THERMOSPHERE), 0);
            Instantiate(fuel, position, Random.rotation);
        }
    }
}
