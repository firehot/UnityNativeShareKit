using UnityEngine;
using System.Collections;
using System.IO;
using NativeSharing;

/*
 * https://github.com/ChrisMaire/unity-native-sharing
 */
public class Test : MonoBehaviour
{
    /// <summary>
    /// Name of the screenshot
    /// </summary>
    [SerializeField]
    string ScreenshotName = "screenshot.png";

    /// <summary>
    /// Shares a screenshot with text
    /// </summary>
    /// <param name="text">Text to share along side the screenshot</param>
    public void ShareScreenshotWithText(string text)
    {
        var screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
        if (File.Exists(screenShotPath)) File.Delete(screenShotPath);

        ScreenCapture.CaptureScreenshot(ScreenshotName);

        StartCoroutine(delayedShare(screenShotPath, text));
    }

    /// <summary>
    /// CaptureScreenshot() runs asynchronously, so you'll need to either capture the screenshot early and wait a fixed time
    /// for it to save, or set a unique image name and check if the file has been created yet before sharing.
    /// </summary>
    /// <param name="screenShotPath">Path the screenshot was saved to</param>
    /// <param name="text">Text to share with the screenshot</param>
    IEnumerator delayedShare(string screenShotPath, string text)
    {
        while (!File.Exists(screenShotPath))
        {
            yield return new WaitForSecondsRealtime(0.05f);
        }
        NativeShare.Share(text, screenShotPath, "", "", "image/png", true, "");
    }
}
