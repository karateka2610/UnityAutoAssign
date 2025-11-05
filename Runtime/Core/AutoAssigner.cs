using UnityEngine;
using System.Reflection;

/// <summary>
/// Clase estática que maneja la auto-asignación de componentes usando reflexión.
/// </summary>
public static class AutoAssigner
{
    /// <summary>
    /// Asigna automáticamente todos los campos con [AutoAssign] en todos los MonoBehaviours de un GameObject
    /// </summary>
    /// <param name="gameObject">GameObject a procesar</param>
    /// <returns>Número total de campos asignados</returns>
    public static int AssignAll(GameObject gameObject)
    {
        if (gameObject == null)
        {
            Debug.LogWarning("[AutoAssigner] GameObject es null");
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
    /// Asigna campos con [AutoAssign] en un MonoBehaviour específico
    /// </summary>
    /// <param name="component">MonoBehaviour a procesar</param>
    /// <returns>Número de campos asignados</returns>
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

            // Verificar si tiene SerializeField o es público
            var serializeFieldAttr = field.GetCustomAttribute<SerializeField>();
            if (serializeFieldAttr == null && !field.IsPublic)
            {
                Debug.LogWarning($"[AutoAssigner] Campo '{field.Name}' en '{type.Name}' tiene [AutoAssign] pero no es serializable. Añade [SerializeField]");
                continue;
            }

            // Si ya tiene un valor asignado, no sobrescribir
            var currentValue = field.GetValue(component);
            if (currentValue != null && !currentValue.Equals(null)) continue;

            // Intentar asignar según las opciones del atributo
            object assignedValue = TryAssign(component, field.FieldType, attribute);

            if (assignedValue != null)
            {
                field.SetValue(component, assignedValue);
                assignedCount++;
            }
            else if (attribute.AutoCreate && typeof(Component).IsAssignableFrom(field.FieldType))
            {
                // Intentar crear el componente si autoCreate está habilitado
                object createdComponent = TryCreateComponent(component, field.FieldType);
                if (createdComponent != null)
                {
                    field.SetValue(component, createdComponent);
                    assignedCount++;
                }
                else
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"[AutoAssigner] No se pudo crear '{field.Name}' de tipo '{field.FieldType.Name}' en '{component.gameObject.name}'");
#endif
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning($"[AutoAssigner] No se encontró '{field.Name}' de tipo '{field.FieldType.Name}' en '{component.gameObject.name}'");
#endif
            }
        }

        return assignedCount;
    }

    /// <summary>
    /// Intenta asignar un componente según la estrategia del atributo
    /// </summary>
    private static object TryAssign(MonoBehaviour component, System.Type fieldType, AutoAssignAttribute attr)
    {
        Object result = null;

        // Prioridad 1: Buscar por Tag si está especificado
        if (!string.IsNullOrEmpty(attr.Tag))
        {
            GameObject taggedObject = GameObject.FindGameObjectWithTag(attr.Tag);
            if (taggedObject != null)
            {
                // Si el field es GameObject, devolver el GameObject
                if (fieldType == typeof(GameObject))
                {
                    return taggedObject;
                }
                // Si es un componente, buscarlo en el GameObject
                else if (typeof(Component).IsAssignableFrom(fieldType))
                {
                    result = taggedObject.GetComponent(fieldType);
                    if (result != null) return result;
                }
            }
        }

        // Prioridad 2: Buscar en el mismo GameObject
        if (typeof(Component).IsAssignableFrom(fieldType))
        {
            result = component.GetComponent(fieldType);
            if (result != null) return result;
        }

        // Prioridad 3: Buscar en hijos si está habilitado
        if (attr.SearchInChildren)
        {
            if (typeof(Component).IsAssignableFrom(fieldType))
            {
                result = component.GetComponentInChildren(fieldType, true);
                if (result != null) return result;
            }
        }

        // Prioridad 4: Buscar en padres si está habilitado
        if (attr.SearchInParent)
        {
            if (typeof(Component).IsAssignableFrom(fieldType))
            {
                result = component.GetComponentInParent(fieldType, true);
                if (result != null) return result;
            }
        }

        // Prioridad 5: Buscar en toda la escena si está habilitado
        if (attr.SearchInScene)
        {
            // Usar FindFirstObjectByType para Unity 2023+ o FindObjectOfType para versiones anteriores
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
    /// Intenta crear un componente en el GameObject
    /// </summary>
    private static object TryCreateComponent(MonoBehaviour component, System.Type componentType)
    {
        if (component == null || componentType == null) return null;
        
        // Verificar que sea un tipo de componente válido
        if (!typeof(Component).IsAssignableFrom(componentType)) return null;
        
        // No se pueden crear componentes abstractos o interfaces
        if (componentType.IsAbstract || componentType.IsInterface) return null;
        
        // Algunos componentes no se pueden añadir mediante script
        if (componentType == typeof(Transform) || componentType == typeof(RectTransform))
        {
#if UNITY_EDITOR
            Debug.LogWarning($"[AutoAssigner] ⚠️ No se puede crear '{componentType.Name}' - es un componente del sistema");
#endif
            return null;
        }

        try
        {
            // Intentar añadir el componente al GameObject
            Component newComponent = component.gameObject.AddComponent(componentType);
            
#if UNITY_EDITOR
            // Marcar el GameObject como modificado para que Unity lo guarde
            UnityEditor.EditorUtility.SetDirty(component.gameObject);
#endif
            
            return newComponent;
        }
        catch (System.Exception e)
        {
#if UNITY_EDITOR
            Debug.LogError($"[AutoAssigner] ❌ Error al crear '{componentType.Name}': {e.Message}");
#endif
            return null;
        }
    }
}