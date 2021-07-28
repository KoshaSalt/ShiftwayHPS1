using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DashboardButtons : MonoBehaviour
{
    public Text activeButtonText;
    private GameObject button;
    private Color startcolor;
    public Animation buttonPressAnim;
    public UnityEvent OnClick = new UnityEvent();
    
    public void Start()
    {
        button = this.gameObject;
        startcolor = this.GetComponent<Renderer>().material.color;
        buttonPressAnim = this.GetComponent<Animation>();
        this.GetComponent<Outline>().enabled = false;
    }

    public void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                DebugMessage();
                buttonPressAnim.Play();
                OnClick.Invoke();
            }
        }

        if(Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject || buttonPressAnim.isPlaying == true)
        {
            this.GetComponent<Outline>().enabled = true;
            activeButtonText.text = this.name;
        }

        else
        {
            this.GetComponent<Outline>().enabled = false;
        }
    }
    
    public void DebugMessage()
    {
        Debug.Log("You have pressed the " + this.name);
    }
}
