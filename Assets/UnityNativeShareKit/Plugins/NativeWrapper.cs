#if UNITY_IOS
using System.Runtime.InteropServices;
#else
using UnityEngine;
#endif

namespace UnityNativeShareKit
{
    //TODO URLS
    //TODO files?
    //TODO emails?
    //TODO Create runtime gifs?
    //TODO facebook / messenger SDK intergration
    public static class NativeWrapper
    {
        /// <summary>
        /// Android package name
        /// </summary>
        const string packageName = "com.NicholasSheehan.UnityNativeShareKit";

        /// <summary>
        /// Android class name
        /// </summary>
        const string className = "Sharing";

        /// <summary>
        /// Android method name to call to share a screenshot
        /// </summary>
        const string shareScreenshotMethodName = "OpenShareDialog";

        /// <summary>
        /// Shares a screenshot with text
        /// </summary>
        /// <param name="shareText">Text to share</param>
        /// <param name="filePath">The path to the attached file</param>
        /// <param name="showShareDialogBox">Should the share dialog be opened (Android only)</param>
        /// <param name="shareDialogBoxText">The text to show on the share dialog (Android only)</param>
        public static void ShareScreenshot(string shareText, string filePath, bool showShareDialogBox = true, string shareDialogBoxText = "Select App To Share With")
        {
#if UNITY_EDITOR
            Debug.Log("Attempting to share a screenshot with the text \"" + shareText + "\"");
#elif UNITY_ANDROID
            using (var sharingJavaClass = new AndroidJavaClass(packageName + "." + className))
            {
                sharingJavaClass.CallStatic(methodName, shareText, filePath, showShareDialogBox, shareDialogBoxText);
            }
#elif UNITY_IOS
            //TODO
#endif
        }

        /// <summary>
        /// Shares multiple files at once
        /// </summary>
        /// <param name="shareText">Text to share</param>
        /// <param name="filePaths">The paths to the attached files</param>
        /// <param name="url">URL to the attached link</param>
        /// <param name="subject">Subject of the share (used for Emails)</param>
        /// <param name="mimeType">The mime type of the content to share
        ///     <para><seealso cref="https://www.freeformatter.com/mime-types-list.html"/></para></param>
        /// <param name="showShareDialogBox">Should the share dialog be opened</param>
        /// <param name="shareDialogBoxText">The text to show on the share dialog</param>
        public static void ShareMultiple(string shareText, string[] filePaths = null, string url = null, string subject = "", string mimeType = "text/html", bool showShareDialogBox = false, string shareDialogBoxText = "Select sharing app")
        {
#if UNITY_ANDROID
            //ShareAndroid(shareText, filePaths, showShareDialogBox, shareDialogBoxText);
#elif UNITY_IOS
            ShareIOS(shareText, subject, url, filePaths);
#else
            Debug.Log("No sharing set up for this platform.");
            Debug.Log("Subject: " + subject);
            Debug.Log("Body: " + shareText);
#endif
        }

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
            public string alertCancelButtonText;
        }

        /// <summary>
        /// External call to the C / Obj-C layer of the iOS app
        /// </summary>
        /// <param name="alertMessageStruct">The alert message information</param>
        [DllImport("__Internal")]
        static extern void showAlertMessage(ref AlertMessageStruct alertMessageStruct);

        /// <summary>
        /// Displays a dialog box with a custom title, message and cancel button text
        /// </summary>
        /// <param name="alertTitle">Alert title</param>
        /// <param name="alertMessage">Alert message</param>
        /// <param name="alertCancelButtonText">Alert cancel button text</param>
        public static void AlertMessageIOS(string alertTitle, string alertMessage, string alertCancelButtonText = "OK")
        {
            var alertMessageStruct = new AlertMessageStruct
            {
                alertTitle = alertTitle,
                alertMessage = alertMessage,
                alertCancelButtonText = alertCancelButtonText
            };
            showAlertMessage(ref alertMessageStruct);
        }
#endregion

        /// <summary>
        /// External call to the C / Obj-C layer of the iOS app
        /// </summary>
        /// <param name="alertMessageStruct">The alert message information</param>
        [DllImport("__Internal")]
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
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="url"></param>
        /// <param name="filePaths"></param>
        public static void ShareIOS(string body, string subject, string url, string[] filePaths)
        {
            var socialSharingStruct = new SocialSharingStruct
            {
                text = body
            };

            var paths = string.Join(";", filePaths);

            if (string.IsNullOrEmpty(paths)) paths = url;
            else if (!string.IsNullOrEmpty(url)) paths += ";" + url;

            socialSharingStruct.filePaths = paths;
            socialSharingStruct.subject = subject;

            showSocialSharing(ref socialSharingStruct);
        }
#endif
    }
}
