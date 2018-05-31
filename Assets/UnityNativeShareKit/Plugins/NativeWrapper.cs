using UnityEngine;

namespace UnityNativeShareKit
{
    public static class NativeWrapper
    {
        /// <summary>
        /// Shares a screenshot with text
        /// </summary>
        /// <param name="shareText">Text to share</param>
        /// <param name="filePath">The path to the attached file</param>
        /// <param name="showShareDialogBox">Should the share dialog be opened (Android only)</param>
        /// <param name="shareDialogBoxText">The text to show on the share dialog (Android only)</param>
        public static void ShareScreenshotAndText(string shareText, string filePath, bool showShareDialogBox = true, string shareDialogBoxText = "Select App To Share With")
        {
#if UNITY_EDITOR
            Debug.Log("Attempting to share a screenshot with the text \"" + shareText + "\"");
#elif UNITY_ANDROID
            using (var sharingJavaClass = new AndroidJavaClass(androidPackageName + "." + androidSharingClassName))
            {
                sharingJavaClass.CallStatic(shareScreenshotWithTextMethodName, shareText, filePath, showShareDialogBox, shareDialogBoxText);
            }
#elif UNITY_IOS
            ShareIOS(shareText, filePath);
#endif
        }

        /// <summary>
        /// Shares text
        /// </summary>
        /// <param name="shareText">Text to share</param>
        /// <param name="showShareDialogBox">Should the share dialog be opened (Android only)</param>
        /// <param name="shareDialogBoxText">The text to show on the share dialog (Android only)</param>
        public static void ShareText(string shareText, bool showShareDialogBox = true, string shareDialogBoxText = "Select App To Share With")
        {
#if UNITY_EDITOR
            Debug.Log("Attempting to share text \"" + shareText + "\"");
#elif UNITY_ANDROID
            using (var sharingJavaClass = new AndroidJavaClass(androidPackageName + "." + androidSharingClassName))
            {
                sharingJavaClass.CallStatic(shareTextMethodName, shareText, showShareDialogBox, shareDialogBoxText);
            }
#elif UNITY_IOS
            ShareIOS(shareText, null);
#endif
        }

        /// <summary>
        /// Shows a toast to the user (Android Only, for iOS, use <seealso cref="ShowAlert"/>)
        /// </summary>
        /// <param name="toastText">Text to display on the toast</param>
        /// <param name="shouldLongToastBeShown">Should the toast duration be long?</param>
        public static void ShowToast(string toastText, bool shouldLongToastBeShown = false)
        {
#if UNITY_EDITOR
            Debug.Log("Attempting to toast text \"" + toastText + "\"");
#elif UNITY_ANDROID
            using (var sharingJavaClass = new AndroidJavaClass(androidPackageName + "." + androidSharingClassName))
            {
                sharingJavaClass.CallStatic(showToastMethodName, toastText, shouldLongToastBeShown);
            }
#endif
        }

        /// <summary>
        /// Shows a alert dialog box to the user
        /// </summary>
        /// <param name="alertTitle">Title of the alert</param>
        /// <param name="alertText">Text of the alert</param>
        /// <param name="dismissButtonText">Text to show on the dismiss button</param>
        public static void ShowAlert(string alertTitle, string alertText, string dismissButtonText = "OK")
        {
#if UNITY_EDITOR
            Debug.Log("Attempting to open a alert \"" + alertTitle + "\"\r\n\"" + alertText + "\"");
#elif UNITY_ANDROID
            using (var sharingJavaClass = new AndroidJavaClass(androidPackageName + "." + androidSharingClassName))
            {
                sharingJavaClass.CallStatic(showAlertMethodName, alertTitle, alertText, dismissButtonText);
            }
#elif UNITY_IOS
            AlertMessageIOS(alertTitle, alertText, dismissButtonText);
#endif
        }

        #region Android Plugin Calls
        #region Android Plugin Constants
        /// <summary>
        /// Android package name
        /// </summary>
        const string androidPackageName = "com.NicholasSheehan.UnityNativeShareKit";

        /// <summary>
        /// Android class name
        /// </summary>
        const string androidSharingClassName = "Sharing";

        /// <summary>
        /// Android method name to call to share a screenshot with text
        /// </summary>
        const string shareScreenshotWithTextMethodName = "ShareScreenshotAndText";

        /// <summary>
        /// Android method name to call to share text
        /// </summary>
        const string shareTextMethodName = "ShareText";

        /// <summary>
        /// Android method name to show a toast
        /// </summary>
        const string showToastMethodName = "ShowToast";

        /// <summary>
        /// Android method name to show a alert
        /// </summary>
        const string showAlertMethodName = "ShowAlert";
        #endregion
        #endregion


        #region iOS Native Calls
#if UNITY_IOS
        #region Alert
        /// <summary>
        /// Struct for iOS alerts
        /// </summary>
        struct AlertMessageStruct
        {
            /// <summary>
            /// Alert title
            /// </summary>
            public string alertTitle;

            /// <summary>
            /// Alert message
            /// </summary>
            public string alertMessage;

            /// <summary>
            /// Alert cancel button text
            /// </summary>
            public string alertDismissButtonText;
        }

        /// <summary>
        /// External call to the C / Obj-C layer of the iOS app
        /// </summary>
        /// <param name="alertMessageStruct">The alert message information</param>
        [System.Runtime.InteropServices.DllImport("__Internal")]
        static extern void showAlertMessage(ref AlertMessageStruct alertMessageStruct);

        /// <summary>
        /// Displays a dialog box with a custom title, message and cancel button text
        /// </summary>
        /// <param name="alertTitle">Alert title</param>
        /// <param name="alertMessage">Alert message</param>
        /// <param name="alertDismissButtonText">Alert cancel button text</param>
        public static void AlertMessageIOS(string alertTitle, string alertMessage, string alertDismissButtonText = "OK")
        {
            var alertMessageStruct = new AlertMessageStruct
            {
                alertTitle = alertTitle,
                alertMessage = alertMessage,
                alertDismissButtonText = alertDismissButtonText
            };
            showAlertMessage(ref alertMessageStruct);
        }
        #endregion

        /// <summary>
        /// External call to the C / Obj-C layer of the iOS app
        /// </summary>
        /// <param name="alertMessageStruct">The alert message information</param>
        [System.Runtime.InteropServices.DllImport("__Internal")]
        static extern void showSocialSharing(ref SocialSharingStruct conf);

        /// <summary>
        /// 
        /// </summary>
        struct SocialSharingStruct
        {
            public string text;
            public string subject;
            public string filePaths;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shareText"></param>
        /// <param name="filePath"></param>
        public static void ShareIOS(string shareText, string filePath)
        {
            var socialSharingStruct = new SocialSharingStruct
            {
                text = shareText,
                filePaths = filePath,
                //this is kept here so we dont have to change the iOS plugin just yet
                subject = ""
            };
            
            showSocialSharing(ref socialSharingStruct);
        }
#endif
        #endregion
    }
}
