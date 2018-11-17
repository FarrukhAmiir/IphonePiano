#import "TapdaqDelegates.h"
#import "JsonHelper.h"
#import "TapdaqNativeAd.h"

@implementation TapdaqDelegates

+ (instancetype)sharedInstance
{
    static dispatch_once_t once;
    static id sharedInstance;
    dispatch_once(&once, ^{
        sharedInstance = [[self alloc] init];
    });
    return sharedInstance;
}

- (void) send:(NSString *) methodName adType:(NSString *) adType tag:(NSString *) tag message: (NSString *) message
{
    NSDictionary* dict = @{
                           @"adType": adType,
                           @"tag": tag,
                           @"message": message
                           };
    [self send: methodName dictionary: dict];
}

- (void) send:(NSString *) methodName dictionary:(NSDictionary *) dictionary
{
    [self send: methodName message: [JsonHelper toJsonString: dictionary]];
}

- (void) send:(NSString *) methodName message:(NSString *) message
{
    UnitySendMessage("TapdaqV1", [methodName UTF8String], [message UTF8String]);
}

#pragma mark - TapdaqDelegate

- (void)didLoadConfig
{
    [self send:@"_didLoadConfig" message:@""];
}

#pragma mark Banner delegate methods

- (void)didLoadBanner
{
    [self send: @"_didLoad" adType: @"BANNER" tag: @"" message: @"LOADED"];
}

- (void)didFailToLoadBanner
{
    [self send: @"_didFailToLoad" adType: @"BANNER" tag: @"" message: @"LOAD_FAILED"];
}

- (void)didClickBanner
{
    [self send: @"_didClick" message: @"BANNER"];
}

- (void)didRefreshBanner
{
    [self send: @"_didRefresh" message: @"BANNER"];
}

#pragma mark Interstitial delegate methods

- (void)didLoadInterstitialForPlacementTag:(NSString *)placementTag
{
    [self send: @"_didLoad" adType: @"INTERSTITIAL" tag: placementTag message: @"LOADED"];
}

- (void)didFailToLoadInterstitialForPlacementTag:(NSString *)placementTag
{
    [self send: @"_didFailToLoad" adType: @"INTERSTITIAL" tag: placementTag message: @"LOAD_FAILED"];
}

- (void)willDisplayInterstitialForPlacementTag:(NSString *) placementTag
{
    [self send: @"_willDisplay" adType: @"INTERSTITIAL" tag: placementTag message: @""];
}

- (void)didDisplayInterstitialForPlacementTag: (NSString *) placementTag
{
    [self send: @"_didDisplay" adType: @"INTERSTITIAL" tag: placementTag message: @""];
}

- (void)didCloseInterstitialForPlacementTag: (NSString *) placementTag
{
    [self send: @"_didClose" adType: @"INTERSTITIAL" tag: placementTag message: @""];
}

- (void)didClickInterstitialForPlacementTag: (NSString *) placementTag
{
    [self send: @"_didClick" adType: @"INTERSTITIAL" tag: placementTag message: @""];
}

#pragma mark Video delegate methods

- (void)didLoadVideoForPlacementTag:(NSString *)placementTag
{
    [self send: @"_didLoad" adType: @"VIDEO" tag: placementTag message: @""];
}

- (void)didFailToLoadVideoForPlacementTag:(NSString *)placementTag
{
    [self send: @"_didFailToLoad" adType: @"VIDEO" tag: placementTag message: @""];
}

- (void)willDisplayVideoForPlacementTag:(NSString *)placementTag
{
    [self send: @"_willDisplay" adType: @"VIDEO" tag: placementTag message: @""];
}

- (void)didDisplayVideoForPlacementTag:(NSString *)placementTag
{
    [self send: @"_didDisplay" adType: @"VIDEO" tag: placementTag message: @""];
}

- (void)didCloseVideoForPlacementTag:(NSString *)placementTag
{
    [self send: @"_didClose" adType: @"VIDEO" tag: placementTag message: @""];
}

- (void)didClickVideoForPlacementTag:(NSString *)placementTag
{
    [self send: @"_didClick" adType: @"VIDEO" tag: placementTag message: @""];
}

#pragma mark Rewarded Video delegate methods

- (void)didLoadRewardedVideoForPlacementTag:(NSString *)placementTag
{
    [self send: @"_didLoad" adType: @"REWARD_AD" tag: placementTag message: @""];
}

- (void)didFailToLoadRewardedVideoForPlacementTag:(NSString *)placementTag
{
    [self send: @"_didFailToLoad" adType: @"REWARD_AD" tag: placementTag message: @""];
}

- (void)willDisplayRewardedVideoForPlacementTag:(NSString *)placementTag
{
    [self send: @"_willDisplay" adType: @"REWARD_AD" tag: placementTag message: @""];
}

- (void)didDisplayRewardedVideoForPlacementTag:(NSString *)placementTag
{
    [self send: @"_didDisplay" adType: @"REWARD_AD" tag: placementTag message: @""];
}

- (void)didCloseRewardedVideoForPlacementTag:(NSString *)placementTag
{
    [self send: @"_didClose" adType: @"REWARD_AD" tag: placementTag message: @""];
}

- (void)didClickRewardedVideoForPlacementTag:(NSString *)placementTag
{
    [self send: @"_didClick" adType: @"REWARD_AD" tag: placementTag message: @""];
}

- (void)rewardValidationSucceededForPlacementTag:(NSString *)placementTag
                                      rewardName:(NSString *)rewardName
                                    rewardAmount:(int)rewardAmount
{
    NSDictionary* dict = @{
                           @"RewardName": rewardName,
                           @"RewardAmount": @(rewardAmount),
                           @"Tag": placementTag
                           };
    [self send: @"_didVerify" dictionary: dict];
}

- (void)rewardValidationErroredForPlacementTag:(NSString *)placementTag
{
    [self send: @"_onValidationFailed" adType: @"REWARD_AD" tag: placementTag message: @""];
}


#pragma mark Native delegate methods

- (void)didLoadNativeAdvertForPlacementTag:(NSString *)placementTag
                                    adType:(TDNativeAdType)nativeAdType
{
    NSDictionary* nativeMessageDict = @{
                                        @"nativeType": [[TapdaqNativeAd sharedInstance] getStringFromAdType:  nativeAdType]
                                        };
    [self send: @"_didLoad" adType: @"NATIVE_AD" tag: placementTag message: [JsonHelper toJsonString: nativeMessageDict]];
}

- (void)didFailToLoadNativeAdvertForPlacementTag:(NSString *)placementTag
                                          adType:(TDNativeAdType)nativeAdType
{
    NSDictionary* nativeMessageDict = @{
                                        @"nativeType": [[TapdaqNativeAd sharedInstance] getStringFromAdType:  nativeAdType]
                                        };
    [self send: @"_didFailToLoad" adType: @"NATIVE_AD" tag: placementTag message: [JsonHelper toJsonString: nativeMessageDict]];
}

#pragma mark - TapdaqDelegate methods

- (void)didLoadMoreApps
{
    NSDictionary* dict = @{
                           @"adType": @"MORE_APPS"
                           };
    [self send:@"_didLoad" dictionary: dict];
}

- (void)didFailToLoadMoreApps
{
    NSDictionary* dict = @{
                           @"adType": @"MORE_APPS"
                           };
    [self send:@"_didFailToLoad" dictionary: dict];
}

- (void)willDisplayMoreApps
{
    NSDictionary* dict = @{
                           @"adType": @"MORE_APPS"
                           };
    [self send:@"_willDisplay" dictionary: dict];
}

- (void)didDisplayMoreApps
{
    NSDictionary* dict = @{
                           @"adType": @"MORE_APPS"
                           };
    [self send:@"_didDisplay" dictionary: dict];
}

- (void)didCloseMoreApps
{
    NSDictionary* dict = @{
                           @"adType": @"MORE_APPS"
                           };
    [self send:@"_didClose" dictionary: dict];
}

@end
