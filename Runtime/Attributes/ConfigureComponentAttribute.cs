using System;
using UnityEngine;

/// <summary>
/// Atributo para configurar propiedades de un componente automáticamente.
/// Permite establecer valores de propiedades cuando se añade o actualiza el script.
/// </summary>
/// <example>
/// // Configurar gravityScale del Rigidbody2D
/// [AutoAssign(autoCreate: true)]
/// [ConfigureComponent("gravityScale", 0f)]
/// [SerializeField] private Rigidbody2D rb;
/// 
/// // Configurar múltiples propiedades
/// [AutoAssign(autoCreate: true)]
/// [ConfigureComponent("gravityScale", 0f)]
/// [ConfigureComponent("drag", 2f)]
/// [ConfigureComponent("freezeRotation", true)]
/// [SerializeField] private Rigidbody2D rb;
/// 
/// // Configurar Collider como trigger
/// [AutoAssign(autoCreate: true)]
/// [ConfigureComponent("isTrigger", true)]
/// [SerializeField] private BoxCollider2D col;
/// </example>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class ConfigureComponentAttribute : Attribute
{
    /// <summary>
    /// Nombre de la propiedad o campo a configurar
    /// </summary>
    public string PropertyName { get; }

    /// <summary>
    /// Valor a asignar a la propiedad
    /// </summary>
    public object Value { get; }

    /// <summary>
    /// Si es true, solo aplica la configuración cuando se crea el componente
    /// Si es false, siempre aplica la configuración
    /// </summary>
    public bool OnlyOnCreate { get; set; }

    /// <summary>
    /// Constructor del atributo ConfigureComponent
    /// </summary>
    /// <param name="propertyName">Nombre de la propiedad a configurar</param>
    /// <param name="value">Valor a asignar</param>
    public ConfigureComponentAttribute(string propertyName, object value)
    {
        PropertyName = propertyName;
        Value = value;
        OnlyOnCreate = false; // Por defecto siempre aplica
    }
}
