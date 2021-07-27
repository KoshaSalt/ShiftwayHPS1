using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLightEpisode : MonoBehaviour
{
    public GameObject redLight;
    public GameObject parade;
    public float waitTime;

    void Start()
    {
        parade.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("PlayerCar"))
        {
            parade.SetActive(true);
            redLight.GetComponent<Renderer>().material.color = Color.red;
            StartCoroutine(RedLightWait());
        }
    }

    IEnumerator RedLightWait()
    {
        yield return new WaitForSeconds(waitTime);
        redLight.GetComponent<Renderer>().material.color = Color.green;
    }
}
