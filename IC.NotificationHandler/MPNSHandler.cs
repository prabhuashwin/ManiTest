using IC.Framework.Logging;
using IC.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IC.NotificationHandler
{
    public class MPNSHandler
    {
        #region Properties.

        /// <summary>
        /// The windows application secret key to fetch details from web configuration.
        /// </summary>
        private const string WinAppSecretKey = "WinAppSecretCode";

        /// <summary>
        /// The windows application sid key to fetch details from web configuration.
        /// </summary>
        private const string WinAppSIDKey = "WinAppSID";

        /// <summary>
        /// The notification type for sending the push notification.
        /// </summary>
        private const string NotificationType = "wns/toast";

        /// <summary>
        /// The content type of the notification
        /// </summary>
        private const string ContentType = "text/xml";

        /// <summary>
        /// The access token to store the token received while authenticating with MPNS.
        /// </summary>
        private static OAuthToken accessToken;

        /// <summary>
        /// The secret
        /// </summary>
        private string secret = ConfigurationManager.AppSettings[WinAppSecretKey];

        /// <summary>
        /// The sid
        /// </summary>
        private string sid = ConfigurationManager.AppSettings[WinAppSIDKey];

        /// <summary>
        /// The default xml for sending toast notification.
        /// </summary>
        private string xml = @"<toast launch=""{&quot;type&quot;:&quot;toast&quot;:&quot;param1&quot;:&quot;12345&quot;:&quot;param2&quot;:&quot;67890&quot;}"">
    <visual>
        <binding template=""ToastImageAndText01"">            
            <text id=""1"">PLACEHOLDER1</text>
<text id=""2"">PLACEHOLDER2</text>
<text id=""3"">PLACEHOLDER3</text>
        </binding>
    </visual>
</toast>";
        #endregion

        #region Methods.
        /// <summary>
        /// Posts to WNS.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="extraDataAsCSV">The extra data as CSV.</param>
        /// <returns>Operation result.</returns>
        public string PostToWns(string uri, string title, string message, string extraDataAsCSV = "")
        {
            try
            {
                Logging.DoLog(new LogEntry()
                {
                    MessageDetails = string.Format("Notification sending for uri {0} and title {1} and message {2} ", uri, title, message),
                    LogCategory = LoggingCategory.Event,
                    LogPriority = LogPriorityID.High,
                    LogEventType = TraceEventType.Information,
                    ClassName = MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodName = MethodBase.GetCurrentMethod().Name
                });
                return Task.Run(() => this.PostToWNSTask(uri, title, message, extraDataAsCSV)).ToString();
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

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="secret">The secret.</param>
        /// <param name="sid">The sid.</param>
        /// <returns>The Authorized token.</returns>
        protected OAuthToken GetAccessToken(string secret, string sid)
        {
            var urlEncodedSecret = HttpUtility.UrlEncode(secret);
            var urlEncodedSid = HttpUtility.UrlEncode(sid);
            var body = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=notify.windows.com", urlEncodedSid, urlEncodedSecret);
            string response;
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                response = client.UploadString("https://login.live.com/accesstoken.srf", body);
            }

            return this.GetOAuthTokenFromJson(response);
        }

        /// <summary>
        /// Posts to WNS task.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="extraDataAsCSV">The extra data as CSV.</param>
        /// <returns>Operation Result.</returns>
        private string PostToWNSTask(string uri, string title, string message, string extraDataAsCSV)
        {
            try
            {
                uri.GuardNullEmpty("Uri");
                title.GuardNullEmpty("Title");
                message.GuardNullEmpty("Message");
                Logging.DoLog(new LogEntry()
                {
                    MessageDetails = string.Format("Notification sending for uri {0} and title {1} and message {2} ", uri, title, message),
                    LogCategory = LoggingCategory.Event,
                    LogPriority = LogPriorityID.High,
                    LogEventType = TraceEventType.Information,
                    ClassName = MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodName = MethodBase.GetCurrentMethod().Name
                });
                this.xml = this.xml.Replace("PLACEHOLDER1", title);
                this.xml = this.xml.Replace("PLACEHOLDER2", message);
                this.xml = this.xml.Replace("PLACEHOLDER3", extraDataAsCSV);
                if (accessToken == null)
                {
                    accessToken = this.GetAccessToken(this.secret, this.sid);
                }

                byte[] contentInBytes = Encoding.UTF8.GetBytes(this.xml);

                HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
                request.Method = "POST";
                request.Headers.Add("X-WNS-Type", NotificationType);
                request.ContentType = ContentType;
                request.Headers.Add("Authorization", string.Format("Bearer {0}", accessToken.AccessToken));

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(contentInBytes, 0, contentInBytes.Length);
                }

                using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
                {
                    return webResponse.StatusCode.ToString();
                }
            }
            catch (WebException webException)
            {
                Logging.DoLog(new LogEntry()
                {
                    MessageDetails = string.Format("Notification sending for uri {0} and title {1} and message {2} ", uri, title, message),
                    LogCategory = LoggingCategory.Error,
                    LogPriority = LogPriorityID.High,
                    LogEventType = TraceEventType.Error,
                    ClassName = MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodName = MethodBase.GetCurrentMethod().Name
                });
                if (webException.Response == null)
                {
                    return "Unknow Error";
                }

                HttpStatusCode status = ((HttpWebResponse)webException.Response).StatusCode;

                if (status == HttpStatusCode.Unauthorized)
                {
                    //// The access token you presented has expired. Get a new one and then try sending
                    //// your notification again.

                    //// Because your cached access token expires after 24 hours, you can expect to get 
                    //// this response from WNS at least once a day.

                    this.GetAccessToken(this.secret, this.sid);

                    //// We recommend that you implement a maximum retry policy.
                    return this.PostToWns(uri, title, message, extraDataAsCSV);
                }
                else if (status == HttpStatusCode.Gone || status == HttpStatusCode.NotFound)
                {
                    //// The channel URI is no longer valid.

                    //// Remove this channel from your database to prevent further attempts
                    //// to send notifications to it.

                    //// The next time that this user launches your app, request a new WNS channel.
                    //// Your app should detect that its channel has changed, which should trigger
                    //// the app to send the new channel URI to your app server.

                    return string.Empty;
                }
                else if (status == HttpStatusCode.NotAcceptable)
                {
                    //// This channel is being throttled by WNS.

                    //// Implement a retry strategy that exponentially reduces the amount of
                    //// notifications being sent in order to prevent being throttled again.

                    //// Also, consider the scenarios that are causing your notifications to be throttled. 
                    //// You will provide a richer user experience by limiting the notifications you send 
                    //// to those that add true value.

                    return string.Empty;
                }
                else
                {
                    //// WNS responded with a less common error. Log this error to assist in debugging.

                    //// You can see a full list of WNS response codes here:
                    //// http://msdn.microsoft.com/en-us/library/windows/apps/hh868245.aspx#wnsresponsecodes

                    string[] debugOutput =
                    {
                        status.ToString(),
                        webException.Response.Headers["X-WNS-Debug-Trace"],
                        webException.Response.Headers["X-WNS-Error-Description"],
                        webException.Response.Headers["X-WNS-Msg-ID"],
                        webException.Response.Headers["X-WNS-Status"]
                    };
                    return string.Join(" | ", debugOutput);
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

                return "EXCEPTION: " + exception.Message;
            }
        }

        /// <summary>
        /// Gets the o authentication token from JSON.
        /// </summary>
        /// <param name="jsonString">The JSON string.</param>
        /// <returns>Authorized Token.</returns>
        private OAuthToken GetOAuthTokenFromJson(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                var ser = new DataContractJsonSerializer(typeof(OAuthToken));
                var oAuthToken = (OAuthToken)ser.ReadObject(ms);
                return oAuthToken;
            }
        }
        #endregion

        /// <summary>
        /// Authorization Token Model class.
        /// </summary>
        [DataContract]
        public class OAuthToken
        {
            /// <summary>
            /// Gets or sets the access token.
            /// </summary>
            /// <value>
            /// The access token.
            /// </value>
            [DataMember(Name = "access_token")]
            public string AccessToken { get; set; }

            /// <summary>
            /// Gets or sets the type of the token.
            /// </summary>
            /// <value>
            /// The type of the token.
            /// </value>
            [DataMember(Name = "token_type")]
            public string TokenType { get; set; }
        }
    }
}
