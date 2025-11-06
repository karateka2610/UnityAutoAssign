using UnityEngine;
using System.Collections.Generic;

namespace UnityAutoAssign
{
    /// <summary>
    /// [NEW v3.0] Caching system to optimize component searches.
    /// Prevents multiple unnecessary lookups.
    /// </summary>
    public static class AutoAssignCache
{
    private static Dictionary<string, Component> componentCache = new Dictionary<string, Component>();
    private static Dictionary<string, GameObject> gameObjectCache = new Dictionary<string, GameObject>();
    private static bool cacheEnabled = true;

    /// <summary>
    /// Enable or disable the caching system
    /// </summary>
    public static void SetCacheEnabled(bool enabled)
    {
        cacheEnabled = enabled;
        if (!enabled)
        {
            ClearCache();
        }
    }

    /// <summary>
    /// Generate a cache key for a component
    /// </summary>
    public static string GenerateComponentCacheKey(GameObject gameObject, System.Type componentType, string searchScope)
    {
        return $"{gameObject.GetInstanceID()}_{componentType.Name}_{searchScope}";
    }

    /// <summary>
    /// Generate a cache key for a GameObject
    /// </summary>
    public static string GenerateGameObjectCacheKey(string tag)
    {
        return $"GameObject_Tag_{tag}";
    }

    /// <summary>
    /// Try to get a component from cache
    /// </summary>
    public static bool TryGetCachedComponent(string key, out Component component)
    {
        component = null;
        if (!cacheEnabled) return false;

        return componentCache.TryGetValue(key, out component);
    }

    /// <summary>
    /// Cache a component
    /// </summary>
    public static void CacheComponent(string key, Component component)
    {
        if (!cacheEnabled) return;

        if (componentCache.ContainsKey(key))
            componentCache[key] = component;
        else
            componentCache.Add(key, component);
    }

    /// <summary>
    /// Try to get a GameObject from cache
    /// </summary>
    public static bool TryGetCachedGameObject(string key, out GameObject gameObject)
    {
        gameObject = null;
        if (!cacheEnabled) return false;

        return gameObjectCache.TryGetValue(key, out gameObject);
    }

    /// <summary>
    /// Cache a GameObject
    /// </summary>
    public static void CacheGameObject(string key, GameObject gameObject)
    {
        if (!cacheEnabled) return;

        if (gameObjectCache.ContainsKey(key))
            gameObjectCache[key] = gameObject;
        else
            gameObjectCache.Add(key, gameObject);
    }

    /// <summary>
    /// Clear all cache
    /// </summary>
    public static void ClearCache()
    {
        componentCache.Clear();
        gameObjectCache.Clear();
    }

    /// <summary>
    /// Get cache statistics
    /// </summary>
    public static (int componentCacheSize, int gameObjectCacheSize) GetCacheStats()
    {
        return (componentCache.Count, gameObjectCache.Count);
    }

    /// <summary>
    /// Clean up null references from cache (maintenance)
    /// </summary>
    public static void CleanupDeadReferences()
    {
        var deadKeys = new List<string>();

        foreach (var kvp in componentCache)
        {
            if (kvp.Value == null)
                deadKeys.Add(kvp.Key);
        }

        foreach (var key in deadKeys)
        {
            componentCache.Remove(key);
        }

        deadKeys.Clear();

        foreach (var kvp in gameObjectCache)
        {
            if (kvp.Value == null)
                deadKeys.Add(kvp.Key);
        }
        foreach (var key in deadKeys)
        {
            gameObjectCache.Remove(key);
        }
    }
    }
}
