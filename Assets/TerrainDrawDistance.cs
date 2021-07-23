using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDrawDistance : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<Terrain>().enabled = false;
    }
    
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            this.GetComponent<Terrain>().enabled = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            this.GetComponent<Terrain>().enabled = false;
        }
    }
}
