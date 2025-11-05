# Changelog

All notable changes to this project will be documented in this file.

## [1.1.0] - 2025-11-05

### Added
- Tag-based search: Find GameObjects by tag using `[AutoAssign(tag: "TagName")]`
- Support for Unity 2023+ with new FindFirstObjectByType API
- Improved error messages and warnings
- Auto-creation marks GameObject as dirty for proper saving

### Changed
- Optimized search priority order
- Reduced console log verbosity (only warnings and errors)
- Improved compatibility detection for Unity versions

### Fixed
- Fixed issue with obsolete FindObjectOfType in Unity 2023+
- Fixed component auto-creation not saving properly

## [1.0.0] - 2025-11-05

### Added
- Initial release
- Basic auto-assignment for components on same GameObject
- Search in children with `searchInChildren` parameter
- Search in parents with `searchInParent` parameter
- Search in scene with `searchInScene` parameter
- Auto-creation of components with `autoCreate` parameter
- Editor menu items: Tools > Auto Assign
- Automatic assignment when component is added to GameObject
- Support for Unity 2021.3 and newer
