using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminus : MonoBehaviour
{
    public GameObject player;
    public Transform startPoint;
    public bool isBacktracking;
    private float playerXCheck;
    private float playerZCheck;
    
    //Connect this later to player speed and/or car speed.
    public float terminusSpeed = 5f;

    void Start()
    {
        playerXCheck = player.transform.position.x;
        playerZCheck = player.transform.position.z;
    }

    void Update()
    {
        //Coroutine records player's new position
        StartCoroutine(RecordPlayerPos());

        //Terminus advances only if the player's current position is greater than it's last recorded position
        if(player.transform.position.x > playerXCheck)
        {
            transform.LookAt(player.transform);
            transform.position += transform.forward * Time.deltaTime * 5f;
        }

        //Check distanc from terminus to player
        float terminusPlayerDistanceX = Mathf.Abs(player.transform.position.x - this.transform.position.x);
        float terminusPlayerDistanceZ = Mathf.Abs(player.transform.position.z - this.transform.position.z);

        //If distance exceeds value, the player must be backtracking
        if(terminusPlayerDistanceX > 300 || terminusPlayerDistanceZ > 300)
        {
            isBacktracking = true;
        }

    }

    IEnumerator RecordPlayerPos()
    {
        yield return new WaitForSeconds(0.25f);
        
        if(player.transform.position.x > playerXCheck)
        {
            playerXCheck = player.transform.position.x;
        }
        
        if(player.transform.position.y > playerZCheck)
        {
            playerZCheck = player.transform.position.z;
        }
    }
}
