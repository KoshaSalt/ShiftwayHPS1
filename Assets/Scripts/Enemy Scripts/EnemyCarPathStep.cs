using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarPathStep : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "EnemyCar")
        {
            this.gameObject.transform.GetComponentInParent<EnemyCarPath>().NextStep();
        }
    }
}
