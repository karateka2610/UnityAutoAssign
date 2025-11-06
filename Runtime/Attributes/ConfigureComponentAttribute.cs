using System;
using UnityEngine;

namespace UnityAutoAssign
{
    /// <summary>
    /// Attribute to automatically configure component properties.
    /// Allows setting property values when the script is added or updated.
    /// </summary>
    /// <example>
    /// // Configure gravityScale of Rigidbody2D
    /// [AutoAssign(autoCreate: true)]
    /// [ConfigureComponent("gravityScale", 0f)]
    /// [SerializeField] private Rigidbody2D rb;
    /// 
    /// // Configure multiple properties
    /// [AutoAssign(autoCreate: true)]
    /// [ConfigureComponent("gravityScale", 0f)]
    /// [ConfigureComponent("drag", 2f)]
    /// [ConfigureComponent("freezeRotation", true)]
    /// [SerializeField] private Rigidbody2D rb;
    /// 
    /// // Configure Collider as trigger
    /// [AutoAssign(autoCreate: true)]
    /// [ConfigureComponent("isTrigger", true)]
    /// [SerializeField] private BoxCollider2D col;
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ConfigureComponentAttribute : Attribute
{
    /// <summary>
    /// Name of the property or field to configure
    /// </summary>
    public string PropertyName { get; }

    /// <summary>
    /// Value to assign to the property
    /// </summary>
    public object Value { get; }

    /// <summary>
    /// If true, only applies the configuration when the component is created
    /// If false, always applies the configuration
    /// </summary>
    public bool OnlyOnCreate { get; set; }

    /// <summary>
    /// Constructor of the ConfigureComponent attribute
    /// </summary>
    /// <param name="propertyName">Name of the property to configure</param>
    /// <param name="value">Value to assign</param>
    public ConfigureComponentAttribute(string propertyName, object value)
    {
        PropertyName = propertyName;
        Value = value;
        OnlyOnCreate = false; // By default always applies
    }
    }
}
