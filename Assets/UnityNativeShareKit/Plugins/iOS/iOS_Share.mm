//
//  iOS_Share.mm
//
//  Created by Nicholas Sheehan on 01/06/2018.
//

#import "iOS_Share.h"

iOS_Share* instance;

void shareScreenshotWithText(const char* shareText, const char* imagePath){
	instance = [iOS_Share withText:shareText withFilePaths:imagePath];
}

void shareText(const char* shareText){
	shareText = [iOS_Share withText:shareText withFilePaths:(char*)@""];
}

@implementation iOS_Share

#ifdef UNITY_4_0 || UNITY_5_0
#import "iPhone_View.h"
#else
extern UIViewController* UnityGetGLViewController();
#endif

+(id) withText:(char*)text withFilePaths:(char*)filePaths{
	return [[iOS_Share alloc] initWithText:text withFilePaths:filePaths];
}

-(id) initWithText:(char*)text withFilePaths:(char*)filePaths{
	self = [super init];
	if( !self ) return self;

	NSString *mText = text ? [[NSString alloc] initWithUTF8String:text] : nil;
	NSString *mfilePath = filePaths ? [[NSString alloc] initWithUTF8String:filePaths] : nil;

    NSMutableArray *items = [NSMutableArray new];
	
	if(mText != NULL && mText.length > 0)
	{
		[items addObject:mText];
	}
	
	if(mfilePath != NULL && mfilePath.length > 0)
	{
        NSArray *paths = [mfilePath componentsSeparatedByString:@";"];
		int i;
		for (i = 0; i < [paths count]; i++) {
			NSString *path = [paths objectAtIndex:i];
			
			if([path hasPrefix:@"http"])
			{
				NSURL *url = [NSURL URLWithString:path];
				NSError *error = nil;
				NSData *dataImage = [NSData dataWithContentsOfURL:url options:0 error:&error];
				
				if (!error) {
					UIImage *imageFromUrl = [UIImage imageWithData:dataImage];
					[items addObject:imageFromUrl];
				} else {
					[items addObject:url];
				}
			}
			else if ( [self isStringValideBase64:path]){
                NSData* imageBase64Data = [[NSData alloc]initWithBase64EncodedString:path options:0];
				UIImage* image = [UIImage imageWithData:imageBase64Data];
				if (image != nil){
					[items addObject:image];
				}
				else{
					NSURL *formattedURL = [NSURL fileURLWithPath:path];
					[items addObject:formattedURL];
				}
			}
			else{
				NSFileManager *fileMgr = [NSFileManager defaultManager];
				if([fileMgr fileExistsAtPath:path]){
					NSData *dataImage = [NSData dataWithContentsOfFile:path];
					UIImage *imageFromUrl = [UIImage imageWithData:dataImage];
					if (imageFromUrl != nil){
						[items addObject:imageFromUrl];
                    }else{
						NSURL *formattedURL = [NSURL fileURLWithPath:path];
						[items addObject:formattedURL];
					}
				}else{
                    
                    char* alertTitle = (char*)"Error";
                    NSString *message = [NSString stringWithFormat:@"Cannot find file %@", path];
                    char* alertMessage = (char*)[message UTF8String];
                    char* alertDismissButtonText = (char*)"OK";
                    
                    showAlert(alertMessage, alertMessage, alertDismissButtonText);
				}
			}
		}
	}
	
	UIActivityViewController *activity = [[UIActivityViewController alloc] initWithActivityItems:items applicationActivities:Nil];
	[activity setValue:@"" forKey:@"subject"];
   	
	UIViewController *rootViewController = UnityGetGLViewController();
    //if iPhone
    if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPhone) {
          [rootViewController presentViewController:activity animated:YES completion:Nil];
    }
    //if iPad
    else {
        // Change Rect to position Popover
        UIPopoverController *popup = [[UIPopoverController alloc] initWithContentViewController:activity];
        [popup presentPopoverFromRect:CGRectMake(rootViewController.view.frame.size.width/2, rootViewController.view.frame.size.height/4, 0, 0)inView:rootViewController.view permittedArrowDirections:UIPopoverArrowDirectionAny animated:YES];
    }
    return self;
}

-(BOOL) isStringValideBase64:(NSString*)string{
    
    NSString *regExPattern = @"^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$";
    
    NSRegularExpression *regEx = [[NSRegularExpression alloc] initWithPattern:regExPattern options:NSRegularExpressionCaseInsensitive error:nil];
    NSUInteger regExMatches = [regEx numberOfMatchesInString:string options:0 range:NSMakeRange(0, [string length])];
    return regExMatches != 0;
}



