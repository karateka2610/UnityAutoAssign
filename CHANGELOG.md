# Changelog

## [2.0.0] - 2025-11-06

### Added
- AutoSetup attribute for automatic GameObject configuration
  - Set tags automatically
  - Set layers automatically
  - Configure sorting layers and order
  - Rename GameObjects
  - Mark as static
  - Apply to children recursively

- ConfigureComponent attribute for automatic component property configuration
  - Configure any component property from code
  - Support for all Unity component types
  - Multiple configurations per component
  - Automatic type conversion
  - OnlyOnCreate option for one-time setup

- ComponentConfigurator system for property configuration via reflection
  - Smart property detection
  - Type-safe conversions
  - Enum support
  - Clear error messages

### Documentation
- Added comprehensive Spanish guide (GUIA_DE_USO.md)
- Added ConfigureComponent documentation (CONFIGURAR_COMPONENTES.md)
- Added feature summary (NUEVA_CARACTERISTICA.md)
- Updated examples with new attributes

### Improvements
- Updated AutoAssignerEditor to integrate all three systems
- Execution order: AutoSetup → AutoAssign → ConfigureComponent
- Better error handling and logging
- Assembly definitions for proper compilation

### Examples
- PlayerController with full automation
- Enemy with AudioSource configuration
- Pickup items with trigger configuration
- Multiple real-world scenarios

## [1.0.0] - Previous Release

### Features
- AutoAssign attribute for component assignment
- Search in children, parents, and scene
- Auto-create missing components
- Tag-based search
- Editor tools for batch assignment
