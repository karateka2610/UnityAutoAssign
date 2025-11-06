using System;
using UnityEngine;

/// <summary>
/// Atributo para configurar automáticamente propiedades del GameObject o componente.
/// Configura tags, layers, sorting layers, nombres, y más propiedades al añadir el componente.
/// </summary>
/// <example>
/// // Configurar tag y layer del GameObject
/// [AutoSetup(tag: "Player", layer: "Default")]
/// public class PlayerController : MonoBehaviour { }
/// 
/// // Configurar sorting layer del SpriteRenderer
/// [AutoSetup(sortingLayer: "Character", sortingOrder: 10)]
/// public class CharacterSprite : MonoBehaviour { }
/// 
/// // Cambiar nombre del GameObject
/// [AutoSetup(gameObjectName: "Player")]
/// public class PlayerController : MonoBehaviour { }
/// </example>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class AutoSetupAttribute : Attribute
{
    /// <summary>
    /// Tag a asignar al GameObject
    /// </summary>
    public string Tag { get; set; }

    /// <summary>
    /// Layer a asignar al GameObject (nombre del layer)
    /// </summary>
    public string Layer { get; set; }

    /// <summary>
    /// Nombre a asignar al GameObject
    /// </summary>
    public string GameObjectName { get; set; }

    /// <summary>
    /// Sorting Layer para SpriteRenderer (nombre del sorting layer)
    /// </summary>
    public string SortingLayer { get; set; }

    /// <summary>
    /// Order in Layer para SpriteRenderer
    /// </summary>
    public int SortingOrder { get; set; }

    /// <summary>
    /// Si es true, hace que el GameObject sea estático
    /// </summary>
    public bool IsStatic { get; set; }

    /// <summary>
    /// Si es true, aplica la configuración a todos los hijos también
    /// </summary>
    public bool ApplyToChildren { get; set; }

    /// <summary>
    /// Constructor del atributo AutoSetup
    /// </summary>
    public AutoSetupAttribute()
    {
        SortingOrder = 0;
        IsStatic = false;
        ApplyToChildren = false;
    }
}
