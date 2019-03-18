# DPVR Unity VR SDK example project

This example project demonstrates how to integrate with the VR SDK and respond to input signals from the headset and controller.

## Installation

Clone this repository to your hard drive

```
git clone https://github.com/wearvr/dpvr-vr-unity-sdk-instructions
```

Open this folder in Unity as a new project.

Once in Unity, open the `Assets\Demo` scene.

![Example project](ExampleProject.png)

## Overview

The project contains the following implementations:

##### GameObjects

* **GOUsingTouchInterface:** How to use touchpad to get guesture inputs
* **GOUsingPointerHandlerInterface:** How to use the [Pointer*Handler](https://docs.unity3d.com/ScriptReference/EventSystems.IPointerClickHandler.html) interfaces to make GameObjects respond to headset or controller inputs
* **GOUsingButtonHandlerInterface:** How to make GameObjects respond to non-pointer button and controller inputs

##### Input system

An example of how to recive inputs from the DPVR device, including key presses and gesture events.

##### Event system

An example of the DPVR event system and prefab set up to work with the flip controller on the DPVR All-In-One P1 device.

To run the example project, download the zip below, extract it and open it with a compatible version of Unity.

## Running the example

If you want to run the example on the device, set the correct [build settings](/docs/building-to-dpvr-all-in-one.md) first.


