using System;
using UnityEngine;

/// <summary>
/// Atributo para autoasignar componentes en campos SerializeField.
/// Usa reflexión para buscar y asignar componentes automáticamente en el Editor.
/// </summary>
/// <example>
/// [AutoAssign] [SerializeField] private Rigidbody rb;
/// [AutoAssign(searchInChildren: true)] [SerializeField] private Animator anim;
/// [AutoAssign(searchInScene: true)] [SerializeField] private Camera mainCam;
/// [AutoAssign(autoCreate: true)] [SerializeField] private BoxCollider col;
/// [AutoAssign(tag: "Player")] [SerializeField] private GameObject player;
/// </example>
[AttributeUsage(AttributeTargets.Field)]
public class AutoAssignAttribute : PropertyAttribute
{
    /// <summary>
    /// Buscar el componente en los hijos del GameObject (incluye inactivos)
    /// </summary>
    public bool SearchInChildren { get; }
    
    /// <summary>
    /// Buscar el componente en los padres del GameObject (incluye inactivos)
    /// </summary>
    public bool SearchInParent { get; }
    
    /// <summary>
    /// Buscar el componente en toda la escena actual
    /// </summary>
    public bool SearchInScene { get; }
    
    /// <summary>
    /// Si no encuentra el componente, lo crea automáticamente en el GameObject
    /// Solo funciona con componentes que puedan ser añadidos (no busca en hijos/padres/escena)
    /// </summary>
    public bool AutoCreate { get; }
    
    /// <summary>
    /// Tag para buscar GameObject en la escena
    /// Busca el primer GameObject con este tag
    /// </summary>
    public string Tag { get; }

    /// <summary>
    /// Constructor del atributo AutoAssign
    /// </summary>
    /// <param name="searchInChildren">Si es true, busca en los hijos</param>
    /// <param name="searchInParent">Si es true, busca en los padres</param>
    /// <param name="searchInScene">Si es true, busca en toda la escena</param>
    /// <param name="autoCreate">Si es true y no encuentra el componente, lo crea en el GameObject</param>
    /// <param name="tag">Tag del GameObject a buscar en la escena</param>
    public AutoAssignAttribute(bool searchInChildren = false, bool searchInParent = false, bool searchInScene = false, bool autoCreate = false, string tag = null)
    {
        SearchInChildren = searchInChildren;
        SearchInParent = searchInParent;
        SearchInScene = searchInScene;
        AutoCreate = autoCreate;
        Tag = tag;
    }
}