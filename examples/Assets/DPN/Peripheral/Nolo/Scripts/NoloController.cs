﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dpn
{
    public class NoloController : DpnBasePeripheral
    {
        [Tooltip("If not set, relative to parent")]
        public Transform origin;

        public Transform model;

        public Transform reticlePointer;

        public enum NoloDevice
        {
            Nolo_Hmd = 0,
            Nolo_Left_Controller = 1,
            Nolo_Right_Controller = 2,
        }

        public NoloDevice controllerindex;

        private string[] NoloDeviceName = { "Nolo_Hmd", "Nolo_Left_Controller", "Nolo_Right_Controller" };

#if UNITY_STANDALONE_WIN
        static private DPNP_DEVICE_TYPE[] s_deviceTypes = 
            {
                DPNP_DEVICE_TYPE.DPNP_DEVICE_TYPE_HEAD_TRACKER,
                DPNP_DEVICE_TYPE.DPNP_DEVICE_TYPE_LEFT_CONTROLLER,
                DPNP_DEVICE_TYPE.DPNP_DEVICE_TYPE_RIGHT_CONTROLLER
            };
#endif

        static public NoloController[] _instance = new NoloController[3] {null, null, null};

        private DpnPointerPhysicsRaycaster raycaster;
        private Transform Pointer;

        public static void SetModelActive(bool active) { modelavtive = active; }
        private static bool modelavtive = true;

        private Quaternion rot;
        private Vector3 pos;
        public Quaternion GetPose() { return rot; }
        public Vector3 GetPosition() { return pos; }

        public void OnEnable()
        {
            if ((int)controllerindex < 0 || (int)controllerindex > 2)
            {
                return;
            }
#if UNITY_ANDROID
            if (!OpenPeripheral(NoloDeviceName[(int)controllerindex]))
#else
            DPNP_DEVICE_TYPE type = s_deviceTypes[(int)controllerindex];
            if (!OpenPeripheral(type, 0))
#endif
            {
                Debug.Log("Open Peripheral " + NoloDeviceName[(int)controllerindex] + " fails.");
                return;
            }
            _instance[(int)controllerindex] = this;

            if (DpnManager.DPVRPointer)
            {
                raycaster = this.gameObject.AddComponent<DpnPointerPhysicsRaycaster>();
                raycaster.raycastMode = DpnBasePointerRaycaster.RaycastMode.Camera;
                raycaster.enabled = false;

                Pointer = Instantiate(reticlePointer);
                Pointer.SetParent(this.transform);
                Pointer.transform.localPosition = new Vector3(0.0f, 0.0f, 2.0f);
                Pointer.gameObject.SetActive(false);
            }

#if UNITY_STANDALONE_WIN
            if (peripheral != null)
            {
                int event_mask = (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_A_TRACK | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_A_UNTRACK;
                peripheral.DpnupRegisterEventNotificationFunction(null, event_mask, IntPtr.Zero);
            }
#endif

            CheckVersion();
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }

        void Update()
        {
        }

        bool _useFakeConnected = false;
        public override void DpnpUpdate()
        {
            if (peripheral == null)
            {
                return;
            }
            base.DpnpUpdate();

            if (_useFakeConnected)
                CheckFakeConnnectState();
            else
            {
                if (!isConnected)
                {
                    if (_isValid)
                    {
                        OnDisconnected();
                    }
                    return;
                }
                else
                {
                    if (!_isValid)
                    {
                        OnConnected();
                    }
                }
            }

            float[] temp = NoloGetData();
            if (temp != null && _useFakeConnected)
            {
                peripheral.peripheralstatus.position_state[0][0] -= temp[1];
                peripheral.peripheralstatus.position_state[0][1] -= temp[2];
                peripheral.peripheralstatus.position_state[0][2] -= temp[3];
            }

            if (controllerindex == NoloDevice.Nolo_Left_Controller || controllerindex == NoloDevice.Nolo_Right_Controller)
            {
                _currButtonState = DpnpGetDeviceCurrentStatus().button_state[(int)dpn.DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0];
                _prevButtonState = DpnpGetDevicePrevStatus().button_state[(int)dpn.DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0];
            }

            UpdatePoses();

        }

        private int _currButtonState = 0;
        private int _prevButtonState = 0;

        private DateTime preSystemButtonTime = DateTime.MinValue;

        void UpdatePoses()
        {
            if (controllerindex == NoloDevice.Nolo_Left_Controller || controllerindex == NoloDevice.Nolo_Right_Controller)
            {
                int value = _currButtonState;
                int prev_value = _prevButtonState;
		        if (((prev_value & (int)dpn.DPNP_POLARIS_BUTTONMASK.DPNP_POLARIS_BUTTONMASK_HOME) != 0)
                    && ((value & (int)dpn.DPNP_POLARIS_BUTTONMASK.DPNP_POLARIS_BUTTONMASK_HOME) == 0))
		        {
                    DateTime nowTime = DateTime.Now;
                    if ((nowTime - preSystemButtonTime).Milliseconds < 300)
			        {
				        Debug.Log("DpnnResetPose");
                        preSystemButtonTime = DateTime.MinValue;
                        DpnCameraRig._instance.Recenter();
			        }
			        else
			        {
                        preSystemButtonTime = nowTime;
			        }
		        }
            }

            float[] pose = DpnpGetDeviceCurrentStatus().pose_state[0];
            float[] postion = DpnpGetDeviceCurrentStatus().position_state[0];

            pos = new Vector3(postion[0], postion[1], -postion[2]) * DpnManager.worldScale;
            rot = new Quaternion(pose[1], pose[2], -pose[3], -pose[0]);

            if (origin != null)
            {
                transform.position = origin.transform.TransformPoint(pos);
                transform.rotation = origin.rotation * rot;
            }
            else
            {
                transform.localPosition = pos;
                transform.localRotation = rot;
            }
        }

        Vector3 _prevPos = new Vector3();
        Quaternion _prevRot = new Quaternion();
        DateTime _prevPosUpdateTime = new DateTime();

        void CheckFakeConnnectState()
        {
            if (controllerindex == NoloDevice.Nolo_Hmd)
                return;

            if (1 == DpnpGetDeviceCurrentStatus().device_status)
                return;

            DateTime nowTime = DateTime.Now;
            if (_prevPosUpdateTime.Ticks == 0)
            {
                _prevPosUpdateTime = nowTime;
            }

            int deltaMS = (nowTime - _prevPosUpdateTime).Milliseconds;
            if (_prevPos == pos && _prevRot == rot)
            {
                if (deltaMS > 300 && _isValid)
                {
                    OnDisconnected();
                }
            }
            else
            {
                _prevPosUpdateTime = nowTime;
                _prevPos = pos;
                _prevRot = rot;
                if (!_isValid)
                {
                    OnConnected();
                }
            }
        }

        static void NoloSetData(float h, float r)
        {
            if (NoloController._instance[(int)NoloController.NoloDevice.Nolo_Hmd] != null)
            {
                float[] uuu = {h, r};
                IntPtr temp = Marshal.AllocHGlobal(2 * sizeof(float));
                Marshal.Copy(uuu, 0, temp, 2);
                NoloController._instance[(int)NoloController.NoloDevice.Nolo_Hmd].peripheral.DpnupSetDeviceAttribute(8, temp, 2 * sizeof(float));
            }
        }

        static private float[] configdata = null;
        static public float[] NoloGetData(bool force = false)
        {
            if (force || configdata == null)
            {
                if (NoloController._instance[(int)NoloController.NoloDevice.Nolo_Hmd] != null)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    if (configdata == null)
                    {
                        configdata = new float[5];
                    }
                    IntPtr temp = Marshal.AllocHGlobal(5 * sizeof(float));
                    NoloController._instance[(int)NoloController.NoloDevice.Nolo_Hmd].peripheral.DpnupReadDeviceAttribute(8, temp, 5 * sizeof(float));
                    Marshal.Copy(temp, configdata, 0, 5);
                    return configdata;
# else
                    return configdata;
#endif
                }
                else
                {
                    return configdata;
                }
            }
            else
            {
                return configdata;
            }
        }

        override public void EnableModel(bool enable)
        {
            if(model && model.gameObject)
            {
                model.gameObject.SetActive(enable);
            }
        }

        override public void EnablePointer(bool enabled)
        {
            if (DpnManager.DPVRPointer)
            {
                if (raycaster)
                    raycaster.enabled = enabled;

                if (Pointer && Pointer.gameObject)
                    Pointer.gameObject.SetActive(enabled);

                if (Pointer && enabled)
                    DpnPointerManager.Pointer = (IDpnPointer)Pointer.GetComponent<ReticlePointer>();
            }
        }

        public override void EnableInternalObjects(bool enabled)
        {
            EnablePointer(enabled);
            EnableModel(enabled);
        }

        public override bool BeingUsed()
        {
            if (controllerindex == NoloDevice.Nolo_Hmd)
                return true;

            return _isValid && _currButtonState != 0;
        }

        private void OnDisconnected()
        {
            _currButtonState = 0;
            _prevButtonState = 0;
            _isValid = false;
            SendMessageUpwards("OnControllerDisconnected", this);
        }

        private void OnConnected()
        {
            _currButtonState = 0;
            _prevButtonState = 0;
            _isValid = true;
            _prevPosUpdateTime = new DateTime();
            SendMessageUpwards("OnControllerConnected", this);
        }

        void CheckConnectedState()
        {
            if (!isConnected)
                OnDisconnected();
        }

        private void Start()
        {
            if (peripheral == null)
            {
                OnDisconnected();
                this.gameObject.SetActive(false);
                return;
            }
            CheckConnectedState();
        }

        public override void DpnpResume()
        {
            base.DpnpResume();
            CheckConnectedState();
            _prevPosUpdateTime = new DateTime();
        }

        public bool isConnected
        {
            get
            {
#if UNITY_ANDROID
                if (controllerindex == NoloDevice.Nolo_Hmd)
                    return true;
                int status = DpnpGetDeviceCurrentStatus().device_status;
                return status != 0;
#else
                return (DpnpGetDeviceCurrentStatus().device_status & (int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_BASE_A_TRACK) != 0;
#endif
            }
        }

        void CheckVersion()
        {
#if UNITY_ANDROID && !UNITY_EDITOR

            string PROPERTY_SDK_MANAGER_VERSION = "sdk_manager_version";
            IntPtr property_name = Marshal.StringToHGlobalAnsi(PROPERTY_SDK_MANAGER_VERSION);
            int version = dpn.Composer.DpnuGetIntValue(property_name, 0);
            
            // Support for new firmware starting from sdk manager version 2.1.34
            // hmd : version 2.2
            // controller : version 2.3
            _useFakeConnected = version < ((2 << 16) | (1 << 8) | 34);
#endif
        }

    }
}
