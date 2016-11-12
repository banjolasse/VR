using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingFanRotation : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float rotSpeed = 100.0f;
        transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
	}
}
