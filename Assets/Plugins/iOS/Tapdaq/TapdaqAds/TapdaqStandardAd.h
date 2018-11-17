#import <Foundation/Foundation.h>
#import <Tapdaq/Tapdaq.h>

@protocol TapdaqStandardAd

+ (instancetype) sharedInstance;
- (BOOL)isReady;
- (void)show;

@optional
- (void)load;
- (void)loadForPlacementTag:(NSString *)tag;
- (BOOL)isReadyForPlacementTag:(NSString *)tag;
- (void)showForPlacementTag:(NSString *)tag;

@end

@interface TapdaqVideoAd : NSObject<TapdaqStandardAd>
@end

@interface TapdaqRewardedVideoAd : NSObject<TapdaqStandardAd>
@end

@interface TapdaqInterstitialAd : NSObject<TapdaqStandardAd>
@end

@interface TapdaqBannerAd : NSObject<TapdaqStandardAd>

- (void)loadForSize:(NSString *)sizeStr;
- (void)hide;
- (void)show:(const char *)position;

@end

@interface TapdaqMoreApps : NSObject<TapdaqStandardAd>

- (void)loadWithConfig:(const char *)moreAppsConfig;

@end
