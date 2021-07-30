using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdGod : MonoBehaviour
{
    private Transform player;
    public float speed = 1f;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        this.transform.position = new Vector3(player.position.x -100 , player.position.y +100, player.position.z -100);
    }
    
    void Update()
    {
        this.transform.LookAt(player);
        float step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.position, step);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().PlayerDeath();
        }
    }
}
