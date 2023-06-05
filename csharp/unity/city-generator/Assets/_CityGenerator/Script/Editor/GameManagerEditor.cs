using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private SerializedProperty tileParent;
    private SerializedProperty tilePrefab;

    private SerializedProperty xMax;
    private SerializedProperty zMax;

    private void OnEnable()
    {
        tileParent = serializedObject.FindProperty("tileParent");
        tilePrefab = serializedObject.FindProperty("tilePrefab");

        xMax = serializedObject.FindProperty("xMax");
        zMax = serializedObject.FindProperty("zMax");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(tileParent, new GUIContent("tileParent"));
        EditorGUILayout.PropertyField(tilePrefab, new GUIContent("tilePrefab"));

        EditorGUILayout.IntSlider(xMax, 1, 1000, new GUIContent("xMax"));
        EditorGUILayout.IntSlider(zMax, 1, 1000, new GUIContent("zMax"));

        if (GUILayout.Button("Generate City"))
        {
            GameManager gm = serializedObject.targetObject as GameManager;
            gm.GenerateCity();
        }

        serializedObject.ApplyModifiedProperties ();
    }
}
