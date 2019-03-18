# Porting considerations

DPVR, as well as the majority of other mobile-vr manufacturers, choose to build their hardware for Android devices. Furthermore many mobile-vr platforms have similar specifications and input layouts. With these facts in mind it shows that DPVR is an ideal platform to port content to and from other similar devices.

Here are some considerations to keep in mind when approaching a new porting project for the DPVR All-In-One.

## Build settings

### Android Manifest
Each VR platform usually requires it's own AndroidManifest file, and often they will come into conflict. If you plan to support multiple platforms on a single project consider [changing the Android Manifest file manually](/docs/android-manifest.md) to support DPVR.

### VR Support

Platforms owned by Valve, Oculus, and Google typically require that 'Player Settings->Other Settings->Virtual Reality Supported' be enabled and have their SDKs enabled through that, whereas other platforms including DPVR should have that setting disabled as they impliment Virtual Reality support in their own way.

If you plan to support a mixture of 'VR Supported' platforms on the same branch it is reccomended that this setting be checked before any builds, espcially as some vr SDKs will enable it at build-time without alerting the developer.

## Controller

### DPVR 'Flip' 3-DOF vs other controllers.

The DPVR controller has a similar form factor and layout to a number of other mobile-vr gamepads. Compared to the Daydream and Pico controllers the DPVR flip controller lacks volume keys, but adds a trigger. This gives it a form overall most comparible to a Pico-Neo or Oculus-Go controller.

By default the 'Back' and 'Home' keys on the Flip controller have reserved functionallity.
*  BACK: The Back key functions the same as the Back key on the Headset.
*  HOME: The Home key returns to launcher, and a long press recenters the device.

In code the DPVR controllers are often reffered to as 'Daydream', or 'DpnDaydreamController'. Despite this they have no relation to the Daydream functions provided by Google SDKs.

## Headset

The DPVR P1 headset features a touch-sensitive button on the side, similar to the touch-pad on a Gear-VR HMD. However, the DPVR device does not track guestures or trackpad-style input like the Gear-VR and instead functions only as a button.

See [Example Scenes](/docs/dpvr-example-scenes-overview.md).
