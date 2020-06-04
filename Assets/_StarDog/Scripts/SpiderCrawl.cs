using UnityEditor;
using UnityEngine;

public class SpiderCrawl : MonoBehaviour
{

}

[CustomEditor(typeof(SpiderCrawl))]
public class SpiderCrawlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This is a help box", MessageType.Info);

        SpiderCrawl myScript = (SpiderCrawl)target;
    }
}
