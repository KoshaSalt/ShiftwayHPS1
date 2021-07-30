using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarOnRoad : MonoBehaviour
{
    [ReadOnly]
    public bool carIsOnRoad;

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Road") || col.CompareTag("SafeFromBird"))
        {
            carIsOnRoad = true;
        }
    }

    IEnumerator OnTriggerExit(Collider col)
    {
        yield return new WaitForSeconds(5);
        if(col.CompareTag("Road") || col.CompareTag("SafeFromBird"))
        {
            carIsOnRoad = false;
            Debug.Log("Left road");
        }
    }
}
