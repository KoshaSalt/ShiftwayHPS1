using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoooyndDetector : MonoBehaviour
{
    public float detectorRadius;

    void Awake()
    {
        this.gameObject.GetComponent<SphereCollider>().radius = detectorRadius;
        GetComponentInParent<NooyndChase>().isPursuing = false;
    }
    
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<NooyndChase>().isPursuing = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<NooyndChase>().isPursuing = false;
        }
    }
}
