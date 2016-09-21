using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using IC.Framework.Constants;
using IC.Framework.Logging;

namespace IC.NotificationHandler
{
    public class NotificationHandler
    {
        /// <summary>
        /// The is production
        /// </summary>
        private static bool isProduction = Convert.ToBoolean(ConfigurationManager.AppSettings["IsProdEnvironment"]);

        /// <summary>
        /// Posts to ANS.
        /// </summary>
        /// <param name="mpnsToken">The MPNS Token</param>
        /// <param name="apnsToken">The APNS Token</param>
        /// <param name="title">The title</param>
        /// <param name="message">The message</param>
        /// <param name="extraDataAsCSV">The extraDataAsCSV</param>
        /// <returns>Operation result.</returns>
        public string Send(string mpnsToken, string apnsToken, string title, string message, string extraDataAsCSV = "")
        {
            string result = string.Empty;
            MPNSHandler handler = new MPNSHandler();
            if (!string.IsNullOrEmpty(mpnsToken))
            {
                foreach (var token in mpnsToken.Split(new string[] { ICConstant.PushNotTokenSeperator }, StringSplitOptions.None))
                {
                    result = handler.PostToWns(token, title, message, extraDataAsCSV);
                }
            }

            if (!string.IsNullOrEmpty(apnsToken))
            {
                System.Threading.Tasks.Task.Run(() => PostToAPNS(apnsToken, message, extraDataAsCSV, result));
            }

            return result;
        }

        /// <summary>
        /// Posts to apns.
        /// </summary>
        /// <param name="apnsToken">The apns token.</param>
        /// <param name="message">The message.</param>
        /// <param name="extraDataAsCSV">The extra data as CSV.</param>
        /// <param name="result">The result.</param>
        private void PostToAPNS(string apnsToken, string message, string extraDataAsCSV, string result)
        {
            try
            {
                var p = new List<NotificationPayload>();
                foreach (var token in apnsToken.Split(new string[] { ICConstant.PushNotTokenSeperator }, StringSplitOptions.None))
                {
                    if (!string.IsNullOrEmpty(token))
                    {
                        var payload = new NotificationPayload(token, message, 1, "default");
                        payload.AddCustom("RegionID", "IDQ10150");
                        if (!string.IsNullOrEmpty(extraDataAsCSV))
                        {
                            payload.AddCustom("Extra", extraDataAsCSV);
                        }

                        p.Add(payload);
                    }
                }

                var push = new PushNotification(!isProduction, isProduction ? ICConstant.APNSCertificatePathForProd : ICConstant.APNSCertificatePathForDev, "1234");
                var rejected = push.SendToApple(p);
                foreach (var item in rejected)
                {
                    result += "Rejected " + item;
                }

                if (string.IsNullOrEmpty(result))
                {
                    Logging.DoLog(new LogEntry()
                    {
                        MessageDetails = "Success Sedning notification for" + apnsToken,
                        LogCategory = LoggingCategory.Event,
                        LogPriority = LogPriorityID.High,
                        LogEventType = TraceEventType.Information,
                        ClassName = MethodBase.GetCurrentMethod().DeclaringType.Name,
                        MethodName = MethodBase.GetCurrentMethod().Name
                    });
                }
                else
                {
                    Logging.DoLog(new LogEntry()
                    {
                        MessageDetails = "Error: Tokens " + apnsToken + " Reason: " + result,
                        LogCategory = LoggingCategory.Error,
                        LogPriority = LogPriorityID.High,
                        LogEventType = TraceEventType.Error,
                        ClassName = MethodBase.GetCurrentMethod().DeclaringType.Name,
                        MethodName = MethodBase.GetCurrentMethod().Name
                    });
                }

            }
            catch (Exception exception)
            {
                Logging.DoLog(new LogEntry()
                {
                    MessageDetails = exception.Message,
                    LogCategory = LoggingCategory.Error,
                    LogPriority = LogPriorityID.High,
                    LogEventType = TraceEventType.Error,
                    ClassName = MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodName = MethodBase.GetCurrentMethod().Name
                });
            }
        }
    }
}
