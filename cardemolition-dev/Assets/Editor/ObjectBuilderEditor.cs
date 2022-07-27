using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ObjectBuilderScript))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ObjectBuilderScript myScript = (ObjectBuilderScript)target;
        if (GUILayout.Button("Delete Data"))
        {
            myScript.DeleteData();
        }
        if (GUILayout.Button("Reset Data"))
        {
            myScript.ResetData();
        }
        if (GUILayout.Button("Set Player Position"))
        {
            myScript.SetPlayerPosition();
        }
        if (GUILayout.Button("Set Enemy Position"))
        {
            myScript.SetEnemyPosition();
        }
        if (GUILayout.Button("Get Player Position"))
        {
            myScript.GetPlayerPosition();
        }
        if (GUILayout.Button("Get Enemy Position"))
        {
            myScript.GetEnemyPosition();
        }
    }
}