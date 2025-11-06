# 🎮 Unity AutoAssign - Guía de Uso Completa

Plugin mejorado para Unity que permite la auto-asignación de componentes y auto-configuración de GameObjects usando atributos.

## 📦 Características

### ✨ AutoAssign
- ✅ Asignación automática de componentes
- ✅ Búsqueda en hijos, padres o escena completa
- ✅ Creación automática de componentes faltantes
- ✅ Búsqueda por tags
- ✅ No sobrescribe valores asignados manualmente

### ⚙️ AutoSetup (NUEVO)
- ✅ Configuración automática de Tags
- ✅ Configuración automática de Layers
- ✅ Configuración de Sorting Layers y Order
- ✅ Renombrado automático de GameObjects
- ✅ Configuración de GameObject como estático
- ✅ Aplicación recursiva a hijos

---

## 🚀 Uso de [AutoAssign]

### Ejemplos Básicos

```csharp
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Auto-asigna Rigidbody2D del mismo GameObject
    [AutoAssign]
    [SerializeField] private Rigidbody2D rb;
    
    // Crea BoxCollider2D si no existe
    [AutoAssign(autoCreate: true)]
    [SerializeField] private BoxCollider2D boxCollider;
    
    // Busca Animator en los hijos
    [AutoAssign(searchInChildren: true)]
    [SerializeField] private Animator animator;
    
    // Busca Canvas en los padres
    [AutoAssign(searchInParent: true)]
    [SerializeField] private Canvas canvas;
    
    // Encuentra Camera en toda la escena
    [AutoAssign(searchInScene: true)]
    [SerializeField] private Camera mainCamera;
    
    // Encuentra GameObject con tag "Enemy"
    [AutoAssign(tag: "Enemy")]
    [SerializeField] private GameObject enemy;
}
```

### Parámetros de [AutoAssign]

| Parámetro | Tipo | Descripción |
|-----------|------|-------------|
| `searchInChildren` | `bool` | Busca el componente en los hijos del GameObject |
| `searchInParent` | `bool` | Busca el componente en los padres del GameObject |
| `searchInScene` | `bool` | Busca el componente en toda la escena |
| `autoCreate` | `bool` | Crea el componente si no lo encuentra |
| `tag` | `string` | Busca GameObject por tag en la escena |

---

## ⚙️ Uso de [AutoSetup]

### Ejemplos Básicos

```csharp
using UnityEngine;

// Configurar tag y layer
[AutoSetup(tag: "Player", layer: "Default")]
public class Player : MonoBehaviour
{
    // Tu código aquí
}

// Configurar sorting layer para sprites
[AutoSetup(sortingLayer: "Characters", sortingOrder: 10)]
public class CharacterSprite : MonoBehaviour
{
    // El SpriteRenderer se configurará automáticamente
}

// Renombrar GameObject automáticamente
[AutoSetup(gameObjectName: "MainPlayer")]
public class PlayerController : MonoBehaviour
{
    // El GameObject se renombrará a "MainPlayer"
}

// Hacer GameObject estático
[AutoSetup(isStatic: true)]
public class StaticProp : MonoBehaviour
{
    // El GameObject se marcará como estático
}

// Configuración completa
[AutoSetup(
    tag: "Player",
    layer: "Characters",
    sortingLayer: "Foreground",
    sortingOrder: 100,
    gameObjectName: "Hero",
    applyToChildren: true
)]
public class Hero : MonoBehaviour
{
    // Todas las configuraciones se aplican automáticamente
}
```

### Parámetros de [AutoSetup]

| Parámetro | Tipo | Descripción |
|-----------|------|-------------|
| `tag` | `string` | Tag a asignar al GameObject |
| `layer` | `string` | Layer a asignar (nombre del layer) |
| `gameObjectName` | `string` | Nombre a asignar al GameObject |
| `sortingLayer` | `string` | Sorting Layer para SpriteRenderer |
| `sortingOrder` | `int` | Order in Layer para SpriteRenderer |
| `isStatic` | `bool` | Marca el GameObject como estático |
| `applyToChildren` | `bool` | Aplica la configuración a todos los hijos |

---

## 💡 Ejemplos Completos

### Ejemplo 1: Player Completo con Todo

```csharp
using UnityEngine;

[AutoSetup(
    tag: "Player",
    layer: "Character",
    sortingLayer: "Player",
    sortingOrder: 10
)]
public class PlayerController : MonoBehaviour
{
    [Header("Componentes Auto-Asignados")]
    [AutoAssign]
    [SerializeField] private Rigidbody2D rb;
    
    [AutoAssign(autoCreate: true)]
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    
    [AutoAssign(searchInChildren: true)]
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [AutoAssign(searchInChildren: true)]
    [SerializeField] private Animator animator;

    [Header("Referencias de Escena")]
    [AutoAssign(searchInScene: true)]
    [SerializeField] private Camera mainCamera;
    
    [AutoAssign(tag: "GameManager")]
    [SerializeField] private GameObject gameManager;

    [Header("Configuración")]
    [SerializeField] private float speed = 5f;

    private void Update()
    {
        // Tu código de movimiento aquí
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2(horizontal, vertical);
        rb.linearVelocity = movement * speed;
    }
}
```

### Ejemplo 2: Enemy con Configuración de Layers

```csharp
using UnityEngine;

[AutoSetup(
    tag: "Enemy",
    layer: "Enemy",
    sortingLayer: "Characters",
    sortingOrder: 5
)]
public class EnemyController : MonoBehaviour
{
    [AutoAssign]
    [SerializeField] private Rigidbody2D rb;
    
    [AutoAssign(autoCreate: true)]
    [SerializeField] private BoxCollider2D boxCollider;
    
    [AutoAssign(tag: "Player")]
    [SerializeField] private GameObject player;
    
    [AutoAssign(searchInChildren: true)]
    [SerializeField] private SpriteRenderer sprite;

    private void Update()
    {
        // Perseguir al jugador
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.linearVelocity = direction * 3f;
        }
    }
}
```

### Ejemplo 3: UI con Búsqueda en Padres

```csharp
using UnityEngine;
using UnityEngine.UI;

[AutoSetup(layer: "UI")]
public class HealthBar : MonoBehaviour
{
    [AutoAssign(searchInParent: true)]
    [SerializeField] private Canvas canvas;
    
    [AutoAssign]
    [SerializeField] private Image fillImage;
    
    [AutoAssign]
    [SerializeField] private Text healthText;
    
    [AutoAssign(tag: "Player")]
    [SerializeField] private GameObject player;

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        float fillAmount = currentHealth / maxHealth;
        if (fillImage != null)
        {
            fillImage.fillAmount = fillAmount;
        }
        
        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
    }
}
```

### Ejemplo 4: Decoración Estática

```csharp
using UnityEngine;

[AutoSetup(
    layer: "Environment",
    isStatic: true,
    applyToChildren: true  // Todos los hijos también serán estáticos
)]
public class StaticDecoration : MonoBehaviour
{
    [AutoAssign(searchInChildren: true)]
    [SerializeField] private SpriteRenderer[] sprites;
    
    // Este GameObject y todos sus hijos serán estáticos
}
```

---

## 🛠️ Herramientas del Editor

### Menú Tools > Auto Assign

1. **Selected GameObjects** - Auto-asigna componentes en GameObjects seleccionados
2. **All Scene GameObjects** - Auto-asigna componentes en toda la escena

### Uso:
1. Selecciona uno o más GameObjects en la jerarquía
2. Ve a `Tools > Auto Assign > Selected GameObjects`
3. Los componentes se asignarán automáticamente

---

## ⚠️ Notas Importantes

### Tags y Layers
- Los tags deben existir en `Edit > Project Settings > Tags and Layers`
- Los layers deben estar creados en el proyecto
- Los sorting layers deben existir en `Edit > Project Settings > Tags and Layers`

### Orden de Ejecución
1. Primero se aplica `[AutoSetup]` (tags, layers, etc)
2. Después se aplica `[AutoAssign]` (componentes)

### Seguridad
- ❌ No sobrescribe valores asignados manualmente
- ✅ Solo asigna si el campo está null/vacío
- ✅ Muestra advertencias si faltan tags/layers

---

## 📋 Tips y Mejores Prácticas

### ✅ Hacer
- Usar `[AutoAssign]` para componentes que siempre estarán presentes
- Usar `autoCreate: true` para componentes opcionales que puedes añadir
- Usar `[AutoSetup]` para configuración inicial del GameObject
- Combinar ambos atributos para máxima automatización

### ❌ Evitar
- No usar en campos que cambiarán en runtime
- No usar para referencias que varían entre instancias
- No olvidar crear los tags/layers necesarios en el proyecto

---

## 🐛 Solución de Problemas

### "Tag 'X' no existe"
➡️ Añade el tag en `Edit > Project Settings > Tags and Layers`

### "Layer 'X' no existe"
➡️ Añade el layer en `Edit > Project Settings > Tags and Layers`

### "Los componentes no se asignan"
➡️ Verifica que el campo tenga `[SerializeField]` o sea público
➡️ Asegúrate de que el componente exista en el lugar correcto
➡️ Revisa la consola para ver mensajes de advertencia

### "AutoSetup no funciona"
➡️ Asegúrate de aplicar el atributo a la **clase**, no a campos
➡️ Verifica que los tags/layers existan en el proyecto

---

## 📝 Licencia

MIT License - Libre para usar en proyectos personales y comerciales

---

## 🎉 ¡Disfruta programando con menos código repetitivo!
