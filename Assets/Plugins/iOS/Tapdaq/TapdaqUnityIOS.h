//
//  TapdaqUnityIOS.h
//  TapdaqUnity
//
//  Created by Rheo Violenes on 05/05/15.
//  Copyright (c) 2015 Nerd. All rights reserved.
//
#import <Foundation/Foundation.h>
#import <Tapdaq/Tapdaq.h>
#import "TapdaqNativeAd.h"

void _LaunchMediationDebugger();

void _ConfigureTapdaq(const char* appIdChar,
                      const char* clientKeyChar,
                      const char* enabledAdTypesChar,
                      const char* testDevices);

// banner

/**
 * Loads a banner.
 * @param size A string. Must be one of following values: TDMBannerStandard, TDMBannerLarge, TDMBannerMedium, TDMBannerFull, TDMBannerLeaderboard, TDMBannerSmartPortrait, TDMBannerSmartLandscape
 */
void _LoadBannerForSize(const char* sizeChar);

bool _IsBannerReady();

void _ShowBanner(const char* position);

void _HideBanner();

// interstitial

void _LoadInterstitialWithTag(const char *tagChar);

bool _IsInterstitialReadyWithTag(const char *tagChar);

void _ShowInterstitialWithTag(const char* tagChar);

void _LoadInterstitial();

bool _IsInterstitialReady();

void _ShowInterstitial();

// video

void _LoadVideoWithTag(const char *tagChar);

bool _IsVideoReadyWithTag(const char *tagChar);

void _ShowVideoWithTag(const char* tagChar);

void _LoadVideo();

bool _IsVideoReady();

void _ShowVideo();

// reward video

void _LoadRewardedVideoWithTag(const char *tagChar);

bool _IsRewardedVideoReadyWithTag(const char *tagChar);

void _ShowRewardedVideoWithTag(const char* tagChar);

void _LoadRewardedVideo();

bool _IsRewardedVideoReady();

void _ShowRewardedVideo();

// native ad

void _LoadNativeAdvertForPlacementTag(const char* tag, const char* nativeAdType);

void _LoadNativeAdvertForAdType(const char* nativeAdType);

void _FetchNative(const char* adTypeInt);

void _FetchNativeAdWithTag (const char *tag, const char* nativeAdType);

void _SendNativeClick(const char *uniqueId);

void _SendNativeImpression(const char *uniqueId);

void _LaunchMediationDebugger();

bool _isEmpty(const char *str);


// more apps

void _ShowMoreApps();

bool _IsMoreAppsReady();

void _LoadMoreApps();

void _LoadMoreAppsWithConfig(const char* config);

@interface TapdaqUnityIOS : NSObject

+ (instancetype)sharedInstance;

- (void)initWithApplicationId:(NSString *)appID
                    clientKey:(NSString *)clientKey
               enabledAdTypes:(NSString *)enabledAdTypes
                  testDevices:(NSString *)testDevices;

@end
