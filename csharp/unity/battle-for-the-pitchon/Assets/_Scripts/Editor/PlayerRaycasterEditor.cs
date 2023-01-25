using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerRaycaster))]
[CanEditMultipleObjects]
public class PlayerRaycasterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Update.
        serializedObject.Update();

        // Notify user about LMB.
        GUILayout.Label
        (
            "This raycaster is hardcoded " +
            "\nto trigger only to LMB."
        );

        // Notify about difference.
        GUILayout.Label
        (
            "Soft Reset - resets only sizes " +
            "\nof arrays and does not " +
            "\naffect other parameters."
        );

        // Trigger only when pressed.
        if (GUILayout.Button("Soft Reset"))
        {
            // Multiple.
            if (serializedObject.isEditingMultipleObjects)
            {
                // Loop over.
                foreach (var item in serializedObject.targetObjects)
                {
                    // Retrieve.
                    PlayerRaycaster raycaster = item as PlayerRaycaster;

                    // Reset by calling this function.
                    raycaster.ResetParameters();
                }
            }
            // Single.
            else
            {
                // Retrieve.
                PlayerRaycaster raycaster = serializedObject.targetObject as PlayerRaycaster;

                // Reset by this function.
                raycaster.ResetParameters();
            }
        }

        // Apply changes.
        serializedObject.ApplyModifiedProperties();
    }
}
