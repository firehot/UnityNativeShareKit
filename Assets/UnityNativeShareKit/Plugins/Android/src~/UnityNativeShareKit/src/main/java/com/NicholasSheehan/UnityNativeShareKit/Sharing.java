package com.NicholasSheehan.UnityNativeShareKit;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.net.Uri;
import android.support.v4.content.FileProvider;
import android.widget.Toast;

import com.unity3d.player.UnityPlayer;

import java.io.File;

/**
 * Created by Nicholas Sheehan on 28/05/2018.
 */

public class Sharing {

    /**
     * Shares text and a image to an app
     * @param shareText - Text to share to the app
     * @param imagePath - Path to the file to share
     * @param showShareDialog - Should the share dialog be opened
     * @param shareDialogBoxText - Title of the share dialog
     */
    public static void ShareScreenshotAndText(String shareText, String imagePath, boolean showShareDialog, String shareDialogBoxText){

        //Generate the intent
        Intent intent = new Intent();
        intent.setAction(Intent.ACTION_SEND);
        intent.setType("image/*");
        intent.putExtra(Intent.EXTRA_TEXT, shareText);
        intent.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);

        Context unityContext = UnityPlayer.currentActivity.getApplicationContext();

        // This will generate a uri based that will get the screenshot we have allowed Android to see.
        // This is done this way to prevent a UriFileException on newer targets of Android
        Uri imageUri = FileProvider.getUriForFile(unityContext, unityContext.getPackageName() + ".provider", new File(imagePath));

        intent.putExtra(Intent.EXTRA_STREAM, imageUri);

        Intent shareIntent = Intent.createChooser(intent, shareDialogBoxText);

        if(showShareDialog) UnityPlayer.currentActivity.startActivity(shareIntent);
        else UnityPlayer.currentActivity.startActivity(intent);
    }

    /**
     * Shares text to an app
     * @param shareText - Text to share to the app
     * @param showShareDialog - Should the share dialog be opened
     * @param shareDialogBoxText - Title of the share dialog
     */
    public static void ShareText(String shareText, boolean showShareDialog, String shareDialogBoxText){

        //Generate the intent
        Intent intent = new Intent();
        intent.setAction(Intent.ACTION_SEND);
        intent.setType("text/plain");
        intent.putExtra(Intent.EXTRA_TEXT, shareText);

        Intent shareIntent = Intent.createChooser(intent, shareDialogBoxText);

        if(showShareDialog) UnityPlayer.currentActivity.startActivity(shareIntent);
        else UnityPlayer.currentActivity.startActivity(intent);
    }

    /**
     * Displays a toast to the user
     * @param toastText - Text to display on the toast
     * @param showLongToast - Should a long toast be shown?
     */
    public static void ShowToast(String toastText, boolean showLongToast)
    {
        Toast.makeText(UnityPlayer.currentActivity, toastText, showLongToast ? Toast.LENGTH_LONG : Toast.LENGTH_SHORT).show();
    }

    /**
     *  Shows a alert to the user
     * @param alertTitle - Title of the alert
     * @param alertText - Text of the alert
     * @param cancelButtonText - Text to display on the cancel button
     */
    public static void ShowAlert(String alertTitle, String alertText, String cancelButtonText)
    {
        AlertDialog.Builder builder = new AlertDialog.Builder(UnityPlayer.currentActivity);
        builder.setTitle(alertTitle)
                .setMessage(alertText)
                .setCancelable(false)
                .setPositiveButton(cancelButtonText, new DialogInterface.OnClickListener() {
                    //by default doing nothing just closes the dialog
                    //this does nothing, it can do stuff though
                    public void onClick(DialogInterface dialog, int id) {}
                });
        AlertDialog alert = builder.create();
        alert.show();
    }
}