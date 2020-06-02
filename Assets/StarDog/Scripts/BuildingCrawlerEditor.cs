using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BuildingCrawler))]
public class BuildingCrawlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This is a help box", MessageType.Info);

        BuildingCrawler myScript = (BuildingCrawler)target;

        if (GUILayout.Button("Add Random Waypoint"))
        {
            myScript.AddRandomWaypoint();
        }

        if (GUILayout.Button("Stop Moving"))
        {
            myScript.StopMoving();
        }
    }
}