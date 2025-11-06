# 🎯 ConfigureComponent - Guía de Uso

## ¿Qué es [ConfigureComponent]?

Es un atributo que te permite **configurar automáticamente propiedades de componentes** directamente desde el código. En lugar de tener que configurar manualmente el `gravityScale`, `isTrigger`, `volume`, etc. en el Inspector, lo defines una vez en el código y se aplica automáticamente.

---

## 🚀 Uso Básico

### Sintaxis:
```csharp
[ConfigureComponent("nombrePropiedad", valor)]
```

### Ejemplo Simple:
```csharp
[AutoAssign(autoCreate: true)]
[ConfigureComponent("gravityScale", 0f)]
[SerializeField] private Rigidbody2D rb;
```

Esto hará que el Rigidbody2D tenga `gravityScale = 0` automáticamente cuando se cree o actualice.

---

## 📋 Ejemplos por Componente

### 🎮 Rigidbody2D

```csharp
[AutoAssign(autoCreate: true)]
[ConfigureComponent("gravityScale", 0f)]          // Sin gravedad
[ConfigureComponent("drag", 2f)]                  // Fricción del aire
[ConfigureComponent("angularDrag", 0.05f)]        // Fricción angular
[ConfigureComponent("mass", 1.5f)]                // Masa
[ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)]  // Bloquear rotación
[SerializeField] private Rigidbody2D rb;
```

**Propiedades disponibles:**
- `gravityScale` (float) - Escala de gravedad
- `drag` (float) - Fricción lineal
- `angularDrag` (float) - Fricción angular
- `mass` (float) - Masa del objeto
- `constraints` (RigidbodyConstraints2D) - Restricciones de movimiento
- `freezeRotation` (bool) - Congelar rotación (alternativa a constraints)

---

### 📦 BoxCollider2D

```csharp
[AutoAssign(autoCreate: true)]
[ConfigureComponent("isTrigger", true)]           // Como trigger
[ConfigureComponent("offset", typeof(Vector2), 0f, 0.5f)]  // Offset (x, y)
[SerializeField] private BoxCollider2D boxCollider;
```

**Propiedades disponibles:**
- `isTrigger` (bool) - Trigger o colisión sólida
- `size` (Vector2) - Tamaño del collider
- `offset` (Vector2) - Desplazamiento

---

### ⭕ CircleCollider2D

```csharp
[AutoAssign(autoCreate: true)]
[ConfigureComponent("isTrigger", false)]          // Colisión sólida
[ConfigureComponent("radius", 0.5f)]              // Radio
[ConfigureComponent("offset", typeof(Vector2), 0f, 0f)]
[SerializeField] private CircleCollider2D circleCollider;
```

**Propiedades disponibles:**
- `isTrigger` (bool) - Trigger o colisión sólida
- `radius` (float) - Radio del círculo
- `offset` (Vector2) - Desplazamiento

---

### 🔊 AudioSource

```csharp
[AutoAssign(autoCreate: true)]
[ConfigureComponent("playOnAwake", false)]        // No reproducir al iniciar
[ConfigureComponent("loop", true)]                // Repetir en bucle
[ConfigureComponent("volume", 0.5f)]              // Volumen al 50%
[ConfigureComponent("pitch", 1.2f)]               // Pitch más agudo
[ConfigureComponent("spatialBlend", 1f)]          // Sonido 3D
[SerializeField] private AudioSource audioSource;
```

**Propiedades disponibles:**
- `playOnAwake` (bool) - Reproducir al iniciar
- `loop` (bool) - Repetir en bucle
- `volume` (float) - Volumen (0-1)
- `pitch` (float) - Tono del sonido
- `spatialBlend` (float) - 2D (0) a 3D (1)
- `priority` (int) - Prioridad (0-256)

---

### 🎥 Camera

```csharp
[AutoAssign(searchInScene: true)]
[ConfigureComponent("orthographic", true)]        // Cámara ortográfica
[ConfigureComponent("orthographicSize", 10f)]     // Tamaño
[ConfigureComponent("backgroundColor", typeof(Color), 0f, 0f, 0f, 1f)]
[SerializeField] private Camera mainCamera;
```

**Propiedades disponibles:**
- `orthographic` (bool) - Ortográfica o perspectiva
- `orthographicSize` (float) - Tamaño de la cámara ortográfica
- `fieldOfView` (float) - Campo de visión en perspectiva
- `nearClipPlane` (float) - Plano de recorte cercano
- `farClipPlane` (float) - Plano de recorte lejano
- `backgroundColor` (Color) - Color de fondo

---

### 💡 Light

```csharp
[AutoAssign(autoCreate: true)]
[ConfigureComponent("type", LightType.Point)]     // Tipo de luz
[ConfigureComponent("color", typeof(Color), 1f, 1f, 0f, 1f)]  // Amarillo
[ConfigureComponent("intensity", 2f)]             // Intensidad
[ConfigureComponent("range", 10f)]                // Alcance
[SerializeField] private Light pointLight;
```

**Propiedades disponibles:**
- `type` (LightType) - Tipo de luz (Point, Spot, Directional, Area)
- `color` (Color) - Color de la luz
- `intensity` (float) - Intensidad
- `range` (float) - Alcance
- `spotAngle` (float) - Ángulo del spot

---

### 🎨 SpriteRenderer

```csharp
[AutoAssign(searchInChildren: true)]
[ConfigureComponent("color", typeof(Color), 1f, 0f, 0f, 1f)]  // Rojo
[ConfigureComponent("flipX", false)]              // No voltear horizontalmente
[ConfigureComponent("flipY", false)]              // No voltear verticalmente
[ConfigureComponent("sortingOrder", 10)]          // Orden de renderizado
[SerializeField] private SpriteRenderer spriteRenderer;
```

**Propiedades disponibles:**
- `color` (Color) - Color tint
- `flipX` (bool) - Voltear horizontalmente
- `flipY` (bool) - Voltear verticalmente
- `sortingOrder` (int) - Orden en el layer
- `sortingLayerName` (string) - Nombre del sorting layer

---

## 🎯 Ejemplos Avanzados

### Configuración Completa de Player

```csharp
[AutoSetup(Tag = "Player", Layer = "Player")]
public class Player : MonoBehaviour
{
    // Rigidbody configurado para top-down sin gravedad
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("gravityScale", 0f)]
    [ConfigureComponent("drag", 0f)]
    [ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)]
    [SerializeField] private Rigidbody2D rb;
    
    // Collider sólido para colisiones
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("isTrigger", false)]
    [ConfigureComponent("radius", 0.5f)]
    [SerializeField] private CircleCollider2D mainCollider;
    
    // Trigger para detección de items
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("isTrigger", true)]
    [ConfigureComponent("radius", 1.5f)]
    [SerializeField] private CircleCollider2D detectionTrigger;
}
```

### Enemy con AudioSource

```csharp
[AutoSetup(Tag = "Enemy", Layer = "Enemy")]
public class Enemy : MonoBehaviour
{
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("gravityScale", 1f)]
    [SerializeField] private Rigidbody2D rb;
    
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("playOnAwake", false)]
    [ConfigureComponent("loop", false)]
    [ConfigureComponent("volume", 0.8f)]
    [SerializeField] private AudioSource hurtSound;
}
```

### Pickup Item (Solo Trigger)

```csharp
[AutoSetup(Tag = "Item", Layer = "Items")]
public class PickupItem : MonoBehaviour
{
    // Sin Rigidbody, solo trigger estático
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("isTrigger", true)]
    [ConfigureComponent("size", typeof(Vector2), 1f, 1f)]
    [SerializeField] private BoxCollider2D pickupTrigger;
}
```

---

## ⚙️ Configuración de Valores Complejos

### Vector2, Vector3
```csharp
// Forma incorrecta (no funciona)
[ConfigureComponent("size", new Vector2(2f, 3f))]  // ❌

// Forma correcta
[ConfigureComponent("size", typeof(Vector2), 2f, 3f)]  // ✅ (requiere modificación futura)
```

### Enums
```csharp
[ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)]  // ✅
[ConfigureComponent("type", LightType.Point)]  // ✅
```

### Booleans
```csharp
[ConfigureComponent("isTrigger", true)]   // ✅
[ConfigureComponent("loop", false)]       // ✅
```

### Números
```csharp
[ConfigureComponent("gravityScale", 0f)]     // float ✅
[ConfigureComponent("sortingOrder", 10)]     // int ✅
```

---

## 🔧 Propiedades del Atributo

```csharp
[ConfigureComponent("propiedad", valor, OnlyOnCreate = true)]
```

- **OnlyOnCreate**: Si es `true`, solo aplica cuando se crea el componente por primera vez
- Por defecto es `false`, lo que significa que siempre actualiza el valor

---

## 💡 Tips y Mejores Prácticas

### ✅ Hacer:
- Usar para valores que siempre serán iguales en todas las instancias
- Combinar con `[AutoAssign(autoCreate: true)]` para configuración completa
- Documentar con comentarios qué hace cada configuración
- Usar para prototipos rápidos

### ❌ Evitar:
- No usar para valores que cambiarán en runtime
- No usar para configuración que varía entre instancias
- No olvidar que sobrescribe valores del Inspector

---

## 🐛 Solución de Problemas

### "No se encontró la propiedad"
➡️ Verifica que el nombre de la propiedad esté escrito correctamente
➡️ Algunas propiedades son de solo lectura y no se pueden configurar

### "Error al configurar propiedad"
➡️ Verifica que el tipo del valor sea compatible
➡️ Usa el tipo correcto (int, float, bool, etc.)

### "Los valores no se aplican"
➡️ Asegúrate de que el componente exista antes de configurar
➡️ Usa `[AutoAssign(autoCreate: true)]` si necesitas crear el componente primero

---

## 🎉 ¡Ahora puedes configurar componentes automáticamente!

Combina `[AutoAssign]`, `[AutoSetup]` y `[ConfigureComponent]` para tener control total sobre la configuración automática de tus GameObjects.
