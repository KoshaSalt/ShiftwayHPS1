using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NooyndChase : MonoBehaviour
{
    [ReadOnly]
    public bool isPursuing = false;
    [ReadOnly]
    public bool coolingDown;
    
    [ReadOnly]
    public Transform player;
    [ReadOnly]
    public GameObject sceneCounter;
    public GameObject noooyidPrefab;
    public float pursuitSpeed;
    public float maxDistFromPlayer = 200;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sceneCounter = GameObject.FindGameObjectWithTag("SceneManager");
        sceneCounter.GetComponent<Counting>().countNoooynd += 1;
        this.gameObject.name = ("Noooynd Chaser"); //#" + sceneCounter.GetComponent<Counting>().countNoooynd);
    }

    void Update()
    {
        if(isPursuing == true)
        {
        
            transform.LookAt(player);

            if (Vector3.Distance(transform.position, player.position) >= 0)
            {
                transform.position += transform.forward * pursuitSpeed * Time.deltaTime;
            }
        }

        if (Vector3.Distance(transform.position, player.position) >= maxDistFromPlayer || sceneCounter.GetComponent<Counting>().countNoooynd > 350)
        {
            KillNoooynd();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player" && coolingDown == false)
        {
            SpawnNoooynd();
        }
    }

    public void SpawnNoooynd()
    {
        float newPosX = (Random.Range(-0.5f, 0.5f));
        float newPosY = (Random.Range(-0.5f, 0.5f));
        float newPosZ = (Random.Range(-0.5f, 0.5f));

        Instantiate(noooyidPrefab, new Vector3(this.transform.position.x + newPosX, this.transform.position.y + newPosY, this.transform.position.z + newPosZ), Quaternion.identity);
            
        coolingDown = true;
        StartCoroutine(MultiCooldown());
    }

    public void KillNoooynd()
    {
        sceneCounter.GetComponent<Counting>().countNoooynd -= 1;
        Destroy(this.gameObject);
    }

    IEnumerator MultiCooldown()
    {
        yield return new WaitForSeconds(.5f);
        coolingDown = false;
    }
}
