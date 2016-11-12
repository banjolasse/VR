using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    const float myMaxTime = 120.0f;
    float myTimeLeft;

	void Start ()
    {
        myTimeLeft = myMaxTime;
        GetComponent<Slider>().maxValue = myMaxTime;
    }
	
	void Update ()
    {
        myTimeLeft -= Time.deltaTime;
        GetComponent<Slider>().value = myTimeLeft;
	}

    float GetTime()
    {
        return myTimeLeft;
    }
}
