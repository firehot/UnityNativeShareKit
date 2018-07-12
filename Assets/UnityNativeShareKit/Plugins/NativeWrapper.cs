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
            shareScreenshotAndText(shareText, filePath);
#endif
        }

        /// <summary>
        /// Shares text
        /// </summary>
        /// <param name="textToShare">Text to share</param>
        /// <param name="showShareDialogBox">Should the share dialog be opened (Android only)</param>
        /// <param name="shareDialogBoxText">The text to show on the share dialog (Android only)</param>
        public static void ShareText(string textToShare, bool showShareDialogBox = true, string shareDialogBoxText = "Select App To Share With")
        {
#if UNITY_EDITOR
            Debug.Log("Attempting to share text \"" + textToShare + "\"");
#elif UNITY_ANDROID
            using (var sharingJavaClass = new AndroidJavaClass(androidPackageName + "." + androidSharingClassName))
            {
                sharingJavaClass.CallStatic(shareTextMethodName, textToShare, showShareDialogBox, shareDialogBoxText);
            }
#elif UNITY_IOS
            shareText(textToShare);
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
#elif UNITY_IOS
            showToast(toastText);
#endif
        }

        /// <summary>
        /// Shows a alert dialog box to the user
        /// </summary>
        /// <param name="alertTitle">Title of the alert</param>
        /// <param name="alertMessage">Message of the alert</param>
        /// <param name="dismissButtonText">Text to show on the dismiss button</param>
        public static void ShowAlert(string alertTitle, string alertMessage, string dismissButtonText = "OK")
        {
#if UNITY_EDITOR
            Debug.Log("Attempting to open a alert \"" + alertTitle + "\"\r\n\"" + alertMessage + "\"");
#elif UNITY_ANDROID
            using (var sharingJavaClass = new AndroidJavaClass(androidPackageName + "." + androidSharingClassName))
            {
                sharingJavaClass.CallStatic(showAlertMethodName, alertTitle, alertMessage, dismissButtonText);
            }
#elif UNITY_IOS
            showAlert(alertTitle, alertMessage, dismissButtonText);
#endif
        }

        #region Android Plugin Constants
#if UNITY_ANDROID
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
#endif
        #endregion

        #region iOS Native Calls
#if UNITY_IOS
        [System.Runtime.InteropServices.DllImport("__Internal")]
        static extern void shareScreenshotAndText(string shareText, string filePath);

        [System.Runtime.InteropServices.DllImport("__Internal")]
        static extern void shareText(string shareText);

        [System.Runtime.InteropServices.DllImport("__Internal")]
        static extern void showToast(string toastText);

        [System.Runtime.InteropServices.DllImport("__Internal")]
        static extern void showAlert(string alertTitle, string alertMessage, string dismissButtonText);
#endif
        #endregion
    }
}
