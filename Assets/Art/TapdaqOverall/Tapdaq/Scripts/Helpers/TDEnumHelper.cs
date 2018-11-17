using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Tapdaq {

	public static class TDEnumHelper {

		public static string FixAndroidAdapterName(string adapterName) {
			if (adapterName == Enum.GetName (typeof(TapdaqAdapter), TapdaqAdapter.FANAdapter)) {
				return "FacebookAdapter";
			}
			return adapterName;
		}

		public static TDAdType GetAdTypeFromNativeType(TDNativeAdType nativeType) {

			var nativeString = nativeType.ToString ();

			var typeString = nativeString.Replace ("TDNativeAdType", "TDAdType");

			return GetEnumFromString<TDAdType>(typeString);
		}

		public static TDNativeAdType GetNativeAdTypeFromAdType(TDAdType adType) {

			var typeString = adType.ToString ();

			var nativeTypeString = typeString.Replace ("TDAdType", "TDNativeAdType");

			return GetEnumFromString<TDNativeAdType>(nativeTypeString, TDNativeAdType.TDNativeAdTypeNone);
		}

		public static T GetEnumFromString<T>(string enumString, T defaultValue = default(T)) {
			Array values = null;
			try {
				values = Enum.GetValues (typeof(T));
			}
			catch(Exception e) {
				Debug.LogError ("Can't GetEnumFromString: " + enumString);
				return defaultValue;
			}

			if (values == null)
				return defaultValue;

			foreach (var val in values) {
				if (val.ToString ().ToLower () == enumString.ToLower())
					return (T)val;
			}

			return defaultValue;
		}
	}
}
