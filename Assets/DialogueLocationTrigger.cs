using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLocationTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip onEnterDialogueClip;
    public AudioClip onExitDialogueClip;
    public GameObject dialogueManager;
    
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        dialogueManager = GameObject.FindGameObjectWithTag("AudioManager");
    }

    void OnTriggerEnter()
    {
        audioSource.clip = onEnterDialogueClip;
        dialogueManager.GetComponent<DialogueManager>().RecordDialogue(audioSource);
        dialogueManager.GetComponent<DialogueManager>().PlayDialogue();
    }

    void OnTriggerExit()
    {
        audioSource.clip = onExitDialogueClip;
        dialogueManager.GetComponent<DialogueManager>().RecordDialogue(audioSource);
        dialogueManager.GetComponent<DialogueManager>().PlayDialogue();
    }
}
