# UnityAutoAssign - Usage Guide

Version 2.0.0

## Table of Contents

1. [Introduction](#introduction)
2. [AutoAssign Attribute](#autoassign-attribute)
3. [AutoSetup Attribute](#autosetup-attribute)
4. [ConfigureComponent Attribute](#configurecomponent-attribute)
5. [Combining Attributes](#combining-attributes)
6. [Best Practices](#best-practices)
7. [Common Patterns](#common-patterns)
8. [Troubleshooting](#troubleshooting)
9. [Advanced Usage](#advanced-usage)

---

## Introduction

UnityAutoAssign is a powerful attribute-based system that automates three key aspects of Unity component management:

1. **Component Assignment** - Automatically find and assign component references
2. **GameObject Configuration** - Set tags, layers, sorting, and other GameObject properties
3. **Component Configuration** - Initialize component properties with specific values

All three systems work together seamlessly and execute automatically in the Unity Editor.

---

## AutoAssign Attribute

The `[AutoAssign]` attribute automatically assigns component references to serialized fields.

### Basic Usage

```csharp
using UnityEngine;

public class BasicExample : MonoBehaviour
{
    // Automatically finds Rigidbody2D on the same GameObject
    [AutoAssign]
    [SerializeField] private Rigidbody2D rb;
    
    // Automatically finds BoxCollider2D on the same GameObject
    [AutoAssign]
    [SerializeField] private BoxCollider2D collider;
}
```

### Auto-Create Components

Create components if they don't exist:

```csharp
public class AutoCreateExample : MonoBehaviour
{
    // Creates AudioSource if missing
    [AutoAssign(autoCreate: true)]
    [SerializeField] private AudioSource audioSource;
    
    // Creates CircleCollider2D if missing
    [AutoAssign(autoCreate: true)]
    [SerializeField] private CircleCollider2D collider;
}
```

### Search in Hierarchy

Search for components in different parts of the GameObject hierarchy:

```csharp
public class HierarchySearch : MonoBehaviour
{
    // Searches in child GameObjects
    [AutoAssign(searchInChildren: true)]
    [SerializeField] private Animator childAnimator;
    
    // Searches in parent GameObjects
    [AutoAssign(searchInParent: true)]
    [SerializeField] private Canvas parentCanvas;
    
    // Searches entire scene
    [AutoAssign(searchInScene: true)]
    [SerializeField] private Camera mainCamera;
}
```

### Find by Tag

Find GameObjects by tag:

```csharp
public class TagSearch : MonoBehaviour
{
    // Finds GameObject with "Player" tag
    [AutoAssign(tag: "Player")]
    [SerializeField] private GameObject player;
    
    // Finds GameObject with "GameController" tag
    [AutoAssign(tag: "GameController")]
    [SerializeField] private GameObject gameController;
}
```

### Multiple Search Options

Combine search options:

```csharp
public class CombinedSearch : MonoBehaviour
{
    // First tries children, then creates if not found
    [AutoAssign(searchInChildren: true, autoCreate: true)]
    [SerializeField] private BoxCollider2D collider;
}
```

### Parameters Reference

| Parameter | Type | Description |
|-----------|------|-------------|
| `autoCreate` | bool | Creates the component if it doesn't exist |
| `searchInChildren` | bool | Searches in child GameObjects |
| `searchInParent` | bool | Searches in parent GameObjects |
| `searchInScene` | bool | Searches in the entire scene |
| `tag` | string | Finds GameObject by tag |

---

## AutoSetup Attribute

The `[AutoSetup]` attribute configures GameObject properties. It's applied to the class itself.

### Basic Configuration

```csharp
using UnityEngine;

// Sets tag and layer
[AutoSetup(Tag = "Enemy", Layer = "Enemies")]
public class Enemy : MonoBehaviour
{
    // Enemy logic...
}
```

### GameObject Naming

Rename GameObjects automatically:

```csharp
// Renames GameObject to "MainPlayer"
[AutoSetup(GameObjectName = "MainPlayer")]
public class Player : MonoBehaviour
{
    // Player logic...
}
```

### 2D Sprite Configuration

Configure sorting for 2D sprites:

```csharp
// Sets sorting layer and order for 2D rendering
[AutoSetup(SortingLayer = "Characters", SortingOrder = 10)]
public class Character2D : MonoBehaviour
{
    [AutoAssign]
    [SerializeField] private SpriteRenderer sprite;
}
```

### Static GameObjects

Mark GameObjects as static:

```csharp
// Marks GameObject as static for performance optimization
[AutoSetup(IsStatic = true)]
public class StaticProp : MonoBehaviour
{
    // Static prop logic...
}
```

### Apply to Children

Apply settings to all child GameObjects:

```csharp
// Applies layer to this GameObject and all children
[AutoSetup(Layer = "UI", ApplyToChildren = true)]
public class UIPanel : MonoBehaviour
{
    // UI logic...
}
```

### Complete Configuration

Combine all options:

```csharp
[AutoSetup(
    Tag = "Player",
    Layer = "Characters",
    GameObjectName = "MainPlayer",
    SortingLayer = "Foreground",
    SortingOrder = 100,
    IsStatic = false,
    ApplyToChildren = false
)]
public class CompleteSetup : MonoBehaviour
{
    // Logic...
}
```

### Properties Reference

| Property | Type | Description |
|----------|------|-------------|
| `Tag` | string | Sets the GameObject tag |
| `Layer` | string | Sets the GameObject layer |
| `GameObjectName` | string | Renames the GameObject |
| `SortingLayer` | string | Sets sorting layer (for SpriteRenderer) |
| `SortingOrder` | int | Sets sorting order |
| `IsStatic` | bool | Marks GameObject as static |
| `ApplyToChildren` | bool | Applies settings to all children |

---

## ConfigureComponent Attribute

The `[ConfigureComponent]` attribute sets component property values. It can be used multiple times on the same field.

### Basic Configuration

```csharp
using UnityEngine;

public class BasicConfig : MonoBehaviour
{
    // Creates Rigidbody2D with gravity disabled
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("gravityScale", 0f)]
    [SerializeField] private Rigidbody2D rb;
}
```

### Multiple Properties

Configure multiple properties on one component:

```csharp
public class MultipleProps : MonoBehaviour
{
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("gravityScale", 0f)]
    [ConfigureComponent("drag", 0f)]
    [ConfigureComponent("angularDrag", 0.05f)]
    [ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)]
    [SerializeField] private Rigidbody2D rb;
}
```

### Collider Configuration

```csharp
public class ColliderConfig : MonoBehaviour
{
    // Box collider as trigger with custom size
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("isTrigger", true)]
    [ConfigureComponent("size", new Vector2(2f, 2f))]
    [SerializeField] private BoxCollider2D triggerZone;
    
    // Circle collider with custom radius
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("isTrigger", false)]
    [ConfigureComponent("radius", 0.5f)]
    [SerializeField] private CircleCollider2D solidCollider;
}
```

### AudioSource Configuration

```csharp
public class AudioConfig : MonoBehaviour
{
    // Background music configuration
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("playOnAwake", false)]
    [ConfigureComponent("loop", true)]
    [ConfigureComponent("volume", 0.5f)]
    [ConfigureComponent("spatialBlend", 0f)]
    [SerializeField] private AudioSource bgMusic;
    
    // 3D sound effect configuration
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("playOnAwake", false)]
    [ConfigureComponent("loop", false)]
    [ConfigureComponent("spatialBlend", 1f)]
    [ConfigureComponent("minDistance", 1f)]
    [ConfigureComponent("maxDistance", 10f)]
    [SerializeField] private AudioSource sfx3D;
}
```

### SpriteRenderer Configuration

```csharp
public class SpriteConfig : MonoBehaviour
{
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("color", Color.red)]
    [ConfigureComponent("flipX", false)]
    [ConfigureComponent("flipY", false)]
    [ConfigureComponent("sortingOrder", 5)]
    [SerializeField] private SpriteRenderer sprite;
}
```

### OnlyOnCreate Option

Only apply configuration when the component is first created:

```csharp
public class OnCreateOnly : MonoBehaviour
{
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("gravityScale", 0f, OnlyOnCreate = true)]
    [SerializeField] private Rigidbody2D rb;
    // This will only set gravityScale to 0 when the component is created
    // If the component already exists, it won't be modified
}
```

### Supported Value Types

ConfigureComponent supports:
- **Primitives**: int, float, double, bool, string
- **Unity Types**: Vector2, Vector3, Vector4, Color, Quaternion
- **Enums**: Any enum type (e.g., RigidbodyConstraints2D)
- **Arrays**: Any array type
- **Complex Types**: Any serializable Unity type

### Parameters Reference

| Parameter | Type | Description |
|-----------|------|-------------|
| `propertyName` | string | Name of the property to configure |
| `value` | object | Value to assign to the property |

| Property | Type | Description |
|----------|------|-------------|
| `OnlyOnCreate` | bool | Only applies when component is created (default: false) |

---

## Combining Attributes

The real power of UnityAutoAssign comes from combining all three attributes.

### Top-Down 2D Character

```csharp
using UnityEngine;

[AutoSetup(Tag = "Player", Layer = "Characters")]
public class TopDownCharacter : MonoBehaviour
{
    [Header("Physics")]
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("gravityScale", 0f)]
    [ConfigureComponent("drag", 0f)]
    [ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)]
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Colliders")]
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("isTrigger", false)]
    [SerializeField] private BoxCollider2D bodyCollider;
    
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("isTrigger", true)]
    [ConfigureComponent("radius", 1.5f)]
    [SerializeField] private CircleCollider2D interactionZone;
    
    [Header("Visual")]
    [AutoAssign(searchInChildren: true)]
    [SerializeField] private SpriteRenderer sprite;
    
    [Header("References")]
    [AutoAssign(searchInScene: true)]
    [SerializeField] private Camera mainCamera;
    
    [Header("Settings")]
    [SerializeField] private float speed = 5f;
    
    void Update()
    {
        Vector2 input = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
        
        if (rb != null)
        {
            rb.velocity = input.normalized * speed;
        }
    }
}
```

### Platformer Character

```csharp
using UnityEngine;

[AutoSetup(Tag = "Player", Layer = "Player", SortingLayer = "Characters")]
public class PlatformerCharacter : MonoBehaviour
{
    [Header("Physics")]
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("gravityScale", 3f)]
    [ConfigureComponent("drag", 0f)]
    [ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)]
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Colliders")]
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("isTrigger", false)]
    [SerializeField] private BoxCollider2D bodyCollider;
    
    [Header("Visual")]
    [AutoAssign(searchInChildren: true)]
    [SerializeField] private Animator animator;
    
    [AutoAssign(searchInChildren: true)]
    [SerializeField] private SpriteRenderer sprite;
    
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        
        if (rb != null)
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }
}
```

### Audio Manager

```csharp
using UnityEngine;

[AutoSetup(GameObjectName = "AudioManager")]
public class AudioManager : MonoBehaviour
{
    [Header("Music")]
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("playOnAwake", true)]
    [ConfigureComponent("loop", true)]
    [ConfigureComponent("volume", 0.3f)]
    [ConfigureComponent("spatialBlend", 0f)]
    [SerializeField] private AudioSource musicSource;
    
    [Header("SFX")]
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("playOnAwake", false)]
    [ConfigureComponent("loop", false)]
    [ConfigureComponent("volume", 0.7f)]
    [ConfigureComponent("spatialBlend", 0f)]
    [SerializeField] private AudioSource sfxSource;
    
    public void PlaySound(AudioClip clip)
    {
        if (sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
```

### UI Panel

```csharp
using UnityEngine;
using UnityEngine.UI;

[AutoSetup(Layer = "UI", ApplyToChildren = true)]
public class UIPanel : MonoBehaviour
{
    [Header("Panel Components")]
    [AutoAssign]
    [SerializeField] private Image background;
    
    [AutoAssign(searchInChildren: true)]
    [SerializeField] private Button closeButton;
    
    [Header("Canvas")]
    [AutoAssign(searchInParent: true)]
    [SerializeField] private Canvas canvas;
    
    void Start()
    {
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ClosePanel);
        }
    }
    
    void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
```

---

## Best Practices

### 1. Use Appropriate Search Scope

Choose the smallest search scope needed:

```csharp
// Good: Searches only on same GameObject
[AutoAssign]
[SerializeField] private Rigidbody2D rb;

// Less efficient: Searches entire scene when not needed
[AutoAssign(searchInScene: true)]
[SerializeField] private Rigidbody2D rb;
```

### 2. Combine AutoAssign with ConfigureComponent

Always use `autoCreate: true` when using ConfigureComponent:

```csharp
// Good: Creates component then configures it
[AutoAssign(autoCreate: true)]
[ConfigureComponent("gravityScale", 0f)]
[SerializeField] private Rigidbody2D rb;

// Bad: ConfigureComponent has nothing to configure if component doesn't exist
[AutoAssign]
[ConfigureComponent("gravityScale", 0f)]
[SerializeField] private Rigidbody2D rb;
```

### 3. Use Headers for Organization

Group related fields with headers:

```csharp
[Header("Physics")]
[AutoAssign(autoCreate: true)]
[SerializeField] private Rigidbody2D rb;

[Header("Colliders")]
[AutoAssign(autoCreate: true)]
[SerializeField] private BoxCollider2D collider;

[Header("Settings")]
[SerializeField] private float speed = 5f;
```

### 4. Apply AutoSetup at Class Level

Always apply AutoSetup to the class, not fields:

```csharp
// Good
[AutoSetup(Tag = "Player")]
public class Player : MonoBehaviour { }

// Wrong: AutoSetup doesn't work on fields
public class Player : MonoBehaviour
{
    [AutoSetup(Tag = "Player")] // This won't work!
    [SerializeField] private int health;
}
```

### 5. Use OnlyOnCreate for Optional Configuration

Use `OnlyOnCreate = true` when you want to set default values but allow manual override:

```csharp
[AutoAssign(autoCreate: true)]
[ConfigureComponent("volume", 0.5f, OnlyOnCreate = true)]
[SerializeField] private AudioSource audio;
// Volume will be 0.5f when created, but you can change it later manually
```

### 6. Keep Property Names Accurate

Use exact property names as they appear in Unity's API:

```csharp
// Correct
[ConfigureComponent("gravityScale", 0f)] // Property is called gravityScale

// Wrong
[ConfigureComponent("gravity", 0f)] // This property doesn't exist
```

### 7. Use Null Checks

Always check for null before using components:

```csharp
[AutoAssign]
[SerializeField] private Rigidbody2D rb;

void Update()
{
    if (rb != null) // Always check!
    {
        rb.velocity = Vector2.zero;
    }
}
```

---

## Common Patterns

### Pattern 1: Physics Object Template

```csharp
using UnityEngine;

[AutoSetup(Layer = "PhysicsObjects")]
public class PhysicsObject : MonoBehaviour
{
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("drag", 0.5f)]
    [ConfigureComponent("angularDrag", 0.5f)]
    [SerializeField] private Rigidbody2D rb;
    
    [AutoAssign(autoCreate: true)]
    [SerializeField] private BoxCollider2D collider;
}
```

### Pattern 2: Trigger Zone

```csharp
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("isTrigger", true)]
    [SerializeField] private BoxCollider2D triggerCollider;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Entered: {other.name}");
    }
}
```

### Pattern 3: Singleton Manager

```csharp
using UnityEngine;

[AutoSetup(GameObjectName = "GameManager")]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [AutoAssign(searchInScene: true)]
    [SerializeField] private Camera mainCamera;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
```

### Pattern 4: Particle System Controller

```csharp
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("playOnAwake", false)]
    [ConfigureComponent("loop", false)]
    [SerializeField] private ParticleSystem particles;
    
    public void Play()
    {
        if (particles != null)
        {
            particles.Play();
        }
    }
}
```

### Pattern 5: Health Bar UI

```csharp
using UnityEngine;
using UnityEngine.UI;

[AutoSetup(Layer = "UI")]
public class HealthBar : MonoBehaviour
{
    [AutoAssign(searchInChildren: true)]
    [SerializeField] private Slider healthSlider;
    
    [AutoAssign(searchInChildren: true)]
    [SerializeField] private Image fillImage;
    
    public void SetHealth(float health, float maxHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.value = health / maxHealth;
        }
    }
}
```

---

## Troubleshooting

### Component Not Assigned

**Problem**: Component field remains null after AutoAssign.

**Solutions**:
1. Check the component exists on the GameObject or in the search scope
2. Verify the search parameters (`searchInChildren`, `searchInParent`, etc.)
3. Try using `autoCreate: true` to create the component
4. Manually trigger assignment by resetting the component in the Inspector

```csharp
// If this doesn't work...
[AutoAssign]
[SerializeField] private Animator animator;

// Try one of these:
[AutoAssign(searchInChildren: true)]
[SerializeField] private Animator animator;

// Or:
[AutoAssign(autoCreate: true)]
[SerializeField] private Animator animator;
```

### ConfigureComponent Not Working

**Problem**: Component properties aren't being set.

**Solutions**:
1. Verify the property name is spelled correctly and matches Unity's API
2. Check the value type matches the property type
3. Ensure AutoAssign with `autoCreate: true` is present
4. Make sure the component exists before configuration

```csharp
// Wrong: Property name typo
[ConfigureComponent("gravitysize", 0f)] // Wrong!

// Correct:
[ConfigureComponent("gravityScale", 0f)] // Correct!
```

### AutoSetup Not Applied

**Problem**: GameObject properties (tag, layer) aren't being set.

**Solutions**:
1. Verify AutoSetup is applied to the class, not a field
2. Check that tag and layer names exist in Project Settings
3. Ensure the GameObject isn't marked as prefab-only

```csharp
// Wrong:
public class Player : MonoBehaviour
{
    [AutoSetup(Tag = "Player")] // Won't work on fields!
    private int health;
}

// Correct:
[AutoSetup(Tag = "Player")] // Apply to class!
public class Player : MonoBehaviour
{
    private int health;
}
```

### Tag or Layer Doesn't Exist

**Problem**: "Tag 'X' is not defined" or "Layer 'X' doesn't exist" error.

**Solution**: Create the tag or layer in Unity:
1. Go to **Edit > Project Settings > Tags and Layers**
2. Add your tag or layer
3. Reset the component to re-apply AutoSetup

### Properties Not Configuring on Existing Components

**Problem**: ConfigureComponent doesn't affect existing components.

**Solution**: By default, ConfigureComponent applies to all components. If you want it only on creation:

```csharp
// Always applies (default)
[ConfigureComponent("gravityScale", 0f)]

// Only applies when component is created
[ConfigureComponent("gravityScale", 0f, OnlyOnCreate = true)]
```

### Multiple Components of Same Type

**Problem**: AutoAssign finds the wrong component when multiple exist.

**Solution**: Be more specific with search scope or use manual assignment:

```csharp
// If there are multiple colliders, be specific:
[AutoAssign] // Finds first on same GameObject
[SerializeField] private BoxCollider2D mainCollider;

[AutoAssign(searchInChildren: true)] // Finds first in children
[SerializeField] private BoxCollider2D childCollider;
```

---

## Advanced Usage

### Custom Component Classes

AutoAssign works with custom components:

```csharp
// Custom component
public class CustomHealth : MonoBehaviour
{
    public int maxHealth = 100;
}

// Using AutoAssign with custom component
public class Character : MonoBehaviour
{
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("maxHealth", 200)]
    [SerializeField] private CustomHealth health;
}
```

### Complex Value Types

ConfigureComponent supports complex types:

```csharp
public class ComplexConfig : MonoBehaviour
{
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("color", new Color(1f, 0.5f, 0f, 1f))]
    [ConfigureComponent("size", new Vector2(2f, 3f))]
    [SerializeField] private SpriteRenderer sprite;
}
```

### Enum Configurations

Use enums for configuration:

```csharp
public class EnumConfig : MonoBehaviour
{
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)]
    [ConfigureComponent("collisionDetectionMode", CollisionDetectionMode2D.Continuous)]
    [SerializeField] private Rigidbody2D rb;
}
```

### Conditional Configuration

Use OnlyOnCreate for conditional setup:

```csharp
public class ConditionalSetup : MonoBehaviour
{
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("volume", 0.5f, OnlyOnCreate = true)]
    [SerializeField] private AudioSource audio;
    // Volume is 0.5f on creation, but can be changed manually later
}
```

### Prefab Workflows

AutoAssign works great with prefabs:

1. Create a prefab with a script using AutoAssign
2. Instantiate the prefab at runtime
3. AutoAssign will automatically run when the component is added

```csharp
public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    
    void SpawnEnemy()
    {
        // AutoAssign will run on the instantiated prefab
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
```

### Runtime Assignment

AutoAssign primarily works in the Editor, but you can trigger it manually at runtime:

```csharp
// This is handled automatically by the editor scripts
// For runtime scenarios, consider manual assignment or dependency injection
```

---

## Summary

UnityAutoAssign provides three powerful attributes:

1. **[AutoAssign]** - Finds and assigns component references
2. **[AutoSetup]** - Configures GameObject properties (tags, layers, etc.)
3. **[ConfigureComponent]** - Sets component property values

Use them together to eliminate boilerplate code and ensure consistent component configuration across your Unity project.

For more information, see:
- [README.md](./README.md) - Quick start and overview
- [CHANGELOG.md](./CHANGELOG.md) - Version history

---

**Questions or Issues?**  
Visit the [GitHub repository](https://github.com/karateka2610/UnityAutoAssign) to report issues or contribute.
