using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float cameraSpeed = 0.1f;
    Camera myCam;
    private StateHandler state;
    // Use this for initialization
    void Start () {
        state = (StateHandler)GameObject.FindWithTag("State Machine").GetComponent(typeof(StateHandler));
        transform.position = state.PartyPosition;
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
