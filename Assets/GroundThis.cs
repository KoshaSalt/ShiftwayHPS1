using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundThis : MonoBehaviour
{
    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}
