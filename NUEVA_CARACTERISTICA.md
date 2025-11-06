# 🎉 Nueva Característica: [ConfigureComponent]

## ✨ ¿Qué se agregó?

He extendido el plugin **UnityAutoAssign** con una nueva característica poderosa: **`[ConfigureComponent]`**

Este nuevo atributo te permite **configurar automáticamente propiedades de componentes** directamente desde el código.

---

## 📦 Archivos Nuevos Creados

### Runtime:
1. ✅ `ConfigureComponentAttribute.cs` - El nuevo atributo
2. ✅ `ComponentConfigurator.cs` - Lógica de configuración

### Scripts de Ejemplo:
3. ✅ `ExampleConfigureComponent.cs` - Ejemplo completo de uso

### Documentación:
4. ✅ `CONFIGURAR_COMPONENTES.md` - Guía completa con ejemplos

---

## 🚀 Cómo Funciona

### Antes (manual en el Inspector):
1. Añades un componente
2. Buscas el componente en el Inspector
3. Cambias `gravityScale` a 0
4. Cambias `drag` a 0
5. Seleccionas `FreezeRotation`
6. **Repites esto para cada instancia** 😫

### Ahora (automático con código):
```csharp
[AutoAssign(autoCreate: true)]
[ConfigureComponent("gravityScale", 0f)]
[ConfigureComponent("drag", 0f)]
[ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)]
[SerializeField] private Rigidbody2D rb;
```
**¡Y listo!** Se configura automáticamente 🎉

---

## 📋 Ejemplos de Uso

### Tu PlayerController Ahora:
```csharp
[AutoSetup(Tag = "Player", Layer = "Default")]
public class PlayerController : MonoBehaviour
{
    // Rigidbody2D auto-creado y configurado
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("gravityScale", 0f)]         // Sin gravedad
    [ConfigureComponent("drag", 0f)]                 // Sin fricción
    [ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)]
    [SerializeField] private Rigidbody2D rb;
    
    // BoxCollider2D auto-creado como sólido
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("isTrigger", false)]
    [SerializeField] private BoxCollider2D boxCollider;
    
    // CircleCollider2D con radio personalizado
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("isTrigger", false)]
    [ConfigureComponent("radius", 0.5f)]
    [SerializeField] private CircleCollider2D circleCollider;
}
```

### Otros Ejemplos:

#### AudioSource Configurado:
```csharp
[AutoAssign(autoCreate: true)]
[ConfigureComponent("playOnAwake", false)]
[ConfigureComponent("loop", true)]
[ConfigureComponent("volume", 0.5f)]
[SerializeField] private AudioSource bgMusic;
```

#### Collider como Trigger:
```csharp
[AutoAssign(autoCreate: true)]
[ConfigureComponent("isTrigger", true)]
[ConfigureComponent("radius", 2f)]
[SerializeField] private CircleCollider2D detectionZone;
```

#### Camera Ortográfica:
```csharp
[AutoAssign(searchInScene: true)]
[ConfigureComponent("orthographic", true)]
[ConfigureComponent("orthographicSize", 10f)]
[SerializeField] private Camera mainCam;
```

---

## 🎯 Componentes Soportados

Funciona con **TODOS los componentes de Unity**. Ejemplos:

### ✅ Física:
- `Rigidbody2D` - gravityScale, drag, mass, constraints, etc.
- `Rigidbody` - useGravity, mass, drag, etc.
- `Collider2D` - isTrigger, offset, radius, size, etc.
- `Collider` - isTrigger, center, size, etc.

### ✅ Audio:
- `AudioSource` - volume, pitch, loop, playOnAwake, etc.

### ✅ Rendering:
- `SpriteRenderer` - color, flipX, flipY, sortingOrder, etc.
- `Camera` - orthographic, fieldOfView, backgroundColor, etc.
- `Light` - color, intensity, range, type, etc.

### ✅ Y CUALQUIER otro componente!
El sistema usa reflexión, así que funciona con componentes custom también.

---

## 🔧 Orden de Ejecución

Cuando añades un script, esto sucede automáticamente:

1. **AutoSetup** - Configura tags, layers, nombres
2. **AutoAssign** - Crea/asigna componentes
3. **ConfigureComponent** - Configura propiedades de componentes ⬅️ **NUEVO**

Todo en orden perfecto! 🎯

---

## ⚡ Características del Sistema

### ✅ Inteligente:
- Usa reflexión para encontrar propiedades
- Soporta propiedades y campos públicos
- Convierte tipos automáticamente
- Maneja enums, bools, floats, ints, etc.

### ✅ Seguro:
- Muestra advertencias si la propiedad no existe
- No rompe si algo falla
- Logs claros en la consola

### ✅ Flexible:
- Múltiples atributos en el mismo campo
- Funciona con cualquier componente
- Parámetro `OnlyOnCreate` para aplicar solo al crear

---

## 📖 Documentación

Lee `CONFIGURAR_COMPONENTES.md` para:
- Ejemplos de todos los componentes comunes
- Sintaxis avanzada
- Solución de problemas
- Tips y mejores prácticas

---

## 🎮 Pruébalo Ahora

1. Abre Unity (espera a que compile)
2. Crea un GameObject vacío
3. Añade el script `PlayerController` o `ExampleConfigureComponent`
4. Observa la consola - verás logs de lo que se configuró
5. Revisa el Inspector - los componentes tendrán los valores correctos

---

## 🤝 Resumen Total del Plugin

Ahora tienes **3 atributos potentes**:

### 1. [AutoSetup] - Configuración de GameObject
```csharp
[AutoSetup(Tag = "Player", Layer = "Character")]
```
Configura: tags, layers, nombres, static, sorting layers

### 2. [AutoAssign] - Asignación de Componentes  
```csharp
[AutoAssign(autoCreate: true)]
```
Crea/busca: componentes automáticamente

### 3. [ConfigureComponent] - Configuración de Propiedades ⭐ NUEVO
```csharp
[ConfigureComponent("gravityScale", 0f)]
```
Configura: propiedades de componentes

---

## 🎉 ¡Todo Automático!

```csharp
[AutoSetup(Tag = "Player")]                          // 1. Configura GameObject
public class Player : MonoBehaviour
{
    [AutoAssign(autoCreate: true)]                   // 2. Crea componente
    [ConfigureComponent("gravityScale", 0f)]         // 3. Configura propiedad
    [SerializeField] private Rigidbody2D rb;
}
```

**Una línea de código = Configuración completa automática** 🚀

---

¿Necesitas más ejemplos o tienes preguntas? ¡Pregunta! 😊
