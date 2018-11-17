using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Tapdaq {
	public static class TDExtensionMethods {

		public static int ParseInt(this string str, int defaultValue) {
			int result;

			if (int.TryParse (str, out result)) {
				return result;
			}

			return defaultValue;
		}

		public static float ParseFloat(this string str, float defaultValue) {
			float result;

			if (float.TryParse (str, out result)) {
				return result;
			}

			return defaultValue;
		}

		public static TV GetValue<TK, TV>(this Dictionary<TK, TV> dict, TK key, TV defaultValue = default(TV)) {
			if (!dict.ContainsKey (key)) {
				return defaultValue;
			}

			return dict [key];
		}

		public static T GetValueOrDefault<T>(this List<T> list, int index, T def = default(T)) {
			if (index >= list.Count)
				return def;
			return list [index];
		}

		public static Vector2 ToVector2 (this TDNativeAdType adType) {
			switch (adType) {

			case TDNativeAdType.TDNativeAdType1x1Large:
				return new Vector2 (750,750);

			case TDNativeAdType.TDNativeAdType1x1Medium:
				return new Vector2 (375,375);

			case TDNativeAdType.TDNativeAdType1x1Small:
				return new Vector2 (150,150);


			case TDNativeAdType.TDNativeAdType1x2Large:
				return new Vector2 (900,1800);

			case TDNativeAdType.TDNativeAdType1x2Medium:
				return new Vector2 (450,900);

			case TDNativeAdType.TDNativeAdType1x2Small:
				return new Vector2 (180,360);


			case TDNativeAdType.TDNativeAdType2x1Large:
				return new Vector2 (1800,900);

			case TDNativeAdType.TDNativeAdType2x1Medium:
				return new Vector2 (900,450);
			
			case TDNativeAdType.TDNativeAdType2x1Small:
				return new Vector2 (360,180);


			case TDNativeAdType.TDNativeAdType2x3Large:
				return new Vector2 (960,1440);

			case TDNativeAdType.TDNativeAdType2x3Medium:
				return new Vector2 (480,720);

			case TDNativeAdType.TDNativeAdType2x3Small:
				return new Vector2 (192,288);


			case TDNativeAdType.TDNativeAdType3x2Large:
				return new Vector2 (1440,960);

			case TDNativeAdType.TDNativeAdType3x2Medium:
				return new Vector2 (720,480);

			case TDNativeAdType.TDNativeAdType3x2Small:
				return new Vector2 (288,192);


			case TDNativeAdType.TDNativeAdType1x5Large:
				return new Vector2 (360,1800);

			case TDNativeAdType.TDNativeAdType1x5Medium:
				return new Vector2 (180,900);

			case TDNativeAdType.TDNativeAdType1x5Small:
				return new Vector2 (72,360);


			case TDNativeAdType.TDNativeAdType5x1Large:
				return new Vector2 (1800,360);

			case TDNativeAdType.TDNativeAdType5x1Medium:
				return new Vector2 (900,180);

			case TDNativeAdType.TDNativeAdType5x1Small:
				return new Vector2 (360,72);


			default:
				return Vector2.zero;

			}
		}
	}
}
