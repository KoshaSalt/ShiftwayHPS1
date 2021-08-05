using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWeedSpawn : MonoBehaviour
{
    public GameObject tWeedPrefab;
    public bool isSpawning = true;

    void Update()
    {
        if(isSpawning == true)
        {
            StartCoroutine(Spawn());
        }
    }

    public IEnumerator Spawn()
    {
        isSpawning = false;
        yield return new WaitForSeconds(12f);
        GameObject weed = Instantiate(tWeedPrefab);
        weed.transform.SetParent(this.gameObject.transform);
        isSpawning = true;
    }
}
