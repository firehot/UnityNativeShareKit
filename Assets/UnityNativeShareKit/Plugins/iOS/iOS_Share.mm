//
//  iOS_Share.mm
//
//  Created by Nicholas Sheehan on 01/06/2018.
//

#import "iOS_Share.h"

void shareText(const char* shareText)
{
    shareScreenshotAndText(shareText, "");
}

void shareScreenshotAndText(const char* shareText, const char* imagePath)
{
    NSString *textToShare = [NSString stringWithUTF8String:shareText];
    NSString *pathToImage = [NSString stringWithUTF8String:imagePath];
    
    NSMutableArray *items = [NSMutableArray new];
    
    if(textToShare != NULL && textToShare.length > 0) [items addObject:textToShare];
    
    if(pathToImage != NULL && pathToImage.length > 0)
    {
        NSFileManager *fileMgr = [NSFileManager defaultManager];
        
        if([fileMgr fileExistsAtPath:pathToImage])
        {
            NSURL *formattedURL = [NSURL fileURLWithPath:pathToImage];
            [items addObject:formattedURL];
        }
        else
        {
            char* alertTitle = (char*)"Error";
            NSString *message = [NSString stringWithFormat:@"Cannot find file %@", pathToImage];
            char* alertMessage = (char*)[message UTF8String];
            char* alertDismissButtonText = (char*)"OK";
            
            showAlert(alertTitle, alertMessage, alertDismissButtonText);
        }
    }
    
    UIActivityViewController *activity = [[UIActivityViewController alloc] initWithActivityItems:items applicationActivities:Nil];
    [activity setValue:@"" forKey:@"subject"];
    
    UIViewController *rootViewController = UnityGetGLViewController();

    //iPhone share view
    if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPhone)
    {
      [rootViewController presentViewController:activity animated:YES completion:nil];
    }
    //iPad share view
    else
    {
        // Change Rect to position Popover
        UIPopoverController *popup = [[UIPopoverController alloc] initWithContentViewController:activity];
        [popup presentPopoverFromRect:CGRectMake(rootViewController.view.frame.size.width/2, rootViewController.view.frame.size.height/4, 0, 0)inView:rootViewController.view permittedArrowDirections:UIPopoverArrowDirectionAny animated:YES];
    }
}
