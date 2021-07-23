using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteDrive : MonoBehaviour
{
    public GameObject target;
    public GameObject infRoadBoundMin;
    public GameObject infRoadBoundMax;
    public float maxInfiniteRoadLength;
    [ReadOnly]
    public float segmentLength;
    [ReadOnly]
    public Vector3 newTeleporterLocation;
    [ReadOnly]
    public GameObject sceneManager;

    void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
        target.transform.position = new Vector3(infRoadBoundMin.transform.position.x, target.transform.position.y, target.transform.position.z);
    }
    
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("PlayerCar"))
        {
            //Recording infinite road distance
            segmentLength = Mathf.Abs(this.gameObject.transform.position.x - target.transform.position.x);
            sceneManager.GetComponent<Counting>().infiniteRoadLength += segmentLength;
            
            //Teleporting
            col.gameObject.transform.position = new Vector3(target.transform.position.x, col.gameObject.transform.position.y, col.gameObject.transform.position.z);
            Debug.Log("Went through " + this.name + "."); 
            Debug.Log("Arrived at " + target.name + ".");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(sceneManager.GetComponent<Counting>().infiniteRoadLength < maxInfiniteRoadLength)
        {
            newTeleporterLocation = new Vector3(Random.Range(infRoadBoundMin.transform.position.x, infRoadBoundMax.transform.position.x), this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            
            //Make the jump.
            this.gameObject.transform.position = newTeleporterLocation;
            return;
        }

        else
        {
            Debug.Log("You have left the infinite road");
            target.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}