﻿/************************************************************************************

Copyright   :   Copyright 2015-2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/

using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

namespace dpn
{
	public class DpnCameraRig : DpnBasePeripheralDevicetype
    {
        public Camera _left_eye { get; private set; }
		public Camera _right_eye { get; private set; }
		public Camera _center_eye { get; private set; }
		public Transform _left_transform { get; private set; }
		public Transform _right_transform { get; private set; }
		public Transform _center_transform { get; private set; }
		public Transform _tracker_transform { get; private set; }
		public Transform _tracking_space { get; private set; }

        public static DpnCameraRig _instance = null;

        private Sensor _sensor = new Sensor();
        private bool _freezed = false;
        private bool _monoscopic = false;

        /// <summary>
        /// public functions
        /// </summary>
        public void Freeze(bool enabled) { _freezed = enabled; }
        public bool GetFreezed() { return _freezed; }

        /// <summary>
        /// public functions
        /// </summary>
        public void MonoScopic(bool enabled) { _monoscopic = enabled; }
        public bool GetMonoScopic() { return _monoscopic; }

        private dpnQuarterion pose;
        private dpnVector3 position;
        public Quaternion GetPose() { return pose.ToQuaternion(); }
        public Vector3 GetPosition() { return new Vector3(position.x, position.y, position.z); }

        static private bool VRsupport;
        static private bool CameraRigInit = false;
        
        //Prefab
        public Transform Polaris;
        public Transform Nolo;
        public Transform Flip;
        //public Transform Wisevision;
        public Transform Boundary;
        public Transform reticlePointer;
        private DpnPointerPhysicsRaycaster _noneRaycaster = null;
        private GameObject _nonePointer = null;

		public override void OnEnable ()
        {
            // version adaptation : Unity 2018
#if UNITY_ANDROID && (!UNITY_EDITOR) && UNITY_2018
            InitComponent(this.transform);
            StartCoroutine(InitPeripheral_Coroutine());
#else
            InitPeripheral();
#endif

        }

  
        /// <summary>
        /// public functions
        /// </summary>
        public void Recenter()
        {
            _sensor.RecenterPose();
        }

        /// <summary>
        /// invoked by Unity Engine
        /// disable this camera.
        /// </summary>
        public override void OnDisable ()
		{
            IntPtr tempPtr = Marshal.StringToHGlobalAnsi("OnDisable");
            Composer.DpnuSetIntValue(tempPtr, 1);
            base.OnDisable();
            if (_instance == this)
			{
                _instance = null;
			}
		}

        private bool _initialized = false;

        public bool Init()
        {
            if (_initialized)
            {
                if (VRsupport
#if UNITY_ANDROID && UNITY_EDITOR
                || !DpnManager.androidEditorUseHmd
#endif
                )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            InitComponent(this.transform);
            if (VRsupport
#if UNITY_ANDROID && UNITY_EDITOR
				|| !DpnManager.androidEditorUseHmd
#endif
                )
            {
                _left_eye.enabled = false;
                _right_eye.enabled = false;
                _center_eye.enabled = true;
                return false;
            }
            else
            {
                _left_eye.enabled = true;
                _right_eye.enabled = true;
                _center_eye.enabled = false;
            }
#if UNITY_EDITOR
            _center_eye.enabled = true;
#endif

            Freeze(false);
            MonoScopic(false);

            _initialized = true;
            return true;
        }


        public void InitComponent(Transform parent)
		{
			if( null == _tracking_space )
				_tracking_space = EntityEnsure( parent , "TrackingSpace" );
			
			if( null == _left_transform )
				_left_transform = EntityEnsure( _tracking_space , "LeftEyeAnchor" );
			
			if( null == _right_transform )
				_right_transform = EntityEnsure( _tracking_space , "RightEyeAnchor" );
			
			if( null == _center_transform )
				_center_transform = EntityEnsure( _tracking_space , "CenterEyeAnchor" );
			
			if( null == _tracker_transform )
				_tracker_transform = EntityEnsure( _tracking_space , "TrackerAnchor" );
			
			if (_left_eye == null)
				_left_eye = CameraEnsure( _left_transform, true );
			
			if (_right_eye == null)
				_right_eye = CameraEnsure(_right_transform, true);

			if (_center_eye == null)
				_center_eye = CameraEnsure(_center_transform, false);
        }

        public override void DpnpUpdate()
        {
            base.DpnpUpdate();
            double displayTime = Composer.DpnuGetPredictedDisplayTime(DpnManager.minimumVsync);
            pose = Composer.DpnuGetPredictedPose(displayTime); // 右手螺旋, 左手系, room坐标系 或者 惯性系
            if (DpnManager.peripheral == DPVRPeripheral.Polaris)
            {
                float[] temp_position = DpnpGetDeviceCurrentStatus().position_state[0];
                position = new dpnVector3(temp_position[0], temp_position[1], temp_position[2]);
                position.z = -position.z;
            }
#if UNITY_ANDROID && !UNITY_EDITOR
            else if (DpnManager.peripheral == DPVRPeripheral.Nolo && NoloController._instance[(int)NoloController.NoloDevice.Nolo_Hmd] != null)
            {
                Vector3 temp_position = NoloController._instance[(int)NoloController.NoloDevice.Nolo_Hmd].transform.localPosition;
                position = new dpnVector3(temp_position.x, temp_position.y, temp_position.z);
            }
#endif
            else
            {
                position = Composer.DpnuGetPredictedPosition(displayTime);
                position.z = -position.z;
            }


            position.x = position.x * DpnManager.worldScale;
            position.y = position.y * DpnManager.worldScale;
            position.z = position.z * DpnManager.worldScale;

            Pose posel = _sensor.GetEyePose(dpncEYE.LEFT, pose, position
                                            , DpnManager.DeviceInfo.ipd);
            Pose poser = _sensor.GetEyePose(dpncEYE.RIGHT, pose, position
                                            , DpnManager.DeviceInfo.ipd);

            //After GetEyePose: dpnQuarterion.ToQuaternion, 变成左手螺旋, 左手系, room坐标系 或者 惯性系

            //update eye's render target and transform
            _Update(posel, poser
                    , _monoscopic, _freezed);

            //Unity 使用的是左手螺旋，左手系

            UpdatePeripheral();
        }
        public void UpdatePose()
        {
            Composer.UpdatePose(pose, position);
        }

        public void _Update
            (Pose left_pose, Pose right_pose
             , bool monoscopic
             , bool freezed
             )
        {
            //entities
            if (false == freezed)
            {
                _left_transform.localRotation = left_pose.orientation;
                _right_transform.localRotation = monoscopic ? left_pose.orientation : right_pose.orientation; // using left eye for now
                _center_transform.localRotation = left_pose.orientation;
                _tracker_transform.localRotation = _center_transform.localRotation;

                Vector3 pos = 0.5f * (left_pose.position + right_pose.position);
                _left_transform.localPosition = monoscopic ? pos : left_pose.position;
                _right_transform.localPosition = monoscopic ? pos : right_pose.position;
                _center_transform.localPosition = pos;
                _tracker_transform.localPosition = _center_transform.localPosition;
            }
        }

        public void _UpdateCam(float fovy, float aspect_xdy)
		{
			CameraSetup(_left_eye, PLUGIN_EVENT_TYPE.LeftEyeEndFrame
						, fovy, aspect_xdy);
			CameraSetup(_right_eye, PLUGIN_EVENT_TYPE.RightEyeEndFrame
						, fovy, aspect_xdy);
		}
		
		private static Transform EntityEnsure( Transform parent , string name )
		{
			Transform entity = parent.Find( name );
			if( entity == null )
				entity = new GameObject( name ).transform;
			
			entity.name = name;
			entity.parent = parent;
			entity.localScale = Vector3.one;
			entity.localPosition = Vector3.zero;
			entity.localRotation = Quaternion.identity;
			
			return entity;
		}
		
		private static Camera CameraEnsure( Transform entity, bool render)
		{
			Camera cam = entity.GetComponent<Camera>();
			if( cam == null )
			{
				cam = entity.gameObject.AddComponent<Camera>();
			}

			// AA is documented to have no effect in deferred, but it causes black screens.
			if (cam.actualRenderingPath == RenderingPath.DeferredLighting ||
				cam.actualRenderingPath == RenderingPath.DeferredShading)
			{
				QualitySettings.antiAliasing = 0;
			}

			if (render && cam.GetComponent<DpnPostRender>() == null && !VRsupport)
			{
				cam.gameObject.AddComponent<DpnPostRender>();
			}
			return cam;
		}
		
		private static void CameraSetup
			( Camera cam , PLUGIN_EVENT_TYPE event_type
			 , float fovy , float aspect_xdy)
		{
			
			cam.fieldOfView = fovy;
			cam.aspect = aspect_xdy;
			if (DpnDevice.bVR9)
            {
                if (event_type == PLUGIN_EVENT_TYPE.LeftEyeEndFrame)
                {
                    cam.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
                }
                else
                {
                    cam.rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
                }
            }
            else
            {
                cam.rect = new Rect(0f, 0f, 1f, 1f);
            }
			
			// Enforce camera render order
			cam.depth = (int)event_type;

			//// AA is documented to have no effect in deferred, but it causes black screens.
			//if (cam.actualRenderingPath == RenderingPath.DeferredLighting ||
			//    cam.actualRenderingPath == RenderingPath.DeferredShading)
			//{
			//    QualitySettings.antiAliasing = 0;
			//    DpnManager.instance.eyeTextureAntiAliasing = TEXTURE_ANTIALIASING._1;
			//}
		}

        public override void DpnpPause()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
		Composer.Pause();
#endif
        }

        //triggered by OnApplicationPause and OnApplicationFocus
        public override void DpnpResume()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
	    Composer.Resume();
#endif
            //SetPeripheralFollowSystem();
            StartCoroutine(SetPeripheralFollowSystem_Delay());
        }

        // hmd Peripheral
        private DpnBasePeripheral _hmdPeripheral = null;
        // current Peripheral
        private DpnBasePeripheral _currentPeripheral = null;
        // default Peripheral by setting
        private DpnBasePeripheral _defaultPeripheral = null;

        public void SetPeripheral(DpnBasePeripheral peripheral)
        {
            if (_currentPeripheral)
            {
                _currentPeripheral.EnableInternalObjects(false);
            }

            _currentPeripheral = peripheral;

            if (_currentPeripheral)
            {
                _currentPeripheral.EnableInternalObjects(true);
            }
        }

        static public bool followSystem
        {
            get
            {
                return _instance._interativeType != -1;
            }
        }
        private int _interativeType = -1;

        void Start()
        {
            SetPeripheralFollowSystem();
#if UNITY_ANDROID && (UNITY_5_5_0 || UNITY_5_4_3)
            // In Unity 5.5.0 and Unity 5.4.3,
            // surface is deleted and rebuilt after the first frame is completed by Unity, the second frame will be black screen.
            // So, Skip the rendering of the first frame to avoid flickering.
            StartCoroutine(Coroutine_EnableCamera());
#endif
        }

        int GetInteractiveType()
        {
            if (_defaultPeripheral == null || _defaultPeripheral.peripheral == null)
                return -1;

            IntPtr buffer = Marshal.AllocHGlobal(sizeof(int));
            int ret = _defaultPeripheral.peripheral.DpnupReadDeviceAttribute(DPNP_VALUE_TYPE.DPNP_VALUE_TYPE_ATTRIBUTE_INTERACTIVE_TYPE - DPNP_VALUE_TYPE.DPNP_VALUE_TYPE_ATTRIBUTE
            , buffer, sizeof(int));
            int type = -1;
            if (ret == 1)
            {
                int[] value = new int[1];
                Marshal.Copy(buffer, value, 0, 1);
                type = value[0];
            }

            Marshal.FreeHGlobal(buffer);
            return type;
        }

        void SetInterctiveType(int type)
        {
            if (_defaultPeripheral == null || _defaultPeripheral.peripheral == null)
                return;

            if (type != 0 && type != 1)
                return;

            IntPtr buffer = Marshal.AllocHGlobal(sizeof(int));
            int[] values = new int[1];
            values[0] = type;
            Marshal.Copy(values, 0, buffer, 1);

            _defaultPeripheral.peripheral.DpnupSetDeviceAttribute(DPNP_VALUE_TYPE.DPNP_VALUE_TYPE_ATTRIBUTE_INTERACTIVE_TYPE - DPNP_VALUE_TYPE.DPNP_VALUE_TYPE_ATTRIBUTE
                , buffer, sizeof(int));
            Marshal.FreeHGlobal(buffer);
        }

        private void SetPeripheralFollowSystem()
        {
            _interativeType = GetInteractiveType();
            
            if (_interativeType == 0)
            {
                SetPeripheral(_hmdPeripheral);
            }
            else if (_interativeType == 1)
            {
                SetPeripheral(_defaultPeripheral);
            }
        }

        public override void EnableInternalObjects(bool enabled)
        {
            if (DpnManager.DPVRPointer)
            {
                if (_nonePointer)
                    _nonePointer.SetActive(enabled);
                if (_noneRaycaster)
                    _noneRaycaster.enabled = enabled;

                if (_nonePointer && enabled)
                    DpnPointerManager.Pointer = (IDpnPointer)_nonePointer.GetComponent<ReticlePointer>();
            }
        }

        void UpdatePeripheral()
        {
            if (!DpnCameraRig.followSystem)
                return;

            if (_defaultPeripheral && _defaultPeripheral.BeingUsed())
            {
                // peripheral
                if (_defaultPeripheral != _currentPeripheral)
                {
                    SetPeripheral(_defaultPeripheral);
                    SetInterctiveType(1);
                }
            }
            else
            {
                // touch pad
                int count = Input.touchCount;
                if (count != 0)
                {
                    if (_currentPeripheral != _hmdPeripheral)
                    {
                        SetPeripheral(_hmdPeripheral);
                        SetInterctiveType(0);
                    }
                }
            }
        }

        void OnPeripheralConnected(DpnBasePeripheral peripheral)
        {
            if (DpnCameraRig.followSystem)
                SetPeripheralFollowSystem();
            else
                SetPeripheral(peripheral);
        }

        void OnPeripheralDisconnected(DpnBasePeripheral peripheral)
        {
            if (DpnCameraRig.followSystem)
                SetPeripheralFollowSystem();
            else
                SetPeripheral(_hmdPeripheral);
        }

        IEnumerator SetPeripheralFollowSystem_Delay()
        {
            yield return new WaitForSeconds(1.0f);
            SetPeripheralFollowSystem();
        }

        void EnableCamera(bool enabled)
        {
            if (_left_eye)
                _left_eye.enabled = enabled;

            if (_right_eye)
                _right_eye.enabled = enabled;

            if (_center_eye)
                _center_eye.enabled = enabled;
        }

#if UNITY_ANDROID && (UNITY_5_5_0 || UNITY_5_4_3)

        IEnumerator Coroutine_EnableCamera()
        {
            EnableCamera(false);

            yield return new WaitForEndOfFrame();

            EnableCamera(true);
        }
#endif

        void InitPeripheral()
        {
            if (!CameraRigInit)
            {
#if UNITY_5_3_OR_NEWER || UNITY_5
                VRsupport = UnityEngine.VR.VRSettings.enabled;
#else
			    VRsupport = false;
#endif
            }
            CameraRigInit = true;
            if (!Init())
            {
                return;
            }
            base.OnEnable();
            if (_instance != this && _instance != null)
            {
                Debug.LogWarning("There is another active DpnCameraRig in a scene, set it unactive");
                _instance.gameObject.SetActive(false);
            }
            _instance = this;
            _UpdateCam(DpnManager.DeviceInfo.fov_y
                , DpnManager.DeviceInfo.fov_x / (float)DpnManager.DeviceInfo.fov_y);

            if (DpnManager.DPVRPointer)
            {
                DpnPointerPhysicsRaycaster raycaster = _center_transform.gameObject.AddComponent<DpnPointerPhysicsRaycaster>();
                raycaster.raycastMode = DpnBasePointerRaycaster.RaycastMode.Direct;
                raycaster.enabled = true;

                Transform Pointer = Instantiate(reticlePointer);
                Pointer.SetParent(raycaster.transform);
                Pointer.transform.localPosition = new Vector3(0.0f, 0.0f, 2.0f);
                Pointer.gameObject.SetActive(true);

                _nonePointer = Pointer.gameObject;
                _noneRaycaster = raycaster;
            }
            _hmdPeripheral = this;
            _hmdPeripheral.EnableInternalObjects(false);

            switch (DpnManager.peripheral)
            {
                case DPVRPeripheral.Polaris:
                    {
                        Transform Peripheral = Instantiate(Polaris);
                        Peripheral.parent = this.transform;

                        _defaultPeripheral = Peripheral.GetComponent<DpnMultiControllerPeripheralPolaris>();
                        _defaultPeripheral.EnableInternalObjects(false);
                        break;
                    }
                case DPVRPeripheral.Nolo:
                    {
                        Transform Peripheral = Instantiate(Nolo);
                        Peripheral.parent = this.transform;

                        Transform boundary = Instantiate(Boundary);
                        boundary.parent = this.transform;

                        _defaultPeripheral = Peripheral.GetComponent<DpnMultiControllerPeripheralNolo>();
                        _defaultPeripheral.EnableInternalObjects(false);
                        break;
                    }
                case DPVRPeripheral.Flip:
                    {
                        Transform Peripheral = Instantiate(Flip);
                        Peripheral.parent = this.transform;
                        Peripheral.localPosition = Vector3.zero;

                        Transform controller_right = Peripheral.Find("controller(right)");
                        if (controller_right == null)
                            break;

                        _defaultPeripheral = controller_right.GetComponent<DpnDaydreamController>();
                        _defaultPeripheral.EnableInternalObjects(false);

                        break;
                    }
                case DPVRPeripheral.None:
                    {
                        _defaultPeripheral = _hmdPeripheral;
                        break;
                    }
                default:
                    break;
            }

            SetPeripheral(_defaultPeripheral);
        }

#if UNITY_ANDROID && (!UNITY_EDITOR) && UNITY_2018 

        IEnumerator InitPeripheral_Coroutine()
        {
            // Wait two frames until the Unity3d initialization is complete.
            EnableCamera(false);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            EnableCamera(true);

            InitPeripheral();

            SetPeripheralFollowSystem();
        }

#endif

        static public Vector3 WorldToScreenPoint(Vector3 position)
        {
            if (_instance && _instance._center_eye)
                return _instance._center_eye.WorldToScreenPoint(position);
            else
                return Vector3.zero;
        }
    }

}
