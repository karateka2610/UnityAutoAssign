using UnityEngine;
using System.Reflection;
using System.Linq;

/// <summary>
/// Clase estática que maneja la configuración automática de componentes.
/// </summary>
public static class ComponentConfigurator
{
    /// <summary>
    /// Aplica la configuración de componentes en un MonoBehaviour basándose en [ConfigureComponent]
    /// </summary>
    /// <param name="component">MonoBehaviour a procesar</param>
    /// <returns>Número de propiedades configuradas</returns>
    public static int ConfigureComponents(MonoBehaviour component)
    {
        if (component == null) return 0;

        var type = component.GetType();
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        int configuredCount = 0;

        foreach (var field in fields)
        {
            // Obtener todos los atributos ConfigureComponent en este campo
            var configAttributes = field.GetCustomAttributes<ConfigureComponentAttribute>().ToArray();
            if (configAttributes.Length == 0) continue;

            // Obtener el componente del campo
            var fieldValue = field.GetValue(component);
            if (fieldValue == null) continue;

            // Si es un Component de Unity, aplicar configuraciones
            if (fieldValue is Component targetComponent)
            {
                foreach (var configAttr in configAttributes)
                {
                    if (ApplyConfiguration(targetComponent, configAttr))
                    {
                        configuredCount++;
                    }
                }
            }
        }

        return configuredCount;
    }

    /// <summary>
    /// Aplica una configuración específica a un componente
    /// </summary>
    private static bool ApplyConfiguration(Component component, ConfigureComponentAttribute config)
    {
        if (component == null || string.IsNullOrEmpty(config.PropertyName)) return false;

        var componentType = component.GetType();

        // Intentar encontrar y establecer como propiedad
        var property = componentType.GetProperty(config.PropertyName, BindingFlags.Public | BindingFlags.Instance);
        if (property != null && property.CanWrite)
        {
            try
            {
                // Convertir el valor al tipo correcto
                object convertedValue = ConvertValue(config.Value, property.PropertyType);
                property.SetValue(component, convertedValue);

#if UNITY_EDITOR
                Debug.Log($"[ConfigureComponent] ✓ '{config.PropertyName}' = {convertedValue} en {componentType.Name}");
#endif
                return true;
            }
            catch (System.Exception e)
            {
#if UNITY_EDITOR
                Debug.LogWarning($"[ConfigureComponent] ⚠️ Error al configurar propiedad '{config.PropertyName}' en {componentType.Name}: {e.Message}");
#endif
                return false;
            }
        }

        // Intentar encontrar y establecer como campo público
        var field = componentType.GetField(config.PropertyName, BindingFlags.Public | BindingFlags.Instance);
        if (field != null)
        {
            try
            {
                // Convertir el valor al tipo correcto
                object convertedValue = ConvertValue(config.Value, field.FieldType);
                field.SetValue(component, convertedValue);

#if UNITY_EDITOR
                Debug.Log($"[ConfigureComponent] ✓ '{config.PropertyName}' = {convertedValue} en {componentType.Name}");
#endif
                return true;
            }
            catch (System.Exception e)
            {
#if UNITY_EDITOR
                Debug.LogWarning($"[ConfigureComponent] ⚠️ Error al configurar campo '{config.PropertyName}' en {componentType.Name}: {e.Message}");
#endif
                return false;
            }
        }

#if UNITY_EDITOR
        Debug.LogWarning($"[ConfigureComponent] ⚠️ No se encontró la propiedad o campo '{config.PropertyName}' en {componentType.Name}");
#endif
        return false;
    }

    /// <summary>
    /// Convierte un valor al tipo objetivo
    /// </summary>
    private static object ConvertValue(object value, System.Type targetType)
    {
        if (value == null) return null;

        // Si el valor ya es del tipo correcto, devolverlo
        if (targetType.IsAssignableFrom(value.GetType()))
        {
            return value;
        }

        // Manejar enums
        if (targetType.IsEnum)
        {
            if (value is string stringValue)
            {
                return System.Enum.Parse(targetType, stringValue, true);
            }
            return System.Enum.ToObject(targetType, value);
        }

        // Manejar tipos primitivos y conversiones estándar
        try
        {
            return System.Convert.ChangeType(value, targetType);
        }
        catch
        {
            // Si falla, intentar cast directo
            return value;
        }
    }

    /// <summary>
    /// Configura un componente específico con valores predeterminados
    /// </summary>
    public static void ConfigureRigidbody2D(Rigidbody2D rb, float gravityScale = 0f, float drag = 0f, float angularDrag = 0.05f, bool freezeRotation = false)
    {
        if (rb == null) return;

        rb.gravityScale = gravityScale;
        rb.linearDamping = drag;
        rb.angularDamping = angularDrag;
        
        if (freezeRotation)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(rb);
#endif
    }

    /// <summary>
    /// Configura un Collider2D como trigger o sólido
    /// </summary>
    public static void ConfigureCollider2D(Collider2D collider, bool isTrigger = false)
    {
        if (collider == null) return;

        collider.isTrigger = isTrigger;

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(collider);
#endif
    }
}
