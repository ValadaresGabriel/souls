using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointObject : MonoBehaviour
{
    [SerializeField] private Transform checkpoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            CheckpointManager.Instance.SetCheckpoint(checkpoint.position);
        }
    }
}
