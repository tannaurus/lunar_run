using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {

    // Public
    public GameObject player;
    public int maxDistanceFromPlayer = 80;

    // Private
    private Vector3 lastKnowPlayerPos;

	// Use this for initialization
	void Start () {
        UpdateBlackHolePosition();
        lastKnowPlayerPos = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 playerPos = player.transform.position;
        // Only update the black hole's position if the player is actively moving upward
        if (lastKnowPlayerPos.y < playerPos.y)
        {
            UpdateBlackHolePosition();
        }
        // Update the last know player position for reference next update
        lastKnowPlayerPos = playerPos;
    }

    void UpdateBlackHolePosition()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 blackHolePosition = transform.position;
        // Only update the player's Y position if the rocket is getting out of view.
        if (!(playerPosition.y - blackHolePosition.y < maxDistanceFromPlayer))
        {
            // Update the black whole's position to that of the player, - the maxDistanceFromPlayer value.
            Vector3 updatedBlackHolePosition = player.transform.position - new Vector3(0, maxDistanceFromPlayer, 0);
            updatedBlackHolePosition.x = 0;
            transform.position = updatedBlackHolePosition;
        }

    }
}
