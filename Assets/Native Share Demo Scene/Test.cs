using System;
using UnityEngine;
using System.Collections;
using System.IO;
using NativeSharing;
using UnityEngine.Events;

/*
 * https://github.com/ChrisMaire/unity-native-sharing
 */
public class Test : MonoBehaviour
{
    /// <summary>
    /// Name of the screenshot
    /// </summary>
    [SerializeField]
    string screenshotName = "screenshot.png";

    #region Events / Actions
    [Header("Screenshot Actions")]
    [SerializeField] UnityEvent OnFrameBeforeScreenshot_UnityEvent;
    [SerializeField] UnityEvent OnFrameAfterScreenshot_UnityEvent;
    public Action OnFrameBeforeScreenshot_Action;
    public Action OnFrameAfterScreenshot_Action;
    #endregion

    /// <summary>
    /// Shares a screenshot with text
    /// </summary>
    /// <param name="text">Text to share along side the screenshot</param>
    public void ShareScreenshotWithText(string text)
    {
        var screenShotPath = Application.persistentDataPath + "/" + screenshotName;
        if (File.Exists(screenShotPath)) File.Delete(screenShotPath);

        print("Screenshot path = " + screenshotName + "\r\nFull path = " + screenShotPath);

        if (OnFrameBeforeScreenshot_Action != null) OnFrameBeforeScreenshot_Action.Invoke();
        if (OnFrameBeforeScreenshot_UnityEvent != null) OnFrameBeforeScreenshot_UnityEvent.Invoke();
        ScreenCapture.CaptureScreenshot(screenshotName);

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
        if (OnFrameAfterScreenshot_Action != null) OnFrameAfterScreenshot_Action.Invoke();
        if (OnFrameAfterScreenshot_UnityEvent != null) OnFrameAfterScreenshot_UnityEvent.Invoke();
        NativeShare.Share(text, screenshotName, "", "", "image/png", true, "");
    }
}
