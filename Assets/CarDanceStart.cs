using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CarDanceStart : MonoBehaviour
{

    private PlayableDirector director;

    void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    public void StartTimeline()
    {
        director.Play();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            StartTimeline();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            Debug.Log("Left the dancefloor.");
        }
    }
}
