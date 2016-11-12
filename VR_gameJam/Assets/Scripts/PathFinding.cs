using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class PathFinding : MonoBehaviour {
	[SerializeField]
	private Transform destination;
    [SerializeField]
    private Transform LeavingTheShopNode;
	 NavMeshAgent Agent;
	// Use this for initialization
	void Start ()
	{
        Agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<RequestScript>().GetIsCompleted() == true)
        {
            Agent.SetDestination(LeavingTheShopNode.position);
        }
        else
        {
            Agent.SetDestination(destination.position);
        }
	}
}

