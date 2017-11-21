using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float cameraSpeed = 0.1f;
    Camera myCam;

	// Use this for initialization
	void Start () {
        myCam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        myCam.orthographicSize = Screen.height / 90f;

        if(target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, cameraSpeed) + new Vector3(0,0,-10);
        }
	}
}
