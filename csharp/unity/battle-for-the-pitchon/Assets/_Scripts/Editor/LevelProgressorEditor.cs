using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelProgressor))]
public class LevelProgressorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Default GUI.
        base.OnInspectorGUI();

        // Update.
        serializedObject.Update();

        if (GUILayout.Button("Toggle Level Waves"))
        {
            // Ignite engine.
            LevelProgressor.Wave.enabled = !LevelProgressor.Wave.enabled;
        }

        // Save changes.
        serializedObject.ApplyModifiedProperties();
    }
}
