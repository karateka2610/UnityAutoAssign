# Unity Auto Assign

![Unity Version](https://img.shields.io/badge/Unity-2021.3%2B-blue)
![License](https://img.shields.io/badge/license-MIT-green)
![Version](https://img.shields.io/badge/version-2.0.0-orange)

A powerful Unity plugin that automates component assignment, GameObject configuration, and property setup using custom attributes and reflection. Eliminates tedious manual configuration in the Inspector.

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Quick Start](#quick-start)
- [Core Attributes](#core-attributes)
- [Usage Examples](#usage-examples)
- [Editor Tools](#editor-tools)
- [Requirements](#requirements)
- [Documentation](#documentation)
- [License](#license)

## Features

### AutoAssign - Component Assignment
- Automatic component assignment when adding scripts
- Search in GameObject, children, parents, or entire scene
- Auto-create missing components
- Find GameObjects by tag
- Safe: never overwrites manually assigned values

### AutoSetup - GameObject Configuration
- Set tags and layers automatically
- Configure sorting layers and order for 2D sprites
- Rename GameObjects programmatically
- Mark objects as static
- Apply settings recursively to children

### ConfigureComponent - Property Configuration
- Configure component properties directly from code
- Works with all Unity components
- Type-safe with automatic conversion
- Multiple configurations per component
- Supports primitives, enums, and Unity types

## Compatibility

- Unity 2021.3 or newer
- .NET Standard 2.1

## Installation

### Option 1: Unity Package Manager (Recommended)

1. Open Unity Package Manager: `Window > Package Manager`
2. Click the `+` button in the top-left corner
3. Select `Add package from git URL`
4. Enter: `https://github.com/karateka2610/UnityAutoAssign.git`
5. Click `Add`

### Option 2: Manual Installation

1. Download the latest release from [GitHub Releases](https://github.com/karateka2610/UnityAutoAssign/releases)
2. Extract the contents to your project's `Assets` folder
3. Unity will automatically import and compile the plugin

## Quick Start

```csharp
using UnityEngine;

// Configure GameObject: tag and layer
[AutoSetup(Tag = "Player", Layer = "Character")]
public class PlayerController : MonoBehaviour
{
    // Auto-assign and configure Rigidbody2D
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("gravityScale", 0f)]
    [ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)]
    [SerializeField] private Rigidbody2D rb;
    
    // Create BoxCollider2D and set as trigger
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("isTrigger", true)]
    [SerializeField] private BoxCollider2D triggerZone;
    
    // Find main camera in scene
    [AutoAssign(searchInScene: true)]
    [SerializeField] private Camera mainCamera;
    
    void Start()
    {
        // Everything is ready to use!
        rb.AddForce(Vector2.up * 10f);
    }
}
```

That's it! Add this script to a GameObject and all components will be created, assigned, and configured automatically.

## Core Attributes

### [AutoAssign]
Automatically assigns component references.

**Parameters:**
- `autoCreate` - Creates the component if it doesn't exist
- `searchInChildren` - Searches in child GameObjects
- `searchInParent` - Searches in parent GameObjects
- `searchInScene` - Searches in the entire scene
- `tag` - Finds GameObject by tag

### [AutoSetup]
Configures GameObject properties. Applied to the class.

**Properties:**
- `Tag` - Sets the GameObject tag
- `Layer` - Sets the GameObject layer
- `GameObjectName` - Renames the GameObject
- `SortingLayer` - Sets sorting layer (for SpriteRenderer)
- `SortingOrder` - Sets sorting order
- `IsStatic` - Marks GameObject as static
- `ApplyToChildren` - Applies settings to all children

### [ConfigureComponent]
Configures component properties. Can be used multiple times on the same field.

**Parameters:**
- `propertyName` - Name of the property to configure
- `value` - Value to assign

**Property:**
- `OnlyOnCreate` - Only applies when component is created (default: false)

## Usage Examples

### Basic Component Assignment

```csharp
public class Character : MonoBehaviour
{
    // Assigns from same GameObject
    [AutoAssign]
    [SerializeField] private Rigidbody2D rb;
    
    // Creates if missing
    [AutoAssign(autoCreate: true)]
    [SerializeField] private BoxCollider2D collider;
    
    // Searches in children
    [AutoAssign(searchInChildren: true)]
    [SerializeField] private Animator animator;
    
    // Searches in parents
    [AutoAssign(searchInParent: true)]
    [SerializeField] private Canvas canvas;
    
    // Finds in scene
    [AutoAssign(searchInScene: true)]
    [SerializeField] private Camera mainCamera;
    
    // Finds by tag
    [AutoAssign(tag: "GameController")]
    [SerializeField] private GameObject controller;
}
```

### GameObject Configuration

```csharp
// Simple tag and layer setup
[AutoSetup(Tag = "Enemy", Layer = "Enemies")]
public class Enemy : MonoBehaviour { }

// 2D sprite configuration
[AutoSetup(SortingLayer = "Characters", SortingOrder = 10)]
public class SpriteCharacter : MonoBehaviour { }

// Complete configuration
[AutoSetup(
    Tag = "Player",
    Layer = "Characters",
    GameObjectName = "MainPlayer",
    IsStatic = false
)]
public class Player : MonoBehaviour { }
```

### Component Property Configuration

```csharp
public class TopDownPlayer : MonoBehaviour
{
    // Rigidbody2D: no gravity, no rotation
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("gravityScale", 0f)]
    [ConfigureComponent("drag", 0f)]
    [ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)]
    [SerializeField] private Rigidbody2D rb;
    
    // Trigger collider with custom size
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("isTrigger", true)]
    [ConfigureComponent("radius", 1.5f)]
    [SerializeField] private CircleCollider2D detectionZone;
    
    // AudioSource: don't play on awake, loop, 50% volume
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("playOnAwake", false)]
    [ConfigureComponent("loop", true)]
    [ConfigureComponent("volume", 0.5f)]
    [SerializeField] private AudioSource bgMusic;
}
```

### Complete Example

```csharp
using UnityEngine;

[AutoSetup(Tag = "Player", Layer = "Player")]
public class CompletePlayer : MonoBehaviour
{
    [Header("Physics")]
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("gravityScale", 0f)]
    [ConfigureComponent("drag", 0f)]
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Colliders")]
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("isTrigger", false)]
    [SerializeField] private BoxCollider2D solidCollider;
    
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("isTrigger", true)]
    [ConfigureComponent("radius", 2f)]
    [SerializeField] private CircleCollider2D interactionZone;
    
    [Header("References")]
    [AutoAssign(searchInScene: true)]
    [SerializeField] private Camera mainCamera;
    
    [SerializeField] private float speed = 5f;
    
    void Update()
    {
        Vector2 input = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
        rb.velocity = input.normalized * speed;
    }
}

## Editor Integration

### Automatic Execution

The AutoAssigner system runs automatically when:
- A script is added to a GameObject
- A script is modified or recompiled
- A script component is reset
- The Unity Editor reloads

### Execution Order

1. **AutoSetup**: Configures GameObject (tag, layer, sorting, etc.)
2. **AutoAssign**: Assigns component references
3. **ConfigureComponent**: Sets component properties

### Manual Trigger

To manually re-apply all attributes:
1. Select the GameObject with the script
2. Right-click the script component in the Inspector
3. Select **Reset** from the context menu

### Menu Tools

- **Tools > Auto Assign > Selected GameObjects**: Re-applies all attributes to selected objects
- **Tools > Auto Assign > All Scene GameObjects**: Re-applies all attributes to all objects in the current scene

## Requirements

- Unity 2021.3 or newer
- .NET Standard 2.1

## Documentation

- **[USAGE_GUIDE.md](./USAGE_GUIDE.md)** - Comprehensive usage guide with detailed examples and best practices
- **[CHANGELOG.md](./CHANGELOG.md)** - Version history and changes

## License

MIT License - See [LICENSE.md](./LICENSE.md)

## Support

- Report bugs via [GitHub Issues](https://github.com/karateka2610/UnityAutoAssign/issues)
- Feature requests welcome
- Pull requests accepted

## Changelog

See [CHANGELOG.md](./CHANGELOG.md) for version history
