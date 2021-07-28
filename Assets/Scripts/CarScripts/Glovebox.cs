using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Glovebox : MonoBehaviour
{
    private GameObject button;
    public Animation gloveboxAnim;
    public bool isOpen;
    public UnityEvent OnClick = new UnityEvent();
    
    public void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject && isOpen == false)
            {
                DebugMessage();
                gloveboxAnim.Play("GloveboxOpen");
                OnClick.Invoke();
                isOpen = true;
                return;
            }

            if(Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject && isOpen == true)
            {
                DebugMessage();
                gloveboxAnim.Play("GloveboxClose");
                OnClick.Invoke();
                isOpen = false;
                return;
            }
        }

        if(Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject || gloveboxAnim.isPlaying == true)
        {
            this.GetComponent<Outline>().enabled = true;
        }

        else
        {
            this.GetComponent<Outline>().enabled = false;
        }
    }
    
    public void DebugMessage()
    {
        if(isOpen == true)
        {
            Debug.Log("You opened the glovebox.");
        }

        if(isOpen == false)
        {
            Debug.Log("You closed the glovebox.");
        }
    }
}
