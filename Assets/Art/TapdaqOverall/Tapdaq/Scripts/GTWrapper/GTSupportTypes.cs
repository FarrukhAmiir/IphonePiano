using System;
using Tapdaq;
//GA Events To Log
public enum TapdaqGAEvents {
	Initializing,Initialized,

	LoadInterstitialAd,LoadVideoAd,LoadRewardedAd,
	InterstitialAdLoaded,VideoAdLoaded,RewardedAdLoaded,
	ShowInterstitialAd,ShowVideoAd,ShowRewardedAd,
	InterstitialAdWillDisplay,VideoAdWillDisplay,RewardedAdWillDisplay,
	InterstitialAdDisplayed,VideoAdDisplayed,RewardedAdDisplayed,
	InterstitialAdNoInventory,VideoAdNoInventory,RewardedAdNoInventory,

	LoadCPOverall,LoadCPCategory,
	CPOverallLoaded,CPCategoryLoaded,
	CPCategoryNoInventory,CPOverallNoInventory,
	CPCategoryShow,CPOverallShow,
	CPCategoryClick,CPCategoryClose,
	CPOverallClick,CPOverallClose,

	AdClicked,AdClosed,AdError
}
//Supported Ad Types
public enum AdTypes {
	CPOverAll,CPCategory,Video,Interstitial,Rewarded
}

public enum AdSizes {
	Small, Medium, Large
}

//optional call back delegates
public delegate void TapdaqReadyDelegate();
public delegate void AdAvailableDelegate(AdTypes ad, AdSizes size);
public delegate void RewardUserDelegate(TDVideoReward ad);
