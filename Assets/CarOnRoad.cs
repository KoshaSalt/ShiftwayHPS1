using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarOnRoad : MonoBehaviour
{
    [ReadOnly]
    public bool carIsOnRoad;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Road" || col.gameObject.tag == "SafeFromBird")
        {
            carIsOnRoad = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Road" || col.gameObject.tag == "SafeFromBird")
        {
            carIsOnRoad = false;
        }
    }
}
