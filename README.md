# UnityAutoAssign# Unity Auto Assign



A powerful Unity plugin that automates component assignment, GameObject configuration, and asset loading using custom attributes and reflection. Eliminates tedious manual configuration.![Unity Version](https://img.shields.io/badge/Unity-2021.3%2B-blue)

![License](https://img.shields.io/badge/license-MIT-green)

## Table of Contents![Version](https://img.shields.io/badge/version-2.0.0-orange)



- [Features](#features)A powerful Unity plugin that automates component assignment, GameObject configuration, and property setup using custom attributes and reflection. Eliminates tedious manual configuration in the Inspector.

- [What's New in v3.0](#whats-new-in-v30)

- [Installation](#installation)## Table of Contents

- [Quick Start](#quick-start)

- [Core Attributes](#core-attributes)- [Features](#features)

- [New Attributes in v3.0](#new-attributes-in-v30)- [Installation](#installation)

- [Usage Examples](#usage-examples)- [Quick Start](#quick-start)

- [Editor Tools](#editor-tools)- [Core Attributes](#core-attributes)

- [Requirements](#requirements)- [Usage Examples](#usage-examples)

- [Documentation](#documentation)- [Editor Tools](#editor-tools)

- [Requirements](#requirements)

## Features- [Documentation](#documentation)

- [License](#license)

### AutoAssign - Component Assignment

- Automatic component assignment## Features

- Search in GameObject, children, parents, or scene

- Auto-create missing components### AutoAssign - Component Assignment

- Find GameObjects by tag- Automatic component assignment when adding scripts

- Never overwrites manually assigned values- Search in GameObject, children, parents, or entire scene

- Auto-create missing components

### AutoSetup - GameObject Configuration- Find GameObjects by tag

- Set tags and layers automatically- Safe: never overwrites manually assigned values

- Configure sorting layers and order for 2D sprites

- Rename GameObjects programmatically### AutoSetup - GameObject Configuration

- Mark objects as static- Set tags and layers automatically

- Apply settings recursively to children- Configure sorting layers and order for 2D sprites

- Rename GameObjects programmatically

### ConfigureComponent - Property Configuration- Mark objects as static

- Configure component properties from code- Apply settings recursively to children

- Works with all Unity components

- Type-safe with automatic conversion### ConfigureComponent - Property Configuration

- Multiple configurations per component- Configure component properties directly from code

- Supports primitives, enums, and Unity types- Works with all Unity components

- Type-safe with automatic conversion

### NEW in v3.0 - ValidateAssignment- Multiple configurations per component

- Validate that assignments completed successfully- Supports primitives, enums, and Unity types

- Multiple validation levels: Error, Warning, Info

- Optional delayed validation## Compatibility

- Console logging with detailed messages

- Unity 2021.3 or newer

### NEW in v3.0 - AutoAssignData- .NET Standard 2.1

- Load ScriptableObjects from Resources

- Load AudioClips and other assets## Installation

- Automatic asset discovery

- Path-based loading### Option 1: Unity Package Manager (Recommended)



### NEW in v3.0 - AutoAssignUI1. Open Unity Package Manager: `Window > Package Manager`

- Type-safe UI component discovery2. Click the `+` button in the top-left corner

- Support for Button, Image, Text, Slider, and more3. Select `Add package from git URL`

- Simplified UI hierarchy searching4. Enter: `https://github.com/karateka2610/UnityAutoAssign.git`

- Dedicated UIComponentType enum5. Click `Add`



### NEW in v3.0 - AutoAssignCallback### Option 2: Manual Installation

- Execute methods on assignment success

- Execute methods on assignment failure1. Download the latest release from [GitHub Releases](https://github.com/karateka2610/UnityAutoAssign/releases)

- Lifecycle-based initialization2. Extract the contents to your project's `Assets` folder

- Multiple callbacks per component3. Unity will automatically import and compile the plugin



## What's New in v3.0## Quick Start



Version 3.0 introduces four powerful new attributes:```csharp

using UnityEngine;

1. **ValidateAssignment** - Runtime validation of assignments with multiple severity levels

2. **AutoAssignData** - Load assets from Resources folder directly// Configure GameObject: tag and layer

3. **AutoAssignUI** - Type-safe UI component discovery with dedicated attribute[AutoSetup(Tag = "Player", Layer = "Character")]

4. **AutoAssignCallback** - Execute methods during assignment lifecyclepublic class PlayerController : MonoBehaviour

{

Plus:    // Auto-assign and configure Rigidbody2D

- AutoAssignCache system for performance optimization    [AutoAssign(autoCreate: true)]

- Enhanced error messages and debugging    [ConfigureComponent("gravityScale", 0f)]

- Full namespace organization (UnityAutoAssign)    [ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)]

    [SerializeField] private Rigidbody2D rb;

## Compatibility    

    // Create BoxCollider2D and set as trigger

- Unity 2023+ (uses modern APIs: FindFirstObjectByType, linearVelocity)    [AutoAssign(autoCreate: true)]

- .NET Standard 2.1    [ConfigureComponent("isTrigger", true)]

    [SerializeField] private BoxCollider2D triggerZone;

## Installation    

    // Find main camera in scene

### Option 1: Copy to Project    [AutoAssign(searchInScene: true)]

    [SerializeField] private Camera mainCamera;

1. Download the latest release    

2. Extract to your project's `Assets` folder    void Start()

3. Unity will automatically compile the plugin    {

        // Everything is ready to use!

### Option 2: Git Integration        rb.AddForce(Vector2.up * 10f);

    }

1. Open Unity Package Manager: `Window > Package Manager`}

2. Click `+` button, select `Add package from git URL````

3. Enter: `https://github.com/karateka2610/UnityAutoAssign.git`

That's it! Add this script to a GameObject and all components will be created, assigned, and configured automatically.

## Quick Start

## Core Attributes

```csharp

using UnityEngine;### [AutoAssign]

using UnityAutoAssign;Automatically assigns component references.



[AutoSetup(Tag = "Player", Layer = "Character")]**Parameters:**

public class PlayerController : MonoBehaviour- `autoCreate` - Creates the component if it doesn't exist

{- `searchInChildren` - Searches in child GameObjects

    [AutoAssign(autoCreate: true)]- `searchInParent` - Searches in parent GameObjects

    [ConfigureComponent("gravityScale", 0f)]- `searchInScene` - Searches in the entire scene

    [ValidateAssignment(level: ValidationLevel.Error)]- `tag` - Finds GameObject by tag

    [SerializeField] private Rigidbody2D rb;

    ### [AutoSetup]

    [AutoAssign(autoCreate: true)]Configures GameObject properties. Applied to the class.

    [ConfigureComponent("isTrigger", true)]

    [ValidateAssignment(level: ValidationLevel.Error)]**Properties:**

    [SerializeField] private BoxCollider2D triggerZone;- `Tag` - Sets the GameObject tag

    - `Layer` - Sets the GameObject layer

    [AutoAssign(searchInScene: true)]- `GameObjectName` - Renames the GameObject

    [ValidateAssignment(level: ValidationLevel.Error)]- `SortingLayer` - Sets sorting layer (for SpriteRenderer)

    [SerializeField] private Camera mainCamera;- `SortingOrder` - Sets sorting order

    - `IsStatic` - Marks GameObject as static

    [AutoAssignData("Player/PlayerStats")]- `ApplyToChildren` - Applies settings to all children

    [ValidateAssignment(level: ValidationLevel.Warning)]

    [SerializeField] private PlayerStats playerStats;### [ConfigureComponent]

    Configures component properties. Can be used multiple times on the same field.

    void Start()

    {**Parameters:**

        rb.AddForce(Vector2.up * 10f);- `propertyName` - Name of the property to configure

    }- `value` - Value to assign

}

```**Property:**

- `OnlyOnCreate` - Only applies when component is created (default: false)

All components are created, assigned, configured, validated, and data loaded automatically.

## Usage Examples

## Core Attributes

### Basic Component Assignment

### [AutoAssign]

```csharp

Automatically assigns component references.public class Character : MonoBehaviour

{

Parameters:    // Assigns from same GameObject

- autoCreate (bool) - Creates component if missing    [AutoAssign]

- searchInChildren (bool) - Searches in child GameObjects    [SerializeField] private Rigidbody2D rb;

- searchInParent (bool) - Searches in parent GameObjects    

- searchInScene (bool) - Searches entire scene    // Creates if missing

- tag (string) - Finds GameObject by tag    [AutoAssign(autoCreate: true)]

    [SerializeField] private BoxCollider2D collider;

Example:    

```csharp    // Searches in children

[AutoAssign]    [AutoAssign(searchInChildren: true)]

[SerializeField] private Rigidbody2D rb;    [SerializeField] private Animator animator;

    

[AutoAssign(autoCreate: true)]    // Searches in parents

[SerializeField] private BoxCollider2D collider;    [AutoAssign(searchInParent: true)]

    [SerializeField] private Canvas canvas;

[AutoAssign(searchInScene: true)]    

[SerializeField] private Camera mainCamera;    // Finds in scene

```    [AutoAssign(searchInScene: true)]

    [SerializeField] private Camera mainCamera;

### [AutoSetup]    

    // Finds by tag

Configures GameObject properties at class level.    [AutoAssign(tag: "GameController")]

    [SerializeField] private GameObject controller;

Properties:}

- Tag (string) - Sets the tag```

- Layer (string) - Sets the layer

- GameObjectName (string) - Renames GameObject### GameObject Configuration

- SortingLayer (string) - Sets sorting layer

- SortingOrder (int) - Sets sorting order```csharp

- IsStatic (bool) - Marks as static// Simple tag and layer setup

- ApplyToChildren (bool) - Applies to all children[AutoSetup(Tag = "Enemy", Layer = "Enemies")]

public class Enemy : MonoBehaviour { }

Example:

```csharp// 2D sprite configuration

[AutoSetup(Tag = "Enemy", Layer = "Enemies")][AutoSetup(SortingLayer = "Characters", SortingOrder = 10)]

public class Enemy : MonoBehaviour { }public class SpriteCharacter : MonoBehaviour { }

```

// Complete configuration

### [ConfigureComponent][AutoSetup(

    Tag = "Player",

Configures component properties. Can be used multiple times.    Layer = "Characters",

    GameObjectName = "MainPlayer",

Parameters:    IsStatic = false

- propertyName (string) - Name of property to configure)]

- value (object) - Value to assignpublic class Player : MonoBehaviour { }

- OnlyOnCreate (bool) - Only applies when component created```



Example:### Component Property Configuration

```csharp

[AutoAssign(autoCreate: true)]```csharp

[ConfigureComponent("gravityScale", 0f)]public class TopDownPlayer : MonoBehaviour

[ConfigureComponent("drag", 0.5f)]{

[SerializeField] private Rigidbody2D rb;    // Rigidbody2D: no gravity, no rotation

```    [AutoAssign(autoCreate: true)]

    [ConfigureComponent("gravityScale", 0f)]

## New Attributes in v3.0    [ConfigureComponent("drag", 0f)]

    [ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)]

### [ValidateAssignment]    [SerializeField] private Rigidbody2D rb;

    

Validates that assignment completed successfully.    // Trigger collider with custom size

    [AutoAssign(autoCreate: true)]

Parameters:    [ConfigureComponent("isTrigger", true)]

- level (ValidationLevel) - Error, Warning, or Info    [ConfigureComponent("radius", 1.5f)]

- delaySeconds (float) - Optional delay before validation    [SerializeField] private CircleCollider2D detectionZone;

    

Example:    // AudioSource: don't play on awake, loop, 50% volume

```csharp    [AutoAssign(autoCreate: true)]

[AutoAssign]    [ConfigureComponent("playOnAwake", false)]

[ValidateAssignment(level: ValidationLevel.Error)]    [ConfigureComponent("loop", true)]

[SerializeField] private Rigidbody2D rb;    [ConfigureComponent("volume", 0.5f)]

    [SerializeField] private AudioSource bgMusic;

[AutoAssign]}

[ValidateAssignment(level: ValidationLevel.Warning, delaySeconds: 1f)]```

[SerializeField] private Collider2D collider;

```### Complete Example



### [AutoAssignData]```csharp

using UnityEngine;

Loads assets from Resources folder.

[AutoSetup(Tag = "Player", Layer = "Player")]

Parameters:public class CompletePlayer : MonoBehaviour

- path (string) - Resource path relative to Resources folder{

    [Header("Physics")]

Example:    [AutoAssign(autoCreate: true)]

```csharp    [ConfigureComponent("gravityScale", 0f)]

[AutoAssignData("Cards/BasicAttack")]    [ConfigureComponent("drag", 0f)]

[SerializeField] private Card attackCard;    [SerializeField] private Rigidbody2D rb;

    

[AutoAssignData("Audio/AmbientSounds")]    [Header("Colliders")]

[SerializeField] private AudioClip[] ambience;    [AutoAssign(autoCreate: true)]

```    [ConfigureComponent("isTrigger", false)]

    [SerializeField] private BoxCollider2D solidCollider;

### [AutoAssignUI]    

    [AutoAssign(autoCreate: true)]

Type-safe UI component discovery.    [ConfigureComponent("isTrigger", true)]

    [ConfigureComponent("radius", 2f)]

Parameters:    [SerializeField] private CircleCollider2D interactionZone;

- componentType (UIComponentType) - Type of UI component    

- searchInChildren (bool) - Search in hierarchy    [Header("References")]

    [AutoAssign(searchInScene: true)]

Supported types: Button, Image, Text, Slider, InputField, Dropdown, Toggle, ScrollRect, Canvas, RectTransform, LayoutGroup, GridLayoutGroup    [SerializeField] private Camera mainCamera;

    

Example:    [SerializeField] private float speed = 5f;

```csharp    

[AutoAssignUI(UIComponentType.Button, searchInChildren: true)]    void Update()

[SerializeField] private Button submitButton;    {

        Vector2 input = new Vector2(

[AutoAssignUI(UIComponentType.Text, searchInChildren: true)]            Input.GetAxis("Horizontal"),

[SerializeField] private Text titleText;            Input.GetAxis("Vertical")

```        );

        rb.velocity = input.normalized * speed;

### [AutoAssignCallback]    }

}

Executes methods during assignment lifecycle.

## Editor Integration

Parameters:

- onSuccess (string) - Method name called on success### Automatic Execution

- onFailure (string) - Method name called on failure

The AutoAssigner system runs automatically when:

Example:- A script is added to a GameObject

```csharp- A script is modified or recompiled

[AutoAssign(autoCreate: true)]- A script component is reset

[AutoAssignCallback(onSuccess: nameof(OnAudioReady))]- The Unity Editor reloads

[SerializeField] private AudioSource audio;

### Execution Order

private void OnAudioReady()

{1. **AutoSetup**: Configures GameObject (tag, layer, sorting, etc.)

    audio.volume = 0.5f;2. **AutoAssign**: Assigns component references

}3. **ConfigureComponent**: Sets component properties

```

### Manual Trigger

## Usage Examples

To manually re-apply all attributes:

### Basic Enemy Setup1. Select the GameObject with the script

2. Right-click the script component in the Inspector

```csharp3. Select **Reset** from the context menu

using UnityEngine;

using UnityAutoAssign;### Menu Tools



[AutoSetup(Tag = "Enemy", Layer = "Enemies")]- **Tools > Auto Assign > Selected GameObjects**: Re-applies all attributes to selected objects

public class Enemy : MonoBehaviour- **Tools > Auto Assign > All Scene GameObjects**: Re-applies all attributes to all objects in the current scene

{

    [AutoAssign(autoCreate: true)]## Requirements

    [ConfigureComponent("gravityScale", 0f)]

    [ValidateAssignment(level: ValidationLevel.Error)]- Unity 2021.3 or newer

    [SerializeField] private Rigidbody2D rb;- .NET Standard 2.1

    

    [AutoAssign(autoCreate: true)]## Documentation

    [ConfigureComponent("isTrigger", true)]

    [ConfigureComponent("radius", 3f)]- **[USAGE_GUIDE.md](./USAGE_GUIDE.md)** - Comprehensive usage guide with detailed examples and best practices

    [ValidateAssignment(level: ValidationLevel.Error)]- **[CHANGELOG.md](./CHANGELOG.md)** - Version history and changes

    [SerializeField] private CircleCollider2D detectionZone;

    ## License

    [AutoAssignData("Enemies/EnemyStats")]

    [ValidateAssignment(level: ValidationLevel.Warning)]MIT License - See [LICENSE.md](./LICENSE.md)

    [SerializeField] private ScriptableObject enemyStats;

}## Support

```

- Report bugs via [GitHub Issues](https://github.com/karateka2610/UnityAutoAssign/issues)

### Battle UI System- Feature requests welcome

- Pull requests accepted

```csharp

using UnityEngine;## Changelog

using UnityAutoAssign;

using UnityEngine.UI;See [CHANGELOG.md](./CHANGELOG.md) for version history


[AutoSetup(Layer = "UI")]
public class BattleUI : MonoBehaviour
{
    [AutoAssignUI(UIComponentType.Slider, searchInChildren: true)]
    [ValidateAssignment(level: ValidationLevel.Error)]
    [SerializeField] private Slider healthSlider;
    
    [AutoAssignUI(UIComponentType.Button, searchInChildren: true)]
    [AutoAssignCallback(onSuccess: nameof(OnButtonReady))]
    [SerializeField] private Button attackButton;
    
    [AutoAssignData("UI/BattleTheme")]
    [SerializeField] private AudioClip battleMusic;
    
    private void OnButtonReady()
    {
        attackButton.onClick.AddListener(OnAttackClicked);
    }
    
    private void OnAttackClicked()
    {
        Debug.Log("Attack clicked");
    }
}
```

### Audio Manager

```csharp
using UnityEngine;
using UnityAutoAssign;

[AutoSetup(GameObjectName = "AudioManager")]
public class AudioManager : MonoBehaviour
{
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("playOnAwake", true)]
    [ConfigureComponent("loop", true)]
    [ConfigureComponent("volume", 0.3f)]
    [ValidateAssignment(level: ValidationLevel.Error)]
    [SerializeField] private AudioSource musicSource;
    
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("playOnAwake", false)]
    [ConfigureComponent("volume", 0.7f)]
    [ValidateAssignment(level: ValidationLevel.Error)]
    [SerializeField] private AudioSource sfxSource;
    
    [AutoAssignData("Audio/Ambience")]
    [SerializeField] private AudioClip[] ambienceClips;
}
```

### Card System

```csharp
using UnityEngine;
using UnityAutoAssign;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [AutoAssignUI(UIComponentType.Image, searchInChildren: true)]
    [SerializeField] private Image cardImage;
    
    [AutoAssignUI(UIComponentType.Text, searchInChildren: true)]
    [SerializeField] private Text damageText;
    
    [AutoAssignData("Cards/BasicAttack")]
    [ValidateAssignment(level: ValidationLevel.Warning)]
    [SerializeField] private Card cardData;
    
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("volume", 0.8f)]
    [AutoAssignCallback(onSuccess: nameof(OnAudioReady))]
    [SerializeField] private AudioSource cardSound;
    
    private void OnAudioReady()
    {
        Debug.Log("Card audio ready");
    }
}
```

## Editor Tools

### Automatic Execution

The system runs automatically when:
- Script added to GameObject
- Script modified or recompiled
- Script component reset
- Editor reloaded

### Execution Order

1. AutoSetup - Configure GameObject properties
2. AutoAssign - Assign component references
3. ConfigureComponent - Set component properties
4. ValidateAssignment - Validate assignments
5. AutoAssignCallback - Execute callbacks

### Manual Trigger

Right-click script component in Inspector and select Reset.

### Menu Items

- Tools > Auto Assign > Selected GameObjects
- Tools > Auto Assign > All Scene GameObjects

## Requirements

- Unity 2023 or newer
- .NET Standard 2.1
- Modern C# (nameof support)

## Documentation

Full documentation:
- USAGE_GUIDE.md - Comprehensive guide with examples and best practices
- CHANGELOG.md - Version history and changes

## Performance Notes

- AutoAssignCache optimizes repeated searches
- Use smallest search scope (default, children, parent, then scene)
- SearchInScene is slower with many GameObjects
- Validation runs in editor, minimal runtime overhead

## Best Practices

1. Use appropriate search scope for performance
2. Validate critical components with Error level
3. Use callbacks for component initialization
4. Organize with Headers for clarity
5. Apply AutoSetup at class level only
6. Always check for null in runtime code
7. Use OnlyOnCreate for optional configuration
8. Load data with AutoAssignData for static assets
9. Use AutoAssignUI for UI hierarchies
10. Chain callbacks for setup sequences

## Support

Report bugs or request features on GitHub.

## License

MIT License - See LICENSE file

## Version History

v3.0.0 - Added ValidateAssignment, AutoAssignData, AutoAssignUI, AutoAssignCallback
v2.0.0 - Initial release with AutoAssign, AutoSetup, ConfigureComponent
