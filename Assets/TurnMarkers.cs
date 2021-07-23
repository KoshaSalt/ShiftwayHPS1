using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TurnMarkers : MonoBehaviour
{
    public bool debugging;
    
    void Start()
    {
        if(debugging == true)
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }

        else
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
