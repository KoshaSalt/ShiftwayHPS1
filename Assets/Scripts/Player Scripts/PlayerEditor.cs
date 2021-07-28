using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Player myScript = (Player)target;
        if(GUILayout.Button("Save"))
        {
            myScript.SavePlayer();
        }

        if(GUILayout.Button("Load"))
        {
            myScript.LoadPlayer();
        }

        if(GUILayout.Button("Clear Save"))
        {
            myScript.ClearSave();
        }
    }
}
