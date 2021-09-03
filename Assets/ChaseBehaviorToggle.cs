using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class ChaseBehaviorToggle : MonoBehaviour
{
    public GameObject truck;
    public GameObject playerCar;
    [ReadOnly]
    public bool isDecel;
    private float startSpeedMax;
    private float maxSpeed;
    [ReadOnly]
    public float currentSpeed;
    public string state = null; 
    
    void Start()
    {
        truck = this.transform.parent.gameObject;
        playerCar = truck.GetComponent<CarDriverAI>().targetPositionTranform.parent.gameObject;
        startSpeedMax = truck.GetComponent<CarDriver>().speedMax;
    }

    void Update()
    {
        //First wo statements for dealing with wiggle.
        if(playerCar.GetComponent<WheelVehicle>().Speed <= 1)
        {
            truck.GetComponent<CarDriver>().enabled = false;
        }
        
        if(playerCar.GetComponent<WheelVehicle>().Speed >= 1)
        {
            truck.GetComponent<CarDriver>().enabled = true;
        }
        
        //For reading in inspector.
        currentSpeed = truck.GetComponent<CarDriver>().speedMax;
    }
}   
//     void OnTriggerEnter(Collider col)
//     {
//         if(col.CompareTag("ChaseTarget"))
//         {
//             SpeedLock(currentSpeed);
//         }
//     }

//     public void SpeedLock(float lockedSpeed)
//     {
//         isDecel = true;
//         maxSpeed = currentSpeed;
//         state = "Speed Locked";
//     }
// }
