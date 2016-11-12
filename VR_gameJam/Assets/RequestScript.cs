using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestScript : MonoBehaviour
{
    [SerializeField]
    bool myIsCompleted;
    [SerializeField]
    bool IsLeaving;
    bool ShouldStartTimer;
    float myLeaveTimer;
    [SerializeField]
    const float LeaveTime = 10.0f;
    [SerializeField]
    GameObject HeldGameObject;
    [SerializeField]
    string myWantedObject;

    Vector3 hitPosition;

    void Start ()
    {
        myIsCompleted = false;
        IsLeaving = false;
        ShouldStartTimer = false;
        myLeaveTimer = LeaveTime;
        HeldGameObject = null;
        int itemIndex = Random.Range(0, 15);
        if(itemIndex == 0)
        {
            myWantedObject = "Carrot";
        }
        else if (itemIndex == 1)
        {
            myWantedObject = "Cucumber";
        }
        else if (itemIndex == 2)
        {
            myWantedObject = "Donut";
        }
        else if (itemIndex == 3)
        {
            myWantedObject = "Dumbbell";
        }
        else if (itemIndex == 4)
        {
            myWantedObject = "Guitar";
        }
        else if (itemIndex == 5)
        {
            myWantedObject = "Knife";
        }
        else if (itemIndex == 6)
        {
            myWantedObject = "NinjaStar";
        }
        else if (itemIndex == 7)
        {
            myWantedObject = "Pie";
        }
        else if (itemIndex == 8)
        {
            myWantedObject = "Pineapple";
        }
        else if (itemIndex == 9)
        {
            myWantedObject = "Bottle";
        }
        else if (itemIndex == 10)
        {
            myWantedObject = "TruckerHat";
        }
        else if (itemIndex == 11)
        {
            myWantedObject = "Wrench";
        }

        GetComponentInChildren<RequestSprite>().SetSprite(myWantedObject);
    }
	
	void Update ()
    {
        if (ShouldStartTimer == true)
        {
            myLeaveTimer -= Time.deltaTime;
            if(myLeaveTimer <= 0.0f)
            {
                IsLeaving = true;
            }
        }
        if(HeldGameObject != null)
        {
            HeldGameObject.GetComponent<Transform>().position = GetComponent<Transform>().position - hitPosition;
        }

        if(myIsCompleted == true)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponentInChildren<SpriteRenderer>().transform.position.Set(hitPosition.x, GetComponentInChildren<SpriteRenderer>().transform.position.y + 0.5f * Time.deltaTime, hitPosition.z);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (myIsCompleted == false)
        {
            if (col.gameObject.tag == myWantedObject)
            {
                HeldGameObject = col.gameObject;
                hitPosition = GetComponent<Transform>().position - col.gameObject.GetComponent<Transform>().position;
                col.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
                GetComponentInChildren<SpriteRenderer>().transform.position = hitPosition;
                myIsCompleted = true;
            }
        }
    }

    public bool GetIsCompleted()
    {
        return myIsCompleted;
    }

    public bool GetIsLeaving()
    {
        return IsLeaving;
    }

    public void StartTimer()
    {
        ShouldStartTimer = true;
    }
}
