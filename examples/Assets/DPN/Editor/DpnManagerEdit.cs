﻿/************************************************************************************

Copyright   :   Copyright 2015-2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace dpn
{
	[InitializeOnLoad]
	class DpnManagerEdit : ScriptableWizard
	{
		private Texture2D logo;

		private static DpnManagerEdit instance;

		private SettingFileInfo settingInfo;

		void OnEnable()
		{
			if (logo == null)
			{
				logo = Resources.Load<Texture2D>("DPN/logo");
			}
			TextAsset file_info = Resources.Load("DPN/DPNUnityConfig") as TextAsset;
			if (!file_info)
			{
				Debug.LogError("DPNUnityConfig file don't exists");
				return;
			}
			if (file_info.text == string.Empty)
			{
				Debug.LogError("DPNUnityConfig file has no data");
				return;
			}
			SettingFileInfo settingFileInfo = JsonUtility.FromJson<SettingFileInfo>(file_info.text);
			settingInfo = settingFileInfo;
		}

		[MenuItem("DPVR/Settings")]
		static void Init()
		{
			instance = DisplayWizard<DpnManagerEdit>("DPVR Settings");
			instance.position = new Rect(150, 150, 400, 312);
			instance.Show();
		}

		void OnGUI()
		{
			var r = EditorGUILayout.BeginVertical(GUILayout.Height(64));
			EditorGUILayout.Space();

			GUI.DrawTexture(r, logo, ScaleMode.ScaleToFit);
			EditorGUILayout.Space();

			EditorGUILayout.EndVertical();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Dpvr Unity Plugin Version: " + DpnManager.DpvrUnityVersion, EditorStyles.boldLabel);
			EditorGUILayout.Space();

			#if !UNITY_ANDROID
			EditorGUILayout.BeginHorizontal();
			settingInfo.pcScreenOutputMode = (dpncOutputMode)EditorGUILayout.EnumPopup("Pc Screen Output Mode", settingInfo.pcScreenOutputMode);
			EditorGUILayout.Space();
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
			#endif

			EditorGUILayout.BeginHorizontal();
			settingInfo.eyeTextureDepth = (TEXTURE_DEPTH)EditorGUILayout.EnumPopup("Eye Texture Depth", settingInfo.eyeTextureDepth);
			EditorGUILayout.Space();
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			settingInfo.worldScale = EditorGUILayout.FloatField("World Scale", settingInfo.worldScale);
			EditorGUILayout.Space();
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			settingInfo.minimumVsync = EditorGUILayout.IntField("Minimum Vsync", settingInfo.minimumVsync);
			EditorGUILayout.Space();
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			settingInfo.resetTrackerOnLoad = EditorGUILayout.Toggle("Reset Tracker On Load", settingInfo.resetTrackerOnLoad);
			EditorGUILayout.Space();
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			#if !UNITY_ANDROID
			settingInfo.pcEyeTextureScale = EditorGUILayout.Slider("Eye Texture Scale", settingInfo.pcEyeTextureScale, 0.1f, 2.0f);
			#else
			settingInfo.mobileEyeTextureScale = EditorGUILayout.Slider("Eye Texture Scale", settingInfo.mobileEyeTextureScale, 0.1f, 2.0f);
			#endif
			EditorGUILayout.Space();
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
            settingInfo.peripheral = (DPVRPeripheral)EditorGUILayout.EnumPopup("Peripheral Support", settingInfo.peripheral);
			EditorGUILayout.Space();
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();

            if (settingInfo.peripheral == DPVRPeripheral.Polaris)
            {
                EditorGUILayout.BeginHorizontal();
                settingInfo.controllerKeyMode = (DPVRKeyMode)EditorGUILayout.EnumPopup("Controller Key Mode", settingInfo.controllerKeyMode);
                EditorGUILayout.Space();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }
            
            #if UNITY_ANDROID
			EditorGUILayout.BeginHorizontal();
			settingInfo.androidEditorUseHmd = EditorGUILayout.Toggle("Android Editor Use Hmd", settingInfo.androidEditorUseHmd);
			EditorGUILayout.Space();
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
            #endif

            EditorGUILayout.BeginHorizontal();
			EditorGUI.HelpBox(new Rect(5, 280, 175, 20), "please apply before playing", MessageType.Info);
			if (GUI.Button(new Rect(205, 280, 95, 20), "apply"))
			{
				SettingFileInfo settingFileInfo = settingInfo;
				string string_json = JsonUtility.ToJson(settingFileInfo);
				FileInfo fileInfo = new FileInfo(Application.dataPath + "/DPN/Resources/DPN/DPNUnityConfig.json");
				using (StreamWriter sw = fileInfo.CreateText())
				{
					sw.WriteLine(string_json);
					sw.Flush();
					sw.Close();
				}
				AssetDatabase.Refresh();
			}
			EditorGUILayout.Space();
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
		}
	}
}
