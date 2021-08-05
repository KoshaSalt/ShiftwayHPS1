using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip onEnterDialogueClip;
    public AudioClip onExitDialogueClip;
    public AudioClip failstateDialogueClip;
    public AudioClip[] otherDialogueClips;
    public GameObject audioManager;
    public AudioClip currentDialogueClip;
    private float bufferTimeSec = 3.0f;
    
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
    }

    public void OnTriggerEnter(Collider col)
    {
        if(onEnterDialogueClip != null)
        {
            //Send the onEnter clip to the audio manager for either scheduling or playing.
            audioSource.clip = onEnterDialogueClip;
            audioManager.GetComponent<AudioManager>().PlayCurrentDialogue(audioSource);
            onEnterDialogueClip = null;
            return;
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if(onExitDialogueClip != null)
        {
            //Send the onEnter clip to the audio manager for either scheduling or playing.
            audioSource.clip = onExitDialogueClip;
            audioManager.GetComponent<AudioManager>().PlayCurrentDialogue(audioSource);
            onExitDialogueClip = null;
            return;
        }
    }

    IEnumerator bufferTime()
    {
        yield return new WaitForSeconds(bufferTimeSec);
    }
}
