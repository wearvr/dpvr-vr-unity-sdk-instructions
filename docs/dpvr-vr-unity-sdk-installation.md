
# DPVR Unity SDK Installation

## Importing the unitypackage

The DPVR Unity VR SDK comes as a .unitypackage that you can import into your project via the **Assets › Import Package › Custom Package...** menu option in Unity.

You can <a href="https://users.wearvr.com/developers/devices/dpvr-goblin/resources/vr-unity-package" target="_blank">download the DPVR Unity SDK</a> from WEARVR. You will be asked to create or sign in to your developer account.

<p align="center">
  <img alt="Import the .unitypackage as custom package"  width="500px" src="assets/ImportUnityPackageImage.png">
</p>

This will add a number of directories to your project:

<p align="center">
  <img alt="Files included in the unity package"  width="500px" src="assets/VRSDKAssetsImage.png">
</p>

Delete the existing `MainCamera` from your scene and drag the prefab `DPN/Prefabs/DpnCameraRig.prefab` in to replace it. If necessary, reposition the new camera prefab to where the old one was.

<p align="center">
  <img alt="Drag the DpnCameraRig.prefab into your scene" width="500px" src="assets/DragPrefabIntoScene.png">
</p>

After replacing the Camera the features associated with the original Camera mayr equire some adjustments.

We reccomend using a WorldSpace canvas for UI elemets, as opposed to ScreenSpace. This is standard for most VR platforms, but can sometimes be a concern in poting projects.
