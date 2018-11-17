using AOT;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using UnityEngine;

namespace Tapdaq {
	public class AdManager {
		
		private static AdManager reference;

		public static AdManager instance {
			get {
				if (AdManager.reference == null) {
					AdManager.reference = new AdManager ();
				}
				return AdManager.reference;
			}
		}

		internal AdManager () {}

		private const string unsupportedPlatformMessage = "We support iOS and Android platforms only.";

		#if UNITY_IPHONE
		
		//================================= Interstitials ==================================================
		[DllImport ("__Internal")]
		private static extern void _ConfigureTapdaq(string appIdChar, string clientKeyChar, 
			string enabledAdTypesChar, string testDevicesChar);

		[DllImport ("__Internal")]
		private static extern void _LaunchMediationDebugger();

		// interstitial
		[DllImport ("__Internal")]
		private static extern void _ShowInterstitial();

		[DllImport ("__Internal")]
		private static extern void _ShowInterstitialWithTag(string tag);

		[DllImport ("__Internal")]
		private static extern void _LoadInterstitial();

		[DllImport ("__Internal")]
		private static extern void _LoadInterstitialWithTag(string tag);

		[DllImport ("__Internal")]
		private static extern bool _IsInterstitialReady();

		[DllImport ("__Internal")]
		private static extern bool _IsInterstitialReadyWithTag(string tag);

		// banner
		[DllImport ("__Internal")]
		private static extern void _LoadBannerForSize(string sizeString);

		[DllImport ("__Internal")]
        private static extern void _ShowBanner(string position);

        [DllImport("__Internal")]
        private static extern void _HideBanner();

		[DllImport("__Internal")]
		private static extern bool _IsBannerReady();

		// video
		[DllImport ("__Internal")]
		private static extern void _ShowVideo();

		[DllImport ("__Internal")]
		private static extern void _ShowVideoWithTag (string tag);

		[DllImport("__Internal")]
		private static extern void _LoadVideo();

		[DllImport("__Internal")]
		private static extern void _LoadVideoWithTag(string tag);

		[DllImport("__Internal")]
		private static extern bool _IsVideoReady();

		[DllImport("__Internal")]
		private static extern bool _IsVideoReadyWithTag(string tag);


		// reward video
		[DllImport ("__Internal")]
		private static extern void _ShowRewardedVideo();

		[DllImport ("__Internal")]
		private static extern void _ShowRewardedVideoWithTag (string tag);

		[DllImport ("__Internal")]
		private static extern void _LoadRewardedVideo();

		[DllImport ("__Internal")]
		private static extern void _LoadRewardedVideoWithTag(string tag);

		[DllImport ("__Internal")]
		private static extern bool _IsRewardedVideoReady();

		[DllImport ("__Internal")]
		private static extern bool _IsRewardedVideoReadyWithTag(string tag);


		//================================== Natives =================================================

		[DllImport ("__Internal")]
		public static extern void _LoadNativeAdvertForPlacementTag(string tag, string nativeType);

		[DllImport ("__Internal")]
		public static extern void _LoadNativeAdvertForAdType(string nativeType);

		[DllImport ("__Internal")]
		private static extern System.IntPtr _GetNative(string nativeType); 

		[DllImport ("__Internal")]
		private static extern System.IntPtr _GetNativeAdWithTag (string tag, string nativeAdType);

		[DllImport ("__Internal")]
		private static extern void _SendNativeClick(string uniqueId);

		[DllImport ("__Internal")]
		private static extern void _SendNativeImpression(string uniqueId);

		//////////  Show More Apps

		[DllImport ("__Internal")]
		private static extern void _ShowMoreApps();

		[DllImport ("__Internal")]
		private static extern bool _IsMoreAppsReady();

		[DllImport ("__Internal")]
		private static extern void _LoadMoreApps();

		[DllImport ("__Internal")]
		private static extern void _LoadMoreAppsWithConfig(string config);

		#endif

		#region Class Variables

		private TDSettings settings;

		#endregion

		public static void Init () {
			instance._Init ();
		}

		private void _Init () {
			if (!settings) {
				settings = Resources.LoadAll<TDSettings> ("Tapdaq")[0];
			}

			TDEventHandler.instance.Init ();

			var applicationId = "";
			var clientKey = "";

			#if UNITY_IPHONE
			applicationId = settings.ios_applicationID;
			clientKey = settings.ios_clientKey;
			#elif UNITY_ANDROID
			applicationId = settings.android_applicationID;
			clientKey = settings.android_clientKey;
			#endif

			LogMessage(TDLogSeverity.debug, "TapdaqSDK/Application ID -- " + applicationId);
			LogMessage(TDLogSeverity.debug, "TapdaqSDK/Client Key -- " + clientKey);

			Initialize (applicationId, clientKey);
		}

		private void Initialize (string appID, string clientKey) {
			LogUnsupportedPlatform ();

			LogMessage (TDLogSeverity.debug, "TapdaqSDK/Initializing");
			var adTags = settings.tags.GetTagsJson();
			Debug.Log ("tags:\n" + adTags);

			#if UNITY_IPHONE
			var testDevices = new TestDevicesList (settings.testDevices, TestDeviceType.iOS).ToString ();
			Debug.Log ("testDevices:\n" + testDevices);
			CallIosMethod(() => _ConfigureTapdaq(appID, clientKey, adTags, testDevices));
			#elif UNITY_ANDROID
			RegisterAdapters();
			CallAndroidStaticMethod("InitiateTapdaq", appID, clientKey, adTags);
			#endif
		}

		#region Platform specific method calling

		#if UNITY_IPHONE 

		private static void CallIosMethod(Action action) {
			LogUnsupportedPlatform ();
			if(Application.platform == RuntimePlatform.IPhonePlayer) {
				if(AdManager.instance != null && action != null) {
					action.Invoke();
				}
			}
		}

		#elif UNITY_ANDROID

		private static T GetAndroidStatic<T>(string methodName, params object[] paramList) {
			LogUnsupportedPlatform();
			if(Application.platform == RuntimePlatform.Android) {
				try {
					using (AndroidJavaClass tapdaqUnity = new AndroidJavaClass("com.nerd.TapdaqUnityPlugin.TapdaqUnity")) {
						return tapdaqUnity.CallStatic<T> (methodName, paramList);
					}
				} catch (Exception e) {
					Debug.LogException (e);
				}
			}
			Debug.LogError ("Error while call static method");
			return default(T);
		}
			
		private static void CallAndroidStaticMethod(string methodName, params object[] paramList) {
			CallAndroidStaticMethodFromClass ( "com.nerd.TapdaqUnityPlugin.TapdaqUnity", methodName, true, paramList);
		}

		private static void CallAndroidStaticMethodFromClass(string className, 
			string methodName, bool logException, params object[] paramList) {
			LogUnsupportedPlatform();
			if(Application.platform == RuntimePlatform.Android) {
				try {
					using (AndroidJavaClass androidClass = new AndroidJavaClass(className)) {
						androidClass.CallStatic (methodName, paramList);
					}
				} catch (Exception e) {
					if (logException) {
						Debug.Log ("CallAndroidStaticMethod:  " + methodName + "    FromClass: " 
							+ className + " failed. Message: " + e.Message);
					}
				}
			}
		}

		private void RegisterAdapters() {

			var testDevices = new TestDevicesList (settings.testDevices, TestDeviceType.Android);
			var adMobDevicesJson = testDevices.GetAdMobListJson ();
			var facebookDevicesJson = testDevices.GetFacebookListJson ();

			Debug.Log ("adMobDevicesJson=" + adMobDevicesJson);
			Debug.Log ("facebookDevicesJson=" + facebookDevicesJson);

			foreach (var adapterName in Enum.GetNames(typeof(TapdaqAdapter))) {

				var androidAdapter = TDEnumHelper.FixAndroidAdapterName (adapterName);
				var shortName = androidAdapter.Replace ("Adapter", "");

				var className = "com.tapdaq.tapdaq" + shortName.ToLower () + "." + androidAdapter;

				if (androidAdapter == "FacebookAdapter") {
					CallAndroidStaticMethodFromClass (className, "RegisterAdapter", false, facebookDevicesJson);
				} else if (androidAdapter == "AdMobAdapter") {
					CallAndroidStaticMethodFromClass (className, "RegisterAdapter", false, adMobDevicesJson);
				} else {
					CallAndroidStaticMethodFromClass (className, "RegisterAdapter", false);
				}
			}
		}

		#endif
		#endregion

		private static void LogUnsupportedPlatform() {
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) {
				LogMessage (TDLogSeverity.warning, unsupportedPlatformMessage);
			}
		}

		public void _UnexpectedErrorHandler (string msg) {
			Debug.Log (":: Ad test ::" + msg);
			LogMessage (TDLogSeverity.error, msg);
		}

		public static void LogMessage (TDLogSeverity severity, string message) {
			string prefix = "Tapdaq Unity SDK: ";
			if (severity == TDLogSeverity.warning) {
				Debug.LogWarning (prefix + message);
			} else if (severity == TDLogSeverity.error) {
				Debug.LogError (prefix + message);
			} else {
				Debug.Log (prefix + message);
			}
		}

		public void FetchFailed (string msg) {
			Debug.Log (msg);
			LogMessage (TDLogSeverity.debug, "unable to fetch more ads");
		}

		public static void LaunchMediationDebugger () {
			#if UNITY_IPHONE
			_LaunchMediationDebugger ();
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("ShowMediationDebugger");
			#endif
		}

		// More Apps

		public static void ShowMoreApps () {
			#if UNITY_IPHONE
			CallIosMethod(_ShowMoreApps);
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("ShowMoreApps");
			#endif
		}

		public static bool IsMoreAppsReady () {
			bool ready = false;
			#if UNITY_IPHONE
			CallIosMethod(() => ready = _IsMoreAppsReady());
			#elif UNITY_ANDROID
			ready = GetAndroidStatic<bool>("IsMoreAppsReady");
			#endif
			return ready;
		}

		public static void LoadMoreApps () {
			#if UNITY_IPHONE
			CallIosMethod(_LoadMoreApps);
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("LoadMoreApps", "{}");
			#endif
		}

		public static void LoadMoreAppsWithConfig (TDMoreAppsConfig config) {

			#if UNITY_IPHONE
			CallIosMethod(() => _LoadMoreAppsWithConfig(config.ToString()));
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("LoadMoreApps", config.ToString());
			#endif
		}

		// interstitial
		public static void ShowInterstitial () {
			#if UNITY_IPHONE
			CallIosMethod(_ShowInterstitial);
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("ShowInterstitial");
			#endif
		}

		public static void ShowInterstitial (string tag) {
			#if UNITY_IPHONE
			CallIosMethod(() => _ShowInterstitialWithTag(tag));
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("ShowInterstitialWithTag", tag);
			#endif
		}
			
		public static void LoadInterstitial() {
			#if UNITY_IPHONE
			CallIosMethod(_LoadInterstitial);
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("LoadInterstitial");
			#endif
		}

		public static void LoadInterstitialWithTag(string tag) {
			#if UNITY_IPHONE
			CallIosMethod(() => _LoadInterstitialWithTag(tag));
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("LoadInterstitialWithTag", tag);
			#endif
		}

		public static bool IsInterstitialReady() {
			bool ready = false;
			#if UNITY_IPHONE
			CallIosMethod(() => ready = _IsInterstitialReady());
			#elif UNITY_ANDROID
			ready = GetAndroidStatic<bool>("IsInterstitialReady");
			#endif
			return ready;
		}

		public static bool IsInterstitialReadyWithTag(string tag) {
			bool ready = false;
			#if UNITY_IPHONE
			CallIosMethod(() => ready = _IsInterstitialReadyWithTag(tag));
			#elif UNITY_ANDROID
			ready = GetAndroidStatic<bool>("IsInterstitialReady", tag);
			#endif
			return ready;
		}
			
		// banner

		public static bool IsBannerReady() {
			bool ready = false;
			#if UNITY_IPHONE
			CallIosMethod(() => ready = _IsBannerReady());
			#elif UNITY_ANDROID
			ready = GetAndroidStatic<bool>("IsBannerReady");
			#endif
			return ready;
		}

		public static void RequestBanner (TDMBannerSize size) {
			#if UNITY_IPHONE
			CallIosMethod(() => _LoadBannerForSize(size.ToString()));
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("LoadBannerOfType", size.ToString());
			#endif
		}

		public static void ShowBanner (TDBannerPosition position) {
			#if UNITY_IPHONE
			CallIosMethod(() => _ShowBanner(position.ToString()));
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("ShowBanner", position.ToString());
			#endif
		}

	    public static void HideBanner()
	    {
			#if UNITY_IPHONE
			CallIosMethod(_HideBanner);
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("HideBanner");
			#endif
	    }


		// video

		public static void ShowVideo () {
			#if UNITY_IPHONE
			CallIosMethod(_ShowVideo);
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("ShowVideo");
			#endif
		}

		public static void ShowVideo (string tag) {
			#if UNITY_IPHONE
			CallIosMethod(() => _ShowVideoWithTag (tag));
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("ShowVideoWithTag", tag);
			#endif
		}

		public static void LoadVideo() {
			#if UNITY_IPHONE
			CallIosMethod(() => _LoadVideo ());
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("LoadVideo");
			#endif
		}

		public static void LoadVideoWithTag(string tag) {
			#if UNITY_IPHONE
			CallIosMethod(() => _LoadVideoWithTag (tag));
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("LoadVideoWithTag", tag);
			#endif
		}

		public static bool IsVideoReady() {
			bool ready = false;
			#if UNITY_IPHONE
			CallIosMethod(() => ready = _IsVideoReady());
			#elif UNITY_ANDROID
			ready = GetAndroidStatic<bool>("IsVideoReady");
			#endif
			return ready;
		}

		public static bool IsVideoReadyWithTag(string tag) {
			bool ready = false;
			#if UNITY_IPHONE
			CallIosMethod(() => ready = _IsVideoReadyWithTag(tag));
			#elif UNITY_ANDROID
			ready = GetAndroidStatic<bool>("IsVideoReady", tag);
			#endif
			return ready;
		}


		// rewarded video

		public static void ShowRewardVideo () {
			#if UNITY_IPHONE
			CallIosMethod(_ShowRewardedVideo);
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("ShowRewardAd");
			#endif
		}

		public static void ShowRewardVideo (string tag) {
			#if UNITY_IPHONE
			CallIosMethod(() => _ShowRewardedVideoWithTag (tag));
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("ShowRewardAdWithTag", tag);
			#endif
		}

		public static void LoadRewardedVideo() {
			#if UNITY_IPHONE
			CallIosMethod(() => _LoadRewardedVideo ());
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("LoadRewardAd");
			#endif
		}

		public static void LoadRewardedVideoWithTag(string tag) {
			#if UNITY_IPHONE
			CallIosMethod(() => _LoadRewardedVideoWithTag (tag));
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("LoadRewardAdWithTag", tag);
			#endif
		}

		public static bool IsRewardedVideoReady() {
			bool ready = false;
			#if UNITY_IPHONE
			CallIosMethod(() => ready = _IsRewardedVideoReady());
			#elif UNITY_ANDROID
			ready = GetAndroidStatic<bool>("IsRewardAdReady");
			#endif
			return ready;
		}

		public static bool IsRewardedVideoReadyWithTag(string tag) {
			bool ready = false;
			#if UNITY_IPHONE
			CallIosMethod(() => ready = _IsRewardedVideoReadyWithTag(tag));
			#elif UNITY_ANDROID
			ready = GetAndroidStatic<bool>("IsRewardAdReady", tag);
			#endif
			return ready;
		}


		// native ad

		public static TDNativeAd GetNativeAd (TDNativeAdType adType) {

			var nativeAdJson = "{}";

			#if UNITY_IPHONE
			nativeAdJson = Marshal.PtrToStringAnsi(_GetNative(adType.ToString()));
			#elif UNITY_ANDROID
			nativeAdJson = GetAndroidStatic<string>("GetNativeAd", adType.ToString ());
			#else
			return null;
			#endif

			return TDNativeAd.CreateNativeAd (nativeAdJson);
		}

		public static TDNativeAd GetNativeAd (TDNativeAdType adType, string tag) {

			var nativeAdJson = "{}";

			#if UNITY_IPHONE
			nativeAdJson = Marshal.PtrToStringAnsi(_GetNativeAdWithTag(tag, adType.ToString()));
			#elif UNITY_ANDROID
			nativeAdJson = GetAndroidStatic<string>("GetNativeAdWithTag", adType.ToString (), tag);
			#else
			return null;
			#endif

			return TDNativeAd.CreateNativeAd (nativeAdJson);
		}

		public static void LoadNativeAdvertForTag(string tag, TDNativeAdType nativeType) {
			#if UNITY_IPHONE
			_LoadNativeAdvertForPlacementTag (tag, nativeType.ToString());
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("LoadNativeAd", nativeType.ToString(), tag);
			#endif
		}

		public static void LoadNativeAdvertForAdType(TDNativeAdType nativeType) {
			#if UNITY_IPHONE
			_LoadNativeAdvertForAdType (nativeType.ToString());
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("LoadNativeAd", nativeType.ToString());
			#endif
		}

		public static bool IsNativeAdReady(TDNativeAdType adType) {
			bool ready = false;
			#if UNITY_ANDROID
			ready = GetAndroidStatic<bool>("IsNativeAdReady", adType.ToString());
			#endif
			return ready;
		}

		public static bool IsNativeAdReady(TDNativeAdType adType, string tag) {
			bool ready = false;
			#if UNITY_ANDROID
			ready = GetAndroidStatic<bool>("IsNativeAdReady", adType.ToString(), tag);
			#endif
			return ready;
		}

		public static void SendNativeImpression (TDNativeAd ad) {
			#if UNITY_IPHONE
			CallIosMethod(() => _SendNativeImpression(ad.uniqueId)); // todo change to Id
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("SendNativeImpression", ad.uniqueId); // todo change to Id
			#endif
		}

		public static void SendNativeClick (TDNativeAd ad) {
			#if UNITY_IPHONE
			CallIosMethod(() => _SendNativeClick(ad.uniqueId)); // todo change to Id
			#elif UNITY_ANDROID
			CallAndroidStaticMethod("SendNativeClick", ad.uniqueId); // todo change to Id
			#endif
		}
	}
}