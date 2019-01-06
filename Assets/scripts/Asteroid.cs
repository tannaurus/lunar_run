using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public GameObject asteroid;

	// Use this for initialization
	void Start () {
        Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Spawn ()
    {
        for(int i = 0; i < 50; i++)
        {
            Vector3 position = new Vector3(Random.Range(-30f, 30f), Random.Range(20f, 100f), 0);
            Instantiate(asteroid, position, Random.rotation); //as GameObject;
        }
    }
}
