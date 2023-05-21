using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RoomProperties))]
public class RoomPropertiesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RoomProperties myScript = (RoomProperties)target;
        if (GUILayout.Button("Change Room Style"))
        {
            myScript.ChangeRoomStyle();
        }
    }
}