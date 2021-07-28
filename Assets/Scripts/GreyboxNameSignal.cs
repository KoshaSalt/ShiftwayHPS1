using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreyboxNameSignal : MonoBehaviour
{
    public GameObject canvas;
    
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("PlayerCar"))
        {
            canvas.transform.GetChild(2).gameObject.GetComponent<Text>().text = this.name;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("PlayerCar"))
        {
            canvas.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Driving";
        }
    }
}
