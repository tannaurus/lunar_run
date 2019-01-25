using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteCameraController : MonoBehaviour {

    public GameObject player;
    public Camera camera;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 camPosition = player.transform.position + offset;
        // Maintain the camera's position at X
        camPosition.x = 0;
        transform.position = camPosition;
	}
}

