# DPVR P1 headset and controller input

> This guide assumes that input will match the 'P1' brand of headsets as well as their associated 3-DOF bluetooth controllers. More specific support for other headsets models will be provided as we gain the ability to supply them.

Once you have [installed the DPVR Unity SDK](/docs/dpvr-vr-unity-sdk-installation.md), you can begin to bind to button events from the headset and controller.

## DPVR All-In-One P1 & Flip controller 

#### P1 Headset buttons

The DPVR P1 headset is a 3 degrees of freedom headset and allows for basic gaze-based pointer input and some additional buttons for operating system functionality.

<p align="center">
  <img alt="P1 button positions" width="500px" src="assets/DPVR-P1-Buttons.svg">
</p>

#### 3-DOF controller

Each headset is compatible with a bluetooth controller. The controller supports 3 degrees of freedom and provides more opportunities for the user to interact with virtual reality experiences.

Integrating with the controller is an **optional** step, but is highly encouraged for experiences that can make use of additional forms of input.

<p align="center">
  <img alt="Controller button positions" width="500px" src="assets/FlipDiagram.svg">
</p>

## Setting up Controller support in Unity

To allow a project to use the DPVR controller some steps must first be taken.

1.  If it's not done already 





