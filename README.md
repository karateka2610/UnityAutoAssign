# Unity Auto Assign

![Unity Version](https://img.shields.io/badge/Unity-2021.3%2B-blue)
![License](https://img.shields.io/badge/license-MIT-green)
![Version](https://img.shields.io/badge/version-1.1.0-orange)

Automatically assigns component references in Unity using custom attributes and reflection. Eliminates manual component assignment in the Inspector.

## Features

- **Zero Configuration**: Components assign automatically when you add the script
- **Flexible Search**: Search in same GameObject, children, parents, or entire scene
- **Auto-Creation**: Automatically creates missing components
- **Tag Search**: Find GameObjects by tag
- **Safe**: Never overwrites manually assigned values
- **Unity 2021.3+**: Compatible with modern Unity versions

## Installation

### Via Git URL (Unity Package Manager)

1. Open Unity Package Manager (Window > Package Manager)
2. Click "+" button
3. Select "Add package from git URL"
4. Enter: `https://github.com/karateka2610/UnityAutoAssign.git`

### Manual Installation

1. Download the latest release
2. Extract to your project's `Assets` folder
3. Unity will automatically detect the plugin

## Quick Start

```csharp
using UnityEngine;

public class Player : MonoBehaviour
{
    // Auto-assigns Rigidbody from same GameObject
    [AutoAssign]
    [SerializeField] private Rigidbody rb;
    
    // Creates BoxCollider if it doesn't exist
    [AutoAssign(autoCreate: true)]
    [SerializeField] private BoxCollider col;
    
    // Finds Camera in scene
    [AutoAssign(searchInScene: true)]
    [SerializeField] private Camera mainCamera;
    
    void Start()
    {
        // All components are already assigned!
        rb.AddForce(Vector3.up * 10f);
    }
}
```

## Usage Examples

### Search in Children

```csharp
[AutoAssign(searchInChildren: true)]
[SerializeField] private Animator animator;
```

### Search in Parents

```csharp
[AutoAssign(searchInParent: true)]
[SerializeField] private Canvas canvas;
```

### Find by Tag

```csharp
[AutoAssign(tag: "Player")]
[SerializeField] private GameObject player;
```

### Auto-Create Component

```csharp
[AutoAssign(autoCreate: true)]
[SerializeField] private AudioSource audioSource;
```

## Editor Tools

**Tools > Auto Assign > Selected GameObjects**  
Auto-assigns components on selected objects

**Tools > Auto Assign > All Scene GameObjects**  
Auto-assigns components on all objects in scene

## Documentation

Full documentation available in [README.md](./README.md)

## Requirements

- Unity 2021.3 or newer
- .NET Standard 2.1

## License

MIT License - See [LICENSE.md](./LICENSE.md)

## Support

- Report bugs via [GitHub Issues](https://github.com/YOUR_USERNAME/UnityAutoAssign/issues)
- Feature requests welcome
- Pull requests accepted

## Changelog

See [CHANGELOG.md](./CHANGELOG.md) for version history
