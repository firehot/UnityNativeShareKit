using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityNativeShareKit
{
    public class ShowAlertBox : MonoBehaviour
    {
        /// <summary>
        /// Title of the alert
        /// </summary>
        [SerializeField]
        string alertTitle = "This is a alert title";

        /// <summary>
        /// Text of the alert
        /// </summary>
        [SerializeField]
        string alertText = "This is a alert text";

        /// <summary>
        /// </summary>
        [SerializeField]
        string alertDismissButtonText = "OKIE";

        /// <summary>
        /// Shows the alert box
        /// </summary>
        public void ShowAlert()
        {
            NativeWrapper.ShowAlert(alertTitle, alertText, alertDismissButtonText);
        }

    }
}
