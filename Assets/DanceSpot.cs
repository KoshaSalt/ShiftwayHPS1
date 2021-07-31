using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceSpot : MonoBehaviour
{
    [ReadOnly]
    public bool isFinal;
    public void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            if(isFinal == false)
            {
                Debug.Log("Arrived at " + this.name + ".");
                this.transform.parent.GetComponent<CarDancing>().onArrive();
                this.gameObject.SetActive(false);
                //playOnArrival
            }

            if(isFinal == true)
            {
                Debug.Log("Completed the car dance");
                this.gameObject.SetActive(false);
            }
        }
    }
}
