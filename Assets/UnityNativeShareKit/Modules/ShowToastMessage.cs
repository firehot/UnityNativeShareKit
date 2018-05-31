using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityNativeShareKit
{
    public class ShowToastMessage : MonoBehaviour
    {
        /// <summary>
        /// Text to display on the toast
        /// </summary>
        [SerializeField]
        string toastText = "This is a toast";

        /// <summary>
        /// Shows a toast
        /// </summary>
        public void ShowToast()
        {
            NativeWrapper.ShowToast(toastText);
        }

        /// <summary>
        /// Shows a toast with the given text
        /// </summary>
        /// <param name="toastText">Text to display on the toast</param>
        public void ShowToast(string toastText)
        {
            NativeWrapper.ShowToast(toastText);
        }
    }
}
