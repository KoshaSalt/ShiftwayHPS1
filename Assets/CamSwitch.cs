using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamSwitch : MonoBehaviour
{
    public int startingPriority;
    
    void Start()
    {
        startingPriority = this.GetComponentInParent<CinemachineVirtualCamera>().Priority;
    }
    
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            this.GetComponentInChildren<CinemachineVirtualCamera>().Priority = 30;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            this.GetComponentInChildren<CinemachineVirtualCamera>().Priority = startingPriority;
        }
    }
}
