using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace Tapdaq {
	[CustomEditor (typeof(TDSettings))]
    public class TDSettingsEditor : UnityEditor.Editor
    {
		private TDSettings settings;

		private bool showOther = false;
		private bool show1x1 = false;
		private bool show1x2 = false;
		private bool show2x1 = false;
		private bool show2x3 = false;
		private bool show3x2 = false;
		private bool show1x5 = false;
		private bool show5x1 = false;

		private bool showTestDevices = false;

		private GUIStyle foldoutStyle;

		private List<TestDevice> deviceToDelete = new List<TestDevice>();

	//	private Texture whiteTexture = new Texture ();

		void OnEnable () {
			settings = (TDSettings)target;
		}

		public override void OnInspectorGUI () {
			foldoutStyle = EditorStyles.foldout;
			foldoutStyle.fontStyle = FontStyle.Bold;

			serializedObject.Update ();

			GUILayout.Label ("You must have an App ID and Client Key to use Tapdaq", EditorStyles.boldLabel);
		
			if (GUILayout.Button ("Visit Tapdaq.com")) {
				Application.OpenURL ("https://tapdaq.com/dashboard/apps");
			}
		
			GUILayout.Label ("Ad Settings", EditorStyles.boldLabel);
		
			settings.ios_applicationID = EditorGUILayout.TextField ("iOS Application ID", settings.ios_applicationID);
			settings.ios_clientKey = EditorGUILayout.TextField ("iOS Client Key", settings.ios_clientKey);

			GUILayout.Space(20);

			settings.android_applicationID = EditorGUILayout.TextField ("Android Application ID", settings.android_applicationID);
			settings.android_clientKey = EditorGUILayout.TextField ("Android Client Key", settings.android_clientKey);

			GUILayout.Space (20);

			ShowTestDevices ();

			GUILayout.Space(15);

			DrawTagGroups ();

			if (GUI.changed)
				EditorUtility.SetDirty (settings);
		}

		private void ShowTestDevices() {

			showTestDevices = EditorGUILayout.Foldout (showTestDevices, "Test devices", foldoutStyle);

			deviceToDelete.Clear ();

			if (showTestDevices) {
				foreach (var device in settings.testDevices) {
					ShowTestDevice (device);
				}

				ShowAddTestDeviceView ();
			}

			foreach (var device in deviceToDelete) {
				settings.testDevices.Remove (device);
			}
		}

		string newTestDeviceName = "";
		TestDeviceType newTestDeviceType = TestDeviceType.Android;

		private void ShowAddTestDeviceView() {
			
			GUIStyle labelStyle = new GUIStyle(EditorStyles.boldLabel);

			GUILayout.Label ("Add new device.", labelStyle, GUILayout.Width (200));

			newTestDeviceName = EditorGUILayout.TextField ("Name:", newTestDeviceName);
			newTestDeviceType = (TestDeviceType)EditorGUILayout.EnumPopup ("Type:", newTestDeviceType);

			ShowButton ("Add", 90, Color.green, () => {
				if (!string.IsNullOrEmpty (newTestDeviceName)) {
					var testDevice = new TestDevice (newTestDeviceName, newTestDeviceType);
					settings.testDevices.Add (testDevice);
					newTestDeviceName = "";
				}
			});

			DrawSeparator (2);
		}

		private void ShowDeleteDialog(TestDevice device) {
			if (EditorUtility.DisplayDialog ("Delete Test Device", 
				"Are you sure you want to delete test device " + device.name + "?", 
				"Delete", "Cancel")) {

				deviceToDelete.Add (device);
			}
		}

		private void ShowTestDevice(TestDevice device) {

			GUIStyle labelStyle = new GUIStyle(EditorStyles.boldLabel);
			var isAndroid = device.type == TestDeviceType.Android;
			labelStyle.normal.textColor = isAndroid ? new Color (0, 0.3f, 0) : new Color (0, 0, 0.3f);

			GUILayout.Label (device.name + " (" + device.type.ToString () + ")", labelStyle, GUILayout.Width(250));

			device.adMobId = EditorGUILayout.TextField ("AdMob ID:", device.adMobId);
			device.facebookId = EditorGUILayout.TextField ("Facebook ID:", device.facebookId);

			ShowButton ("Delete", 50, new Color (0.85f, 0.15f, 0), () => {
				ShowDeleteDialog (device);
			});

			DrawSeparator (2);
		}

		private void DrawTagGroups () {

			GUILayout.Label ("Ad Types Tags", EditorStyles.boldLabel);
			GUILayout.Label ("--Interstitials & Video Ads--", EditorStyles.boldLabel);

			showOther = DrawBlock(showOther, "Interstitials & Video Ads",
				TDAdType.TDAdTypeInterstitial, TDAdType.TDAdTypeVideo, TDAdType.TDAdTypeRewardedVideo);

			GUILayout.Label ("--Natives--", EditorStyles.boldLabel);

			show1x1 = DrawBlock (show1x1, "1x1",
				TDAdType.TDAdType1x1Large, TDAdType.TDAdType1x1Medium, TDAdType.TDAdType1x1Small);

			show1x2 = DrawBlock (show1x2, "1x2", 
				TDAdType.TDAdType1x2Large, TDAdType.TDAdType1x2Medium, TDAdType.TDAdType1x2Small);

			show2x1 = DrawBlock (show2x1, "2x1", 
				TDAdType.TDAdType2x1Large, TDAdType.TDAdType2x1Medium, TDAdType.TDAdType2x1Small);

			show2x3 = DrawBlock (show2x3, "2x3", 
				TDAdType.TDAdType2x3Large, TDAdType.TDAdType2x3Medium, TDAdType.TDAdType2x3Small);

			show3x2 = DrawBlock (show3x2, "3x2", 
				TDAdType.TDAdType3x2Large, TDAdType.TDAdType3x2Medium, TDAdType.TDAdType3x2Small);

			show1x5 = DrawBlock (show1x5, "1x5", 
				TDAdType.TDAdType1x5Large, TDAdType.TDAdType1x5Medium, TDAdType.TDAdType1x5Small);

			show5x1 = DrawBlock (show5x1, "5x1",
				TDAdType.TDAdType5x1Large, TDAdType.TDAdType5x1Medium, TDAdType.TDAdType5x1Small);
		}

		private bool DrawBlock(bool show, string blockName, params TDAdType[] types) {

			DrawSeparator (3);

			show = EditorGUILayout.Foldout (show, blockName, foldoutStyle);

			if (show) {
				foreach (var t in types) {
					var index = (int)t;
					var tags = settings.tags.tags;

					tags [index] = EditorGUILayout.TextField (t.ToString (), tags [index]);
				}
			}

			return show;
		}

		private void DrawSeparator(int height) {
		//	GUILayout.Box (whiteTexture, GUILayout.ExpandWidth (true), GUILayout.Height (height));
		}

		private void ShowButton(string text, int width, Color color, Action action) {
			GUI.backgroundColor = color;
			if (GUILayout.Button (text, GUILayout.Width (width))) {
				if (action != null)
					action.Invoke ();
			}
			GUI.backgroundColor = Color.white;
		}
	}
}