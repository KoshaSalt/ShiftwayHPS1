using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expander : MonoBehaviour
{
    public float maxSize;
    public float minSize;
    public float scalingRate = 0.5f;
    [ReadOnly]
    public float scaleTime;
    [ReadOnly]
    public float targetScale;
    [ReadOnly]
    public float oldScale;

    void Start()
    {
        oldScale = Random.Range(minSize, maxSize);
        targetScale = Random.Range(minSize, maxSize);
    }
    
    void FixedUpdate()
    {
        transform.localScale = new Vector3(Mathf.Lerp(oldScale, targetScale, scaleTime), Mathf.Lerp(oldScale, targetScale, scaleTime), Mathf.Lerp(oldScale, targetScale, scaleTime)); 

        scaleTime += scalingRate * Time.deltaTime;
        if(scaleTime > 1.0f)
        {
            oldScale = transform.localScale.x;
            targetScale = Random.Range(minSize, maxSize);
            scaleTime = 0.0f;
        }
    }
}
