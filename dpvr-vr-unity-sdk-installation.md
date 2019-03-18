# DPVR Unity SDK Installation

## Importing the unitypackage

The DPVR SDK comes as a .unitypackage that you can import into your project via the **Assets › Import Package › Custom Package...** menu option in Unity.

You can <a href="https://users.wearvr.com/developers/devices/dpvr-all-in-one/resources/vr-unity-package" target="_blank">download the DPVR Unity SDK</a> from WEARVR. You will be asked to create or sign in to your developer account.

<p align="center">
  <img alt="Import the .unitypackage as custom package"  width="500px" src="assets/ImportUnityPackageImage.png">
</p>

This will add a number of directories to your project:

<p align="center">
  <img alt="Files included in the unity package"  width="500px" src="assets/VRSDKAssetsImage.png">
</p>

Delete the existing `MainCamera` from your scene and drag the prefab `/DPN/Prefabs/DpnCameraRig.prefab` in to replace it. If necessary, reposition the new camera prefab to where the old one was.

<p align="center">
  <img alt="Drag the DpnCameraRig.prefab into your scene" width="500px" src="assets/DragPrefabIntoScene.png">
</p>

## Modifying AndroidManifest.xml

The AndroidManifest.xml will be replaced by default as the plugin is imported. However, if you wish to modify AndroidManifest.xml, below are the changes needed to make the manifest run on DPVRAll-in-OneVR devices correctly. 

Please refer to the plugin Assets \Plugins\Android\AndroidManifest.xml. 

1. Inherit from class DpvrActivity
If thegame inherits from UnityPlayerActivity, please change the inheritance to inherit from com.dpvr.sdk.DpvrActivity. If the game does not inherit from UnityPlayerActivity, please set com.dpvr.sdk.DpvrActivity as the main Activity.

2. Add the following meta data to declare this application as a VR application. <meta-dataandroid:name="com.softwinner.vr.mode" android:value="vr"/>

3. Add a VrListener statement
> <serviceandroid:name="com.dpvr.aw.vrsdk.VrListener" 
>   android:permission="android.permission.BIND_VR_LISTENER_SERVICE"> 
>   <intent-filter> 
>     <actionandroid:name="android.service.vr.VrListenerService"/> 
>   </intent-filter> 
> </service>

## Project settings

### Quality Settings

Open **Edit › Project Settings › Quality**. In all Quality levels set VSync to 'Don't Sync'. The DPVR SDK will hande VSync signals internally.

If your application stuggles with performance it is reccomended that 'Rendering > Anti-Aliasing' be set to 'Disabled'.

<p align="center">
  <img alt="Disable VSync for all quality levels"  width="500px" src="assets/DisableVSync.png">
</p>

### Disable the splash screen

The default unity splash image does not display correctly in the headset, rendering a single image across both eye displays.

If you are using the premium version of Unity, it is recommended to disable the splash screen and set the static splash image to a solid black image in **Project Settings**.

<p align="center">
  <img alt="Disable the splash screen" width="500px" src="assets/DisableSplashImage.png">
</p>

### Player Settings

In Edit->Project Settings->Player, under 'Resolution and Presentation' please change 'Default Orientation' to 'Landscape Left'.

In Edit->Project Settings->Player, under 'Android Setting->Other Settings' please enable 'Multithreaded Rendering'.

In Edit->Project Settings->Player, under 'Android Setting->Resolution and Presentation' please enable 'Use 32-bit Display Buffer'. 
> This is a must only for P1, it is optional for M2/M2 Pro or P1 pro.

### Time Settings

Open 'Edit->ProjectSettings->Time' and set the "Fixed Timestep" to 0.01.

### Disable bundled Unity VR SDKs

Depending on the version of Unity you are using, the **Virtual Reality Supported** option can be found in **Other Settings** or **XR Settings**. Make sure it is **NOT** checked to avoid conflicts with the DPVR SDK.

<p align="center">
  <img alt="Uncheck Virtual Reality Supported" width="500px" src="assets/DisabledVirtualRealitySupportImage.png">
</p>

### Next: Camera setup and input

See [Camera & input module setup](/docs/dpvr-vr-camera-setup.md)
