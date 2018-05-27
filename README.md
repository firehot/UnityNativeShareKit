# unity-native-sharing
A Unity plugin to open native sharing dialogs on iOS and Android, primarily for sharing screenshots.

To use, call the Share method in NativeShare.cs. See NativeShare.cs for details. For Android, you'll need to have Write Access set to External(SDCard) under Android Build Settings/Other Settings.

You can either hook into the static actions in NativeShare.cs or pass a action for OnFrameBeforeScreenshot and OnFrameAfterScreenshot as parameters to get callbacks for when the screenshot is about to be taken, and when it has been taken. This can be useful if you need to disable some UI or ads and create nice screenshots for sharing. 

Unity screenshots are run asynchronously and as such you will need to check that the file has been written, or put a delay between capturing the screenshot and sharing it using a coroutine. Otherwise you will likely end up trying to access a file that does not yet exist or will access a previous version of the screenshot.

Also, note that some apps cannot reliably accept a link, image, and text at once (at least Facebook and Whatsapp). If you pass both a link and an image to Facebook, the link 'takes over' and the image will not be posted. See this comment for more info: https://github.com/ChrisMaire/unity-native-sharing/issues/19#issuecomment-282422571. Thank you ryhok for letting me know about this!

This plugin combines code from:

Android Native sharing by Daniele Olivieri
http://www.daniel4d.com/blog/sharing-image-unity-android/

iOS Native sharing by Tushar Sonu Lambole 
http://tusharlambole.blogspot.com/2014/06/ios-native-plugin-for-unity3d.html

Huge thank you to both of those folks for sharing their code!

Documentation on Android intents
http://developer.android.com/reference/android/content/Intent.html
Documentation on iOS UIActivityViewController
https://developer.apple.com/library/ios/documentation/UIKit/Reference/UIActivityViewController_Class/index.html

Built in Unity 2017.1f1 and tested on Android 5.1 (HTC One M7) and iOS 10.3 (iPhone 5C)

For iOS builds, you'll need to add a key in info.plist for "NSPhotoLibraryUsageDescription", otherwise when the player will try and save a screenshot to their libary via the share dialog, it will crash the app with this message:
"[access] This app has crashed because it attempted to access privacy-sensitive data without a usage description. The app's Info.plist must contain an NSPhotoLibraryUsageDescription key with a string value explaining to the user how the app uses this data."

by Christopher Maire (http://www.twitter.com/DINOSAURSSSSSSS)
www.chrismaire.com

This software is licensed under the DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE http://www.wtfpl.net/

            DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
                    Version 2, December 2004

 Copyright (C) 2004 Sam Hocevar <sam@hocevar.net>

 Everyone is permitted to copy and distribute verbatim or modified
 copies of this license document, and changing it is allowed as long
 as the name is changed.

            DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
   TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION

  0. You just DO WHAT THE FUCK YOU WANT TO.
