using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityNativeShareKit
{
    public class ShareText : MonoBehaviour
    {
        /// <summary>
        /// Text to share
        /// </summary>
        [SerializeField]
        string textToShare = "";

        /// <summary>
        /// Called via a button to share the text to an app
        /// </summary>
        public void ShareText_Button()
        {
            ShareTextToApp(textToShare);
        }

        /// <summary>
        /// Shares the text to an app
        /// </summary>
        /// <param name="text">Text to share</param>
        public void ShareTextToApp(string text)
        {
            //TODO
        }
    }
}
