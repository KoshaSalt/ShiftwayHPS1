using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviorToggle : MonoBehaviour
{
    public GameObject truck;
    [ReadOnly]
    public bool isDecel;
    public float startSpeedMax;
    
    void Start()
    {
        truck = this.transform.parent.gameObject;
        startSpeedMax = truck.GetComponent<CarDriver>().speedMax;
    }

    void Update()
    {
        if(isDecel == true)
        {
            truck.GetComponent<CarDriver>().speedMax -= Time.deltaTime * 3;
            if(truck.GetComponent<CarDriver>().speedMax < 0)
            {
                truck.GetComponent<CarDriver>().speedMax = 0.001f;
            }
        }

        else
        {
            truck.GetComponent<CarDriver>().speedMax = startSpeedMax;
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("PlayerCar"))
        {
            //truck.GetComponent<CarDriver>().enabled = false;
            Debug.Log("Truck has reached its target.");
            isDecel = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("PlayerCar"))
        {
            //truck.GetComponent<CarDriver>().enabled = true;
            Debug.Log("Truck resuming chase.");
            isDecel = false;
        }
    }
}
