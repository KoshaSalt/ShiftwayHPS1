using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private int sec;
    private int min;
    public float timeCount;
    public Text clock;

    void Start()
    {
        clock = this.gameObject.GetComponent<Text>();
    }
    
    void Update()
    {
        timeCount += Time.deltaTime;
        
        min = Mathf.FloorToInt(timeCount / 60);
        sec = Mathf.FloorToInt(timeCount % 60);

        clock.text = min.ToString("00") + ":" + sec.ToString("00");
    }
}
