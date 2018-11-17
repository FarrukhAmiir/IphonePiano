//
//  TapdaqUnityIOS.m
//  TapdaqUnity
//
//  Created by Rheo Violenes on 05/05/15.
//  Copyright (c) 2015 Nerd. All rights reserved.
//
#import "TapdaqUnityIOS.h"
#import "TapdaqDelegates.h"
#import "TapdaqStandardAd.h"

static NSString *const kTapdaqLogPrefix = @"[TapdaqUnity]";

void _ConfigureTapdaq(const char* appIdChar,
                      const char* clientKeyChar,
                      const char* enabledAdTypesChar,
                      const char* testDevicesChar) {
    
    bool isValid = true;
    
    if (_isEmpty(appIdChar)) {
        NSLog(@"%@ iOS App ID is empty", kTapdaqLogPrefix);
        isValid = false;
    }
    
    if (_isEmpty(clientKeyChar)) {
        NSLog(@"%@ iOS Client Key is empty", kTapdaqLogPrefix);
        isValid = false;
    }
    
    if (_isEmpty(enabledAdTypesChar)) {
        NSLog(@"%@ No placements are registered", kTapdaqLogPrefix);
        isValid = false;
    }
    
    if (!isValid) {
        NSLog(@"%@ Tapdaq did not initialise", kTapdaqLogPrefix);
        return;
    }
    
    NSString *appId = [[NSString stringWithUTF8String:appIdChar] copy];
    NSString *clientKey = [[NSString stringWithUTF8String:clientKeyChar] copy];
    NSString *enabledAdTypes = [[NSString stringWithUTF8String:enabledAdTypesChar] copy];
    NSString *testDevices = [[NSString stringWithUTF8String:testDevicesChar] copy];
    
    [[TapdaqUnityIOS sharedInstance] initWithApplicationId:appId
                                                 clientKey:clientKey
                                            enabledAdTypes:enabledAdTypes
                                               testDevices:testDevices];
    
}

#pragma mark - Banner (Bridge)

void _LoadBannerForSize(const char* sizeChar) {
    
    if (_isEmpty(sizeChar)) {
        NSLog(@"%@ No banner size specified, cannot load banner", kTapdaqLogPrefix);
        return;
    }
    
    NSString *sizeStr = [[NSString stringWithUTF8String:sizeChar] copy];
    
    [[TapdaqBannerAd sharedInstance] loadForSize:sizeStr];
    
}

bool _IsBannerReady() {
    
    return (bool) [[TapdaqBannerAd sharedInstance] isReady];
    
}

void _ShowBanner(const char* position) {
    
    if (_isEmpty(position)) {
        NSLog(@"%@ No banner position given, failed to show banner", kTapdaqLogPrefix);
        return;
    }
    
    [[TapdaqBannerAd sharedInstance] show: position];
    
}

void _HideBanner() {
    
    [[TapdaqBannerAd sharedInstance] hide];
    
}

#pragma mark - Interstitial (Bridge)

void _LoadInterstitialWithTag(const char *tagChar) {
    
    if (_isEmpty(tagChar)) {
        NSLog(@"%@ No tag given, failed to load interstitial", kTapdaqLogPrefix);
        return;
    }
    
    NSString *tag = [NSString stringWithUTF8String:tagChar];
    
    [[TapdaqInterstitialAd sharedInstance] loadForPlacementTag:tag];
    
}

bool _IsInterstitialReadyWithTag(const char *tagChar) {
    
    if (_isEmpty(tagChar)) {
        NSLog(@"%@ No tag given, failed to check if interstitial is ready", kTapdaqLogPrefix);
        return false;
    }
    
    NSString *tag = [NSString stringWithUTF8String:tagChar];
    
    return (bool) [[TapdaqInterstitialAd sharedInstance] isReadyForPlacementTag:tag];
    
}

void _ShowInterstitialWithTag(const char* tagChar) {
    
    if (_isEmpty(tagChar)) {
        NSLog(@"%@ No tag given, failed to show interstitial", kTapdaqLogPrefix);
        return;
    }
    
    NSString *tag = [NSString stringWithUTF8String:tagChar];
    
    [[TapdaqInterstitialAd sharedInstance] showForPlacementTag:tag];
    
}

void _LoadInterstitial() {
    [[TapdaqInterstitialAd sharedInstance] load];
}

bool _IsInterstitialReady() {
    return (bool) [[TapdaqInterstitialAd sharedInstance] isReady];
}

void _ShowInterstitial() {
    [[TapdaqInterstitialAd sharedInstance] show];
}

#pragma mark - Video (Bridge)

void _LoadVideoWithTag(const char *tagChar) {
    
    if (_isEmpty(tagChar)) {
        NSLog(@"%@ No tag given, failed to load video", kTapdaqLogPrefix);
        return;
    }
    
    NSString *tag = [NSString stringWithUTF8String:tagChar];
    
    [[TapdaqVideoAd sharedInstance] loadForPlacementTag:tag];
    
}

bool _IsVideoReadyWithTag(const char *tagChar) {
    
    if (_isEmpty(tagChar)) {
        NSLog(@"%@ No tag given, failed to check if video is ready", kTapdaqLogPrefix);
        return false;
    }
    
    NSString *tag = [NSString stringWithUTF8String:tagChar];
    
    return (bool) [[TapdaqVideoAd sharedInstance] isReadyForPlacementTag:tag];
    
}

void _ShowVideoWithTag(const char* tagChar) {
    
    if (_isEmpty(tagChar)) {
        NSLog(@"%@ No tag given, failed to show video", kTapdaqLogPrefix);
        return;
    }
    
    NSString *tag = [NSString stringWithUTF8String:tagChar];
    
    [[TapdaqVideoAd sharedInstance] showForPlacementTag:tag];
    
}

void _LoadVideo() {
    [[TapdaqVideoAd sharedInstance] load];
}

bool _IsVideoReady() {
    return (bool) [[TapdaqVideoAd sharedInstance] isReady];
}

void _ShowVideo() {
    [[TapdaqVideoAd sharedInstance] show];
}

#pragma mark - Rewarded Video (Bridge)

void _LoadRewardedVideoWithTag(const char *tagChar) {
    
    if (_isEmpty(tagChar)) {
        NSLog(@"%@ No tag given, failed to load rewarded video", kTapdaqLogPrefix);
        return;
    }
    
    NSString *tag = [NSString stringWithUTF8String:tagChar];
    
    [[TapdaqRewardedVideoAd sharedInstance] loadForPlacementTag:tag];
    
}

bool _IsRewardedVideoReadyWithTag(const char *tagChar) {
    
    if (_isEmpty(tagChar)) {
        NSLog(@"%@ No tag given, failed to check if rewarded video is ready", kTapdaqLogPrefix);
        return false;
    }
    
    NSString *tag = [NSString stringWithUTF8String:tagChar];
    
    return (bool) [[TapdaqRewardedVideoAd sharedInstance] isReadyForPlacementTag:tag];
    
}

void _ShowRewardedVideoWithTag(const char* tagChar) {
    
    if (_isEmpty(tagChar)) {
        NSLog(@"%@ No tag given, failed to show rewarded video", kTapdaqLogPrefix);
        return;
    }
    
    NSString *tag = [NSString stringWithUTF8String:tagChar];
    
    [[TapdaqRewardedVideoAd sharedInstance] showForPlacementTag:tag];
    
}

void _LoadRewardedVideo() {
    [[TapdaqRewardedVideoAd sharedInstance] load];
}

bool _IsRewardedVideoReady() {
    return (bool) [[TapdaqRewardedVideoAd sharedInstance] isReady];
}

void _ShowRewardedVideo() {
    [[TapdaqRewardedVideoAd sharedInstance] show];
}

#pragma mark - Native Ads (Bridge)

void _LoadNativeAdvertForPlacementTag(const char* tag, const char* nativeAdType)
{
    bool isValid = true;
    
    if (_isEmpty(tag)) {
        NSLog(@"%@ No tag given", kTapdaqLogPrefix);
        isValid = false;
    }
    
    if (_isEmpty(nativeAdType)) {
        NSLog(@"%@ No nativeAdType given", kTapdaqLogPrefix);
        isValid = false;
    }
    
    if (!isValid) {
        NSLog(@"%@ Failed to load native ad", kTapdaqLogPrefix);
        return;
    }
    
    [[TapdaqNativeAd sharedInstance] loadNativeAdvertForPlacementTag: tag nativeAdType: nativeAdType];
}

void _LoadNativeAdvertForAdType(const char* nativeAdType)
{
    if (_isEmpty(nativeAdType)) {
        NSLog(@"%@ No nativeAdType given, failed to load native advert", kTapdaqLogPrefix);
        return;
    }
    
    [[TapdaqNativeAd sharedInstance] loadNativeAdvertForAdType: nativeAdType];
}

const char* _GetNative(const char* adType) {
    
    if (_isEmpty(adType)) {
        NSLog(@"%@ No nativeAdType given, failed to get native advert", kTapdaqLogPrefix);
        return "";
    }
    
    return [[TapdaqNativeAd sharedInstance] fetchNativeForAdType:adType];
}

const char* _GetNativeAdWithTag (const char* tag, const char* nativeAdType) {
    
    bool isValid = true;
    
    if (_isEmpty(tag)) {
        NSLog(@"%@ No tag given", kTapdaqLogPrefix);
        isValid = false;
    }
    
    if (_isEmpty(nativeAdType)) {
        NSLog(@"%@ No nativeAdType given", kTapdaqLogPrefix);
        isValid = false;
    }
    
    if (!isValid) {
        NSLog(@"%@ Failed to get native ad", kTapdaqLogPrefix);
        return "";
    }
    
    return [[TapdaqNativeAd sharedInstance] fetchNativeForAdWithTag:tag AdType:nativeAdType];
}

void _SendNativeClick(const char* uniqueId) {
    
    if (_isEmpty(uniqueId)) {
        NSLog(@"%@ No uniqueId given, failed to send native click", kTapdaqLogPrefix);
        return;
    }
    
    [[TapdaqNativeAd sharedInstance] triggerClickForNativeAdvert:uniqueId];
    
}

void _SendNativeImpression(const char* uniqueId) {
    
    if (_isEmpty(uniqueId)) {
        NSLog(@"%@ No uniqueId given, failed to send native impression", kTapdaqLogPrefix);
        return;
    }
    
    [[TapdaqNativeAd sharedInstance] triggerImpressionForNativeAdvert:uniqueId];
    
}

#pragma mark - Show More Apps

void _ShowMoreApps() {
    [[TapdaqMoreApps sharedInstance] show];
}

bool _IsMoreAppsReady() {
    return [[TapdaqMoreApps sharedInstance] isReady];
}

void _LoadMoreApps() {
    [[TapdaqMoreApps sharedInstance] load];
}

void _LoadMoreAppsWithConfig(const char* config) {
    [[TapdaqMoreApps sharedInstance] loadWithConfig: config];
}

void _LaunchMediationDebugger() {
    // TODO stub
}

bool _isEmpty(const char* str) {
    return str == NULL;
}

@implementation TapdaqUnityIOS

+ (instancetype)sharedInstance
{
    static dispatch_once_t once;
    static id sharedInstance;
    dispatch_once(&once, ^{
        sharedInstance = [[self alloc] init];
    });
    return sharedInstance;
}

+ (void)load
{
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(createPlugin:) name:UIApplicationDidFinishLaunchingNotification object:nil];
}

+ (void)createPlugin:(NSNotification *)notification
{
    [TapdaqUnityIOS sharedInstance];
}

// Init
//Configure Tapdaq with credentials and ad settings
- (void)initWithApplicationId:(NSString *)appID
                    clientKey:(NSString *)clientKey
               enabledAdTypes:(NSString *)enabledAdTypes
                  testDevices:(NSString *)testDevices
{
    NSLog(@"enabledAdTypes: %@", enabledAdTypes);
    
    TDProperties *properties = [[TDProperties alloc] init];
    [properties setPluginVersion:@"unity_4.2.1"];
    
    if (enabledAdTypes && [enabledAdTypes length] > 0) {
        
        NSData *data = [enabledAdTypes dataUsingEncoding:NSUTF8StringEncoding];
        
        NSError *error = nil;
        NSArray *arr = [NSJSONSerialization JSONObjectWithData:data options:kNilOptions error:&error];
        
        if (!error) {
            
            if (enabledAdTypes && [enabledAdTypes length] > 0) {
                
                NSDictionary *validAdTypes = @{
                                               
                                               @"TDAdTypeNone": @(0),
                                               @"TDAdTypeInterstitial": @(1),
                                               @"TDAdType1x1Large": @(2),
                                               @"TDAdType1x1Medium": @(3),
                                               @"TDAdType1x1Small": @(4),
                                               
                                               @"TDAdType1x2Large": @(5),
                                               @"TDAdType1x2Medium": @(6),
                                               @"TDAdType1x2Small": @(7),
                                               
                                               @"TDAdType2x1Large": @(8),
                                               @"TDAdType2x1Medium": @(9),
                                               @"TDAdType2x1Small": @(10),
                                               
                                               @"TDAdType2x3Large": @(11),
                                               @"TDAdType2x3Medium": @(12),
                                               @"TDAdType2x3Small": @(13),
                                               
                                               @"TDAdType3x2Large": @(14),
                                               @"TDAdType3x2Medium": @(15),
                                               @"TDAdType3x2Small": @(16),
                                               
                                               @"TDAdType1x5Large": @(17),
                                               @"TDAdType1x5Medium": @(18),
                                               @"TDAdType1x5Small": @(19),
                                               
                                               @"TDAdType5x1Large": @(20),
                                               @"TDAdType5x1Medium": @(21),
                                               @"TDAdType5x1Small": @(22),
                                               
                                               @"TDAdTypeVideo": @(23),
                                               @"TDAdTypeRewardedVideo": @(24),
                                               @"TDAdTypeBanner": @(25)
                                               
                                               };
                
                NSMutableDictionary *tagsWithAdTypes = [[NSMutableDictionary alloc] init];
                
                for (NSDictionary *dict in arr) {
                    
                    NSString *adTypeStr = [dict objectForKey:@"ad_type"];
                    NSArray *placementTags = [dict objectForKey:@"placement_tags"];
                    
                    if ([placementTags count] > 0) {
                        for (NSString *placementTag in placementTags) {
                            
                            // update tagsWithAdTypes
                            NSNumber *combinedAdTypeNum = [tagsWithAdTypes objectForKey:placementTag];
                            
                            if (!combinedAdTypeNum) {
                                combinedAdTypeNum = @(0);
                            }
                            
                            TDAdTypes adTypesCombined = [combinedAdTypeNum integerValue];
                            
                            NSNumber *adTypeNum = [validAdTypes objectForKey:adTypeStr];
                            NSInteger adTypeInt = [adTypeNum integerValue];
                            
                            adTypesCombined |= 1 << adTypeInt;
                            
                            combinedAdTypeNum = @(adTypesCombined);
                            
                            [tagsWithAdTypes setObject:combinedAdTypeNum forKey:placementTag];
                            
                        }
                    }
                    
                }
                
                
                for (id key in tagsWithAdTypes) {
                    
                    if ([key isKindOfClass:[NSString class]] && [[tagsWithAdTypes objectForKey:key] integerValue] > 0) {
                        NSString *tag = (NSString *) key;
                        TDAdTypes adTypes = (TDAdTypes) [[tagsWithAdTypes objectForKey:key] integerValue];
                        
                        if (tag && [tag length] > 0) {
                            TDPlacement *placement = [[TDPlacement alloc] initWithAdTypes:adTypes forTag:tag];
                            NSLog(@"placement: %@", placement);
                            [properties registerPlacement:placement];
                        }
                        
                    }
                    
                }
                
            }
            
        }
    }
    
    [self setTestDevices: testDevices toProperties:properties];
    
    [[Tapdaq sharedSession] setApplicationId:appID
                                   clientKey:clientKey
                                  properties:properties];
    
    [(Tapdaq *)[Tapdaq sharedSession] setDelegate: [TapdaqDelegates sharedInstance]];
    
    
    [[Tapdaq sharedSession] launch];
}

- (void) setTestDevices:(NSString *)testDevicesJson toProperties:(TDProperties *)properties
{
    NSData *data = [testDevicesJson dataUsingEncoding:NSUTF8StringEncoding];
    
    NSError *error = nil;
    NSDictionary *testDevicesDictionary = [NSJSONSerialization JSONObjectWithData:data options:kNilOptions error:&error];
    
    if(error == nil) {
        NSArray* amArray = testDevicesDictionary[@"adMobDevices"];
        NSArray* fbArray = testDevicesDictionary[@"facebookDevices"];
        
        if(amArray != nil) {
            TDTestDevices *amTestDevices = [[TDTestDevices alloc] initWithNetwork:TDMAdMob testDevices:amArray];
            [properties registerTestDevices: amTestDevices];
        }
        
        if(fbArray != nil) {
            TDTestDevices *fbTestDevices = [[TDTestDevices alloc] initWithNetwork:TDMFacebookAudienceNetwork testDevices:fbArray];
            [properties registerTestDevices: fbTestDevices];
        }
    }
}

@end
