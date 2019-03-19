# DPVR P1 headset and controller input

> This guide assumes that input will match the 'P1' brand of headsets as well as their associated 3-DOF Bluetooth controllers. More specific support for other headsets models will be provided as we gain the ability to supply them.

> This guide also assumes that the latest version of the DPVRUnity plugin (0.7.4) is used, as some content referenced on this page does not exist on legacy versions of the plugin.

Once you have [installed the DPVR Unity SDK](/docs/dpvr-vr-unity-sdk-installation.md), you can begin to bind to button events from the headset and controller.

## DPVR All-In-One P1 & Flip controller 

#### P1 Headset buttons

The DPVR P1 headset is a 3 degrees of freedom headset and allows for basic gaze-based pointer input and some additional buttons for operating system functionality.

<p align="center">
  <img alt="P1 button positions" width="500px" src="assets/DPVR P1 Labeled.svg">
</p>

HMD Touchpad.

A: HMD Action button.
> Maps to Input.touchCount and Input.GetMouseButton(0)

<p align="center">
  <img alt="P1 button positions" width="500px" src="assets/DPVR P1 Labeled Underside.svg">
</p>

System.

A: Back Button

B: SD-Card Slot

C: Volume + -

D: Power

E: Micro USB dock

F: 3.5mm Headphone Jack

#### 3-DOF controller

Each headset is compatible with a Bluetooth controller. The controller supports 3 degrees of freedom and provides more opportunities for the user to interact with virtual reality experiences.

Integrating with the controller is an **optional** step, but is highly encouraged for experiences that can make use of additional forms of input.

<p align="center">
  <img alt="Controller button positions" width="500px" src="assets/DPVR P1 Controller side Labelled.svg">
</p>

Trigger.

A: Trigger Button
> Maps to DpnDaydreamController.TriggerButton;

<p align="center">
  <img alt="Controller button positions" width="500px" src="assets/DPVR P1 Controller Labelled.svg">
</p>

Face Buttons.

A: Click Button
> Maps to DpnDaydreamController.ClickButton;

B: Power Indicator

C: Back Button
> Intended for system use, Avoid mapping to this key.

D: Home Button
> Intended for system use, Avoid mapping to this key.

## Setting up Controller support in Unity

To allow a project to use the DPVR controller some steps must first be taken.

1.  If it's not already installed, [Install the DPVR SDK.](/docs/dpvr-vr-unity-sdk-installation.md)

2.  In the menu at 'DPVR->Settings' set the 'Peripheral Support' option to 'Flip'.

No other steps are necessary. The DpnCameraRig prefab will generate all controller related prefabs, including code and models.

<p align="center">
  <img alt="Enabling flip controller" width="500px" src="assets/FlipWindow.svg">
</p>

## Setting up in-Editor Input

By default the DpnCameraRig prefab does not allow input or interaction inside Unity Editor mode, which can make it difficult to develop and test for. This can be somewhat mitigated by attaching the script 'DpnAuxiliaryMover.cs' that comes included in the DPVR plugin onto the scenes 'DpnCameraRig' prefab.

The script does not cause issues on device, and as a time saving measure it's recommended to simply apply this change onto the prefab itself rather than onto each instance of DpnCameraRig in scene hierarchy.

To use the Auxiliary Mover script hold the 'Alt' and 'Right Mouse' buttons to enable looking around with the mouse.

### Next: Setting up AndroidManifest.xml

See [Android Manifest](/docs/android-manifest.md)
