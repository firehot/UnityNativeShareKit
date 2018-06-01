//
//  iOS_Alert.mm
//
//  Created by Nicholas Sheehan on 01/06/2018.
//

#import "iOS_Alert.h"

void showAlertMessage(const char* alertTitle, const char* alertMessage, const char* dismissButtonText){

    NSString *title = [alertTitle];
    NSString *message = [alertMessage];
    NSString *cancelButtonText = [NSString stringWithUTF8String:dismissButtonText];
    
	UIAlertView *alert = [[UIAlertView alloc] initWithTitle:title
	                      	                      message:message
	                      	                      delegate:nil
	                      	                      cancelButtonTitle:dismissButtonText
	                      	                      otherButtonTitles: nil];
	[alert show];
}
