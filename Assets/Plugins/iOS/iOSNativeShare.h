#import "UnityAppController.h"
#import "iOSNativeAlert.h"

@interface iOSNativeShare : UIViewController
{
	UINavigationController *navController;
}

struct SocialSharingStruct {
	char* text;
	char* subject;
	char* filePaths;
};

#ifdef __cplusplus
extern "C" {
#endif
	void showSocialSharing(struct SocialSharingStruct *socialSharingStruct);
#ifdef __cplusplus
}
#endif

@end
