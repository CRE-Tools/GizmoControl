> [!TIP]
> <a href="/Documentation~/UserManual.pdf">View this manual as PDF</a>

<h1 align="center">Gizmo Control - User Manual</h1>
<p align="right">v1.0.0</p>

## Table of Contents
1. [Introduction](#introduction)
   - [Overview](#overview)
   - [Features](#features)
   - [Requirements](#requirements)
2. [Installation](#installation)
   - [Via Package Manager](#via-package-manager)
   - [Manual Installation](#manual-installation)
3. [Getting Started](#getting-started)
   - [Basic Usage](#basic-usage)
   - [Quick Example](#quick-example)
4. [GizmoDrawMode Reference](#gizmodrawmode-reference)
   - [Available Modes](#available-modes)
   - [Implementation Details](#implementation-details)
5. [Advanced Usage](#advanced-usage)
   - [Custom Gizmo Implementation](#custom-gizmo-implementation)
   - [Best Practices](#best-practices)
6. [Troubleshooting](#troubleshooting)
   - [Common Issues](#common-issues)
   - [FAQ](#faq)

---
## Introduction
### Overview

Gizmo Control is a lightweight Unity Editor utility that centralizes and simplifies gizmo rendering for your custom scripts.
Instead of writing `[DrawGizmo]` or `OnDrawGizmos()` attributes for each component, you simply implement an interface — and the system automatically handles when and how your gizmos are drawn.

It acts as a “gizmo interceptor,” managing all gizmo rendering logic in a single place while keeping your components clean and decoupled.

### Features

- **Plug-and-Play**: Just implement `IGizmoControl`; no need for custom `Editor` scripts.
- **Centralized Control**: All gizmos are managed by the internal `GizmoDrawer` system.
- **Editor-Only**: Automatically excluded from builds (wrapped in `#if UNITY_EDITOR`).
- **Flexible Visibility**: Choose when your gizmos are drawn using `GizmoDrawMode`.
- **No Setup Needed**: Works automatically once the interface is implemented.

### Requirements

- Unity 2020.3 or later (recommended)
- Basic understanding of C# and Unity’s `Gizmos` API

## Installation

### Via Package Manager (Recommended)

1. Open your Unity project
2. Go to `Window > Package Manager`
3. Click the `+` button in the top-left corner
4. Select `Add package from git URL...`
5. Enter: `https://github.com/CRE-Tools/GizmoControl.git`
6. Click `Add`

### Manual Installation

1. Clone or download this repository
2. Copy the contents to your project's `Packages` folder
3. The package will be automatically imported into your Unity project

## Getting Started

### Basic Usage

To use Gizmo Control, implement the `IGizmoControl` interface in any `MonoBehaviour` where you want custom gizmo drawing.

The interface defines:
```csharp
public GizmoDrawMode DrawMode { get; }
void DrawGizmos();
```
The system will automatically detect your component and handle when to call `DrawGizmos()`.

### Quick Example

``` csharp
using UnityEngine;
using PUCPR.GizmoControl;

public class MyGizmo : MonoBehaviour, IGizmoControl
{
    [Tooltip("Controls when this gizmo will be visible in the Scene View.")]
    public GizmoDrawMode drawMode = GizmoDrawMode.Always;

    public GizmoDrawMode DrawMode => drawMode;

    public void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.0f);
    }
}
```
Once added to a GameObject, the gizmo will appear automatically — no extra setup required.

## GizmoDrawMode Reference

### Available Modes

>| Mode             | Description                                     |
>| ---------------- | ----------------------------------------------- |
>| **None**         | Gizmos are never drawn.                         |
>| **SelectedOnly** | Only visible when the object is selected.       |
>| **NotSelected**  | Visible only when the object is *not* selected. |
>| **Always**       | Always visible in the Scene View.               |

### Implementation Details

The system uses an internal editor class GizmoDrawer that leverages Unity’s `[DrawGizmo]` attribute.
When Unity requests gizmo rendering, the drawer:
1. Receives all components implementing `IGizmoControl`.
2. Checks each one’s current `DrawMode`.
3. Calls `DrawGizmos()` only when the mode matches the current selection context.

This all happens automatically — you don’t need to add or modify editor scripts.

## Advanced Usage

### Custom Gizmo Implementation

Any class implementing IGizmoControl can fully customize its gizmo rendering logic.

``` csharp
using UnityEngine;
using PUCPR.GizmoControl;

public class AdvancedGizmo : MonoBehaviour, IGizmoControl
{
    [Header("Gizmo Settings")]
    public GizmoDrawMode drawMode = GizmoDrawMode.SelectedOnly;
    public Color gizmoColor = Color.cyan;
    public float radius = 2f;

    public GizmoDrawMode DrawMode => drawMode;

    public void DrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawRay(transform.position, transform.forward * radius);
    }
}
```

### Best Practices

1. **Keep It Lightweight**: Gizmos are for visualization, try use minimal or none logic.
2. **Serialize Settings**: Use serialized fields for color, size, etc.
3. **Consistent Modes**: Use SelectedOnly for detailed visuals, and Always for general markers.
4. **Organize by Purpose**: Consider grouping gizmo scripts (e.g., “Spawn Gizmos”, “AI Gizmos”).
5. **Avoid Editor Dependencies**: Implement IGizmoControl in runtime scripts; the editor handles the rest.

## Troubleshooting

### Common Issues

#### Gizmos not appearing
- Ensure your class implements `IGizmoControl` correctly.
- Verify that `DrawMode` is not set to `None`.
- Check that the Scene View’s Gizmos toggle (top toolbar) is enabled.

#### Gizmos not updating when changing modes
- The `DrawMode` property should return the current mode value.
- Make sure you're not overriding the property with a hardcoded value.

#### Gizmos not visible when expected
- Remember that `SelectedOnly` and `NotSelected` behave oppositely.
- If using `Always`, gizmos should appear regardless of selection state.

### FAQ

**Q: Does this affect my build performance?**      
A: No. All editor-related code is wrapped in `#if UNITY_EDITOR` and excluded from builds.

**Q: Can I use this with Unity's built-in Gizmos drawing?**    
A: Yes, you can use both systems together. The Gizmo Control system works alongside Unity's built-in Gizmos system.

**Q: Can I change the draw mode at runtime?**      
A: Yes. Modifying the `drawMode` field will immediately affect when your gizmo is drawn in the editor.

**Q: How do I report a bug or request a feature?**    
A: Please open an issue on the [GitHub repository](https://github.com/CRE-Tools/GizmoControl/issues).
