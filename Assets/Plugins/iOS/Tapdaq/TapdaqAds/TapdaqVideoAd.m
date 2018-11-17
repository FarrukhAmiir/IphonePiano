#import "TapdaqStandardAd.h"

@implementation TapdaqVideoAd

+ (TapdaqVideoAd*)sharedInstance
{
    static dispatch_once_t once;
    static TapdaqVideoAd* sharedInstance;
    dispatch_once(&once, ^{
        sharedInstance = [[self alloc] init];
    });
    return sharedInstance;
}

- (void)loadForPlacementTag:(NSString *)tag {
    [[Tapdaq sharedSession] loadVideoForPlacementTag:tag];
}

- (BOOL)isReadyForPlacementTag:(NSString *)tag {
    return [[Tapdaq sharedSession] isVideoReadyForPlacementTag:tag];
}

- (void)showForPlacementTag:(NSString *)tag {
    [[Tapdaq sharedSession] showVideoForPlacementTag:tag];
}

- (void)load {
    [[Tapdaq sharedSession] loadVideo];
}

- (BOOL)isReady {
    return [[Tapdaq sharedSession] isVideoReady];
}

- (void)show {
    [[Tapdaq sharedSession] showVideo];
}

@end
