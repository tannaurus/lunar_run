using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public GameObject asteroid;
    public int asteroidCount = 75;

	// Use this for initialization
	void Start () {
        Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Spawn ()
    {
        for(int i = 0; i < asteroidCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-30f, 30f), Random.Range(20f, Constants.THERMOSPHERE), 0);
            Instantiate(asteroid, position, Random.rotation);
        }
    }
}
