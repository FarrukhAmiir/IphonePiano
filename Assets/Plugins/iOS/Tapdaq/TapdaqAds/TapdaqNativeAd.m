#import "TapdaqNativeAd.h"
#import "JsonHelper.h"

@implementation TapdaqNativeAd

+ (instancetype)sharedInstance
{
    static dispatch_once_t once;
    static id sharedInstance;
    dispatch_once(&once, ^{
        sharedInstance = [[self alloc] init];
    });
    return sharedInstance;
}

- (instancetype)init
{
    self = [super init];
    if (self) {
        self.nativeAds = [[NSMutableDictionary alloc] init];
        [self initTypesDictionary];
    }
    return self;
}

- (void) initTypesDictionary
{
    self.nativeAdTypesDictionary = @{
                                     @"TDNativeAdType1x1Large": @((int)TDNativeAdType1x1Large),
                                     @"TDNativeAdType1x1Medium": @((int)TDNativeAdType1x1Medium),
                                     @"TDNativeAdType1x1Small": @((int)TDNativeAdType1x1Small),
                                     
                                     @"TDNativeAdType1x2Large": @((int)TDNativeAdType1x2Large),
                                     @"TDNativeAdType1x2Medium": @((int)TDNativeAdType1x2Medium),
                                     @"TDNativeAdType1x2Small": @((int)TDNativeAdType1x2Small),
                                     
                                     @"TDNativeAdType2x1Large": @((int)TDNativeAdType2x1Large),
                                     @"TDNativeAdType2x1Medium": @((int)TDNativeAdType2x1Medium),
                                     @"TDNativeAdType2x1Small": @((int)TDNativeAdType2x1Small),
                                     
                                     @"TDNativeAdType2x3Large": @((int)TDNativeAdType2x3Large),
                                     @"TDNativeAdType2x3Medium": @((int)TDNativeAdType2x3Medium),
                                     @"TDNativeAdType2x3Small": @((int)TDNativeAdType2x3Small),
                                     
                                     @"TDNativeAdType3x2Large": @((int)TDNativeAdType3x2Large),
                                     @"TDNativeAdType3x2Medium": @((int)TDNativeAdType3x2Medium),
                                     @"TDNativeAdType3x2Small": @((int)TDNativeAdType3x2Small),
                                     
                                     @"TDNativeAdType1x5Large": @((int)TDNativeAdType1x5Large),
                                     @"TDNativeAdType1x5Medium": @((int)TDNativeAdType1x5Medium),
                                     @"TDNativeAdType1x5Small": @((int)TDNativeAdType1x5Small),
                                     
                                     @"TDNativeAdType5x1Large": @((int)TDNativeAdType5x1Large),
                                     @"TDNativeAdType5x1Medium": @((int)TDNativeAdType5x1Medium),
                                     @"TDNativeAdType5x1Small": @((int)TDNativeAdType5x1Small)
                                     };
}

- (void)loadNativeAdvertForPlacementTag:(const char*)tag nativeAdType:(const char*)adType
{
    TDNativeAdType nativeAdType = [self getTypeFromCString: adType];
    NSLog(@"loadNativeAdvertForPlacementTag: %@ nativeAdType: %d", [NSString stringWithUTF8String: tag], (int)nativeAdType);
    [[Tapdaq sharedSession] loadNativeAdvertForPlacementTag: [NSString stringWithUTF8String: tag] adType: nativeAdType];
}

- (void)loadNativeAdvertForAdType:(const char*)adType
{
    TDNativeAdType nativeAdType = [self getTypeFromCString: adType];
    [[Tapdaq sharedSession] loadNativeAdvertForAdType: nativeAdType];
}

- (char const*)fetchNativeForAdType:(const char*)adType
{
    TDNativeAdType nativeAdType = [self getTypeFromCString: adType];
    TDNativeAdvert *nativeAdvert = [[Tapdaq sharedSession] getNativeAdvertForAdType: nativeAdType];
    
    return [self fetchNativeAd: nativeAdvert];
}

- (char const*)fetchNativeForAdWithTag:(const char*)tag
                                AdType:(const char*)adType
{
    TDNativeAdType nativeAdType = [self getTypeFromCString: adType];
    
    TDNativeAdvert *nativeAdvert = [[Tapdaq sharedSession] getNativeAdvertForPlacementTag:
                                    [NSString stringWithCString:tag encoding:NSUTF8StringEncoding]
                                                                                   adType:nativeAdType];
    
    return [self fetchNativeAd: nativeAdvert];
}

- (char const*) fetchNativeAd:(TDNativeAdvert *) nativeAd
{
    if (nativeAd != nil) {
        
        NSData *imageData = UIImagePNGRepresentation(nativeAd.creative.image);
        NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
        NSString *documentsDirectory = [paths objectAtIndex:0];
        NSString *filePath = [documentsDirectory stringByAppendingPathComponent:@"selected2.png"]; //Add the file name
        [imageData writeToFile:filePath atomically:YES];
        
        imageData = UIImagePNGRepresentation(nativeAd.icon);
        NSString *iconPath = [documentsDirectory stringByAppendingPathComponent:@"iconSelected2.png"]; //Add the file name
        [imageData writeToFile:iconPath atomically:YES];
        
        NSString *uniqueId = [NSString stringWithFormat:@"%@-%@", [self getStringFromAdType: nativeAd.adType], nativeAd.subscriptionId];
        
        NSDictionary* dict = @{
                               @"applicationId": nativeAd.applicationId,
                               @"targetingId": nativeAd.targetingId,
                               @"subscriptionId": nativeAd.subscriptionId,
                               @"appName": nativeAd.appName,
                               @"description": nativeAd.appDescription,
                               @"buttonText": nativeAd.buttonText,
                               @"developerName": nativeAd.developerName,
                               @"ageRating": nativeAd.ageRating,
                               @"appSize": @(nativeAd.appSize),
                               @"averageReview": @(nativeAd.averageReview),
                               @"totalReviews": @(nativeAd.totalReviews),
                               @"category": nativeAd.category,
                               @"appVersion": nativeAd.appVersion,
                               @"price": @(nativeAd.price),
                               @"iconUrl": [nativeAd.iconUrl absoluteString],
                               @"iconPath": iconPath,
                               @"creativeIdentifier": nativeAd.creative.identifier,
                               @"imageUrl": [nativeAd.creative.url absoluteString],
                               @"uniqueId": uniqueId
                               };
        self.nativeAds[uniqueId] = nativeAd;
        NSLog(@"save nativeAd for: %@", uniqueId);
        return [[JsonHelper toJsonString: dict] UTF8String];
    }
    else
    {
        NSLog(@"No nativeAd available");
        return [@"{}" UTF8String];
    }
    
}

- (void)triggerClickForNativeAdvert:(const char*)uniqueIdChar
{
    NSString *uniqueId = [NSString stringWithUTF8String: uniqueIdChar];
    if(self.nativeAds[uniqueId] != nil) {
        [self.nativeAds[uniqueId] triggerClick];
        NSLog(@"Trigger click for: %@", uniqueId);
    } else {
        NSLog(@"Could not find nativeAd with uniquId: %@", uniqueId);
    }
}

-(void)triggerImpressionForNativeAdvert:(const char*)uniqueIdChar
{
    NSString *uniqueId = [NSString stringWithUTF8String: uniqueIdChar];
    if(self.nativeAds[uniqueId] != nil) {
        [self.nativeAds[uniqueId] triggerImpression];
        NSLog(@"Trigger impression for: %@", uniqueId);
    } else {
        NSLog(@"Could not find nativeAd with uniquId: %@", uniqueId);
    }
}

-(TDNativeAdType) getTypeFromCString:(char const*) cString {
    
    NSString* typeString = [NSString stringWithUTF8String: cString];
    NSNumber* typeNumber = [self.nativeAdTypesDictionary objectForKey: typeString];
    
    if(typeNumber == nil) {
        NSLog(@"ERROR! getTypeFromCString. Wrong type string: %@", typeString);
        return TDNativeAdType1x1Small;
    }
    
    return (TDNativeAdType)[typeNumber intValue];
}

-(NSString *) getStringFromAdType:(TDNativeAdType) adType {
    
    NSArray* typeNumbers = [self.nativeAdTypesDictionary allKeysForObject: [NSNumber numberWithInt: (int)adType]];
    if(typeNumbers.count < 1)
        return @"TDNativeAdType1x1Large";
    return typeNumbers[0];
}

@end
