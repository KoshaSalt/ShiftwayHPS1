using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceSpot : MonoBehaviour
{
    public bool isFinal;
    public AudioClip arrivalClip;
    public void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {            
            if(isFinal == false)
            {
                Debug.Log("Arrived at " + this.name + ".");
                this.transform.parent.GetComponent<CarDancing>().onArrive(arrivalClip);
                this.gameObject.SetActive(false);
            }

            if(isFinal == true)
            {
                Debug.Log("Completed the car dance");
                this.gameObject.SetActive(false);
            }
        }
    }
}
