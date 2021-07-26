using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class PlacingCactus : MonoBehaviour
{
    public bool isGrounded;
    public float distToGround;
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            this.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
