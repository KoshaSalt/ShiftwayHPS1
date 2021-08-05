using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Varibles begining with a lowercase r are the most recent saved audio clip.
    //Varibles begining with a lowercase c are the current audio clip, the one to be played or scheduled.
    [ReadOnly]
    public bool dialogueIsPlaying;
    public AudioClip rDialogueClip;
    public AudioSource rDialogueSource;
    public AudioSource currentDialogueClipForCheck;
    public double dialogueTime;
    public double rDialogueTimeOffset;
    public float clipCooldownSeconds;
    
    void Update()
    {
        dialogueTime = AudioSettings.dspTime;

        if(currentDialogueClipForCheck != null && currentDialogueClipForCheck.isPlaying == true)
        {
            dialogueIsPlaying = true;
        }

        else
        {
            dialogueIsPlaying = false;
        }
    }
    
    public void PlayCurrentDialogue(AudioSource currentDialogueSource)
    {   
        currentDialogueClipForCheck = currentDialogueSource;

        if(rDialogueClip == null && rDialogueSource == null) //Makes sure the lower part of method doesn't null out.
        {
            rDialogueClip = currentDialogueSource.clip;
            rDialogueSource = currentDialogueSource;
        }

        if(dialogueIsPlaying == true)
        {
            //Schedule current dialogue clip to after the most recent clip (time + duration of recent clip).
            currentDialogueSource.PlayScheduled(dialogueTime + (rDialogueSource.clip.length - (dialogueTime - rDialogueTimeOffset)));

            rDialogueClip = currentDialogueSource.clip;
            rDialogueSource = currentDialogueSource;
        }

        else
        {
            ///There's nothing playing, so roll the clip!
            currentDialogueSource.PlayOneShot(currentDialogueSource.clip);

            //Mark the time on the timeline that the clip began.
            rDialogueTimeOffset = dialogueTime;

            //After a length of time equal to this clip's length, reset bool.
            clipCooldownSeconds = currentDialogueSource.clip.length;

            rDialogueClip = currentDialogueSource.clip;
            rDialogueSource = currentDialogueSource;
        }

        //Records the currrent clip as the most recent clip.
        //StartCoroutine(clipCooldown(currentDialogueSource));
    }

    // IEnumerator clipCooldown(AudioSource currentDialogueSource)
    // {   
    //     yield return new WaitForSeconds(clipCooldownSeconds);

    //     //Set the current dialogue clip as the most recent clip after it is played or scheduled.
    //     rDialogueClip = currentDialogueSource.clip;
    //     rDialogueSource = currentDialogueSource;
    // } 
}
