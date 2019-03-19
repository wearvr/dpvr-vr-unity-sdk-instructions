# API overview

> Instuctions relevant for DPVR SDK version 0.7.4

Sections:
* Static classes
* Mobile bluetooth controller - DpnDaydreamController
* Camera Rig - DpnCameraRig

## (1) Static classes

#### DpnDaydreamController
| Name | public static DpnDaydreamController DpnDaydreamController |
| :---: | :----: |
| Functionality | Entry point for the contorller API |
| Parameters | None |
| Returns | Static instance of the controller API |
| Call Method | DpnDaydreamController |

#### DpnCameraRig
| Name | public static DpnCameraRig DpnCameraRig |
| :---: | :----: |
| Functionality | Entry point for the camera rig, allowing access to information via it's '\_instance' property |
| Parameters | None |
| Returns | Static instance of the DpnCameraRig |
| Call Method | DpnCameraRig |

## (2) Mobile bluetooth controller - DpnDaydreamController

#### State
| Name | public static DpnConnectionState State |
| :---: | :----: |
| Functionality | Returns the controller's current connection state |
| Parameters | None |
| Returns | DpnConnectionState enumerator |
| Call Method | DpnDaydreamController.State; |

> DpnConnectionState values: Error -1, Disconnected 0, Scanning 1, Connecting 2, Connected 3, Bond 4, Unbond 5

#### ApiStatus
| Name | public static DpnControllerApiStatus ApiStatus |
| :---: | :----: |
| Functionality | Returns the API status of the current controller state. |
| Parameters | None |
| Returns | DpnControllerApiStatus enumerator |
| Call Method | DpnDaydreamController.ApiStatus; |

> DpnControllerApiStatus values: Error -1, Ok 0, Unsupported 1, NotAuthorized 2, Unavailable 3, ApiServiceObsolete 4, ApiClientObsolete 5, ApiMalfunction 6

#### Orientation
| Name | public static Quaternion Orientation |
| :---: | :----: |
| Functionality | Returns the controllers rotation. |
| Parameters | None |
| Returns | The controllers current orientation as a Quaternion, in Unity space |
| Call Method | DpnDaydreamController.Orientation; |

#### Gyro
| Name | public static Vector3 Gyro |
| :---: | :----: |
| Functionality | Returns the contorllers gyroscope reading. |
| Parameters | None |
| Returns | The controllers current gyroscope reading, as right hand rule |
| Call Method | DpnDaydreamController.Gyro; |

#### Accel
| Name | public static Vector3 Accel |
| :---: | :----: |
| Functionality | Returns the contorllers acceleromiter reading. |
| Parameters | None |
| Returns | The controllers current acceleromiter reading, as right hand rule in meters per second squared |
| Call Method | DpnDaydreamController.Gyro; |

> The acceleromiter for the DPVR Daydream/Flip controller includes forces exerted by gravity. Unless the controller is in freefall or in a zero gravity enviroment it will read 9.8 m/s^2 on the Y axis.

#### IsTouching
| Name | public static bool IsTouching |
| :---: | :----: |
| Functionality | Acquire if touchpad is touched |
| Parameters | None |
| Returns | Is the user is currently touching the controller's touchpad |
| Call Method | DpnDaydreamController.IsTouching; |

> For on frame down, use: TouchDown

> For on frame released, use: TouchUp

#### TouchPos
| Name | public static Vector2 TouchPos |
| :---: | :----: |
| Functionality | Get touch position |
| Parameters | None |
| Returns | Touch position |
| Call Method | DpnDaydreamController.TouchPos; |

> For on frame released, use: TouchPosUp

#### Recentering
| Name | public static bool Recentering |
| :---: | :----: |
| Functionality | Used to tell if the user is performing a recentre. Many apps will want to stop interactions if this is the case |
| Parameters | None |
| Returns | True if user is recentering |
| Call Method | DpnDaydreamController.Recentering; |

> For on recentre completed, use: Recentered

#### ClickButton
| Name | public static bool ClickButton |
| :---: | :----: |
| Functionality | Retrive the state of the touchpad button |
| Parameters | None |
| Returns | If button is currently down |
| Call Method | DpnDaydreamController.ClickButton; |

> For on frame down, use: ClickButtonDown

> For on frame released, use: ClickButtonUp

#### TriggerButton
| Name | public static bool TriggerButton |
| :---: | :----: |
| Functionality | Retrive the state of the trigger |
| Parameters | None |
| Returns | If trigger is currently down |
| Call Method | DpnDaydreamController.TriggerButton; |

> For on frame down, use: TriggerButtonDown

> For on frame released, use: TriggerButtonUp

#### volumeUpButton
| Name | public static bool volumeUpButton |
| :---: | :----: |
| Functionality | Retrive the state of the volume up button |
| Parameters | None |
| Returns | If volume up button is currently down |
| Call Method | DpnDaydreamController.volumeUpButton; |

> For on frame down, use: volumeUpButtonDown

> For on frame released, use: volumeUpButtonUp

#### volumeDownButton
| Name | public static bool volumeDownButton |
| :---: | :----: |
| Functionality | Retrive the state of the volume down button |
| Parameters | None |
| Returns | If volume down button is currently down |
| Call Method | DpnDaydreamController.volumeDownButton; |

> For on frame down, use: volumeDownButtonDown

> For on frame released, use: volumeDownButtonUp

#### ErrorDetails
| Name | public static string ErrorDetails |
| :---: | :----: |
| Functionality | If State == DpnConnectionState.Error, this contains details about the error. |
| Parameters | None |
| Returns | Error string |
| Call Method | DpnDaydreamController.ErrorDetails; |

#### TouchGestureUp
| Name | public static bool TouchGestureUp |
| :---: | :----: |
| Functionality | Acquire touchpad swipe direction |
| Parameters | None |
| Returns | Did swipe this frame |
| Call Method | DpnDaydreamController.TouchGestureUp; |

> Similar functions exist for each cardinal direction. (TouchGestureUp, TouchGestureLeft, TouchGestureDown, TouchGestureRight)

#### BackButton
| Name | public static bool BackButton |
| :---: | :----: |
| Functionality | Retrive the state of the back button |
| Parameters | None |
| Returns | If back button is currently down |
| Call Method | DpnDaydreamController.BackButton; |

> For on frame down, use: BackButtonDown

> For on frame released, use: BackButtonUp

#### interactiveHand
| Name | public static int interactiveHand |
| :---: | :----: |
| Functionality | Get/Set propery of the interactive hand index |
| Parameters | int of 0 or 1 |
| Returns | int of 0 or 1 |
| Call Method | DpnDaydreamController.interactiveHand; |

> interactiveHand: 0 = right, 1 = left

## (3) Camera Rig - DpnCameraRig

#### \_center_eye
| Name | public Camera \_center_eye |
| :---: | :----: |
| Functionality | public Get propery of a HMD camera |
| Parameters | can only be set privately |
| Returns | eye camera |
| Call Method | DpnCameraRig.\_instance.\_center_eye; |

> An internal value, but accessible and useful in other classes.

> Has similar properties for left and right eyes. \_left_eye, \_right_eye.

#### \_center_transform
| Name | public Transform \_center_transform |
| :---: | :----: |
| Functionality | public Get propery of a HMD camera Transform |
| Parameters | can only be set privately |
| Returns | eye transform |
| Call Method | DpnCameraRig.\_instance.\_center_transform; |

> An internal value, but accessible and useful in other classes.

> Has similar properties for left and right eyes. \_left_transform, \_right_transform.

| Name | public void Freeze(bool enabled) |
| :---: | :----: |
| Functionality | Freeze camera rig |
| Parameters | enable or disable freeze |
| Returns | None |
| Call Method | DpnCameraRig.\_instance.Freeze(bool); |

> Value for Freeze() can be gotten with GetFreezed()

| Name | public void MonoScopic(bool enabled) |
| :---: | :----: |
| Functionality | set MonoScopic rendering |
| Parameters | enable or disable MonoScopic rendering |
| Returns | None |
| Call Method | DpnCameraRig.\_instance.MonoScopic(bool); |

> Value for MonoScopic() can be gotten with GetMonoScopic()

| Name | public void GetPose() |
| :---: | :----: |
| Functionality | Get pose as Quaternion |
| Parameters | None |
| Returns | Pose as Quaternion |
| Call Method | DpnCameraRig.\_instance.GetPose(); |

| Name | public void GetPosition() |
| :---: | :----: |
| Functionality | Get position as Vector3 |
| Parameters | None |
| Returns | position as Vector3 |
| Call Method | DpnCameraRig.\_instance.GetPosition(); |

| Name | public void Recenter() |
| :---: | :----: |
| Functionality | Force pose recentre |
| Parameters | None |
| Returns | None |
| Call Method | DpnCameraRig.\_instance.Recenter(); |

| Name | public Vector3 WorldToScreenPoint(Vector3 position) |
| :---: | :----: |
| Functionality | Get world space to screen position, relative to centre eye |
| Parameters | Vector3 world space position |
| Returns | Returns screen space position of a world postion |
| Call Method | DpnCameraRig.\_instance.WorldToScreenPoint(Vector3); |

## Next: Device user interface

See [Device user interface](/docs/device-user-interface-guide.md).
