using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class TruckChaseBehavior_SM : MonoBehaviour
{
    #region Declarations
    [Header("Ramming Behavior")]
    public float ramTime;
    public bool ramming;
    public Transform currentTarget;
    public GameObject ramPointPrefab;
    
    [Header("Hangback Behavior")]
    public bool hangingBack;
    public float hangBackTime;

    [Header("Truck Speed")]
    public float currentMaxSpeed;
    public bool isAccelerating;
    public bool isDecelerating;
    public bool speedLocked;

    [Header("Assign These Manually")]
    public Transform leftTarget;
    public Transform rightTarget;
    public Transform returnPoint;
    private GameObject playerCar;
    private GameObject thisTruck;
    #endregion

    #region Behavior tree summary.
    //---A short summary of the truck's behavior tree---
    //Our truck's goal is to ram thye player off the road.
    //It accomplishes this by (1) approaching the player from behind, (2) going to the left or right side of the player, (3) then cutting across in front of the player after a set amount of time.
    //If the truck fails to knock the player off the road, it will (4) fall behind the player, decelletrating to speed 0, then (1) begin the tree anew.
    
    
    //(1) is called Accelerating, it is controlled by the isAccelerating bool. While the truck is accelerating, it is increasing speed until it is at the same speed as the player.
    //The truck stops accelerating if it is at any point Decelerating or Pacing with the player.

    //(2) is called Pacing, it is controlelled by the speedLocked bool. While the truck is pacing, it matches the player speed on the left or right side of the player.
    //While pacing, the truck will not fall behind the player [due to the CarDriverAI script's target] nor will it overtake the player [due to the new lockedSpeed float].
    //The truck begins pacing [and stops accelerating] on collision indicators that are on the left or right side of the player.

    //(3) is called Ramming and it is controlled by a couple lines, namely the isRamming section of the OnTriggerEnter function and the BeginRamming coroutine.
    //While the truck is pacing, a ramTime is counting down in the BeginRamming coroutine. At the end of that countdown, a new target will spawn in front of the player on the opposite side.
    //The truck will then seek that target, thus causing it to cut across in front of the player.

    //(4) is called Hangback and it is controlled by the HangBack coroutine and the hangingBack section of the OnTriggerEnter function.
    //After a failed ramming, the truck will fall back behind the player, decellerating all the while, until it hits a trigger behind the player car.
    //The truck will wait on where that trigger was, a good distance behind the player, for the ammount of time hangBackTime. Afterwhich, RepeatBehavior() is called and the tree begins again.


    
    //These "states" will usually occur in the same order. But, some funky driving may result in the order being switched up.
    //Despire this, the truck will never accelerate in the wrong order, brcause if the player is stopped, behind the truck, or slower than the truck, the truck can only be Decellerating or Pacing.
    //This should prevent any really nasty stuff from happening, such as the truck accelrating into the distance or rippin mad donuts in front of the player.
    #endregion
    
    //Sets truck and player.
    void Start()
    {
        thisTruck = this.transform.parent.gameObject;
        playerCar = thisTruck.GetComponent<CarDriverAI>().targetPositionTranform.parent.gameObject;
    }

    void Update()
    {
        //Display on inspector whether left or right side of car is currently targeted by truck.
        if(thisTruck.GetComponent<CarDriverAI>().targetPositionTranform != null)

        //Sideswitch debug.
        if(Input.GetKeyDown("p"))
        {
            RepeatBehavior();
        }
        
        //First two statements for dealing with wiggle.
        if(playerCar.GetComponent<WheelVehicle>().Speed <= 1)
        {
            thisTruck.GetComponent<CarDriver>().enabled = false;
        }
        if(playerCar.GetComponent<WheelVehicle>().Speed >= 1)
        {
            thisTruck.GetComponent<CarDriver>().enabled = true;
        }

        //If is accelerating, the truck's speed is apporaching the player's speed.
        //The truck is accelerating until it reaches the player's left or right side and begins to pace.
        if(isAccelerating == true)
        {
            //Make sure that the other two aren't happening.
            // isDecelerating = false;
            // speedLocked = false;
            if(thisTruck.GetComponent<CarDriver>().speedMax < Mathf.Abs(playerCar.GetComponent<WheelVehicle>().Speed))
            {
                thisTruck.GetComponent<CarDriver>().speedMax += (Time.deltaTime * 5);
            }
            
            else
            {
                thisTruck.GetComponent<CarDriver>().speedMax = Mathf.Abs(playerCar.GetComponent<WheelVehicle>().Speed);
            }
        }

        //Truck is decelerating after it begins to hang back.
        //Truck will decelerate toward 0, then begin accelerating after it hits 0.
        if(isDecelerating == true)
        {
            isAccelerating = false;
            speedLocked = false;

            thisTruck.GetComponent<CarDriver>().speedMax -= (Time.deltaTime * 10);

            if(thisTruck.GetComponent<CarDriver>().speedMax <= 0)
            {
                isAccelerating = true;
                isDecelerating = false;
                return;
            }
        }

        //The truck will not exceed what was the player's top speed at the time that the truck began to pace.
        //This begins as soon as the truck reaches the left or right side of the player.
        if(speedLocked == true)
        {
            isAccelerating = false;
            isDecelerating = false;

            float lockSpeed = currentMaxSpeed;
            thisTruck.GetComponent<CarDriver>().speedMax = lockSpeed;
        }

        //Inspector info and vars for speedLocked's if statement.
        currentMaxSpeed = thisTruck.GetComponent<CarDriver>().speedMax;
        currentTarget = thisTruck.GetComponent<CarDriverAI>().targetPositionTranform;
    }
    
    //Depending on what the truck collides with, the truck will assume the next behavior in the chain.
    void OnTriggerEnter(Collider col)
    {
        //Pacing with car behavior.
        //Begins when the truck reaches the left or right side of the player car
        if(col.name == "ChaseTargetRight" || col.name == "ChaseTargetLeft")
        {
            //The truck has reached the point alongside the player, pacing with them.
            //After coroutine amount of time, the truck will attempt to sideswipe the car.
            ramming = true;
            speedLocked = true;
            StartCoroutine(BeginRamBehavior());
            Debug.Log("Reached " + col.name + ".");

            //This should toggle before the coroutine is over. By toggling, the truck's speed is locked to the player's.
            //isAccelerating = false;
            ////work on this later
        }

        //Ramming behavior.
        //Begins after the ramTime counts down.
        if(ramming == true)
        {
            if(col.CompareTag("RamTarget"))
            {
                //The truck attempted to sideswipe the player. This sets its new target to a point behind the player.
                thisTruck.GetComponent<CarDriverAI>().targetPositionTranform  = returnPoint;

                //Prepare the truck to hang back after hitting the return point.
                hangingBack = true;
                //This may seem strange to put it here, but we don't want a collision with the return point before the initial side swipe to trigger. 
                //This way, the truck only reacts with the back position after the swipe.

                ramming = false;
                //This bool should probably be in the coroutine, that way ramming can toggle ONLY if the player hasn't moved.
            }
        }

        //Hanging back behavior.
        //Begins after the truck reaches the rear of the player car, after it fails to ram the player
        if(hangingBack == true)
        {
            if(col.name == "ChaseTargetRear" && isAccelerating == false)
            {
                //The truck has crossed in front of the car and returned to behind it.
                //This begins the "hanging back" behavior. The truck will stop for a second before repeating the chase behavior.
                isDecelerating = true;
                    //thisTruck.GetComponent<CarDriver>().speedMax = 0.0001f; //Can't be zero because angular velocity reasons. Bleh.
                Debug.Log("Begin hangback.");
                StartCoroutine(HangBack());
            }
        }
    }

    //Called when the truck begins pacing with a player. After coroutine complets, truck rams player.
    IEnumerator BeginRamBehavior()
    {
        yield return new WaitForSeconds(ramTime);
        if(thisTruck.GetComponent<CarDriverAI>().targetPositionTranform.transform.childCount > 0)
        {
            //Creates ramming targte point.
            GameObject ramPoint = Instantiate(ramPointPrefab);
            ramPoint.transform.position = thisTruck.GetComponent<CarDriverAI>().targetPositionTranform.GetChild(0).transform.position;
            
            //Makes ram point into the target of the car driver ai script.
            thisTruck.GetComponent<CarDriverAI>().targetPositionTranform = ramPoint.transform;
        }
    }

    //Called to create a cooldown time for the truck, after which it will attempt another ram.
    IEnumerator HangBack()
    {
        //This is the amount of time the truck is hagning back before reengaging.
        yield return new WaitForSeconds(hangBackTime);
        thisTruck.GetComponent<CarDriver>().enabled = true;
        
        //Resetting boolz.
        hangingBack = false;
        isAccelerating = true;
        
        //Begin the behaviors again by selecting left or right to acellerate to.
        RepeatBehavior();
    }

    //Called to restart the chace behavior after a failed ram.
    public void RepeatBehavior()
    {
        //Assigning a new target side.
        float targetSide = Random.Range(1, 3);
        Mathf.RoundToInt(targetSide);

        //Left or right? 1, left. 2, right.
        if(targetSide == 1)
        {
            thisTruck.GetComponent<CarDriverAI>().targetPositionTranform = leftTarget;
        }

        if(targetSide == 2)
        {
            thisTruck.GetComponent<CarDriverAI>().targetPositionTranform = rightTarget;
        }

        Debug.Log(targetSide);
    }
}