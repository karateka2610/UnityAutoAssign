#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Reflection;
using UnityAutoAssign;

/// <summary>
/// Editor that adds auto-assignment functionality to the Unity inspector
/// </summary>
[CustomEditor(typeof(MonoBehaviour), true)]
public class AutoAssignerEditor : Editor
{
    /// <summary>
    /// Context menu to auto-assign components in selected GameObjects
    /// </summary>
    [MenuItem("Tools/AutoAssign/Selected GameObjects")]
    public static void AutoAssignSelected()
    {
        if (Selection.gameObjects.Length == 0)
        {
            Debug.LogWarning("[AutoAssigner] No GameObjects selected");
            return;
        }

        int totalAssigned = 0;
        foreach (GameObject obj in Selection.gameObjects)
        {
            int assigned = AutoAssigner.AssignAll(obj);
            totalAssigned += assigned;
            
            if (assigned > 0)
            {
                EditorUtility.SetDirty(obj);
            }
        }

        if (totalAssigned > 0)
        {
            Debug.Log($"[AutoAssigner] SUCCESS: Total fields assigned: {totalAssigned}");
        }
        else
        {
            Debug.Log("[AutoAssigner] No fields found to auto-assign");
        }
    }

    /// <summary>
    /// Context menu to auto-assign in the entire scene
    /// </summary>
    [MenuItem("Tools/AutoAssign/All Scene GameObjects")]
    public static void AutoAssignAllInScene()
    {
        // Use FindObjectsByType for Unity 2023+ or FindObjectsOfType for earlier versions
#if UNITY_2023_1_OR_NEWER
        var allMonoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
#else
        #pragma warning disable CS0618 // Type or member is obsolete
        var allMonoBehaviours = FindObjectsOfType<MonoBehaviour>();
        #pragma warning restore CS0618 // Type or member is obsolete
#endif
        int totalAssigned = 0;

        foreach (var mb in allMonoBehaviours)
        {
            if (mb == null) continue;
            
            int assigned = AutoAssigner.AssignComponent(mb);
            totalAssigned += assigned;
            
            if (assigned > 0)
            {
                EditorUtility.SetDirty(mb.gameObject);
            }
        }

        if (totalAssigned > 0)
        {
            Debug.Log($"[AutoAssigner] SUCCESS: Total scene fields assigned: {totalAssigned}");
        }
        else
        {
            Debug.Log("[AutoAssigner] No fields found to auto-assign in scene");
        }
    }

    /// <summary>
    /// Called when the component is validated in the Inspector
    /// </summary>
    private void OnEnable()
    {
        // Auto-assign when component is added or reset
        if (target is MonoBehaviour mb)
        {
            bool shouldProcess = ShouldAutoAssign(mb) || HasAutoSetup(mb) || HasConfigureComponent(mb);
            
            if (shouldProcess)
            {
                // 1. Apply AutoSetup first (tags, layers, etc)
                AutoSetup.ApplySetup(mb);
                
                // 2. Then apply AutoAssign (components)
                AutoAssigner.AssignComponent(mb);
                
                // 3. Finally configure components (component properties)
                ComponentConfigurator.ConfigureComponents(mb);
                
                EditorUtility.SetDirty(target);
            }
        }
    }

    /// <summary>
    /// Checks if the MonoBehaviour has fields with [AutoAssign]
    /// </summary>
    private bool ShouldAutoAssign(MonoBehaviour mb)
    {
        var type = mb.GetType();
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var field in fields)
        {
            if (field.GetCustomAttribute<AutoAssignAttribute>() != null)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Checks if the MonoBehaviour has the [AutoSetup] attribute
    /// </summary>
    private bool HasAutoSetup(MonoBehaviour mb)
    {
        var type = mb.GetType();
        return type.GetCustomAttribute<AutoSetupAttribute>() != null;
    }

    /// <summary>
    /// Checks if the MonoBehaviour has fields with [ConfigureComponent]
    /// </summary>
    private bool HasConfigureComponent(MonoBehaviour mb)
    {
        var type = mb.GetType();
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var field in fields)
        {
            if (field.GetCustomAttribute<ConfigureComponentAttribute>() != null)
            {
                return true;
            }
        }

        return false;
    }
}
#endif