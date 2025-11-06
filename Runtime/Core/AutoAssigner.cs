using UnityEngine;
using System.Reflection;

namespace UnityAutoAssign
{
    /// <summary>
    /// Static class that handles auto-assignment of components using reflection.
    /// </summary>
    public static class AutoAssigner
{
    /// <summary>
    /// Automatically assigns all fields with [AutoAssign] in all MonoBehaviours of a GameObject
    /// </summary>
    /// <param name="gameObject">GameObject to process</param>
    /// <returns>Total number of fields assigned</returns>
    public static int AssignAll(GameObject gameObject)
    {
        if (gameObject == null)
        {
            Debug.LogWarning("[AutoAssigner] GameObject is null");
            return 0;
        }

        int totalAssigned = 0;
        var components = gameObject.GetComponents<MonoBehaviour>();

        foreach (var component in components)
        {
            if (component == null) continue;
            totalAssigned += AssignComponent(component);
        }

        return totalAssigned;
    }

    /// <summary>
    /// Assigns fields with [AutoAssign] in a specific MonoBehaviour
    /// </summary>
    /// <param name="component">MonoBehaviour to process</param>
    /// <returns>Number of fields assigned</returns>
    public static int AssignComponent(MonoBehaviour component)
    {
        if (component == null) return 0;

        var type = component.GetType();
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        int assignedCount = 0;

        foreach (var field in fields)
        {
            var attribute = field.GetCustomAttribute<AutoAssignAttribute>();
            if (attribute == null) continue;

            // Check if it has SerializeField or is public
            var serializeFieldAttr = field.GetCustomAttribute<SerializeField>();
            if (serializeFieldAttr == null && !field.IsPublic)
            {
                Debug.LogWarning($"[AutoAssigner] Field '{field.Name}' in '{type.Name}' has [AutoAssign] but is not serializable. Add [SerializeField]");
                continue;
            }

            // If already has a value assigned, don't overwrite
            var currentValue = field.GetValue(component);
            if (currentValue != null && !currentValue.Equals(null)) continue;

            // Try to assign according to attribute options
            object assignedValue = TryAssign(component, field.FieldType, attribute);

            if (assignedValue != null)
            {
                field.SetValue(component, assignedValue);
                assignedCount++;
            }
            else if (attribute.AutoCreate && typeof(Component).IsAssignableFrom(field.FieldType))
            {
                // Try to create the component if autoCreate is enabled
                object createdComponent = TryCreateComponent(component, field.FieldType);
                if (createdComponent != null)
                {
                    field.SetValue(component, createdComponent);
                    assignedCount++;
                }
                else
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"[AutoAssigner] Could not create '{field.Name}' of type '{field.FieldType.Name}' in '{component.gameObject.name}'");
#endif
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning($"[AutoAssigner] Could not find '{field.Name}' of type '{field.FieldType.Name}' in '{component.gameObject.name}'");
#endif
            }
        }

        return assignedCount;
    }

    /// <summary>
    /// Tries to assign a component according to the attribute strategy
    /// </summary>
    private static object TryAssign(MonoBehaviour component, System.Type fieldType, AutoAssignAttribute attr)
    {
        Object result = null;

        // Priority 1: Search by Tag if specified
        if (!string.IsNullOrEmpty(attr.Tag))
        {
            GameObject taggedObject = GameObject.FindGameObjectWithTag(attr.Tag);
            if (taggedObject != null)
            {
                // If the field is GameObject, return the GameObject
                if (fieldType == typeof(GameObject))
                {
                    return taggedObject;
                }
                // If it is a component, search for it in the GameObject
                else if (typeof(Component).IsAssignableFrom(fieldType))
                {
                    result = taggedObject.GetComponent(fieldType);
                    if (result != null) return result;
                }
            }
        }

        // Priority 2: Search in the same GameObject
        if (typeof(Component).IsAssignableFrom(fieldType))
        {
            result = component.GetComponent(fieldType);
            if (result != null) return result;
        }

        // Priority 3: Search in children if enabled
        if (attr.SearchInChildren)
        {
            if (typeof(Component).IsAssignableFrom(fieldType))
            {
                result = component.GetComponentInChildren(fieldType, true);
                if (result != null) return result;
            }
        }

        // Priority 4: Search in parents if enabled
        if (attr.SearchInParent)
        {
            if (typeof(Component).IsAssignableFrom(fieldType))
            {
                result = component.GetComponentInParent(fieldType, true);
                if (result != null) return result;
            }
        }

        // Priority 5: Search in entire scene if enabled
        if (attr.SearchInScene)
        {
            // Use FindFirstObjectByType for Unity 2023+ or FindObjectOfType for older versions
#if UNITY_2023_1_OR_NEWER
            result = Object.FindFirstObjectByType(fieldType);
#else
            #pragma warning disable CS0618 // Type or member is obsolete
            result = Object.FindObjectOfType(fieldType);
            #pragma warning restore CS0618 // Type or member is obsolete
#endif
            if (result != null) return result;
        }

        return null;
    }

    /// <summary>
    /// Tries to create a component in the GameObject
    /// </summary>
    private static object TryCreateComponent(MonoBehaviour component, System.Type componentType)
    {
        if (component == null || componentType == null) return null;
        
        // Check that it is a valid component type
        if (!typeof(Component).IsAssignableFrom(componentType)) return null;
        
        // Cannot create abstract components or interfaces
        if (componentType.IsAbstract || componentType.IsInterface) return null;
        
        // Some components cannot be added via script
        if (componentType == typeof(Transform) || componentType == typeof(RectTransform))
        {
#if UNITY_EDITOR
            Debug.LogWarning($"[AutoAssigner] ⚠️ Cannot create '{componentType.Name}' - it is a system component");
#endif
            return null;
        }

        try
        {
            // Try to add the component to the GameObject
            Component newComponent = component.gameObject.AddComponent(componentType);
            
#if UNITY_EDITOR
            // Mark the GameObject as modified so Unity saves it
            UnityEditor.EditorUtility.SetDirty(component.gameObject);
#endif
            
            return newComponent;
        }
        catch (System.Exception e)
        {
#if UNITY_EDITOR
            Debug.LogError($"[AutoAssigner] Error creating '{componentType.Name}': {e.Message}");
#endif
            return null;
        }
    }
    }
}