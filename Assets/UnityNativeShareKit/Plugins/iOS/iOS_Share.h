//
//  iOS_Share.h
//
//  Created by Nicholas Sheehan on 01/06/2018.
//

#import "UnityAppController.h"
#import "iOS_Alert.h"

@interface iOS_Share : UIViewController
{
    UINavigationController *navController;
}

#ifdef __cplusplus
extern "C" {
#endif
    void shareScreenshotWithText(const char* shareText, const char* imagePath);
    void shareText(const char* shareText);
#ifdef __cplusplus
}
#endif
@end
