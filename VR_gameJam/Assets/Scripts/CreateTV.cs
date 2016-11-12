using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTV : MonoBehaviour {

    public Transform TV;
    
    // Use this for initialization
    void Start ()
    {
        Instantiate(TV, new Vector3(-3.925577f, 2.812607f, -1.361452f), Quaternion.AngleAxis(45.0f, Vector3.up));
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
