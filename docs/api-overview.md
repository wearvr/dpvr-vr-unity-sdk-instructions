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

#### TouchDown
| Name | public static bool TouchDown |
| :---: | :----: |
| Functionality | Acquire if touchpad was just touched this frame |
| Parameters | None |
| Returns | Is the user is currently touching the controller's touchpad but was not last frame |
| Call Method | DpnDaydreamController.TouchDown; |

#### TouchUp
| Name | public static bool TouchUp |
| :---: | :----: |
| Functionality | Acquire if touchpad was just released this frame |
| Parameters | None |
| Returns | Is the user is not currently touching the controller's touchpad but was last frame |
| Call Method | DpnDaydreamController.TouchUp; |

#### TouchPos
| Name | public static Vector2 TouchPos |
| :---: | :----: |
| Functionality | Get touch position |
| Parameters | None |
| Returns | Touch position |
| Call Method | DpnDaydreamController.TouchPos; |

#### TouchPosUp
| Name | public static Vector2 TouchPosUp |
| :---: | :----: |
| Functionality | Get touch position |
| Parameters | None |
| Returns | Touch position |
| Call Method | DpnDaydreamController.TouchPosUp; |

#### Recentering
| Name | public static bool Recentering |
| :---: | :----: |
| Functionality | Used to tell if the user is performing a recentre. Many apps will want to stop interactions if this is the case |
| Parameters | None |
| Returns | True if user is recentering |
| Call Method | DpnDaydreamController.Recentering; |

#### TouchGestureUp
| Name | public static bool TouchGestureUp |
| :---: | :----: |
| Functionality | Acquire touchpad swipe direction |
| Parameters | None |
| Returns | Did swipe this frame |
| Call Method | DpnDaydreamController.TouchGestureUp; |

> Similar functions exist for each cardinal direction. (TouchGestureUp, TouchGestureLeft, TouchGestureDown, TouchGestureRight)


## Next: Device user interface

See [Device user interface](/docs/device-user-interface-guide.md).
