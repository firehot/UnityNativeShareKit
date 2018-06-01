//
//  iOS_Share.h
//
//  Created by Nicholas Sheehan on 01/06/2018.
//

#import "UnityAppController.h"
#import "iOS_Alert.h"

#ifdef UNITY_4_0 || UNITY_5_0
#import "iPhone_View.h"
#else
extern UIViewController* UnityGetGLViewController();
#endif

#ifdef __cplusplus
extern "C" {
#endif
    void shareScreenshotAndText(const char* shareText, const char* imagePath);
    void shareText(const char* shareText);
#ifdef __cplusplus
}
#endif
