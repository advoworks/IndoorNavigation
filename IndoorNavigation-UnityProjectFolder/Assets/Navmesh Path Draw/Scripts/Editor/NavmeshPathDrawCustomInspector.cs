using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NavmeshPathDraw))]
public class NavmeshPathDrawCustomInspector : Editor
{
    SerializedProperty destination,
    recalculatePath,
    recalculationTime,
        yOffset; //Kev Added

    void OnEnable(){
        destination = serializedObject.FindProperty("destination");
        recalculatePath = serializedObject.FindProperty("recalculatePath");
        recalculationTime = serializedObject.FindProperty("recalculationTime");
        yOffset = serializedObject.FindProperty("yOffset"); // Kev added
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

        EditorGUILayout.PropertyField(yOffset, new GUIContent("Y Offset", "Y offset for LR")); //Kev Added

        serializedObject.ApplyModifiedProperties();
    }
}
