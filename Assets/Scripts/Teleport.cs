using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // Copy x and z axis of teleport target to player's position
            Vector3 newPosition = new Vector3(teleportTarget.transform.position.x,
                                               player.transform.position.y,
                                               teleportTarget.transform.position.z);
            player.transform.position = newPosition;
        }
    }
}
