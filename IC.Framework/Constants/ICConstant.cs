using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IC.Framework.Constants
{
    public class ICConstant
    {
        /// <summary>
        /// Gets or sets the GenericGaurdMsg.
        /// </summary>
        /// <value>
        /// The GenericGaurdMsg.
        /// </value>
        public const string GenericGaurdMsg = "{0}: Please provide proper value.";

        /// <summary>
        /// Gets or sets the APNS certificate path.
        /// </summary>
        /// <value>
        /// The APNS certificate path.
        /// </value>
        public static string APNSCertificatePathForDev { get; set; }

        /// <summary>
        /// Gets or sets the APNS certificate path for product.
        /// </summary>
        /// <value>
        /// The APNS certificate path for product.
        /// </value>
        public static string APNSCertificatePathForProd { get; set; }

        /// <summary>
        /// The push notification token separator
        /// </summary>
        public const string PushNotTokenSeperator = "PushNotTokenSeperator";

        public const string MsgForUserRegistration = "Your registration was a success";
    }
}
