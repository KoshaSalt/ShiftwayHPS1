using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject nextCheckpoint;
    public GameObject lastCheckpoint;
    [ReadOnly]
    public bool isBacktracking;

    public void CheckpointCheck()
    {    
        int lastIndex = lastCheckpoint.transform.GetSiblingIndex();
        int nextIndex = nextCheckpoint.transform.GetSiblingIndex();
        
        if(nextCheckpoint == null)
        {
            nextCheckpoint = lastCheckpoint;
        }

        if(lastIndex < nextIndex)
        {
            //Player is backtracking
            isBacktracking = true;
        }
        
        if(lastIndex == nextIndex)
        {
            //Player is progression
            isBacktracking = false;
            nextCheckpoint = this.gameObject.transform.GetChild(lastIndex+1).gameObject;
        }
    }
}
