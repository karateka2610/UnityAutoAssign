using UnityEngine;
using System.Reflection;

/// <summary>
/// Clase estática que maneja la auto-configuración de GameObjects usando reflexión.
/// </summary>
public static class AutoSetup
{
    /// <summary>
    /// Aplica la configuración automática a un MonoBehaviour basándose en el atributo [AutoSetup]
    /// </summary>
    /// <param name="component">MonoBehaviour a configurar</param>
    /// <returns>True si se aplicó alguna configuración</returns>
    public static bool ApplySetup(MonoBehaviour component)
    {
        if (component == null) return false;

        var type = component.GetType();
        var setupAttr = type.GetCustomAttribute<AutoSetupAttribute>();

        if (setupAttr == null) return false;

        bool applied = false;
        GameObject gameObject = component.gameObject;

        // Configurar Tag
        if (!string.IsNullOrEmpty(setupAttr.Tag))
        {
            if (IsValidTag(setupAttr.Tag))
            {
                gameObject.tag = setupAttr.Tag;
                applied = true;
#if UNITY_EDITOR
                Debug.Log($"[AutoSetup] ✓ Tag '{setupAttr.Tag}' asignado a '{gameObject.name}'");
#endif
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning($"[AutoSetup] ⚠️ Tag '{setupAttr.Tag}' no existe en el proyecto. Añádelo en Edit > Project Settings > Tags and Layers");
#endif
            }
        }

        // Configurar Layer
        if (!string.IsNullOrEmpty(setupAttr.Layer))
        {
            int layerIndex = LayerMask.NameToLayer(setupAttr.Layer);
            if (layerIndex != -1)
            {
                gameObject.layer = layerIndex;
                applied = true;
#if UNITY_EDITOR
                Debug.Log($"[AutoSetup] ✓ Layer '{setupAttr.Layer}' asignado a '{gameObject.name}'");
#endif

                // Aplicar a hijos si está configurado
                if (setupAttr.ApplyToChildren)
                {
                    SetLayerRecursively(gameObject, layerIndex);
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning($"[AutoSetup] ⚠️ Layer '{setupAttr.Layer}' no existe en el proyecto. Añádelo en Edit > Project Settings > Tags and Layers");
#endif
            }
        }

        // Configurar Sorting Layer y Order (para SpriteRenderer)
        if (!string.IsNullOrEmpty(setupAttr.SortingLayer))
        {
            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                if (IsValidSortingLayer(setupAttr.SortingLayer))
                {
                    spriteRenderer.sortingLayerName = setupAttr.SortingLayer;
                    spriteRenderer.sortingOrder = setupAttr.SortingOrder;
                    applied = true;
#if UNITY_EDITOR
                    Debug.Log($"[AutoSetup] ✓ Sorting Layer '{setupAttr.SortingLayer}' ({setupAttr.SortingOrder}) asignado a '{gameObject.name}'");
#endif

                    // Aplicar a hijos si está configurado
                    if (setupAttr.ApplyToChildren)
                    {
                        SetSortingLayerRecursively(gameObject, setupAttr.SortingLayer, setupAttr.SortingOrder);
                    }
                }
                else
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"[AutoSetup] ⚠️ Sorting Layer '{setupAttr.SortingLayer}' no existe. Añádelo en Edit > Project Settings > Tags and Layers");
#endif
                }
            }
        }

        // Configurar nombre del GameObject
        if (!string.IsNullOrEmpty(setupAttr.GameObjectName))
        {
            gameObject.name = setupAttr.GameObjectName;
            applied = true;
#if UNITY_EDITOR
            Debug.Log($"[AutoSetup] ✓ Nombre '{setupAttr.GameObjectName}' asignado al GameObject");
#endif
        }

        // Configurar si es estático
        if (setupAttr.IsStatic)
        {
            gameObject.isStatic = true;
            applied = true;
#if UNITY_EDITOR
            Debug.Log($"[AutoSetup] ✓ GameObject '{gameObject.name}' marcado como estático");
#endif

            // Aplicar a hijos si está configurado
            if (setupAttr.ApplyToChildren)
            {
                SetStaticRecursively(gameObject);
            }
        }

        return applied;
    }

    /// <summary>
    /// Verifica si un tag existe en el proyecto
    /// </summary>
    private static bool IsValidTag(string tag)
    {
        try
        {
            GameObject.FindGameObjectWithTag(tag);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Verifica si un sorting layer existe en el proyecto
    /// </summary>
    private static bool IsValidSortingLayer(string sortingLayerName)
    {
        // Intenta obtener el ID del sorting layer
        int sortingLayerID = UnityEngine.SortingLayer.NameToID(sortingLayerName);
        return sortingLayerID != 0 || sortingLayerName == "Default";
    }

    /// <summary>
    /// Establece el layer recursivamente en todos los hijos
    /// </summary>
    private static void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }

    /// <summary>
    /// Establece el sorting layer recursivamente en todos los SpriteRenderers de los hijos
    /// </summary>
    private static void SetSortingLayerRecursively(GameObject obj, string sortingLayerName, int sortingOrder)
    {
        var spriteRenderers = obj.GetComponentsInChildren<SpriteRenderer>(true);
        foreach (var sr in spriteRenderers)
        {
            sr.sortingLayerName = sortingLayerName;
            sr.sortingOrder = sortingOrder;
        }
    }

    /// <summary>
    /// Establece el GameObject como estático recursivamente
    /// </summary>
    private static void SetStaticRecursively(GameObject obj)
    {
        obj.isStatic = true;
        foreach (Transform child in obj.transform)
        {
            SetStaticRecursively(child.gameObject);
        }
    }
}
