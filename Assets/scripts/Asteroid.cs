using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    // Public
    public int explosionForce = 10;
    public float explosionRadius = 30.0f;

    // Private
    private Rigidbody asteroid;
    private Vector3 scaleRef;
    private Vector3[] piecePositions = new Vector3[4];

	// Use this for initialization
	void Start ()
    {
        asteroid = GetComponent<Rigidbody>();
        scaleRef = asteroid.transform.localScale;
        asteroid.mass = scaleRef.x / 2;
    }

    void Update ()
    {
        ForceZAxis();
    }

    void ForceZAxis ()
    {
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }

    void BuildPiecePosArr()
    {
        Vector3 posRef = transform.position;
        // Each Vector3 is based on the original position but modified slightly
        piecePositions[0] = posRef + new Vector3(-5, 6, 0);
        piecePositions[1] = posRef + new Vector3(5, 6, 0);
        piecePositions[2] = posRef + new Vector3(5, -6, 0);
        piecePositions[3] = posRef + new Vector3(-5, -6, 0);
    }

    void GenPiece(int pieceCount) {
        // Create a new scale that is always at least 1/2 the original scale.
        float newScaleSize = Random.Range(scaleRef.x / 2.5f, scaleRef.x / 2f);
        Vector3 newScale = new Vector3(newScaleSize, newScaleSize, newScaleSize);
        transform.localScale = newScale;
        Rigidbody clone = Instantiate(asteroid, piecePositions[pieceCount], Random.rotation);
    }


    void OnExplosion()
    { 
        // When the explosion occurs, build 4 positions that will dictate where the pieces will spawn.
        BuildPiecePosArr();
        // Then loop 4 times, each time creating a piece in a specifed location.
        for (int i = 0; i < 4; i++)
        {
            GenPiece(i);
        }
        ExplodeInRadius();
        // Once the pieces spawn, delete the orginal gamePiece.
        Destroy(gameObject);
    }

    void ExplodeInRadius()
    {
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, 0);
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            // When we interact with a projectile, explode.
            case "Projectile":
                {
                    OnExplosion();
                    break;
                }
            // Used for debugging purposes. 
            default:
                print("Nothing happened!");
                break;
        }
    }
}
