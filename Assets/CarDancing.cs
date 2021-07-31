using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDancing : MonoBehaviour
{
    [ReadOnly]
    public GameObject nextSpot;
    public GameObject playerCar;
    public GameObject [] dance; 
    [ReadOnly]
    public AudioSource [] arrivalClips;

    
    void Start()
    {
        foreach(GameObject spots in dance)
        {
            spots.SetActive(false);
        }

        nextSpot = dance[0];
        nextSpot.SetActive(true);
    }

    void Update()
    {
        
    }

    public void onArrive()
    {
        int clipNum = System.Array.IndexOf(dance, nextSpot);
        clipNum++;
        nextSpot = dance[clipNum];
        nextSpot.SetActive(true);

        if(clipNum == 5)
        {
            nextSpot.GetComponent<DanceSpot>().isFinal = true;
        }

        //arrivalClips[clipNum].Play();
        
    }
}
