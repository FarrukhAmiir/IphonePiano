#import "TapdaqStandardAd.h"

@implementation TapdaqInterstitialAd

+ (TapdaqInterstitialAd *)sharedInstance
{
    static dispatch_once_t once;
    static TapdaqInterstitialAd* sharedInstance;
    dispatch_once(&once, ^{
        sharedInstance = [[self alloc] init];
    });
    return sharedInstance;
}

- (void)loadForPlacementTag:(NSString *)tag
{
    [[Tapdaq sharedSession] loadInterstitialForPlacementTag:tag];
}

- (BOOL)isReadyForPlacementTag:(NSString *)tag
{
    return [[Tapdaq sharedSession] isInterstitialReadyForPlacementTag:tag];
}

- (void)showForPlacementTag:(NSString *)tag
{
    [[Tapdaq sharedSession] showInterstitialForPlacementTag:tag];
}

- (void)load
{
    [[Tapdaq sharedSession] loadInterstitial];
}

- (BOOL)isReady
{
    return [[Tapdaq sharedSession] isInterstitialReady];
}

- (void)show
{
    [[Tapdaq sharedSession] showInterstitial];
}

@end
