using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NooyndChase : MonoBehaviour
{
    [ReadOnly]
    public bool isPursuing = false;
    [ReadOnly]
    public bool isFleeing = false;
    [ReadOnly]
    public bool coolingDown;
    
    [ReadOnly]
    public Transform player;
    [ReadOnly]
    public GameObject sceneCounter;
    public GameObject noooyidPrefab;
    public float pursuitSpeed;
    private float spawnCooldown = 2f;
    public float maxDistFromPlayer = 200;
    public float nooooyndLiftForce = 100f;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sceneCounter = GameObject.FindGameObjectWithTag("SceneManager");
        sceneCounter.GetComponent<Counting>().countNoooynd += 1;
        this.gameObject.name = ("Noooynd Chaser"); //#" + sceneCounter.GetComponent<Counting>().countNoooynd);
        coolingDown = true;
        StartCoroutine(MultiCooldown());
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

        if (Vector3.Distance(transform.position, player.position) >= maxDistFromPlayer || sceneCounter.GetComponent<Counting>().countNoooynd > 100)
        {
            KillNoooynd();
        }

        if(this.GetComponent<CharacterJoint>() != null)
        {
            spawnCooldown = 8f;

            if(coolingDown == false)
            {
                SpawnNoooynd();
            }

            this.GetComponent<Rigidbody>().AddForce(transform.up * nooooyndLiftForce);
        }

        if(isFleeing == true)
        {
            transform.position += -(transform.forward * pursuitSpeed * 2 * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == ("Player") && coolingDown == false)
        {
            spawnCooldown = 1f;
            SpawnNoooynd();
        }

        if(col.gameObject.tag == ("PlayerCar") && coolingDown == false)
        {
            spawnCooldown = 4f;
            player = col.gameObject.transform;
            SpawnNoooynd();
            this.transform.GetChild(0).GetComponent<Expander>().enabled = false;
            this.transform.GetChild(1).GetComponent<Expander>().enabled = false;

            this.gameObject.AddComponent<CharacterJoint>();
            this.GetComponent<CharacterJoint>().connectedBody = player.gameObject.GetComponent<Rigidbody>();
            this.GetComponent<CharacterJoint>().enableCollision = true;
        }
    }

    void OnTriggerEnter(Collider trig)
    {
        if(trig.gameObject.CompareTag("BreakPoint"))
        {   
            if(this.GetComponent<CharacterJoint>() != null)
            {
                Destroy(this.gameObject.GetComponent<CharacterJoint>());
            }
            
            Destroy(this.gameObject.GetComponent<Collider>());
            this.player = trig.transform;

            isPursuing = false;
            isFleeing = true;
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
        yield return new WaitForSeconds(spawnCooldown);
        coolingDown = false;
    }
}
