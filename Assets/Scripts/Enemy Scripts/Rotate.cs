using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotateRate = 1.0f;
    float rotationtarget;
    
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(45f, rotationtarget, 45f); 
        rotationtarget += rotateRate;
    }
}
