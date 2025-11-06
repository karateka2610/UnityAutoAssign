using System;
using UnityEngine;

namespace UnityAutoAssign
{
    /// <summary>
    /// Attribute to auto-assign components in SerializeField fields.
    /// Uses reflection to search and assign components automatically in the Editor.
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
    /// Search for the component in the GameObject's children (includes inactive)
    /// </summary>
    public bool SearchInChildren { get; }
    
    /// <summary>
    /// Search for the component in the GameObject's parents (includes inactive)
    /// </summary>
    public bool SearchInParent { get; }
    
    /// <summary>
    /// Search for the component in the entire current scene
    /// </summary>
    public bool SearchInScene { get; }
    
    /// <summary>
    /// If the component is not found, create it automatically in the GameObject
    /// Only works with components that can be added (does not search in children/parents/scene)
    /// </summary>
    public bool AutoCreate { get; }
    
    /// <summary>
    /// Tag to search for GameObject in the scene
    /// Searches for the first GameObject with this tag
    /// </summary>
    public string Tag { get; }

    /// <summary>
    /// Constructor of the AutoAssign attribute
    /// </summary>
    /// <param name="searchInChildren">If true, searches in children</param>
    /// <param name="searchInParent">If true, searches in parents</param>
    /// <param name="searchInScene">If true, searches in the entire scene</param>
    /// <param name="autoCreate">If true and the component is not found, it creates it in the GameObject</param>
    /// <param name="tag">Tag of the GameObject to search in the scene</param>
    public AutoAssignAttribute(bool searchInChildren = false, bool searchInParent = false, bool searchInScene = false, bool autoCreate = false, string tag = null)
    {
        SearchInChildren = searchInChildren;
        SearchInParent = searchInParent;
        SearchInScene = searchInScene;
        AutoCreate = autoCreate;
        Tag = tag;
    }
    }
}