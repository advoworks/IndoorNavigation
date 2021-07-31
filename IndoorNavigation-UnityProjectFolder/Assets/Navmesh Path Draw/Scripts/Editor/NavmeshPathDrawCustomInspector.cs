using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NavmeshPathDraw))]
public class NavmeshPathDrawCustomInspector : Editor
{
    SerializedProperty destination,
    recalculatePath,
    recalculationTime,
    lineOffset, //Kev Added
    destinationMarkerPrefab; //Kev Added

    void OnEnable(){
        destination = serializedObject.FindProperty("destination");
        recalculatePath = serializedObject.FindProperty("recalculatePath");
        recalculationTime = serializedObject.FindProperty("recalculationTime");
        lineOffset = serializedObject.FindProperty("lineOffset"); // Kev added
        destinationMarkerPrefab = serializedObject.FindProperty("destinationMarkerPrefab"); // Kev added
    }

    public override void OnInspectorGUI(){
        var button = GUILayout.Button(Resources.Load("NavmeshPathDrawArtwork") as Texture, GUILayout.Width(370), GUILayout.Height(200));
        EditorGUILayout.HelpBox("Please don't forget to leave a nice review if you like this package. Click on the image to be taken to the store.", MessageType.Info);

        if (button) Application.OpenURL("http://u3d.as/22Nv");
        EditorGUILayout.Space();

        NavmeshPathDraw script = (NavmeshPathDraw) target;

        EditorGUILayout.PropertyField(destination, new GUIContent("Destination", "Transform position of the end destination"));
        EditorGUILayout.PropertyField(recalculatePath, new GUIContent("Recalculate Path", "If set to true, the pathfinding will be recalculated every set amount of time"));
        
        EditorGUI.BeginDisabledGroup(script.recalculatePath == false);
            EditorGUILayout.PropertyField(recalculationTime, new GUIContent("Recalculation Time", "The amount of time in seconds to recalculate the path. The higher the number, the more performant on CPU but slower to pathfind. It all depends on your game and target hardware. It's usually best to keep this from 0.1 - 0.5 seconds"));
        EditorGUI.EndDisabledGroup ();

        EditorGUILayout.PropertyField(lineOffset, new GUIContent("Line Offset", "Offset for LR")); //Kev Added
        EditorGUILayout.PropertyField(destinationMarkerPrefab, new GUIContent("Dest Marker", "Destination Marker Prefab")); //Kev Added

        serializedObject.ApplyModifiedProperties();
    }
}
