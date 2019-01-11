using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    // Private
    private Rigidbody asteroidClone;
    private Vector3 scaleRef;
    private Vector3[] piecePositions = new Vector3[4];

	// Use this for initialization
	void Start () {
        scaleRef = gameObject.transform.localScale;
        asteroidClone = GetComponent<Rigidbody>();
        BuildPiecePosArr();
    }

    Vector3 GetRandomDir()
    {
        // Generate a random number which we'll use to select a random direction
        float randomNumber = Mathf.Floor(Random.Range(0f, 4f));
        if (randomNumber == 0f)
        {
            return Vector3.up;
        } 
        else if (randomNumber == 1f)
        {
            return Vector3.right;
        }
        else if (randomNumber == 2f)
        {
            return Vector3.down;
        }
        else if (randomNumber == 3f)
        {
            return Vector3.left;
        }
        else
        {
            print("Hit default");
            return Vector3.up;
        }
    }

    void BuildPiecePosArr()
    {
        Vector3 posRef = gameObject.transform.position;
        // Each Vector3 is based on the original position but modified slightly
        piecePositions[0] = posRef + new Vector3(5, 0, 0);
        piecePositions[1] = posRef + new Vector3(-5, 0, 0);
        piecePositions[2] = posRef + new Vector3(5, 5, 0);
        piecePositions[3] = posRef + new Vector3(-5, -5, 0);

    }

    void GenPiece(int pieceCount) {
        // Create a new scale that is always at least 1/2 the original scale.
        float newScaleSize = Random.Range(scaleRef.x / 8f, scaleRef.x / 2f);
        Vector3 newScale = new Vector3(newScaleSize, newScaleSize, newScaleSize);
        asteroidClone.transform.localScale = newScale;
        Rigidbody clone = Instantiate(asteroidClone, piecePositions[pieceCount], Random.rotation);
        // Get a random direction to send the piece in.
        Vector3 dir = GetRandomDir();
        // Apply a healthy amout of force to the new piece.
        clone.AddRelativeForce(dir * 1500f);
    }


    void OnExplosion()
    { 
        for (int i = 0; i < 4; i++)
        {
            GenPiece(i);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Projectile":
                {
                    OnExplosion();
                    break;
                }
            default:
                print("Nothing happened!");
                break;
        }
    }
}
