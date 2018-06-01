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
        /// Message of the alert
        /// </summary>
        [SerializeField]
        string alertMessage = "This is a alert message";

        /// <summary>
        /// </summary>
        [SerializeField]
        string alertDismissButtonText = "OKIE";

        /// <summary>
        /// Shows the alert box
        /// </summary>
        public void ShowAlert()
        {
            NativeWrapper.ShowAlert(alertTitle, alertMessage, alertDismissButtonText);
        }

    }
}
