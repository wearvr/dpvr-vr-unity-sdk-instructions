# AndroidManifest.xml file

Your projects `Assets/Plugins/Android/AndroidManifest.xml` file should be replaced by a file contained within the DPVR plugin as it is imported.

### Custom AndroidManifest files (Optional)

If you want to use a self-defined `AndroidManifest.xml` you can make one compatible with DPVR devices by following these steps:

1.  Inherit from class DpvrActivity
If the game inherits from UnityPlayerActivity please change the inheritance to inherit from `com.dpvr.sdk.DpvrActivity`. If the game does not inherit from UnityPlayerActivity please set `com.dpvr.sdk.DpvrActivity` as the main Activity.

2.  Add the following metadata to declare this application as a VR application: `<meta-dataandroid:name="com.softwinner.vr.mode" android:value="vr"/>`

3.  Add a VrListener statement:

` <serviceandroid:name="com.dpvr.aw.vrsdk.VrListener" android:permission="android.permission.BIND_VR_LISTENER_SERVICE"> `
   
`   <intent-filter>`
  
`     <actionandroid:name="android.service.vr.VrListenerService"/> `
  
`   </intent-filter>`
   
` </service>`

### Next: Building to device

See [Building to device](/docs/building-to-dpvr-all-in-one.md)
