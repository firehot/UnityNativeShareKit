#import "iOSNativeAlert.h"

void showAlertMessage(struct AlertMessageStruct *alertStruct){

    NSString *title = [NSString stringWithUTF8String:alertStruct->alertTitle];
    NSString *message = [NSString stringWithUTF8String:alertStruct->alertMessage];
    NSString *cancelButtonText = [NSString stringWithUTF8String:alertStruct->alertCancelButtonText];
    
	UIAlertView *alert = [[UIAlertView alloc] initWithTitle:title
	                      	                      message:message
	                      	                      delegate:nil
	                      	                      cancelButtonTitle:cancelButtonText
	                      	                      otherButtonTitles: nil];
	[alert show];
}
