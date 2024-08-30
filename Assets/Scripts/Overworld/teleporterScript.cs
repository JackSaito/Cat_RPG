using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporterScript : MonoBehaviour
{
    public GameObject otherDoor;
    public Vector3 teleportOffset;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Teleporting");
            float diffX = otherDoor.transform.position.x - transform.position.x;
            float diffY = otherDoor.transform.position.y - transform.position.y;
            float diffZ = otherDoor.transform.position.z - transform.position.z;

            Vector3 destination = new Vector3(diffX + teleportOffset.x, diffY + teleportOffset.y, diffZ + teleportOffset.z);
            other.gameObject.transform.parent.gameObject.transform.position += destination;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            
        }
    }
}
