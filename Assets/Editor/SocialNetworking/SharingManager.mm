//
//  SharingManager.mm
//  Unity-iPhone
//
//  Created by Mike Desaro on 1/28/13.
//
//

#import "SharingManager.h"


#if UNITY_VERSION < 500
void UnityPause( bool pause );
#else
void UnityPause( int pause );
#endif

void UnitySendMessage( const char * className, const char * methodName, const char * param );
UIView *UnityGetGLView();
UIViewController *UnityGetGLViewController();


@implementation SharingManager

+ (SharingManager*)sharedManager
{
	static dispatch_once_t pred;
	static SharingManager *_sharedInstance = nil;
	
	dispatch_once( &pred, ^{ _sharedInstance = [[self alloc] init]; } );
	return _sharedInstance;
}


+ (void)shareItems:(NSArray*)items excludedActivityTypes:(NSArray*)excludedActivityTypes
{
	if( !NSClassFromString( @"UIActivityViewController" ) )
	{
		NSLog( @"Not running on iOS 6 or later so sharing disabled" );
		return;
	}
	
	UnityPause( true );
	
	UIActivityViewController *activityController = [[[UIActivityViewController alloc] initWithActivityItems:items applicationActivities:nil] autorelease];
	activityController.excludedActivityTypes = excludedActivityTypes;

	if( [activityController respondsToSelector:@selector(setCompletionWithItemsHandler:)] )
	{
#if __IPHONE_OS_VERSION_MAX_ALLOWED >= 80000
		activityController.completionWithItemsHandler = ^( NSString *activityType, BOOL completed, NSArray *returnedItems, NSError *activityError )
		{
			if( activityError )
				NSLog( @"UIActivityViewController error: %@", activityError );
			if( activityType )
				NSLog( @"UIActivityViewController activityType: %@", activityType );
			if( returnedItems )
				NSLog( @"UIActivityViewController returnedItems: %@", returnedItems );

			UnityPause( false );

			if( completed )
				UnitySendMessage( "SharingManager", "sharingFinishedWithActivityType", activityType.UTF8String );
			else
				UnitySendMessage( "SharingManager", "sharingCancelled", "" );
		};
#endif
	}
	else
	{
		activityController.completionHandler = ^( NSString *activityType, BOOL completed )
		{
			UnityPause( false );
			
			if( completed )
				UnitySendMessage( "SharingManager", "sharingFinishedWithActivityType", activityType.UTF8String );
			else
				UnitySendMessage( "SharingManager", "sharingCancelled", "" );
		};
	}

#if __IPHONE_OS_VERSION_MAX_ALLOWED >= 80000
	if( UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad && [activityController respondsToSelector:@selector(setCompletionWithItemsHandler:)] )
	{
		activityController.popoverPresentationController.sourceView = UnityGetGLView();
		activityController.popoverPresentationController.sourceRect = CGRectMake( [SharingManager sharedManager].popoverRootPoint.x, [SharingManager sharedManager].popoverRootPoint.y, 5, 5 );
	}
#endif

	[UnityGetGLViewController() presentViewController:activityController animated:YES completion:NULL];
}

@end
