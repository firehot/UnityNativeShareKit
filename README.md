# UnityNativeShareKit
A Unity plugin to open native sharing dialogs on iOS and Android, used mainly for sharing screenshots.

Please note, if you are coming from Unity-Native-Share, that some of the method calls have changed, this is a part of an ongoing refactor to help make the plugin more modular, which should help with narrowing down bugs and also creating new features without introducing bugs else where, also 300+ line classes aren't fun or practical.

Some of the things that have been removed:
* Email sharing
  * Compicated the code
* Sharing multiple files
  * Also complicated code, and also was inconsistant
* Sharing Links w/ Text
  * Never got it to work in the first place
 
## Features

- Share Text
- Share Screenshots w/ Text
- Show Alert Dialog Boxes
- Show Toasts (Android Only, Looking into iOS equivalent)

## Usage:
To use, call the Share method in [NativeShare.cs](UnityNativeShareKit/Assets/Plugins/NativeShare.cs). See the comments in [NativeShare.cs](UnityNativeShareKit/Assets/Plugins/NativeShare.cs) for details.

You can hook into the actions in [Test.cs](Assets/Native%20Share%20Demo%20Scene/Test.cs) to get callbacks for when the screenshot is about to be taken, and when it has been taken. The callbacks can also be accessed via the Inspector.
This can be useful if you need to disable some UI or banner ads and create nice screenshots for sharing.

Unity screenshots are run asynchronously and as such you will need to check that the file has been written, or put a delay between capturing the screenshot and sharing it using a coroutine. Otherwise you will likely end up trying to access a file that does not yet exist or will access a previous version of the screenshot.

### This plugin contains modified code from:
 - iOS Native sharing by Tushar Sonu Lambole: http://tusharlambole.blogspot.com/2014/06/ios-native-plugin-for-unity3d.html

### Native Documentation:
 - Android Intents: http://developer.android.com/reference/android/content/Intent.html
 - Android FileProvider: https://developer.android.com/reference/android/support/v4/content/FileProvider
 - iOS UIActivityViewController: https://developer.apple.com/library/ios/documentation/UIKit/Reference/UIActivityViewController_Class/index.html

### Testing
Built with `Unity 2017.1.1f1`

#### Screenshot and text
Facebook, Messenger and Instagram cannot share text at all. [Here is more info about it](https://answers.unity.com/questions/871846/can-i-post-to-facebook-with-my-own-text.html)

--            | `Android 8.0.0 (OnePlus 3T / OxygenOS 5.0.1)`| `iOS 10.3.3 (iPhone 5C)`               | `iOS 11.3.1 (iPad 2 Air)`
------------- | ---------------------------------------------| ---------------------------------------| ------------------------ 
Facebook      | No Text                                      | No Text                                | No Text
Messenger     | No Text                                      | No Text                                | No Text
Instagram     | No Text                                      | No Text (Needs Access to Photo Library)| No Text (Needs Access to Photo Library)
Twitter       | ✔                                           | ✔                                     | ✔
WhatsApp      | ✔                                           | (WhatsApp not loading properly)        | N/A
Discord       | ✔                                           | ✔                                     | ✔

### Platform Notes
#### iOS
For iOS builds, you'll need to add a key in the apps `info.plist` for `NSPhotoLibraryUsageDescription`, otherwise when the player tries to save a screenshot to their library via the share dialog, it will crash the app with this message:
```
"[access] This app has crashed because it attempted to access privacy-sensitive data without a usage description. The app's Info.plist must contain an NSPhotoLibraryUsageDescription key with a string value explaining to the user how the app uses this data."
```

#### Android
The Android plugin requires `android.support-v4:26.1.0` to run.

The Android plugin has support for [Play Services Resolver for Unity](https://github.com/googlesamples/unity-jar-resolver), so no need to go and find the library files manually and worry about library conflicts with other plugins. By default, [Play Services Resolver for Unity](https://github.com/googlesamples/unity-jar-resolver) is not included in the project. Built and tested using [Play Services Resolver v1.2.68.0](https://github.com/googlesamples/unity-jar-resolver/blob/9941cb212b63ee130565b65baf3f0cd69678546b/play-services-resolver-1.2.68.0.unitypackage)

### Todo / Notes
- [ ] Move all existing issues in the original repo and fix them here
  * [Using a tool called Issue Mover for GitHub](https://github-issue-mover.appspot.com/)
- [ ] Runtime gifs
  * [Look into this to create gifs](https://github.com/Maximus5/gif-animate)
- [ ] [Create generic function that shares any type of file](https://github.com/ChrisMaire/unity-native-sharing/issues/25)

### Credits
by Nicholas Sheehan (http://www.twitter.com/NSheehanDev)

[www.NicholasSheehan.co.uk](www.NicholasSheehan.co.uk)

[If you find this repo helpful, please consider donating to help me spend more time maintaning and expaning the plugin!](https://www.paypal.me/NicholasSheehan/5.00)

[Repo forked from unity-native-sharing](https://github.com/ChrisMaire/unity-native-sharing)

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
