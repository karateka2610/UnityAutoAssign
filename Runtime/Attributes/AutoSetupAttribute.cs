using System;
using UnityEngine;

namespace UnityAutoAssign
{
    /// <summary>
    /// Attribute to automatically configure GameObject or component properties.
    /// Configures tags, layers, sorting layers, names, and more properties when the component is added.
    /// </summary>
    /// <example>
    /// // Configure tag and layer of the GameObject
    /// [AutoSetup(Tag = "Player", Layer = "Default")]
    /// public class PlayerController : MonoBehaviour { }
    /// 
    /// // Configure sorting layer of the SpriteRenderer
    /// [AutoSetup(SortingLayer = "Character", SortingOrder = 10)]
    /// public class CharacterSprite : MonoBehaviour { }
    /// 
    /// // Change GameObject name
    /// [AutoSetup(GameObjectName = "Player")]
    /// public class PlayerController : MonoBehaviour { }
    /// </example>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AutoSetupAttribute : Attribute
{
    /// <summary>
    /// Tag to assign to the GameObject
    /// </summary>
    public string Tag { get; set; }

    /// <summary>
    /// Layer to assign to the GameObject (layer name)
    /// </summary>
    public string Layer { get; set; }

    /// <summary>
    /// Name to assign to the GameObject
    /// </summary>
    public string GameObjectName { get; set; }

    /// <summary>
    /// Sorting Layer for SpriteRenderer (sorting layer name)
    /// </summary>
    public string SortingLayer { get; set; }

    /// <summary>
    /// Order in Layer for SpriteRenderer
    /// </summary>
    public int SortingOrder { get; set; }

    /// <summary>
    /// If true, makes the GameObject static
    /// </summary>
    public bool IsStatic { get; set; }

    /// <summary>
    /// If true, applies the configuration to all children as well
    /// </summary>
    public bool ApplyToChildren { get; set; }

    /// <summary>
    /// Constructor of the AutoSetup attribute
    /// </summary>
    public AutoSetupAttribute()
    {
        SortingOrder = 0;
        IsStatic = false;
        ApplyToChildren = false;
    }
    }
}
