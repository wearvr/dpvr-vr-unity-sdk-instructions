# API overview

> Instuctions relevant for DPVR SDK version 0.7.4

Sections:
* Static classes
* Mobile bluetooth controller - DpnDaydreamController

## (1) Static classes

#### DpnDaydreamController
| Name | public static DpnDaydreamController DpnDaydreamController |
| :---: | :----: |
| Functionality | Entry point for the contorller API |
| Parameters | None |
| Returns | Static instance of the controller API |
| Call Method | DpnDaydreamController |

## (2) Mobile bluetooth controller - DpnDaydreamController

#### State
| Name | public DpnConnectionState State |
| :---: | :----: |
| Functionality | Returns the controller's current connection state |
| Parameters | None |
| Returns | DpnConnectionState enumerator |
| Call Method | DpnDaydreamController.State; |

> DpnConnectionState values: Error -1, Disconnected 0, Scanning 1, Connecting 2, Connected 3, Bond 4, Unbond 5

#### ApiStatus
| Name | public DpnControllerApiStatus ApiStatus |
| :---: | :----: |
| Functionality | Returns the API status of the current controller state. |
| Parameters | None |
| Returns | DpnControllerApiStatus enumerator |
| Call Method | DpnDaydreamController.ApiStatus; |

> DpnControllerApiStatus values: Error -1, Ok 0, Unsupported 1, NotAuthorized 2, Unavailable 3, ApiServiceObsolete 4, ApiClientObsolete 5, ApiMalfunction 6

#### Orientation
| Name | public Quaternion Orientation |
| :---: | :----: |
| Functionality | Returns the controllers rotation. |
| Parameters | None |
| Returns | The controllers current orientation as a Quaternion, in Unity space |
| Call Method | DpnDaydreamController.Orientation; |

#### Gyro
| Name | public Vector3 Gyro |
| :---: | :----: |
| Functionality | Returns the contorllers gyroscope reading. |
| Parameters | None |
| Returns | The controllers current gyroscope reading, as right hand rule |
| Call Method | DpnDaydreamController.Gyro; |

#### Accel
| Name | public Vector3 Accel |
| :---: | :----: |
| Functionality | Returns the contorllers acceleromiter reading. |
| Parameters | None |
| Returns | The controllers current acceleromiter reading, as right hand rule in meters per second squared |
| Call Method | DpnDaydreamController.Gyro; |

> The acceleromiter for the DPVR Daydrea/Flip controller includes forces excerted by gravity. Unless the controller is in freefall or in a zero gravity enviroment it will read 9.8 m/s^2 on the Y axis.

#### TouchGestureUp
| Name | public bool TouchGestureUp () |
| :---: | :----: |
| Functionality | Acquire touchpad swipe direction |
| Parameters | None |
| Returns | Did swipe this frame |
| Call Method | DpnDaydreamController.TouchGestureUp; |

> Similar functions exist for each cardinal direction. (TouchGestureUp, TouchGestureLeft, TouchGestureDown, TouchGestureRight)


## Next: Device user interface

See [Device user interface](/docs/device-user-interface-guide.md).
