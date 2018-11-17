#import <Foundation/Foundation.h>
#import <Tapdaq/Tapdaq.h>

@interface JsonHelper : NSObject

+ (NSString *) toJsonString:(NSDictionary *) dict;
+ (NSDictionary *) fromJsonString:(NSString *) json;

@end
