using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    // Public variables
    public GameObject asteroid;
    public GameObject fuel;
    public int fuelCount = 15;

    // Private variables
    // Since arrays must be constructed with a specified amount of index,
    private int asteroidsSoFar = 0;
    private int asteroidCount = 150;
    private float[] xPositions = new float[150];
    private float[] yPositions = new float[150];

    // Use this for initialization
    void Start()
    {
        //SpawnAsteroids();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void SpawnAsteroids()
    {
        // USED FOR DEBUGGING ONLY.
        // So we can keep track of how many times this while loop runs while improving the algorithm.
        int debugLoopCount = 0;
        // Spawn asteroids
        while (asteroidsSoFar < asteroidCount && debugLoopCount < 5000)
        {
            debugLoopCount++;
            print(debugLoopCount);
            Vector3 position = new Vector3(Random.Range(-50f, 50f), Random.Range(20f, Constants.OUTER_SPACE), 0);
            // If we can spawn here, go for it. 
            if (CanSpawnHere(15, position))
            {
                TrackCord(position);
                asteroidsSoFar++;
                float randomScale = Random.Range(1f, 5f);
                asteroid.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                Instantiate(asteroid, position, Random.rotation);
            }
        }
        print(xPositions);
        print(asteroidsSoFar);
    }

    void TrackCord(Vector3 cord)
    {
        xPositions[asteroidsSoFar] = cord.x;
        yPositions[asteroidsSoFar] = cord.y;
    }

    // Takes in a distance from other objects and will return a boolean
    // whether or not you can spawn an object at this location while keeping that distance.
    bool CanSpawnHere(int distance, Vector3 cord)
    {
        if (asteroidsSoFar == 0)
        {
            return true;
        }
        // Assume positive vibes.
        bool canSpawn = true;
        // Loop through each of our positions, checking if they are too close to this cord.
        for (int i = 0; i < asteroidsSoFar - 1; i++)
        {
            // If we've already determine the object can't be spawned here, abort.
            if (!canSpawn)
            {
                break;
            }
            float objX = xPositions[i];
            float objY = yPositions[i];
            float xDiff = Mathf.Abs(objX - cord.x);
            float yDiff = Mathf.Abs(objY - cord.y);
            // If the absolute values of the difference between this cord in our array and the provided cord
            // are less than eacher, this cord is too close to another object.
            if (xDiff < distance && yDiff < distance)
            {
                canSpawn = false;
            }
        }
        // Return our findings.
        return canSpawn;
    }



    void SpawnFuel()
    {
        for (int i = 0; i < fuelCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-30f, 30f), Random.Range(20f, Constants.THERMOSPHERE), 0);
            Instantiate(fuel, position, Random.rotation);
        }
    }
}
