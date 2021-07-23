using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarPath : MonoBehaviour
{
    public GameObject thisCar;

    void Awake()
    {
        thisCar.GetComponent<CarDriverAI>().targetPositionTranform = this.gameObject.transform.GetChild(0);
    }

    public void NextStep()
    {
        if(transform.childCount == 1)
        {
            thisCar.GetComponent<CarDriver>().enabled = false;
            thisCar.GetComponent<CarDriverAI>().enabled = false;
            Destroy(this.gameObject.transform.GetChild(0).gameObject);
        }

        else
        {
            thisCar.GetComponent<CarDriverAI>().targetPositionTranform = this.gameObject.transform.GetChild(1).gameObject.transform;
            Destroy(this.gameObject.transform.GetChild(0).gameObject);
        }
    }
}
