#import "TapdaqStandardAd.h"

extern UIViewController *UnityGetGLViewController();
extern UIView *UnityGetGLView();

static NSString *const kTDUnityBannerStandard = @"TDMBannerStandard";
static NSString *const kTDUnityBannerLarge = @"TDMBannerLarge";
static NSString *const kTDUnityBannerMedium = @"TDMBannerMedium";
static NSString *const kTDUnityBannerFull = @"TDMBannerFull";
static NSString *const kTDUnityBannerLeaderboard = @"TDMBannerLeaderboard";
static NSString *const kTDUnityBannerSmartPortrait = @"TDMBannerSmartPortrait";
static NSString *const kTDUnityBannerSmartLandscape = @"TDMBannerSmartLandscape";

#pragma mark - Banner Native


@implementation TapdaqBannerAd
    
+ (TapdaqBannerAd *)sharedInstance
{
    static dispatch_once_t once;
    static TapdaqBannerAd* sharedInstance;
    dispatch_once(&once, ^{
        sharedInstance = [[self alloc] init];
    });
    return sharedInstance;
}

UIView *bannerView;

- (void)loadForSize:(NSString *)sizeStr
{
    TDMBannerSize bannerSize = [self bannerSizeFromString:sizeStr];
    
    [[Tapdaq sharedSession] loadBanner:bannerSize];
}

- (BOOL)isReady
{
    return [[Tapdaq sharedSession] isBannerReady];
}

- (void)show
{
    [self show:"bottom"];
}

// position could be "Top" or "Bottom" (ignore case)
- (void)show:(const char *)position
{
    NSString * posString = [NSString stringWithUTF8String: position];
    bannerView = [[Tapdaq sharedSession] getBanner];
    
    if (bannerView != nil) {
        CGSize bannerSize = bannerView.frame.size;
        CGSize unityViewSize = UnityGetGLView().frame.size;
        
        // calculating Y of banner for "top" or "bottom" position
        float bannerY = [[posString lowercaseString] isEqualToString:@"top"] ?
        0 : unityViewSize.height-bannerSize.height;
        
        bannerView.frame = CGRectMake((unityViewSize.width-bannerSize.width)/2,
                                      bannerY,
                                      bannerSize.width,
                                      bannerSize.height);
        
        [UnityGetGLView() addSubview:bannerView];
    }
}

- (void)hide
{
    if (bannerView != nil) {
        [bannerView removeFromSuperview];
        bannerView = nil;
    }
}

- (TDMBannerSize)bannerSizeFromString:(NSString *)sizeStr
{
    TDMBannerSize bannerSize = TDMBannerStandard;
    
    if ([sizeStr isEqualToString:kTDUnityBannerStandard]) {
        bannerSize = TDMBannerStandard;
    } else if ([sizeStr isEqualToString:kTDUnityBannerLarge]) {
        bannerSize = TDMBannerLarge;
    } else if ([sizeStr isEqualToString:kTDUnityBannerMedium]) {
        bannerSize = TDMBannerMedium;
    } else if ([sizeStr isEqualToString:kTDUnityBannerFull]) {
        bannerSize = TDMBannerFull;
    } else if ([sizeStr isEqualToString:kTDUnityBannerLeaderboard]) {
        bannerSize = TDMBannerLeaderboard;
    } else if ([sizeStr isEqualToString:kTDUnityBannerSmartPortrait]) {
        bannerSize = TDMBannerSmartPortrait;
    } else if ([sizeStr isEqualToString:kTDUnityBannerSmartLandscape]) {
        bannerSize = TDMBannerSmartLandscape;
    }
    
    return bannerSize;
}

@end
