# Alembic for Unity

Use the Alembic package to import and export [Alembic](http://www.alembic.io/) files into your Unity Scenes, where you can playback and record animation directly in Unity. The Alembic format bakes animation data into a file so that you can stream the animation in Unity directly from the file. This saves a significant amount of resources, because the modeling and animating does not happen directly inside Unity.

The Alembic package brings in vertex cache data from a 3D modeling software, such as facial animation (skinning) and cloth simulation (dynamics). When you play it back inside Unity, it looks exactly the same as it did in the 3D modeling software.

>**Important:** before you start using this package, you must be aware of its [known issues and limitations](#known-issues-and-limitations).


## Features

The Alembic package supports these features:

- [Importing](import.md) data from Meshes, Particle Cloud Points, and Cameras.
- Applying [Alembic Shaders](matshad.md#shaders) and [Motion Blur](matshad.md#blur) effects.
- Customizing [particle and point cloud effects](particles.md).
- Playing animation by streaming data through [Timeline](timeline.md) or [Unity Animation](animClip.md).
- Playing Alembic animation [using imported animation clips](time_ImportedClip.md).
- [Exporting](export.md) Unity GameObjects to an Alembic file (through Exporter or Recorder).

> **Note:** If you need to use the Alembic Clip Recorder feature, you must also install the [Unity Recorder](https://docs.unity3d.com/Packages/com.unity.recorder@latest/index.html) package (minimum version: 2.0.0).


## Package technical details

### Installation

To install this package, follow the instructions in the [Package Manager documentation](https://docs.unity3d.com/Manual/upm-ui-install.html).

### Requirements

This version of Alembic for Unity is compatible with the following versions of the Unity Editor:

* 2019.3 and later (recommended)

Alembic for Unity is available on these 64-bit desktop platforms:
* Microsoft® Windows® 10
* macOS® Sierra (10.12)
* GNU/Linux (CentOS 7, Ubuntu 16.x and Ubuntu 17.x)

### Known issues and limitations

* Alembic for Unity **only supports** the following build targets: **Windows 64-bit**, **MacOS X** and **Linux 64-bit**.
* The Alembic format does not support Material import and export.
* Importing meshes with non-convex polygons results in malformed geometry (for example, triangles with flipped normals).
* Adding an FBX GameObject as a child of an Alembic Prefab prevents you from building your project. Instead, Create a new Prefab with both the FBX GameObject and the Alembic Prefab as children.


## Feedback

Tell Unity about your experience using Alembic for Unity on [the Alembic-For-Unity forum](https://forum.unity.com/threads/alembic-for-unity.521649/).
