#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Reflection;

/// <summary>
/// Editor que añade funcionalidades de auto-asignación al inspector de Unity
/// </summary>
[CustomEditor(typeof(MonoBehaviour), true)]
public class AutoAssignerEditor : Editor
{
    /// <summary>
    /// Menú contextual para auto-asignar componentes en GameObjects seleccionados
    /// </summary>
    [MenuItem("Tools/Auto Assign/Selected GameObjects")]
    public static void AutoAssignSelected()
    {
        if (Selection.gameObjects.Length == 0)
        {
            Debug.LogWarning("[AutoAssigner] No hay GameObjects seleccionados");
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
            Debug.Log($"[AutoAssigner] 🎉 Total de campos asignados: {totalAssigned}");
        }
        else
        {
            Debug.Log("[AutoAssigner] No se encontraron campos para auto-asignar");
        }
    }

    /// <summary>
    /// Menú contextual para auto-asignar en toda la escena
    /// </summary>
    [MenuItem("Tools/Auto Assign/All Scene GameObjects")]
    public static void AutoAssignAllInScene()
    {
        // Usar FindObjectsByType para Unity 2023+ o FindObjectsOfType para versiones anteriores
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
            Debug.Log($"[AutoAssigner] 🎉 Total de campos asignados en escena: {totalAssigned}");
        }
        else
        {
            Debug.Log("[AutoAssigner] No se encontraron campos para auto-asignar en la escena");
        }
    }

    /// <summary>
    /// Se llama cuando el componente es validado en el Inspector
    /// </summary>
    private void OnEnable()
    {
        // Auto-asignar cuando se añade el componente o se resetea
        if (target is MonoBehaviour mb && ShouldAutoAssign(mb))
        {
            AutoAssigner.AssignComponent(mb);
            EditorUtility.SetDirty(target);
        }
    }

    /// <summary>
    /// Verifica si el MonoBehaviour tiene campos con [AutoAssign]
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
}
#endif