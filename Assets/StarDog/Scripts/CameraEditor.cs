using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraMovement))]
public class CameraEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This is a help box", MessageType.Info);

        CameraMovement myScript = (CameraMovement)target;

        if (GUILayout.Button("Left View"))
        {
            myScript.LeftView();
        }

        if (GUILayout.Button("Right View"))
        {
            myScript.RightView();
        }

        if (GUILayout.Button("Rear View"))
        {
            myScript.RearView();
        }

        if (GUILayout.Button("Top View"))
        {
            myScript.TopView();
        }
    }
}
