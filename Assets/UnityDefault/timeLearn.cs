using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeLearn : MonoBehaviour
{
    public enum stateMachine {state1, state2, state3}
    public float timer;
    
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 60f && timer < 80f)
        {
             
        }
    }
}
