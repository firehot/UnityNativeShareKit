#if UNITY_IOS
using System.Runtime.InteropServices;
#else
using UnityEngine;
#endif

namespace NativeSharing
{
    /// <summary>
    /// https://github.com/ChrisMaire/unity-native-sharing
    /// </summary>
    public static class NativeShare
    {
        /// <summary>
        /// Shares a file
        /// </summary>
        /// <param name="shareText">Text to share</param>
        /// <param name="filePath">The path to the attached file</param>
        /// <param name="url">URL to the attached link</param>
        /// <param name="subject">Subject of the share (used for Emails)</param>
        /// <param name="mimeType">The mime type of the content to share
        ///     <para><seealso cref="https://www.freeformatter.com/mime-types-list.html"/></para></param>
        /// <param name="showShareDialogBox">Should the share dialog be opened</param>
        /// <param name="shareDialogBoxText">The text to show on the share dialog</param>
        public static void Share(string shareText, string filePath = null, string url = null, string subject = "", string mimeType = "text/html", bool showShareDialogBox = false, string shareDialogBoxText = "Select sharing app")
        {
            ShareMultiple(shareText, new[] { filePath }, url, subject, mimeType, showShareDialogBox, shareDialogBoxText);
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
            ShareAndroid(shareText, subject, url, filePaths, mimeType, showShareDialogBox, shareDialogBoxText);
#elif UNITY_IOS
            ShareIOS(shareText, subject, url, filePaths);
#else
            Debug.Log("No sharing set up for this platform.");
            Debug.Log("Subject: " + subject);
            Debug.Log("Body: " + shareText);
#endif
        }

#if UNITY_ANDROID
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
        public static void ShareAndroid(string shareText, string subject, string url, string[] filePaths, string mimeType, bool showShareDialogBox, string shareDialogBoxText)
        {
            using (AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent"))
            using (AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent"))
            {
                using (intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"))) { }
                using (intentObject.Call<AndroidJavaObject>("setType", mimeType)) { }
                using (intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject)) { }
                using (intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareText)) { }

                if (!string.IsNullOrEmpty(url))
                {
                    // attach url
                    using (AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri"))
                    using (AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", url))
                    using (intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject)) { }
                }
                else if (filePaths != null)
                {
                    // attach extra files (pictures, pdf, etc.)
                    using (AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri"))
                    using (AndroidJavaObject uris = new AndroidJavaObject("java.util.ArrayList"))
                    {
                        for (var i = 0; i < filePaths.Length; i++)
                        {
                            //instantiate the object Uri with the parse of the url's file
                            using (AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + filePaths[i]))
                                uris.Call<bool>("add", uriObject);
                        }
                        using (intentObject.Call<AndroidJavaObject>("putParcelableArrayListExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uris)) { }
                    }
                }

                // finally start application
                using (AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    if (showShareDialogBox)
                    {
                        AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, shareDialogBoxText);
                        currentActivity.Call("startActivity", jChooser);
                    }
                    else
                    {
                        currentActivity.Call("startActivity", intentObject);
                    }
                }
            }
        }
#endif

#if UNITY_IOS
        public struct ConfigStruct
        {
            public string title;
            public string message;
        }

        [DllImport("__Internal")] static extern void showAlertMessage(ref ConfigStruct conf);

        public struct SocialSharingStruct
        {
            public string text;
            public string subject;
            public string filePaths;
        }

        [DllImport("__Internal")] private static extern void showSocialSharing(ref SocialSharingStruct conf);

        public static void ShareIOS(string title, string message)
        {
            ConfigStruct conf = new ConfigStruct();
            conf.title = title;
            conf.message = message;
            showAlertMessage(ref conf);
        }

        public static void ShareIOS(string body, string subject, string url, string[] filePaths)
        {
            SocialSharingStruct conf = new SocialSharingStruct();
            conf.text = body;
            string paths = string.Join(";", filePaths);
            if (string.IsNullOrEmpty(paths))
                paths = url;
            else if (!string.IsNullOrEmpty(url))
                paths += ";" + url;
            conf.filePaths = paths;
            conf.subject = subject;

            showSocialSharing(ref conf);
        }
#endif
    }
}
