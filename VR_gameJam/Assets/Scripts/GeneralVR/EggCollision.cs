using UnityEngine;
using System.Collections;

public class EggCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject EggSplatter;

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        // location at the collisionpoint
        Vector3 position = contact.point;

        // instantiate splatterobjetcr
        // Instantiate(EggSplatter, this.transform.position, transform.rotation);
        GameObject Splatter = Instantiate(EggSplatter);
        Splatter.transform.position = this.transform.position;
        Splatter.transform.LookAt(Camera.main.transform);
        
        Destroy(this.gameObject);
    }
}
