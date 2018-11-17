#import <Foundation/Foundation.h>
#import <Tapdaq/Tapdaq.h>
#import "TapdaqDelegates.h"

typedef void (*InterstitialCallback)();
typedef void (*NativeCallback)();

typedef void (*InterstitialDelegateCallback)();
typedef void (*NativeDelegateCallback)();


@interface TapdaqNativeAd : NSObject

+ (instancetype)sharedInstance;

- (void)loadNativeAdvertForPlacementTag:(const char*)tag nativeAdType:(const char*)adType;

- (void)loadNativeAdvertForAdType:(const char*)adType;

- (char const*)fetchNativeForAdType:(const char*)adType;

- (char const*)fetchNativeForAdWithTag:(const char*)tag
                                AdType:(const char*)adType;

- (char const*) fetchNativeAd:(TDNativeAdvert *) nativeAdvert;

- (void)triggerClickForNativeAdvert:(const char*)uniqueId;

- (void)triggerImpressionForNativeAdvert:(const char*)uniqueId;

- (NSString *) getStringFromAdType:(TDNativeAdType) adType;

@property (nonatomic, strong, retain) NSMutableDictionary *nativeAds;
@property (nonatomic, strong, retain) NSDictionary* nativeAdTypesDictionary;


@end
