using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    //c is current.
    //r is recieving.
    public AudioSource c_DialogueSource;
    public AudioClip c_PlayingAudioClip;
    public AudioSource[] unplayedDialogue;
    public bool isPlaying;

    //Time stuff.
    public double startTime;
    public double dialogueTime;
    public double c_ClipOffsetSec;
    
    void Start()
    {
        startTime = AudioSettings.dspTime;
    }
    void Update()
    {
        dialogueTime = AudioSettings.dspTime;

        //Resetting the space between clips.
        if(c_ClipOffsetSec >= 0)
        {
            c_ClipOffsetSec -= (dialogueTime - startTime);
        }
        
        if(c_DialogueSource.isPlaying == true) 
        {
            isPlaying = true;
        }
        
        if(c_DialogueSource.isPlaying == false)
        {
            if(unplayedDialogue[0] = null)
            {
                isPlaying = false;
            }

            else
            {
                c_DialogueSource = unplayedDialogue[0];
            }
        }
    }
    
    public void RecordDialogue(AudioSource r_DialogueSource)
    {
        //Recieve audio clip from source and add it to the list of unplayed clips.
        int queueCount = unplayedDialogue.Length;
        queueCount++;
        unplayedDialogue = new AudioSource[queueCount];
        unplayedDialogue[unplayedDialogue.Length - 1] = r_DialogueSource;
    }

    public void PlayDialogue()
    {
        if(isPlaying == true)
        {
            AudioSource newClip = unplayedDialogue[0];
            newClip.PlayScheduled(dialogueTime + c_ClipOffsetSec);
            c_ClipOffsetSec += newClip.clip.length;
        }
        
        else
        {
            c_DialogueSource.PlayScheduled(dialogueTime);
        }
    }
}
