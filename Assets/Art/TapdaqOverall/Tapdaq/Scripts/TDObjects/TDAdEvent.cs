using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Tapdaq {
	[Serializable]
	public class TDAdEvent {
		public string adType;
		public string message;
		public string tag;

		public TDAdEvent() {
		}

		public TDAdEvent(string adType, string message, string tag = null) {
			this.adType = adType;
			this.message = message;
			this.tag = tag;
		}

		public TDAdType GetTypeOfEvent()  {
			if (adType == "INTERSTITIAL")
				return TDAdType.TDAdTypeInterstitial;
			
			if (adType == "BANNER")
				return TDAdType.TDAdTypeBanner;
			
			if (adType == "VIDEO")
				return TDAdType.TDAdTypeVideo;
			
			if (adType == "REWARD_AD")
				return TDAdType.TDAdTypeRewardedVideo;
			
			if (IsNativeAdEvent()) {
				var nativeType = GetNativeEventType ();
				return TDEnumHelper.GetAdTypeFromNativeType (nativeType);
			}

			if (IsMoreAppsEvent())
				return TDAdType.TDAdTypeNone;

			return TDAdType.TDAdTypeNone;
		}

		public bool IsInterstitialEvent()  {
			return GetTypeOfEvent() == TDAdType.TDAdTypeInterstitial;
		}

		public bool IsVideoEvent()  {
			return GetTypeOfEvent() == TDAdType.TDAdTypeVideo;
		}

		public bool IsRewardedVideoEvent()  {
			return GetTypeOfEvent() == TDAdType.TDAdTypeRewardedVideo;
		}

		public bool IsBannerEvent()  {
			return GetTypeOfEvent() == TDAdType.TDAdTypeBanner;
		}

		public bool IsNativeAdEvent() {
			return adType == "NATIVE_AD";
		}

		public bool IsMoreAppsEvent() {
			return adType == "MORE_APPS";
		}

		public TDNativeAdType GetNativeEventType() {
			
			if (IsNativeAdEvent() && message != null) {

				var nativeAdMessage = JsonConvert.DeserializeObject<TDNativeAdMessage> (message);

				if (nativeAdMessage != null) {
					return TDEnumHelper.GetEnumFromString<TDNativeAdType>(nativeAdMessage.nativeType);
				}
			}

			return TDNativeAdType.TDNativeAdTypeNone;
		}

		public string GetNativeEventMessage() {
			if (IsNativeAdEvent() && message != null) {

				var nativeAdMessage = JsonConvert.DeserializeObject<TDNativeAdMessage> (message);

				if (nativeAdMessage != null) {
					return nativeAdMessage.messageText;
				}
			}
			return null;
		}
	}

	[Serializable]
	public class TDNativeAdMessage {
		public string nativeType;
		public string messageText;
	}
}