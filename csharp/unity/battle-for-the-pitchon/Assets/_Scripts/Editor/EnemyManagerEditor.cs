using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyManager))]
[CanEditMultipleObjects]
public class EnemyManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw default GUI.
        base.OnInspectorGUI();

        // Default update properties.
        serializedObject.Update();

        // Generic button.
        if (GUILayout.Button("Scan all Path Groups"))
        {
            // Shortcut. em for EnemyManager.
            EnemyManager em = serializedObject.targetObject as EnemyManager;

            // Iterate over all groups.
            for (int i = 0; i < em.pathGroups.Length; i++)
            {
                // Scane children.
                em.pathGroups[i].ScanChildren();
            }
        }

        // Generic button.
        if (GUILayout.Button("Scan all Boss Path Groups"))
        {
            // Shortcut. em for EnemyManager.
            EnemyManager em = serializedObject.targetObject as EnemyManager;

            // Iterate over all groups.
            for (int i = 0; i < em.bossPathGroups.Length; i++)
            {
                // Scane children.
                em.bossPathGroups[i].ScanChildren();
            }
        }

        // Generic button.
        if (GUILayout.Button("Spawn Enemies"))
        {
            // Shortcut. em for EnemyManager.
            EnemyManager em = serializedObject.targetObject as EnemyManager;

            // Spawn enemies.
            em.Spawn(em.enemyPrefabIndex, em.pathGroupIndex, em.pathStep, em.spawneeSpeed, em.spawneeReachThreshold);
        }

        // Generic button.
        if (GUILayout.Button("Spawn a Boss"))
        {
            // Shortcut. em for EnemyManager.
            EnemyManager em = serializedObject.targetObject as EnemyManager;

            // Spawn enemies.
            em.Spawn(em.bossPrefabIndex, em.bossPathGroupIndex, em.bossSpeed, em.bossReachThreshold);
        }

        // Apply changes made here.
        serializedObject.ApplyModifiedProperties();
    }
}
