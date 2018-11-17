using System;

namespace Tapdaq {
	public enum TDAdType {
		TDAdTypeNone         = 0,

		TDAdTypeInterstitial = 1,

		TDAdType1x1Large     = 2,
		TDAdType1x1Medium    = 3,
		TDAdType1x1Small     = 4,

		TDAdType1x2Large     = 5,
		TDAdType1x2Medium    = 6,
		TDAdType1x2Small     = 7,

		TDAdType2x1Large     = 8,
		TDAdType2x1Medium    = 9,
		TDAdType2x1Small     = 10,

		TDAdType2x3Large     = 11,
		TDAdType2x3Medium    = 12,
		TDAdType2x3Small     = 13,

		TDAdType3x2Large     = 14,
		TDAdType3x2Medium    = 15,
		TDAdType3x2Small     = 16,

		TDAdType1x5Large     = 17,
		TDAdType1x5Medium    = 18,
		TDAdType1x5Small     = 19,

		TDAdType5x1Large     = 20,
		TDAdType5x1Medium    = 21,
		TDAdType5x1Small     = 22,

		TDAdTypeVideo        = 23,
		TDAdTypeRewardedVideo= 24,
		TDAdTypeBanner       = 25
	}

	public enum TDNativeAdType {

		TDNativeAdTypeNone = -1,
			
		TDNativeAdType1x1Large = 0,
		TDNativeAdType1x1Medium = 1,
		TDNativeAdType1x1Small = 2,

		TDNativeAdType1x2Large = 3,
		TDNativeAdType1x2Medium = 4,
		TDNativeAdType1x2Small = 5,

		TDNativeAdType2x1Large = 6,
		TDNativeAdType2x1Medium = 7,
		TDNativeAdType2x1Small = 8,

		TDNativeAdType2x3Large = 9,
		TDNativeAdType2x3Medium = 10,
		TDNativeAdType2x3Small = 11,

		TDNativeAdType3x2Large = 12,
		TDNativeAdType3x2Medium = 13,
		TDNativeAdType3x2Small = 14,

		TDNativeAdType1x5Large = 15,
		TDNativeAdType1x5Medium = 16,
		TDNativeAdType1x5Small = 17,

		TDNativeAdType5x1Large = 18,
		TDNativeAdType5x1Medium = 19,
		TDNativeAdType5x1Small = 20
	}

	public enum TDMBannerSize {
		TDMBannerStandard = 0,
		TDMBannerLarge = 1,
		TDMBannerMedium = 2,
		TDMBannerFull = 3,
		TDMBannerLeaderboard = 4,
		TDMBannerSmartPortrait = 5,
		TDMBannerSmartLandscape = 6
	}

	public enum TDOrientation {
		portrait = 0,
		landscape = 1,
		universal = 2
	}

	public enum TDLogSeverity {
		debug = 0,
		warning = 1,
		error = 2
	}

	public enum TDBannerPosition {
		Bottom,
		Top
	}

	public enum TapdaqAdapter {
		AdColonyAdapter,
		AdMobAdapter,
		AppLovinAdapter,
		ChartboostAdapter,
		FANAdapter,
		UnityAdsAdapter,
		VungleAdapter,
		TapjoyAdapter
	}
}

