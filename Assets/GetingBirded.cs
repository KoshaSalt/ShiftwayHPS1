using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetingBirded : MonoBehaviour
{
    public GameObject birdGod;
    public GameObject markerPrefab;
    public Text roadIndicator;
    public int birdCountDownMax = 60;
    public int birdCountDown;
    public float playerMaxBacktrack;
    [ReadOnly]
    public float playerMarkerDistZ;
    [ReadOnly]
    public float playerMarkerDistX;
    private string roadName;
    [ReadOnly]
    public bool isOnRoad;
    [ReadOnly]
    public bool isBacktracking;
    [ReadOnly]
    public bool birdingInitiated;
    private float timeCount;
    private GameObject newMarker;
    private GameObject oldMarker;
    private bool droppingMarker = false;
    
    void Start()
    {
        birdCountDown = birdCountDownMax;
    }

    void Update()
    {
        //Begin dropping the markers for backtrack checking.
        if(droppingMarker == false && isBacktracking == false)
        {
            StartCoroutine(MarkerDropCountdown());
        }

        //Check to make sure player is not too far from newest marker
        //...keeping in mind that newest marker will only spawn while progressing.
        //We could also modify the MaxBacktrack value based on track segment.
        if(newMarker != null)
        {
            playerMarkerDistX = Mathf.Abs(this.transform.position.x - newMarker.transform.position.x);
            playerMarkerDistZ = Mathf.Abs(this.transform.position.z - newMarker.transform.position.z);

            if(playerMarkerDistX + playerMarkerDistZ >= playerMaxBacktrack)
            {
                isBacktracking = true;
            }

            else
            {
                isBacktracking = false;
            }
        }
        
        //When coundown hits zero, if birding hasnt started, bird.
        if (birdCountDown <= 0 && birdingInitiated == false)
        {
            Bird();
        }
        
        //Adds time to countdown. 
        //This occurs 3x faster than it is depleted by backtracking or offroading.
        if(isOnRoad == true && isBacktracking == false)
        {
            roadIndicator.text = (roadName);
            roadIndicator.color = Color.green;

            if(birdCountDown < birdCountDownMax)
            {
                timeCount = timeCount + 1*Time.deltaTime;
            }
        }

        //Subtract from countdown if player is off the road and not backtracking.
        //This is becuase if the player is backtracking and is on the road, we still want to deduct.
        if(isOnRoad == false && isBacktracking == false)
        {
            roadIndicator.text = ("Offroading, Bird Countdown " + birdCountDown.ToString("00"));
            roadIndicator.color = Color.red;
            
            timeCount -= 1*Time.deltaTime;

            birdCountDown = birdCountDownMax + Mathf.FloorToInt(timeCount % 60);
        }

        //Subtract from countdown if player is backtracking.
        //Just like addition to countdown is modified above by multiplying Time.deltaTime by 3
        //...the same could be done here to modify the depletion rate. 
        //Maybe we want to "punish" offroading more heavily than backtracking.
        if(isBacktracking == true)
        {
            roadIndicator.text = ("Backtracking, bird countdown: " + birdCountDown.ToString("00"));
            roadIndicator.color = Color.yellow;
            
            timeCount = timeCount - 1*Time.deltaTime;

            birdCountDown = birdCountDownMax + Mathf.FloorToInt(timeCount % 60);
        }

        //Make sure bird countdown does not exceed max. This isn't an infinite loop right?
        if(birdCountDown > birdCountDownMax)
        {
            birdCountDown--;
        }
    }

    IEnumerator OnTriggerEnter(Collider col)
    {
        yield return new WaitForSeconds(.25f);

        if(col.gameObject.tag == "Road" || col.gameObject.tag == "SafeFromBird")
        {
            isOnRoad = true;
            roadName = col.gameObject.transform.parent.name;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Road" || col.gameObject.tag == "SafeFromBird")
        {
            isOnRoad = false;
        }
    }

    void Bird()
    {
        //Play bird animation
        //Load last checkpoint

        birdingInitiated = true;
        Instantiate(birdGod);
        Debug.LogError("You were unworthy of my death.");
    }

    public void DropMarker()
    {
        Destroy(oldMarker);
        oldMarker = newMarker;
        newMarker = Instantiate(markerPrefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
    }

    IEnumerator MarkerDropCountdown()
    {
        droppingMarker = true;
        
        //Initial marker placement.
        if(oldMarker == null)
        {
            oldMarker = Instantiate(markerPrefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
        }

        if(newMarker == null)
        {
            newMarker = Instantiate(markerPrefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
        }

        //Every five seconds, if the player has progressed, drops a marker and measures distance.
        yield return new WaitForSeconds(5f);
        
        //If player has progressed in the x direction (most of the track), then drop a new marker.
        if(this.transform.position.x >= oldMarker.transform.position.x)
        {
            DropMarker();
        }

        // //If player has not progressed in the X or Z direction, they are probably backtracking...
        // else {isBacktracking = true;}

        //Debug colors and restart coroutine.
        oldMarker.gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
        newMarker.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        droppingMarker = false;
    }
}
