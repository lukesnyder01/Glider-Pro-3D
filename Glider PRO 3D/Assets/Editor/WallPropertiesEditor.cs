using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(WallProperties))]
public class WallPropertiesEdotr : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WallProperties myScript = (WallProperties)target;
        if (GUILayout.Button("Change Wall Style"))
        {
            myScript.ReplacePrefab();
        }
    }
}