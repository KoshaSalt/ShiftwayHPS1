using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using VehicleBehaviour;

//This could/should be renamed CarManager or DriverManager or something.

public class GetInOutCar : MonoBehaviour
{
    public Transform cameraParent;
    public GameObject interCamera;
    public GameObject playerCamera;
    [ReadOnly]
    public Transform frontSeat;
    [ReadOnly]
    public Transform outsideDoor;
    [ReadOnly]
    public Transform targetCar;
    [ReadOnly]
    public float camSwitchTime;
    [ReadOnly]
    public bool canSit;
    [ReadOnly]
    public bool isSeated;
    [ReadOnly]
    public bool failSafe;
    
    void Update()
    {
        if(Input.GetKeyDown("x"))
        {   
            //Gets you out of car.
            if(isSeated == true)
            {
                failSafe = true;
                this.gameObject.transform.parent = null;
                this.gameObject.transform.position = outsideDoor.position;
                this.gameObject.GetComponent<FirstPersonController>().playerCanMove = true;
                this.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                this.gameObject.GetComponent<CapsuleCollider>().enabled = true; //fixes car hanging left for some reason.
                
                //Smooth transition from driving cam to player cam.
                interCamera.GetComponent<CinemachineVirtualCamera>().Priority = 1;
                playerCamera.GetComponent<CinemachineVirtualCamera>().Priority = 20;
                this.gameObject.GetComponent<MeshRenderer>().enabled = true;
                
                //Failsafe.
                StartCoroutine(FailSafe());
                isSeated = false;
                Debug.Log("Exited car.");

                //Cam transition time. After coroutine, player can look around again. 
                ////This could be zero I think.
                camSwitchTime = 0.25f;
                StartCoroutine(CamSwitch());

                return;
            }

            //Gets you into car and "turns it on".
            if(isSeated == false && canSit == true)
            {
                failSafe = true;
                this.gameObject.GetComponent<FirstPersonController>().playerCanMove = false;
                this.gameObject.transform.parent = targetCar;
                this.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                this.gameObject.transform.position = frontSeat.position;
                this.gameObject.GetComponent<CapsuleCollider>().enabled = false; //fixes car hanging left for some reason.
                
                //Smooth transition from player cam to driving cam.
                playerCamera.GetComponent<CinemachineVirtualCamera>().Priority = 1;
                interCamera.GetComponent<CinemachineVirtualCamera>().Priority = 20;
                this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                
                //Failsafe.
                StartCoroutine(FailSafe());
                isSeated = true;
                Debug.Log("Entered car.");

                //Cam transition time. After coroutine, player can look around again.
                camSwitchTime = 3.0f;
                StartCoroutine(CamSwitch());

                return;
            }
        }


        //It's the code for ignition,
        if(Input.GetKeyDown("f"))
        {
            if(isSeated == true && targetCar.gameObject.GetComponent<WheelVehicle>().isPlayer == false)
            {
                targetCar.gameObject.GetComponent<WheelVehicle>().isPlayer = true;
                return;
            }
        }

        //hot an fresh out the kitchen.
        if(Input.GetKeyDown("f"))
        {
            if(isSeated == true && targetCar.gameObject.GetComponent<WheelVehicle>().isPlayer == true)
            {
                targetCar.gameObject.GetComponent<WheelVehicle>().isPlayer = false;
                return;
            }
        }
    }

    //Enables car entering.
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Seat"))
        {
            canSit = true;
            outsideDoor = col.gameObject.transform.GetChild(0);
            frontSeat = col.gameObject.transform.GetChild(1);
            targetCar = col.gameObject.transform.parent;
        }
    }

    //Disables car entering.
    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Seat"))
        {
            canSit = false;
            outsideDoor = null;
            frontSeat = null;
            targetCar = null;
        }
    }

    //Failsafe because we're running a couple bools.
    IEnumerator FailSafe()
    {
        yield return new WaitForSeconds(0.25f);
        failSafe = false;
    }

    //This handles player controls during camera transition.
    IEnumerator CamSwitch()
    {
        this.GetComponent<FirstPersonController>().enabled = false;
        Debug.Log("Switching the camera.");

        yield return new WaitForSeconds(camSwitchTime);
        this.GetComponent<FirstPersonController>().enabled = true;
        Debug.Log("Camera switch complete.");
    }
}