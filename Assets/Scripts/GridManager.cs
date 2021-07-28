using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class GridManager : MonoBehaviour
{
    [ReadOnly]
    public int objectCount;
    
    private float x_Start, z_Start;
    public int columnLength, rowLength;
    private float x_Space, z_Space;
    public GameObject sceneryObject1;
    public GameObject sceneryObject2;
    public GameObject sceneryObject3;
    private GameObject chosenObject;
    [Range (0f, 100f)]
    public  float cactusScatter;
    void Update()
    {
        //Don't wanna crash.
        objectCount = this.transform.childCount;
        
        //Setting location and size of grid.
        x_Start = this.transform.position.x;
        z_Start = this.transform.position.z;
        x_Space = cactusScatter;
        z_Space = cactusScatter;

        ////This would be cool to try to make work.
        //columnLength = Mathf.RoundToInt(this.transform.lossyScale.x / 100);
        //rowLength = Mathf.RoundToInt(this.transform.lossyScale.z / 100);
        
        
        if(objectCount < rowLength * columnLength)
        {
            for(int i = 0; i < columnLength * rowLength; i++)
            {
                //Random offset and object.
                float randomX = Random.Range(-x_Space, x_Space);
                float randomZ = Random.Range(-z_Space, z_Space);
                int objectSelector = Random.Range(1, 4);

                if(objectSelector == 1) {chosenObject = sceneryObject1;}
                if(objectSelector == 2) {chosenObject = sceneryObject2;}
                if(objectSelector == 3) {chosenObject = sceneryObject3;}
                
                var newObject = Instantiate(chosenObject, new Vector3(x_Start + (x_Space*(i%columnLength)) + randomX, this.transform.position.y, z_Start + (-z_Space*(i/columnLength)) + randomZ), Quaternion.identity);
                newObject.transform.parent = this.transform;
            }
        }
    }

    public void Regenerate()
    {
        foreach(Transform child in transform)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
    }
}
