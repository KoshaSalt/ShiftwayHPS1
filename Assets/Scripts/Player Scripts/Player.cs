using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [ReadOnly]
    public Vector3 playerPosition;
    [ReadOnly]
    public Quaternion playerRotation;
    [ReadOnly]
    public bool isSeated;
    public GameObject deathScreen;
    void Update()
    {
        playerPosition = this.gameObject.transform.position;
        playerRotation = this.gameObject.transform.rotation;
        isSeated = this.GetComponentInChildren<GetInOutCar>().isSeated;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        
        Vector3 loadPosition;
        loadPosition.x = data.playerPosition[0];
        loadPosition.y = data.playerPosition[1];
        loadPosition.z = data.playerPosition[2];
        this.transform.position = loadPosition;

        Quaternion loadRotation;
        loadRotation.x = data.playerRotation[0];
        loadRotation.y = data.playerRotation[1];
        loadRotation.z = data.playerRotation[2];
        loadRotation.w = data.playerRotation[3];
        this.transform.rotation = loadRotation;

        isSeated = data.isSeated;
    }

    public void ClearSave()
    {
        //button to clear the save.
    }

    public void PlayerDeath()
    {
        deathScreen.SetActive(true);
        //Play tape rewind sound effect.
        //LoadPlayer(), once this is working.
    }
}
