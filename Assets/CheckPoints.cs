using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            this.GetComponentInParent<CheckpointManager>().lastCheckpoint = this.gameObject;
            this.GetComponentInParent<CheckpointManager>().CheckpointCheck();
        }
    }
}
