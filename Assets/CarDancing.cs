using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CarDancing : MonoBehaviour
{
    [ReadOnly]
    public float danceCountdown = 15f;
    [ReadOnly]
    public GameObject nextSpot;
    [ReadOnly]
    public bool danceHasBegun;
    public GameObject playerCar;
    public AudioSource lightOnFX;
    public GameObject [] dance; 
    public AudioSource danceAudioSource;

    void Awake()
    {
        foreach(GameObject spots in dance)
        {
            spots.SetActive(false);
        }

        nextSpot = dance[0];
    }

    void Update()
    {
        danceCountdown -= Time.deltaTime;
    }

    public void onArrive(AudioClip arrivalClip)
    {
        int clipNum = System.Array.IndexOf(dance, nextSpot);
        clipNum++;
        nextSpot = dance[clipNum];
        nextSpot.SetActive(true);

        danceAudioSource.clip = arrivalClip;
        lightOnFX.PlayOneShot(lightOnFX.clip);
        danceAudioSource.PlayScheduled(lightOnFX.clip.length + 3f);
    }


}
