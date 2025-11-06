# UnityAutoAssign - Usage Guide# UnityAutoAssign - Usage Guide



Version 3.0.0Version 3.0.0



## Table of Contents## Table of Contents



1. [Introduction](#introduction)1. [Introduction](#introduction)

2. [AutoAssign Attribute](#autoassign-attribute)2. [AutoAssign Attribute](#autoassign-attribute)

3. [AutoSetup Attribute](#autosetup-attribute)3. [AutoSetup Attribute](#autosetup-attribute)

4. [ConfigureComponent Attribute](#configurecomponent-attribute)4. [ConfigureComponent Attribute](#configurecomponent-attribute)

5. [ValidateAssignment Attribute](#validateassignment-attribute)5. [ValidateAssignment Attribute](#validateassignment-attribute)

6. [AutoAssignData Attribute](#autoassigndata-attribute)6. [AutoAssignData Attribute](#autoassigndata-attribute)

7. [AutoAssignUI Attribute](#autoassignui-attribute)7. [AutoAssignUI Attribute](#autoassignui-attribute)

8. [AutoAssignCallback Attribute](#autoassigncallback-attribute)8. [AutoAssignCallback Attribute](#autoassigncallback-attribute)

9. [Combining Attributes](#combining-attributes)9. [Combining Attributes](#combining-attributes)

10. [Best Practices](#best-practices)10. [Best Practices](#best-practices)

11. [Common Patterns](#common-patterns)11. [Common Patterns](#common-patterns)

12. [Troubleshooting](#troubleshooting)12. [Troubleshooting](#troubleshooting)

13. [Advanced Usage](#advanced-usage)13. [Advanced Usage](#advanced-usage)



------



## Introduction## Introduction



UnityAutoAssign v3.0 is a powerful attribute-based system that automates component management:UnityAutoAssign is a powerful attribute-based system that automates component management in Unity:



1. Component Assignment - Automatically find and assign component references1. **Component Assignment** - Automatically find and assign component references

2. GameObject Configuration - Set tags, layers, sorting properties2. **GameObject Configuration** - Set tags, layers, sorting, and other GameObject properties

3. Component Configuration - Initialize component properties3. **Component Configuration** - Initialize component properties with specific values

4. Data Assignment - Load assets from Resources4. **Data Assignment** - Load assets from Resources and AssetDatabase

5. UI Assignment - Easily find and assign UI components5. **UI Assignment** - Easily find and assign UI components

6. Validation - Verify component assignments at runtime6. **Validation** - Verify component assignments at runtime

7. Callbacks - Execute methods when assignments complete7. **Callbacks** - Execute methods when assignments complete



All systems work together seamlessly in the Unity Editor and runtime.All systems work together seamlessly and execute automatically in the Unity Editor.



------



## AutoAssign Attribute## AutoAssign Attribute



The [AutoAssign] attribute automatically assigns component references to serialized fields.The [AutoAssign] attribute automatically assigns component references to serialized fields.



### Basic Usage### Basic Usage



```csharp```csharp

using UnityEngine;using UnityEngine;

using UnityAutoAssign;using UnityAutoAssign;



public class BasicExample : MonoBehaviourpublic class BasicExample : MonoBehaviour

{{

    [AutoAssign]    // Automatically finds Rigidbody2D on the same GameObject

    [SerializeField] private Rigidbody2D rb;    [AutoAssign]

        [SerializeField] private Rigidbody2D rb;

    [AutoAssign]    

    [SerializeField] private BoxCollider2D collider;    // Automatically finds BoxCollider2D on the same GameObject

}    [AutoAssign]

```    [SerializeField] private BoxCollider2D collider;

}

### Auto-Create Components```



```csharp### Auto-Create Components

public class AutoCreateExample : MonoBehaviour

{Create components if they don't exist:

    [AutoAssign(autoCreate: true)]

    [SerializeField] private AudioSource audioSource;```csharp

    public class AutoCreateExample : MonoBehaviour

    [AutoAssign(autoCreate: true)]{

    [SerializeField] private CircleCollider2D collider;    // Creates AudioSource if missing

}    [AutoAssign(autoCreate: true)]

```    [SerializeField] private AudioSource audioSource;

    

### Search in Hierarchy    // Creates CircleCollider2D if missing

    [AutoAssign(autoCreate: true)]

```csharp    [SerializeField] private CircleCollider2D collider;

public class HierarchySearch : MonoBehaviour}

{```

    [AutoAssign(searchInChildren: true)]

    [SerializeField] private Animator childAnimator;### Search in Hierarchy

    

    [AutoAssign(searchInParent: true)]Search for components in different parts of the GameObject hierarchy:

    [SerializeField] private Canvas parentCanvas;

    ```csharp

    [AutoAssign(searchInScene: true)]public class HierarchySearch : MonoBehaviour

    [SerializeField] private Camera mainCamera;{

}    // Searches in child GameObjects

```    [AutoAssign(searchInChildren: true)]

    [SerializeField] private Animator childAnimator;

### Find by Tag    

    // Searches in parent GameObjects

```csharp    [AutoAssign(searchInParent: true)]

public class TagSearch : MonoBehaviour    [SerializeField] private Canvas parentCanvas;

{    

    [AutoAssign(tag: "Player")]    // Searches entire scene

    [SerializeField] private GameObject player;    [AutoAssign(searchInScene: true)]

        [SerializeField] private Camera mainCamera;

    [AutoAssign(tag: "GameController")]}

    [SerializeField] private GameObject gameController;```

}

```### Find by Tag



### AutoAssign ParametersFind GameObjects by tag:



| Parameter | Type | Description |```csharp

|-----------|------|-------------|public class TagSearch : MonoBehaviour

| autoCreate | bool | Creates the component if missing |{

| searchInChildren | bool | Searches in child GameObjects |    // Finds GameObject with "Player" tag

| searchInParent | bool | Searches in parent GameObjects |    [AutoAssign(tag: "Player")]

| searchInScene | bool | Searches entire scene |    [SerializeField] private GameObject player;

| tag | string | Finds GameObject by tag |    

    // Finds GameObject with "GameController" tag

---    [AutoAssign(tag: "GameController")]

    [SerializeField] private GameObject gameController;

## AutoSetup Attribute}

```

The [AutoSetup] attribute configures GameObject properties at class level.

### Parameters Reference

### Basic Configuration

| Parameter | Type | Description |

```csharp|-----------|------|-------------|

using UnityEngine;| autoCreate | bool | Creates the component if it doesn't exist |

using UnityAutoAssign;| searchInChildren | bool | Searches in child GameObjects |

| searchInParent | bool | Searches in parent GameObjects |

[AutoSetup(Tag = "Enemy", Layer = "Enemies")]| searchInScene | bool | Searches in the entire scene |

public class Enemy : MonoBehaviour| tag | string | Finds GameObject by tag |

{

}---

```

## AutoSetup Attribute

### Complete Configuration

The [AutoSetup] attribute configures GameObject properties. It's applied to the class itself.

```csharp

[AutoSetup(### Basic Configuration

    Tag = "Player",

    Layer = "Characters",```csharp

    GameObjectName = "MainPlayer",using UnityEngine;

    SortingLayer = "Foreground",

    SortingOrder = 100,// Sets tag and layer

    IsStatic = false,[AutoSetup(Tag = "Enemy", Layer = "Enemies")]

    ApplyToChildren = falsepublic class Enemy : MonoBehaviour

)]{

public class CompleteSetup : MonoBehaviour    // Enemy logic...

{}

}```

```

### GameObject Naming

### AutoSetup Properties

Rename GameObjects automatically:

| Property | Type | Description |

|----------|------|-------------|```csharp

| Tag | string | Sets the GameObject tag |// Renames GameObject to "MainPlayer"

| Layer | string | Sets the GameObject layer |[AutoSetup(GameObjectName = "MainPlayer")]

| GameObjectName | string | Renames the GameObject |public class Player : MonoBehaviour

| SortingLayer | string | Sets sorting layer |{

| SortingOrder | int | Sets sorting order |    // Player logic...

| IsStatic | bool | Marks GameObject as static |}

| ApplyToChildren | bool | Applies settings to children |```



---### 2D Sprite Configuration



## ConfigureComponent AttributeConfigure sorting for 2D sprites:



The [ConfigureComponent] attribute sets component property values.```csharp

// Sets sorting layer and order for 2D rendering

### Basic Configuration[AutoSetup(SortingLayer = "Characters", SortingOrder = 10)]

public class Character2D : MonoBehaviour

```csharp{

using UnityEngine;    [AutoAssign]

using UnityAutoAssign;    [SerializeField] private SpriteRenderer sprite;

}

public class BasicConfig : MonoBehaviour```

{

    [AutoAssign(autoCreate: true)]### Static GameObjects

    [ConfigureComponent("gravityScale", 0f)]

    [SerializeField] private Rigidbody2D rb;Mark GameObjects as static:

}

``````csharp

// Marks GameObject as static for performance optimization

### Multiple Properties[AutoSetup(IsStatic = true)]

public class StaticProp : MonoBehaviour

```csharp{

public class MultipleProps : MonoBehaviour    // Static prop logic...

{}

    [AutoAssign(autoCreate: true)]```

    [ConfigureComponent("gravityScale", 0f)]

    [ConfigureComponent("drag", 0f)]### Apply to Children

    [ConfigureComponent("angularDrag", 0.05f)]

    [ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)]Apply settings to all child GameObjects:

    [SerializeField] private Rigidbody2D rb;

}```csharp

```// Applies layer to this GameObject and all children

[AutoSetup(Layer = "UI", ApplyToChildren = true)]

### AudioSource Configurationpublic class UIPanel : MonoBehaviour

{

```csharp    // UI logic...

public class AudioConfig : MonoBehaviour}

{```

    [AutoAssign(autoCreate: true)]

    [ConfigureComponent("playOnAwake", false)]### Complete Configuration

    [ConfigureComponent("loop", true)]

    [ConfigureComponent("volume", 0.5f)]Combine all options:

    [ConfigureComponent("spatialBlend", 0f)]

    [SerializeField] private AudioSource bgMusic;```csharp

    [AutoSetup(

    [AutoAssign(autoCreate: true)]    Tag = "Player",

    [ConfigureComponent("playOnAwake", false)]    Layer = "Characters",

    [ConfigureComponent("loop", false)]    GameObjectName = "MainPlayer",

    [ConfigureComponent("spatialBlend", 1f)]    SortingLayer = "Foreground",

    [ConfigureComponent("minDistance", 1f)]    SortingOrder = 100,

    [ConfigureComponent("maxDistance", 10f)]    IsStatic = false,

    [SerializeField] private AudioSource sfx3D;    ApplyToChildren = false

})]

```public class CompleteSetup : MonoBehaviour

{

### OnlyOnCreate Option    // Logic...

}

```csharp```

public class OnCreateOnly : MonoBehaviour

{### Properties Reference

    [AutoAssign(autoCreate: true)]

    [ConfigureComponent("gravityScale", 0f, OnlyOnCreate = true)]| Property | Type | Description |

    [SerializeField] private Rigidbody2D rb;|----------|------|-------------|

}| Tag | string | Sets the GameObject tag |

```| Layer | string | Sets the GameObject layer |

| GameObjectName | string | Renames the GameObject |

---| SortingLayer | string | Sets sorting layer (for SpriteRenderer) |

| SortingOrder | int | Sets sorting order |

## ValidateAssignment Attribute| IsStatic | bool | Marks GameObject as static |

| ApplyToChildren | bool | Applies settings to all children |

NEW IN V3.0: The [ValidateAssignment] attribute validates that assignments succeeded.

---

### Basic Validation

## ConfigureComponent Attribute

```csharp

using UnityEngine;The `[ConfigureComponent]` attribute sets component property values. It can be used multiple times on the same field.

using UnityAutoAssign;

### Basic Configuration

public class ValidatedComponent : MonoBehaviour

{```csharp

    [AutoAssign]using UnityEngine;

    [ValidateAssignment(level: ValidationLevel.Error)]

    [SerializeField] private Rigidbody2D rb;public class BasicConfig : MonoBehaviour

}{

```    // Creates Rigidbody2D with gravity disabled

    [AutoAssign(autoCreate: true)]

### Validation Levels    [ConfigureComponent("gravityScale", 0f)]

    [SerializeField] private Rigidbody2D rb;

```csharp}

public class ValidationLevels : MonoBehaviour```

{

    [AutoAssign]### Multiple Properties

    [ValidateAssignment(level: ValidationLevel.Error)]

    [SerializeField] private Rigidbody2D rb;Configure multiple properties on one component:

    

    [AutoAssign]```csharp

    [ValidateAssignment(level: ValidationLevel.Warning)]public class MultipleProps : MonoBehaviour

    [SerializeField] private Collider2D collider;{

        [AutoAssign(autoCreate: true)]

    [AutoAssign]    [ConfigureComponent("gravityScale", 0f)]

    [ValidateAssignment(level: ValidationLevel.Info)]    [ConfigureComponent("drag", 0f)]

    [SerializeField] private Animator animator;    [ConfigureComponent("angularDrag", 0.05f)]

}    [ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)]

```    [SerializeField] private Rigidbody2D rb;

}

### Delayed Validation```



```csharp### Collider Configuration

public class DelayedValidation : MonoBehaviour

{```csharp

    [AutoAssign(autoCreate: true)]public class ColliderConfig : MonoBehaviour

    [ValidateAssignment(level: ValidationLevel.Error, delaySeconds: 1f)]{

    [SerializeField] private AudioSource audio;    // Box collider as trigger with custom size

}    [AutoAssign(autoCreate: true)]

```    [ConfigureComponent("isTrigger", true)]

    [ConfigureComponent("size", new Vector2(2f, 2f))]

---    [SerializeField] private BoxCollider2D triggerZone;

    

## AutoAssignData Attribute    // Circle collider with custom radius

    [AutoAssign(autoCreate: true)]

NEW IN V3.0: The [AutoAssignData] attribute loads assets from Resources folder.    [ConfigureComponent("isTrigger", false)]

    [ConfigureComponent("radius", 0.5f)]

### Load from Resources    [SerializeField] private CircleCollider2D solidCollider;

}

```csharp```

using UnityEngine;

using UnityAutoAssign;### AudioSource Configuration



public class DataLoader : MonoBehaviour```csharp

{public class AudioConfig : MonoBehaviour

    [AutoAssignData("Cards/BasicAttack")]{

    [SerializeField] private Card basicAttackCard;    // Background music configuration

        [AutoAssign(autoCreate: true)]

    [AutoAssignData("Audio/Ambience")]    [ConfigureComponent("playOnAwake", false)]

    [SerializeField] private AudioClip ambienceClip;    [ConfigureComponent("loop", true)]

}    [ConfigureComponent("volume", 0.5f)]

```    [ConfigureComponent("spatialBlend", 0f)]

    [SerializeField] private AudioSource bgMusic;

---    

    // 3D sound effect configuration

## AutoAssignUI Attribute    [AutoAssign(autoCreate: true)]

    [ConfigureComponent("playOnAwake", false)]

NEW IN V3.0: The [AutoAssignUI] attribute safely finds UI components.    [ConfigureComponent("loop", false)]

    [ConfigureComponent("spatialBlend", 1f)]

### UI Component Discovery    [ConfigureComponent("minDistance", 1f)]

    [ConfigureComponent("maxDistance", 10f)]

```csharp    [SerializeField] private AudioSource sfx3D;

using UnityEngine;}

using UnityAutoAssign;```

using UnityEngine.UI;

### SpriteRenderer Configuration

public class UIPanel : MonoBehaviour

{```csharp

    [AutoAssignUI(UIComponentType.Button, searchInChildren: true)]public class SpriteConfig : MonoBehaviour

    [SerializeField] private Button submitButton;{

        [AutoAssign(autoCreate: true)]

    [AutoAssignUI(UIComponentType.Image, searchInChildren: true)]    [ConfigureComponent("color", Color.red)]

    [SerializeField] private Image backgroundImage;    [ConfigureComponent("flipX", false)]

        [ConfigureComponent("flipY", false)]

    [AutoAssignUI(UIComponentType.Text, searchInChildren: true)]    [ConfigureComponent("sortingOrder", 5)]

    [SerializeField] private Text titleText;    [SerializeField] private SpriteRenderer sprite;

}}

``````



### Supported UI Component Types### OnlyOnCreate Option



```csharpOnly apply configuration when the component is first created:

public enum UIComponentType

{```csharp

    Button,public class OnCreateOnly : MonoBehaviour

    Image,{

    Text,    [AutoAssign(autoCreate: true)]

    Slider,    [ConfigureComponent("gravityScale", 0f, OnlyOnCreate = true)]

    InputField,    [SerializeField] private Rigidbody2D rb;

    Dropdown,    // This will only set gravityScale to 0 when the component is created

    Toggle,    // If the component already exists, it won't be modified

    ScrollRect,}

    Canvas,```

    RectTransform,

    LayoutGroup,### Supported Value Types

    GridLayoutGroup

}ConfigureComponent supports:

```- **Primitives**: int, float, double, bool, string

- **Unity Types**: Vector2, Vector3, Vector4, Color, Quaternion

---- **Enums**: Any enum type (e.g., RigidbodyConstraints2D)

- **Arrays**: Any array type

## AutoAssignCallback Attribute- **Complex Types**: Any serializable Unity type



NEW IN V3.0: The [AutoAssignCallback] attribute executes methods during assignment lifecycle.### Parameters Reference



### Success and Failure Callbacks| Parameter | Type | Description |

|-----------|------|-------------|

```csharp| `propertyName` | string | Name of the property to configure |

using UnityEngine;| `value` | object | Value to assign to the property |

using UnityAutoAssign;

| Property | Type | Description |

public class CallbackComponent : MonoBehaviour|----------|------|-------------|

{| `OnlyOnCreate` | bool | Only applies when component is created (default: false) |

    [AutoAssign]

    [AutoAssignCallback(onSuccess: nameof(OnComponentAssigned), onFailure: nameof(OnAssignmentFailed))]---

    [SerializeField] private Rigidbody2D rb;

    ## ValidateAssignment Attribute

    private void OnComponentAssigned()

    {NEW IN V3.0: The [ValidateAssignment] attribute validates that assignments completed successfully.

        Debug.Log("Rigidbody successfully assigned");

    }### Basic Validation

    

    private void OnAssignmentFailed()```csharp

    {using UnityEngine;

        Debug.LogError("Failed to assign Rigidbody");using UnityAutoAssign;

    }

}public class ValidatedComponent : MonoBehaviour

```{

    [AutoAssign]

### Multiple Callbacks    [ValidateAssignment(level: ValidationLevel.Error)]

    [SerializeField] private Rigidbody2D rb;

```csharp}

public class MultipleCallbacks : MonoBehaviour```

{

    [AutoAssign(autoCreate: true)]### Validation Levels

    [AutoAssignCallback(onSuccess: nameof(OnAudioCreated))]

    [SerializeField] private AudioSource audioSource;```csharp

    public class ValidationLevels : MonoBehaviour

    [AutoAssign(searchInChildren: true)]{

    [AutoAssignCallback(onSuccess: nameof(OnAnimatorFound))]    // Error - Shows error in console if null

    [SerializeField] private Animator animator;    [AutoAssign]

        [ValidateAssignment(level: ValidationLevel.Error)]

    private void OnAudioCreated()    [SerializeField] private Rigidbody2D rb;

    {    

        audioSource.volume = 0.5f;    // Warning - Shows warning in console if null

    }    [AutoAssign]

        [ValidateAssignment(level: ValidationLevel.Warning)]

    private void OnAnimatorFound()    [SerializeField] private Collider2D collider;

    {    

        Debug.Log("Animator found in children");    // Info - Shows message in console if null

    }    [AutoAssign]

}    [ValidateAssignment(level: ValidationLevel.Info)]

```    [SerializeField] private Animator animator;

}

---```



## Combining Attributes### Delayed Validation



The power comes from combining all attributes together.```csharp

public class DelayedValidation : MonoBehaviour

### Complete Example: Enemy with v3.0{

    [AutoAssign(autoCreate: true)]

```csharp    [ValidateAssignment(level: ValidationLevel.Error, delaySeconds: 1f)]

using UnityEngine;    [SerializeField] private AudioSource audio;

using UnityAutoAssign;    // Validation runs 1 second after scene load

}

[AutoSetup(Tag = "Enemy", Layer = "Enemies")]```

public class Enemy : MonoBehaviour

{---

    [Header("Physics")]

    [AutoAssign(autoCreate: true)]## AutoAssignData Attribute

    [ConfigureComponent("gravityScale", 0f)]

    [ConfigureComponent("drag", 0.5f)]NEW IN V3.0: The [AutoAssignData] attribute loads assets from Resources or AssetDatabase.

    [ValidateAssignment(level: ValidationLevel.Error)]

    [SerializeField] private Rigidbody2D rb;### Load from Resources

    

    [Header("Colliders")]```csharp

    [AutoAssign(autoCreate: true)]using UnityEngine;

    [ConfigureComponent("isTrigger", false)]using UnityAutoAssign;

    [ValidateAssignment(level: ValidationLevel.Error)]

    [SerializeField] private BoxCollider2D bodyCollider;public class DataLoader : MonoBehaviour

    {

    [AutoAssign(autoCreate: true)]    // Loads from Resources/Cards/BasicAttack

    [ConfigureComponent("isTrigger", true)]    [AutoAssignData("Cards/BasicAttack")]

    [ConfigureComponent("radius", 3f)]    [SerializeField] private Card basicAttackCard;

    [ValidateAssignment(level: ValidationLevel.Warning)]    

    [SerializeField] private CircleCollider2D detectionZone;    // Loads from Resources/Audio/Ambience

        [AutoAssignData("Audio/Ambience")]

    [Header("Audio")]    [SerializeField] private AudioClip ambienceClip;

    [AutoAssign(autoCreate: true)]}

    [ConfigureComponent("spatialBlend", 1f)]```

    [AutoAssignCallback(onSuccess: nameof(OnAudioReady))]

    [SerializeField] private AudioSource audioSource;---

    

    private void OnAudioReady()## AutoAssignUI Attribute

    {

        Debug.Log("Enemy audio system ready");NEW IN V3.0: The [AutoAssignUI] attribute safely finds UI components.

    }

}### UI Component Discovery

```

```csharp

### Complete Example: Battle UIusing UnityEngine;

using UnityAutoAssign;

```csharpusing UnityEngine.UI;

using UnityEngine;

using UnityAutoAssign;public class UIPanel : MonoBehaviour

using UnityEngine.UI;{

    // Finds Button in children

[AutoSetup(Layer = "UI")]    [AutoAssignUI(UIComponentType.Button, searchInChildren: true)]

public class BattleUI : MonoBehaviour    [SerializeField] private Button submitButton;

{    

    [Header("Health Display")]    // Finds Image in children

    [AutoAssignUI(UIComponentType.Slider, searchInChildren: true)]    [AutoAssignUI(UIComponentType.Image, searchInChildren: true)]

    [ValidateAssignment(level: ValidationLevel.Error)]    [SerializeField] private Image backgroundImage;

    [SerializeField] private Slider healthSlider;    

        // Finds Text in children

    [AutoAssignUI(UIComponentType.Text, searchInChildren: true)]    [AutoAssignUI(UIComponentType.Text, searchInChildren: true)]

    [ValidateAssignment(level: ValidationLevel.Error)]    [SerializeField] private Text titleText;

    [SerializeField] private Text healthText;}

    ```

    [Header("Buttons")]

    [AutoAssignUI(UIComponentType.Button, searchInChildren: true)]### Supported UI Components

    [AutoAssignCallback(onSuccess: nameof(OnAttackButtonFound))]

    [SerializeField] private Button attackButton;```csharp

    public enum UIComponentType

    [Header("Audio")]{

    [AutoAssign(autoCreate: true)]    Button,

    [ConfigureComponent("volume", 0.7f)]    Image,

    [SerializeField] private AudioSource uiSounds;    Text,

        Slider,

    [Header("Data")]    InputField,

    [AutoAssignData("UI/BattleTheme")]    Dropdown,

    [SerializeField] private AudioClip battleTheme;    Toggle,

        ScrollRect,

    private void OnAttackButtonFound()    Canvas,

    {    RectTransform,

        attackButton.onClick.AddListener(OnAttackClicked);    LayoutGroup,

    }    GridLayoutGroup

    }

    private void OnAttackClicked()```

    {

        if (uiSounds != null) uiSounds.Play();---

    }

}## AutoAssignCallback Attribute

```

NEW IN V3.0: The [AutoAssignCallback] attribute executes methods during assignment lifecycle.

---

### Success and Failure Callbacks

## Best Practices

```csharp

### 1. Use Appropriate Search Scopeusing UnityEngine;

using UnityAutoAssign;

```csharp

[AutoAssign]public class CallbackComponent : MonoBehaviour

[SerializeField] private Rigidbody2D rb;{

    [AutoAssign]

[AutoAssign(searchInScene: true)]    [AutoAssignCallback(onSuccess: nameof(OnComponentAssigned), onFailure: nameof(OnAssignmentFailed))]

[SerializeField] private Camera mainCamera;    [SerializeField] private Rigidbody2D rb;

```    

    private void OnComponentAssigned()

### 2. Always Validate Critical Components    {

        Debug.Log("Rigidbody successfully assigned");

```csharp    }

[AutoAssign(autoCreate: true)]    

[ValidateAssignment(level: ValidationLevel.Error)]    private void OnAssignmentFailed()

[SerializeField] private Rigidbody2D rb;    {

```        Debug.LogError("Failed to assign Rigidbody");

    }

### 3. Use Callbacks for Setup}

```

```csharp

[AutoAssign(autoCreate: true)]### Multiple Callbacks

[AutoAssignCallback(onSuccess: nameof(SetupAudio))]

[SerializeField] private AudioSource audio;```csharp

public class MultipleCallbacks : MonoBehaviour

private void SetupAudio(){

{    [AutoAssign(autoCreate: true)]

    audio.volume = 0.5f;    [AutoAssignCallback(onSuccess: nameof(OnAudioCreated))]

}    [SerializeField] private AudioSource audioSource;

```    

    [AutoAssign(searchInChildren: true)]

### 4. Combine AutoAssign with ConfigureComponent    [AutoAssignCallback(onSuccess: nameof(OnAnimatorFound))]

    [SerializeField] private Animator animator;

```csharp    

[AutoAssign(autoCreate: true)]    private void OnAudioCreated()

[ConfigureComponent("gravityScale", 0f)]    {

[SerializeField] private Rigidbody2D rb;        audioSource.volume = 0.5f;

```    }

    

### 5. Use Headers for Organization    private void OnAnimatorFound()

    {

```csharp        Debug.Log("Animator component found in children");

[Header("Physics")]    }

[AutoAssign(autoCreate: true)]}

[SerializeField] private Rigidbody2D rb;```



[Header("Settings")]---

[SerializeField] private float speed = 5f;

```## Combining Attributes



### 6. Apply AutoSetup at Class LevelThe real power comes from combining all attributes together.



```csharp### Complete Example: Enemy with v3.0 Features

[AutoSetup(Tag = "Player")]

public class Player : MonoBehaviour { }```csharp

```using UnityEngine;

using UnityAutoAssign;

### 7. Use OnlyOnCreate for Optional Configuration

[AutoSetup(Tag = "Enemy", Layer = "Enemies")]

```csharppublic class Enemy : MonoBehaviour

[AutoAssign(autoCreate: true)]{

[ConfigureComponent("volume", 0.5f, OnlyOnCreate = true)]    [Header("Physics")]

[SerializeField] private AudioSource audio;    [AutoAssign(autoCreate: true)]

```    [ConfigureComponent("gravityScale", 0f)]

    [ConfigureComponent("drag", 0.5f)]

### 8. Load Data with AutoAssignData    [ValidateAssignment(level: ValidationLevel.Error)]

    [SerializeField] private Rigidbody2D rb;

```csharp    

[AutoAssignData("Cards/BasicAttack")]    [Header("Colliders")]

[ValidateAssignment(level: ValidationLevel.Error)]    [AutoAssign(autoCreate: true)]

[SerializeField] private Card attackCard;    [ConfigureComponent("isTrigger", false)]

```    [ValidateAssignment(level: ValidationLevel.Error)]

    [SerializeField] private BoxCollider2D bodyCollider;

### 9. Use Proper UI Types    

    [AutoAssign(autoCreate: true)]

```csharp    [ConfigureComponent("isTrigger", true)]

[AutoAssignUI(UIComponentType.Button, searchInChildren: true)]    [ConfigureComponent("radius", 3f)]

[SerializeField] private Button submitButton;    [ValidateAssignment(level: ValidationLevel.Warning)]

```    [SerializeField] private CircleCollider2D detectionZone;

    

### 10. Always Check for Null    [Header("Audio")]

    [AutoAssign(autoCreate: true)]

```csharp    [ConfigureComponent("spatialBlend", 1f)]

void Update()    [AutoAssignCallback(onSuccess: nameof(OnAudioReady))]

{    [SerializeField] private AudioSource audioSource;

    if (rb != null)    

    {    [Header("Data")]

        rb.velocity = Vector2.zero;    [AutoAssignData("Enemies/EnemyStats")]

    }    [ValidateAssignment(level: ValidationLevel.Warning)]

}    [SerializeField] private ScriptableObject enemyStats;

```    

    private void OnAudioReady()

---    {

        Debug.Log("Enemy audio system ready");

## Common Patterns    }

}

### Pattern 1: Physics Object```



```csharp### Complete Example: Battle UI with v3.0

using UnityEngine;

using UnityAutoAssign;```csharp

using UnityEngine;

[AutoSetup(Layer = "PhysicsObjects")]using UnityAutoAssign;

public class PhysicsObject : MonoBehaviourusing UnityEngine.UI;

{

    [AutoAssign(autoCreate: true)][AutoSetup(Layer = "UI")]

    [ConfigureComponent("drag", 0.5f)]public class BattleUI : MonoBehaviour

    [ConfigureComponent("angularDrag", 0.5f)]{

    [ValidateAssignment(level: ValidationLevel.Error)]    [Header("Health Display")]

    [SerializeField] private Rigidbody2D rb;    [AutoAssignUI(UIComponentType.Slider, searchInChildren: true)]

        [ValidateAssignment(level: ValidationLevel.Error)]

    [AutoAssign(autoCreate: true)]    [SerializeField] private Slider healthSlider;

    [ValidateAssignment(level: ValidationLevel.Error)]    

    [SerializeField] private BoxCollider2D collider;    [AutoAssignUI(UIComponentType.Text, searchInChildren: true)]

}    [ValidateAssignment(level: ValidationLevel.Error)]

```    [SerializeField] private Text healthText;

    

### Pattern 2: Singleton Manager    [Header("Buttons")]

    [AutoAssignUI(UIComponentType.Button, searchInChildren: true)]

```csharp    [AutoAssignCallback(onSuccess: nameof(OnAttackButtonFound))]

using UnityEngine;    [SerializeField] private Button attackButton;

using UnityAutoAssign;    

    [AutoAssignUI(UIComponentType.Button, searchInChildren: true)]

[AutoSetup(GameObjectName = "GameManager")]    [SerializeField] private Button defendButton;

public class GameManager : MonoBehaviour    

{    [Header("Audio")]

    public static GameManager Instance { get; private set; }    [AutoAssign(autoCreate: true)]

        [ConfigureComponent("volume", 0.7f)]

    [AutoAssign(searchInScene: true)]    [SerializeField] private AudioSource uiSounds;

    [ValidateAssignment(level: ValidationLevel.Error)]    

    [SerializeField] private Camera mainCamera;    [Header("Data")]

        [AutoAssignData("UI/BattleTheme")]

    void Awake()    [SerializeField] private AudioClip battleTheme;

    {    

        if (Instance == null)    private void OnAttackButtonFound()

        {    {

            Instance = this;        attackButton.onClick.AddListener(OnAttackClicked);

            DontDestroyOnLoad(gameObject);    }

        }    

        else    private void OnAttackClicked()

        {    {

            Destroy(gameObject);        if (uiSounds != null)

        }        {

    }            uiSounds.Play();

}        }

```    }

}

### Pattern 3: Audio Manager```



```csharp```csharp

using UnityEngine;using UnityEngine;

using UnityAutoAssign;

[AutoSetup(Tag = "Player", Layer = "Characters")]

[AutoSetup(GameObjectName = "AudioManager")]public class TopDownCharacter : MonoBehaviour

public class AudioManager : MonoBehaviour{

{    [Header("Physics")]

    [Header("Music")]    [AutoAssign(autoCreate: true)]

    [AutoAssign(autoCreate: true)]    [ConfigureComponent("gravityScale", 0f)]

    [ConfigureComponent("playOnAwake", true)]    [ConfigureComponent("drag", 0f)]

    [ConfigureComponent("loop", true)]    [ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)]

    [ConfigureComponent("volume", 0.3f)]    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private AudioSource musicSource;    

        [Header("Colliders")]

    [Header("SFX")]    [AutoAssign(autoCreate: true)]

    [AutoAssign(autoCreate: true)]    [ConfigureComponent("isTrigger", false)]

    [ConfigureComponent("playOnAwake", false)]    [SerializeField] private BoxCollider2D bodyCollider;

    [ConfigureComponent("loop", false)]    

    [ConfigureComponent("volume", 0.7f)]    [AutoAssign(autoCreate: true)]

    [SerializeField] private AudioSource sfxSource;    [ConfigureComponent("isTrigger", true)]

        [ConfigureComponent("radius", 1.5f)]

    [Header("Ambience")]    [SerializeField] private CircleCollider2D interactionZone;

    [AutoAssignData("Audio/AmbientSounds")]    

    [SerializeField] private AudioClip[] ambienceClips;    [Header("Visual")]

        [AutoAssign(searchInChildren: true)]

    public void PlaySound(AudioClip clip)    [SerializeField] private SpriteRenderer sprite;

    {    

        if (sfxSource != null)    [Header("References")]

        {    [AutoAssign(searchInScene: true)]

            sfxSource.PlayOneShot(clip);    [SerializeField] private Camera mainCamera;

        }    

    }    [Header("Settings")]

}    [SerializeField] private float speed = 5f;

```    

    void Update()

### Pattern 4: Battle Panel    {

        Vector2 input = new Vector2(

```csharp            Input.GetAxis("Horizontal"),

using UnityEngine;            Input.GetAxis("Vertical")

using UnityAutoAssign;        );

using UnityEngine.UI;        

        if (rb != null)

[AutoSetup(Layer = "UI")]        {

public class BattlePanel : MonoBehaviour            rb.velocity = input.normalized * speed;

{        }

    [Header("Display")]    }

    [AutoAssignUI(UIComponentType.Text, searchInChildren: true)]}

    [ValidateAssignment(level: ValidationLevel.Error)]```

    [SerializeField] private Text turnText;

    ### Platformer Character

    [AutoAssignUI(UIComponentType.Slider, searchInChildren: true)]

    [ValidateAssignment(level: ValidationLevel.Error)]```csharp

    [SerializeField] private Slider healthSlider;using UnityEngine;

    

    [Header("Controls")][AutoSetup(Tag = "Player", Layer = "Player", SortingLayer = "Characters")]

    [AutoAssignUI(UIComponentType.Button, searchInChildren: true)]public class PlatformerCharacter : MonoBehaviour

    [AutoAssignCallback(onSuccess: nameof(SetupButtons))]{

    [SerializeField] private Button[] actionButtons;    [Header("Physics")]

        [AutoAssign(autoCreate: true)]

    private void SetupButtons()    [ConfigureComponent("gravityScale", 3f)]

    {    [ConfigureComponent("drag", 0f)]

        for (int i = 0; i < actionButtons.Length; i++)    [ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)]

        {    [SerializeField] private Rigidbody2D rb;

            int index = i;    

            actionButtons[i].onClick.AddListener(() => OnActionClicked(index));    [Header("Colliders")]

        }    [AutoAssign(autoCreate: true)]

    }    [ConfigureComponent("isTrigger", false)]

        [SerializeField] private BoxCollider2D bodyCollider;

    private void OnActionClicked(int actionIndex)    

    {    [Header("Visual")]

        Debug.Log($"Action clicked: {actionIndex}");    [AutoAssign(searchInChildren: true)]

    }    [SerializeField] private Animator animator;

}    

```    [AutoAssign(searchInChildren: true)]

    [SerializeField] private SpriteRenderer sprite;

### Pattern 5: Card System    

    [Header("Settings")]

```csharp    [SerializeField] private float moveSpeed = 5f;

using UnityEngine;    [SerializeField] private float jumpForce = 10f;

using UnityAutoAssign;    

using UnityEngine.UI;    void Update()

    {

public class CardUI : MonoBehaviour        float horizontal = Input.GetAxis("Horizontal");

{        

    [Header("Display")]        if (rb != null)

    [AutoAssignUI(UIComponentType.Image, searchInChildren: true)]        {

    [SerializeField] private Image cardImage;            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

                

    [AutoAssignUI(UIComponentType.Text, searchInChildren: true)]            if (Input.GetKeyDown(KeyCode.Space))

    [SerializeField] private Text nameText;            {

                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);

    [AutoAssignUI(UIComponentType.Text, searchInChildren: true)]            }

    [SerializeField] private Text damageText;        }

        }

    [Header("Card Data")]}

    [AutoAssignData("Cards/BasicAttack")]```

    [ValidateAssignment(level: ValidationLevel.Warning)]

    [SerializeField] private Card cardData;### Audio Manager

    

    [Header("Audio")]```csharp

    [AutoAssign(autoCreate: true)]using UnityEngine;

    [ConfigureComponent("volume", 0.8f)]

    [AutoAssignCallback(onSuccess: nameof(OnAudioReady))][AutoSetup(GameObjectName = "AudioManager")]

    [SerializeField] private AudioSource cardSound;public class AudioManager : MonoBehaviour

    {

    public void PlayCard()    [Header("Music")]

    {    [AutoAssign(autoCreate: true)]

        if (cardSound != null)    [ConfigureComponent("playOnAwake", true)]

        {    [ConfigureComponent("loop", true)]

            cardSound.Play();    [ConfigureComponent("volume", 0.3f)]

        }    [ConfigureComponent("spatialBlend", 0f)]

    }    [SerializeField] private AudioSource musicSource;

        

    private void OnAudioReady()    [Header("SFX")]

    {    [AutoAssign(autoCreate: true)]

        Debug.Log("Card audio initialized");    [ConfigureComponent("playOnAwake", false)]

    }    [ConfigureComponent("loop", false)]

}    [ConfigureComponent("volume", 0.7f)]

```    [ConfigureComponent("spatialBlend", 0f)]

    [SerializeField] private AudioSource sfxSource;

---    

    public void PlaySound(AudioClip clip)

## Troubleshooting    {

        if (sfxSource != null)

### Component Not Assigned        {

            sfxSource.PlayOneShot(clip);

Problem: Component field remains null after AutoAssign.        }

    }

Solutions:}

1. Check component exists on GameObject in search scope```

2. Verify search parameters (searchInChildren, searchInParent, etc.)

3. Try using autoCreate: true### UI Panel

4. Use ValidateAssignment to see errors

5. Manually reset component in Inspector```csharp

using UnityEngine;

### ConfigureComponent Not Workingusing UnityEngine.UI;



Problem: Component properties aren't being set.[AutoSetup(Layer = "UI", ApplyToChildren = true)]

public class UIPanel : MonoBehaviour

Solutions:{

1. Verify property name matches exactly    [Header("Panel Components")]

2. Check value type matches property type    [AutoAssign]

3. Ensure AutoAssign with autoCreate: true is present    [SerializeField] private Image background;

4. Make sure component exists before configuration    

    [AutoAssign(searchInChildren: true)]

### AutoSetup Not Applied    [SerializeField] private Button closeButton;

    

Problem: GameObject properties (tag, layer) aren't being set.    [Header("Canvas")]

    [AutoAssign(searchInParent: true)]

Solutions:    [SerializeField] private Canvas canvas;

1. Verify AutoSetup applied to class, not field    

2. Check tag and layer exist in Project Settings    void Start()

3. Ensure GameObject isn't prefab-only    {

        if (closeButton != null)

### ValidateAssignment Not Showing        {

            closeButton.onClick.AddListener(ClosePanel);

Problem: Validation errors not in console.        }

    }

Solutions:    

1. Check ValidationLevel is Error or Warning    void ClosePanel()

2. Verify component is null    {

3. Check script is enabled on GameObject        gameObject.SetActive(false);

4. Make sure GameObject is active    }

}

### AutoAssignData Not Loading```



Problem: Data fields remain null.---



Solutions:## Best Practices

1. Verify asset path is correct (case-sensitive)

2. Check asset exists in Resources folder### 1. Use Appropriate Search Scope

3. Ensure asset type matches field type

4. Use ValidateAssignment for errorsChoose the smallest search scope needed:



### AutoAssignUI Not Finding Components```csharp

// Good: Searches only on same GameObject

Problem: UI components remain null.[AutoAssign]

[SerializeField] private Rigidbody2D rb;

Solutions:

1. Verify UI component type is correct// Less efficient: Searches entire scene when not needed

2. Check searchInChildren is true[AutoAssign(searchInScene: true)]

3. Make sure component exists in hierarchy[SerializeField] private Rigidbody2D rb;

4. Use ValidateAssignment for errors```



### Callback Methods Not Executing### 2. Always Validate Critical Components



Problem: Callbacks aren't being called.Use ValidateAssignment for components that must exist:



Solutions:```csharp

1. Verify method name in nameof() matches// Good: Validates that assignment succeeded

2. Check method accessibility[AutoAssign(autoCreate: true)]

3. Make sure method has no parameters[ValidateAssignment(level: ValidationLevel.Error)]

4. Verify method on same MonoBehaviour[SerializeField] private Rigidbody2D rb;



---// Less safe: No validation

[AutoAssign(autoCreate: true)]

## Advanced Usage[SerializeField] private Rigidbody2D rb;

```

### Custom Component Classes

### 3. Use Callbacks for Setup

AutoAssign works with custom components:

Initialize component state with callbacks:

```csharp

using UnityEngine;```csharp

using UnityAutoAssign;// Good: Callback ensures setup after assignment

[AutoAssign(autoCreate: true)]

public class CustomHealth : MonoBehaviour[AutoAssignCallback(onSuccess: nameof(SetupAudio))]

{[SerializeField] private AudioSource audio;

    public int maxHealth = 100;

}private void SetupAudio()

{

public class Character : MonoBehaviour    audio.volume = 0.5f;

{    audio.loop = true;

    [AutoAssign(autoCreate: true)]}

    [ConfigureComponent("maxHealth", 200)]

    [ValidateAssignment(level: ValidationLevel.Error)]// Less flexible: Manual setup needed

    [SerializeField] private CustomHealth health;[AutoAssign(autoCreate: true)]

}[SerializeField] private AudioSource audio;

``````



### Complex Value Types### 4. Combine AutoAssign with ConfigureComponent



ConfigureComponent supports complex types:Always use autoCreate: true when using ConfigureComponent:



```csharp```csharp

public class ComplexConfig : MonoBehaviour// Good: Creates component then configures it

{[AutoAssign(autoCreate: true)]

    [AutoAssign(autoCreate: true)][ConfigureComponent("gravityScale", 0f)]

    [ConfigureComponent("color", new Color(1f, 0.5f, 0f, 1f))][SerializeField] private Rigidbody2D rb;

    [ConfigureComponent("size", new Vector2(2f, 3f))]

    [SerializeField] private SpriteRenderer sprite;// Bad: ConfigureComponent has nothing to configure if component doesn't exist

}[AutoAssign]

```[ConfigureComponent("gravityScale", 0f)]

[SerializeField] private Rigidbody2D rb;

### Enum Configurations```



Use enums for configuration:### 5. Use Headers for Organization



```csharpGroup related fields with headers:

public class EnumConfig : MonoBehaviour

{```csharp

    [AutoAssign(autoCreate: true)][Header("Physics")]

    [ConfigureComponent("constraints", RigidbodyConstraints2D.FreezeRotation)][AutoAssign(autoCreate: true)]

    [ConfigureComponent("collisionDetectionMode", CollisionDetectionMode2D.Continuous)][SerializeField] private Rigidbody2D rb;

    [ValidateAssignment(level: ValidationLevel.Error)]

    [SerializeField] private Rigidbody2D rb;[Header("Colliders")]

}[AutoAssign(autoCreate: true)]

```[SerializeField] private BoxCollider2D collider;



### Chained Callbacks[Header("Settings")]

[SerializeField] private float speed = 5f;

Execute multiple callbacks in sequence:```



```csharp### 6. Apply AutoSetup at Class Level

public class ChainedCallbacks : MonoBehaviour

{Always apply AutoSetup to the class, not fields:

    [AutoAssign(autoCreate: true)]

    [AutoAssignCallback(onSuccess: nameof(OnPhysicsReady))]```csharp

    [SerializeField] private Rigidbody2D rb;// Good

    [AutoSetup(Tag = "Player")]

    [AutoAssign(autoCreate: true)]public class Player : MonoBehaviour { }

    [AutoAssignCallback(onSuccess: nameof(OnAudioReady))]

    [SerializeField] private AudioSource audio;// Wrong: AutoSetup doesn't work on fields

    public class Player : MonoBehaviour

    private void OnPhysicsReady(){

    {    [AutoSetup(Tag = "Player")] // This won't work!

        Debug.Log("Physics initialized");    [SerializeField] private int health;

    }}

    ```

    private void OnAudioReady()

    {### 7. Use OnlyOnCreate for Optional Configuration

        Debug.Log("Audio initialized");

    }Use OnlyOnCreate = true when you want to set default values but allow manual override:

}

``````csharp

[AutoAssign(autoCreate: true)]

### Prefab Workflows[ConfigureComponent("volume", 0.5f, OnlyOnCreate = true)]

[SerializeField] private AudioSource audio;

AutoAssign works great with prefabs:// Volume will be 0.5f when created, but you can change it later manually

```

```csharp

public class PrefabSpawner : MonoBehaviour### 8. Load Data with AutoAssignData

{

    [SerializeField] private GameObject enemyPrefab;Use AutoAssignData for asset loading:

    

    void SpawnEnemy()```csharp

    {// Good: Data loaded from Resources

        Instantiate(enemyPrefab, transform.position, Quaternion.identity);[AutoAssignData("Cards/BasicAttack")]

    }[ValidateAssignment(level: ValidationLevel.Error)]

}[SerializeField] private Card attackCard;

```

// Less organized: Manual loading

---private Card attackCard;

void Awake()

## Summary{

    attackCard = Resources.Load<Card>("Cards/BasicAttack");

UnityAutoAssign v3.0 provides seven powerful attributes:}

```

1. AutoAssign - Finds and assigns component references

2. AutoSetup - Configures GameObject properties### 9. Use Proper UI Types with AutoAssignUI

3. ConfigureComponent - Sets component property values

4. ValidateAssignment - Validates assignment completionSpecify UI component types for better discovery:

5. AutoAssignData - Loads assets from Resources

6. AutoAssignUI - Safely finds UI components```csharp

7. AutoAssignCallback - Executes lifecycle callbacks// Good: Explicit UI type

[AutoAssignUI(UIComponentType.Button, searchInChildren: true)]

Use them together to eliminate boilerplate code and ensure consistent component configuration across your Unity project.[SerializeField] private Button submitButton;



For more information:// Works but less explicit

- README.md - Quick start and overview[AutoAssign(searchInChildren: true)]

- CHANGELOG.md - Version history and v3.0 features[SerializeField] private Button submitButton;

```

Questions or Issues?

Visit the GitHub repository to report issues or contribute.### 10. Always Check for Null


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
using UnityAutoAssign;

[AutoSetup(Layer = "PhysicsObjects")]
public class PhysicsObject : MonoBehaviour
{
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("drag", 0.5f)]
    [ConfigureComponent("angularDrag", 0.5f)]
    [ValidateAssignment(level: ValidationLevel.Error)]
    [SerializeField] private Rigidbody2D rb;
    
    [AutoAssign(autoCreate: true)]
    [ValidateAssignment(level: ValidationLevel.Error)]
    [SerializeField] private BoxCollider2D collider;
}
```

### Pattern 2: Singleton Manager

```csharp
using UnityEngine;
using UnityAutoAssign;

[AutoSetup(GameObjectName = "GameManager")]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance CURRENT { get; private set; }
    
    [AutoAssign(searchInScene: true)]
    [ValidateAssignment(level: ValidationLevel.Error)]
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

### Pattern 3: Audio Manager with v3.0

```csharp
using UnityEngine;
using UnityAutoAssign;

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
    
    [Header("Ambience")]
    [AutoAssignData("Audio/AmbientSounds")]
    [SerializeField] private AudioClip[] ambienceClips;
    
    public void PlaySound(AudioClip clip)
    {
        if (sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
```

### Pattern 4: Battle UI Panel

```csharp
using UnityEngine;
using UnityAutoAssign;
using UnityEngine.UI;

[AutoSetup(Layer = "UI")]
public class BattlePanel : MonoBehaviour
{
    [Header("Display")]
    [AutoAssignUI(UIComponentType.Text, searchInChildren: true)]
    [ValidateAssignment(level: ValidationLevel.Error)]
    [SerializeField] private Text turnText;
    
    [AutoAssignUI(UIComponentType.Slider, searchInChildren: true)]
    [ValidateAssignment(level: ValidationLevel.Error)]
    [SerializeField] private Slider healthSlider;
    
    [Header("Controls")]
    [AutoAssignUI(UIComponentType.Button, searchInChildren: true)]
    [AutoAssignCallback(onSuccess: nameof(SetupButtons))]
    [SerializeField] private Button[] actionButtons;
    
    private void SetupButtons()
    {
        for (int i = 0; i < actionButtons.Length; i++)
        {
            int index = i;
            actionButtons[i].onClick.AddListener(() => OnActionClicked(index));
        }
    }
    
    private void OnActionClicked(int actionIndex)
    {
        Debug.Log($"Action clicked: {actionIndex}");
    }
}
```

### Pattern 5: Card System with v3.0

```csharp
using UnityEngine;
using UnityAutoAssign;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [Header("Display")]
    [AutoAssignUI(UIComponentType.Image, searchInChildren: true)]
    [SerializeField] private Image cardImage;
    
    [AutoAssignUI(UIComponentType.Text, searchInChildren: true)]
    [SerializeField] private Text nameText;
    
    [AutoAssignUI(UIComponentType.Text, searchInChildren: true)]
    [SerializeField] private Text damageText;
    
    [Header("Card Data")]
    [AutoAssignData("Cards/BasicAttack")]
    [ValidateAssignment(level: ValidationLevel.Warning)]
    [SerializeField] private Card cardData;
    
    [Header("Audio")]
    [AutoAssign(autoCreate: true)]
    [ConfigureComponent("volume", 0.8f)]
    [AutoAssignCallback(onSuccess: nameof(OnAudioReady))]
    [SerializeField] private AudioSource cardSound;
    
    public void PlayCard()
    {
        if (cardSound != null)
        {
            cardSound.Play();
        }
    }
    
    private void OnAudioReady()
    {
        Debug.Log("Card audio initialized");
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
