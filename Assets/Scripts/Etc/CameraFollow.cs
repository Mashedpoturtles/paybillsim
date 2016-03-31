using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    Camera myCam;

	// Use this for initialization
	void Start () {
        myCam = gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        myCam.orthographicSize = (Screen.height / 100f) / 0.3f;

        if (target)
            transform.position = Vector3.Lerp(transform.position,target.position,0.1f) + new Vector3(0,1, -10);

	}



}
