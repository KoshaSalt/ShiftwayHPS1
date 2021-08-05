using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TitleCanvas : MonoBehaviour
{
    public GameObject gameCanvas;
    public GameObject menuCamera;
    public GameObject playerCamera;
    public GameObject player;
    public GameObject menuWeedSpawn;
    
    void Start()
    {
        gameCanvas.SetActive(false);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void StartGame()
    {
        gameCanvas.SetActive(true);
        playerCamera.GetComponent<CinemachineVirtualCamera>().Priority = 100;
        menuCamera.GetComponent<CinemachineVirtualCamera>().Priority = 1;
        player.GetComponent<FirstPersonController>().enabled = true;

        menuWeedSpawn.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void Options()
    {
        Debug.Log("You pressed the options button.");
    }

    public void Credits()
    {
        Debug.Log("You pressed the credits button.");
    }
}
