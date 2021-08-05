using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridManager))]
public class SceneryGridEditor : Editor
{
    public override void OnInspectorGUI ()
    {
        DrawDefaultInspector();

        GridManager myScript = (GridManager)target;
        if(GUILayout.Button("Regenerate Scenery Objects"))
        {
            myScript.Regenerate();
        }
    }
}
