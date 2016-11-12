using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_static : MonoBehaviour {

    public Texture[] textures;
    float timer = 0;
    float interval = 0.15f;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (timer < interval)
        {
            GetComponent<MeshRenderer>().material.mainTexture = textures[0];
        }
        else if (timer < interval * 2.0f)
        {
            GetComponent<MeshRenderer>().material.mainTexture = textures[1];
        }
        else if (timer < interval * 3.0f)
        {
            GetComponent<MeshRenderer>().material.mainTexture = textures[2];
        }
        else
        {
            timer = 0;
        }
        timer += Time.deltaTime;
	}
}
