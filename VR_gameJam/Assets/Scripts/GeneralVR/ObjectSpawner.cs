using UnityEngine;
using System.Collections;

/// <summary>
/// Spawns objects out of thing air when grebing with objects grabing inside collider
/// </summary>
[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private int myObjectIndex;
    [SerializeField]
    private Transform SpawnPoint;

    [SerializeField]
    private Rigidbody ObjectPrefab1;
    [SerializeField]
    private Rigidbody ObjectPrefab2;
    [SerializeField]
    private Rigidbody ObjectPrefab3;
    [SerializeField]
	private Rigidbody ObjectPrefab4;
	[SerializeField]
	private Rigidbody ObjectPrefab5;
    [SerializeField]
    private Rigidbody ObjectPrefab6;
    [SerializeField]
    private Rigidbody ObjectPrefab7;
    [SerializeField]
    private Rigidbody ObjectPrefab8;
    [SerializeField]
    private Rigidbody ObjectPrefab9;
    [SerializeField]
    private Rigidbody ObjectPrefab10;
    [SerializeField]
    private Rigidbody ObjectPrefab11;
    [SerializeField]
    private Rigidbody ObjectPrefab12;
    [SerializeField]
    private Rigidbody ObjectPrefab13;
    [SerializeField]
    private Rigidbody ObjectPrefab14;
    [SerializeField]
    private Rigidbody ObjectPrefab15;
    [SerializeField]
    private Rigidbody ObjectPrefab16;

    [SerializeField]
    private bool SpawnObjectInOrigo;
    [SerializeField]
    private Pickupable PickupablePrefab;
    [SerializeField]
    private bool SpawnPickupableInOrigo;

    void Start()
    { }

    public Rigidbody RequestObjectInstance(Transform grabberTrans)
    {
        //Failsafe
        if (ObjectPrefab1 == null)
            return null;
        if (ObjectPrefab2 == null)
            return null;
        if (ObjectPrefab3 == null)
            return null;
		if (ObjectPrefab4 == null)
			return null;
		if (ObjectPrefab5 == null)
			return null;
        if (ObjectPrefab6 == null)
            return null;
        if (ObjectPrefab7 == null)
            return null;
        if (ObjectPrefab8 == null)
            return null;
        if (ObjectPrefab9 == null)
            return null;
        if (ObjectPrefab10 == null)
            return null;
        if (ObjectPrefab11 == null)
            return null;
        if (ObjectPrefab12 == null)
            return null;
        if (ObjectPrefab13 == null)
            return null;
        if (ObjectPrefab14 == null)
            return null;
        if (ObjectPrefab15 == null)
            return null;
        if (ObjectPrefab16 == null)
            return null;


        //Decide Spawnpoint
        Transform tempSpawnPoint;
        if (SpawnObjectInOrigo == true)
        {
            tempSpawnPoint = grabberTrans;
        }
        else
        {
            tempSpawnPoint = SpawnPoint;
        }

        //Randomize Object
        myObjectIndex = Random.Range(0, 16);

        //Spawn Object
        if (myObjectIndex == 0)
        {
            return (Rigidbody)Instantiate(ObjectPrefab1, grabberTrans.position, tempSpawnPoint.rotation);
        }
        else if (myObjectIndex == 1)
        {
            return (Rigidbody)Instantiate(ObjectPrefab2, grabberTrans.position, tempSpawnPoint.rotation);
        }
        else if (myObjectIndex == 2)
        {
            return (Rigidbody)Instantiate(ObjectPrefab3, grabberTrans.position, tempSpawnPoint.rotation);
        }
		else if (myObjectIndex == 3)
		{
			return (Rigidbody)Instantiate(ObjectPrefab4, grabberTrans.position, tempSpawnPoint.rotation);
		}	
		else if (myObjectIndex == 4)
		{
			return (Rigidbody)Instantiate(ObjectPrefab5, grabberTrans.position, tempSpawnPoint.rotation);
		}
        else if (myObjectIndex == 5)
        {
            return (Rigidbody)Instantiate(ObjectPrefab6, grabberTrans.position, tempSpawnPoint.rotation);
        }
        else if (myObjectIndex == 6)
        {
            return (Rigidbody)Instantiate(ObjectPrefab7, grabberTrans.position, tempSpawnPoint.rotation);
        }
        else if (myObjectIndex == 7)
        {
            return (Rigidbody)Instantiate(ObjectPrefab8, grabberTrans.position, tempSpawnPoint.rotation);
        }
        else if (myObjectIndex == 8)
        {
            return (Rigidbody)Instantiate(ObjectPrefab9, grabberTrans.position, tempSpawnPoint.rotation);
        }
        else if (myObjectIndex == 9)
        {
            return (Rigidbody)Instantiate(ObjectPrefab10, grabberTrans.position, tempSpawnPoint.rotation);
        }
        else if (myObjectIndex == 10)
        {
            return (Rigidbody)Instantiate(ObjectPrefab11, grabberTrans.position, tempSpawnPoint.rotation);
        }
        else if (myObjectIndex == 11)
        {
            return (Rigidbody)Instantiate(ObjectPrefab12, grabberTrans.position, tempSpawnPoint.rotation);
        }
        else if (myObjectIndex == 12)
        {
            return (Rigidbody)Instantiate(ObjectPrefab13, grabberTrans.position, tempSpawnPoint.rotation);
        }
        else if (myObjectIndex == 13)
        {
            return (Rigidbody)Instantiate(ObjectPrefab14, grabberTrans.position, tempSpawnPoint.rotation);
        }
        else if (myObjectIndex == 14)
        {
            return (Rigidbody)Instantiate(ObjectPrefab15, grabberTrans.position, tempSpawnPoint.rotation);
        }
        else if (myObjectIndex == 15)
        {
            return (Rigidbody)Instantiate(ObjectPrefab15, grabberTrans.position, tempSpawnPoint.rotation);
        }

        //If no object was found with the given index
        return null;
    }
    public Pickupable RequestPickupableInstance(Transform grabberTrans)
    {
        if (PickupablePrefab == null)
            return null;

        if (SpawnPickupableInOrigo)
            return (Pickupable)Instantiate(PickupablePrefab, grabberTrans.position, grabberTrans.rotation);
        else
            return (Pickupable)Instantiate(PickupablePrefab, SpawnPoint.position, SpawnPoint.rotation);
    }
}
