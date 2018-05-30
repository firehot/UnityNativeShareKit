package com.NicholasSheehan.UnityNativeShareKit;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.support.v4.content.FileProvider;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

import java.io.File;

/**
 * Created by Nicholas Sheehan on 28/05/2018.
 */

public class Sharing {

    /**
     *
     * @param shareText
     * @param filePath
     * @param showShareDialog
     * @param shareDialogBoxText
     */
    public static void OpenShareDialog(String shareText, String filePath, boolean showShareDialog, String shareDialogBoxText){

        //Generate the intent
        Intent intent = new Intent();
        intent.setAction(Intent.ACTION_SEND);
        intent.setType("image/*");
        intent.putExtra(Intent.EXTRA_TEXT, shareText);
        intent.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);

        Context unityContext = UnityPlayer.currentActivity.getApplicationContext();

        // This will generate a uri based that will get the screenshot we have allowed Android to see.
        // This is done this way to prevent a UriFileException on newer targets of Android
        Uri imageUri = FileProvider.getUriForFile(unityContext, unityContext.getPackageName() + ".provider", new File(filePath));

        intent.putExtra(Intent.EXTRA_STREAM, imageUri);

        Intent shareIntent = Intent.createChooser(intent, shareDialogBoxText);
        UnityPlayer.currentActivity.startActivity(shareIntent);
    }
}
