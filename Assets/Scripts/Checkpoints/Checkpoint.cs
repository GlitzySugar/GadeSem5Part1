using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public CheckpointManager manager; // Assign in Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.CheckpointReached(this);
            Destroy(gameObject);
        }
    }
}
