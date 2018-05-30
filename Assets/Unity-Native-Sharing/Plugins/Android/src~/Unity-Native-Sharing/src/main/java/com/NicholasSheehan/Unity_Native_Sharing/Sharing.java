package com.NicholasSheehan.Unity_Native_Sharing;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.support.v4.content.FileProvider;

import com.unity3d.player.UnityPlayer;

import java.io.File;

/**
 * Created by Nicholas Sheehan on 28/05/2018.
 */

public class Sharing {

    public static void OpenShareDialog(String shareText, String filePath, boolean showShareDialog, String shareDialogBoxText){

        Intent intent = new Intent();
        intent.setAction(Intent.ACTION_SEND);
        intent.setType("image/png");
        intent.putExtra(Intent.EXTRA_TEXT, shareText);

        Context unityContext = UnityPlayer.currentActivity.getApplicationContext();
        //Uri imageUri = SharingFileProvider.getUriForFile(unityContext, unityContext.getApplicationContext().getPackageName() + ".provider", new File(filePath));
        Uri imageUri = FileProvider.getUriForFile(unityContext, BuildConfig.APPLICATION_ID + ".provider", new File(filePath));

        intent.putExtra(Intent.EXTRA_STREAM, imageUri);
        intent.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);

        Intent shareIntent = Intent.createChooser(intent, shareDialogBoxText);
        UnityPlayer.currentActivity.startActivity(shareIntent);
    }
}

//    public static void ShareAndroid(string shareText, string subject, string url, string[] filePaths, string mimeType, bool showShareDialogBox, string shareDialogBoxText)
//    {
//        using (AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent"))
//        using (AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent"))
//        {
//            using (intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"))) { }
//            using (intentObject.Call<AndroidJavaObject>("setType", mimeType)) { }
//            using (intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject)) { }
//            using (intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareText)) { }
//
//            if (!string.IsNullOrEmpty(url))
//            {
//                // attach url
//                using (AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri"))
//                using (AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", url))
//                using (intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject)) { }
//            }
//            else if (filePaths != null)
//            {
//                // attach extra files (pictures, pdf, etc.)
//                using (AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri"))
//                using (AndroidJavaObject uris = new AndroidJavaObject("java.util.ArrayList"))
//                {
//                    for (var i = 0; i < filePaths.Length; i++)
//                    {
//                        //instantiate the object Uri with the parse of the url's file
//                        using (AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + filePaths[i]))
//                        uris.Call<bool>("add", uriObject);
//                    }
//                    using (intentObject.Call<AndroidJavaObject>("putParcelableArrayListExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uris)) { }
//                }
//            }
//
//            // finally start application
//            using (AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
//            using (AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity"))
//            {
//                if (showShareDialogBox)
//                {
//                    AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, shareDialogBoxText);
//                    currentActivity.Call("startActivity", jChooser);
//                }
//                else
//                {
//                    currentActivity.Call("startActivity", intentObject);
//                }
//            }
//        }
//    }