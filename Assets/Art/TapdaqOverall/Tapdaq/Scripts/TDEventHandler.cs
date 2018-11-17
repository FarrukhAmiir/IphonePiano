using UnityEngine;
using Newtonsoft.Json;

namespace Tapdaq {
	public class TDEventHandler : MonoBehaviour {
		private static TDEventHandler reference;

		public static TDEventHandler instance {
			get {
				if (!reference) {
					TDEventHandler[] references = FindObjectsOfType<TDEventHandler> ();

					if (references.Length > 0) {
						reference = references [0]; 
					} else {
						reference = new GameObject ("TapdaqV1").AddComponent<TDEventHandler> ();
						Debug.Log (":: Ad test ::" + "Spawned Event Handler");
					}
				}

				return reference;
			}
		}

		private void Awake () {
			DontDestroyOnLoad (gameObject);
		}

		// Currently just used as an empty call to make a TDEventHandler GameObject
		public void Init () {

		}

		void _didLoadConfig(string message) {
			Debug.LogWarning ("_didLoadConfig");
			TDCallbacks.instance.OnTapdaqConfigLoaded ();
		}
			
		void _didLoad (string jsonMessage) {
			Debug.Log("_didLoad " + jsonMessage);
			var adEvent = JsonConvert.DeserializeObject<TDAdEvent> (jsonMessage);
			TDCallbacks.instance.OnAdAvailable (adEvent);
		}

		void _didFailToLoad (string jsonMessage) {
			Debug.Log("_didFailToLoad " + jsonMessage);
			var adEvent = JsonConvert.DeserializeObject<TDAdEvent> (jsonMessage);
			TDCallbacks.instance.OnAdNotAvailable (adEvent);
		}

		void _didClose (string jsonMessage) {
			Debug.Log("_didClose " + jsonMessage);
			var adEvent = JsonConvert.DeserializeObject<TDAdEvent> (jsonMessage);
			TDCallbacks.instance.OnAdClosed (adEvent);
		}

		void _didClick (string jsonMessage) {
			Debug.Log("_onAdClick " + jsonMessage);
			var adEvent = JsonConvert.DeserializeObject<TDAdEvent> (jsonMessage);
			TDCallbacks.instance.OnAdClicked (adEvent);
		}

		void _didDisplay (string jsonMessage) {
			Debug.Log("_didDisplay " + jsonMessage);
			var adEvent = JsonConvert.DeserializeObject<TDAdEvent> (jsonMessage);
			TDCallbacks.instance.OnAdDidDisplay (adEvent);
		}

		void _willDisplay (string jsonMessage) {
			Debug.Log("_willDisplay " + jsonMessage);
			var adEvent = JsonConvert.DeserializeObject<TDAdEvent> (jsonMessage);
			TDCallbacks.instance.OnAdWillDisplay (adEvent);
		}

		// video
		void _didComplete (string adType) {
			Debug.Log("_didComplete " + adType);
		}

		void _didEngagement (string adType) {
			Debug.Log("_didEngagement " + adType);
		}

		// reeward video
		void _didReachLimit (string adType) {
			Debug.Log("_didReachLimit " + adType);
			TDCallbacks.instance.OnAdError (new TDAdEvent (adType, "VALIDATION_EXCEEDED_QUOTA"));
		}

		void _onRejected (string adType) {
			Debug.Log("_onRejected " + adType);
			TDCallbacks.instance.OnAdError (new TDAdEvent (adType, "VALIDATION_REJECTED"));
		}

		void _didFail (string jsonMessage) {
			Debug.Log("_didFail " + jsonMessage);
			var adEvent = JsonConvert.DeserializeObject<TDAdEvent> (jsonMessage);
			TDCallbacks.instance.OnAdError (adEvent);
		}

		void _onUserDeclined (string adType) {
			Debug.Log("_onUserDeclined " + adType);
			TDCallbacks.instance.OnAdClosed (new TDAdEvent (adType, "DECLINED_TO_VIEW"));
		}

		void _didVerify (string message) {
			Debug.Log("_didVerify " + message);
			var reward = JsonConvert.DeserializeObject<TDVideoReward> (message);
			TDCallbacks.instance.OnRewardedVideoValidated (reward);
		}

		void _onValidationFailed(string jsonMessage) {
			Debug.Log("_onValidationFailed " + jsonMessage);
			var adEvent = JsonConvert.DeserializeObject<TDAdEvent> (jsonMessage);
			TDCallbacks.instance.OnAdError (adEvent);
		}

		// banner
		void _didRefresh(string adType) {
			Debug.Log ("_didRefresh " + adType);
			TDCallbacks.instance.OnAdAvailable (new TDAdEvent (adType, "REFRESH"));
		}

		//native
		void _didFailToFetchNative(string message) {
			Debug.Log ("_didFailToFetchNative " + message);
			TDCallbacks.instance.OnAdError (new TDAdEvent("NATIVE_AD", message));
		}
	}
}