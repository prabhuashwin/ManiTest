using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IC.Framework.Logging
{
    public static class LogUtilities
    {
        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="eventMessage">The message.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        public static void LogEvent(string eventMessage, LogPriorityID priority = LogPriorityID.High, string className = "NotAvailable", string methodName = "NotAvailable")
        {
            Log(
                eventMessage,
                LoggingCategory.Event,
                priority,
                System.Diagnostics.TraceEventType.Information,
                className,
                methodName,
                0);
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        public static void LogException(System.Exception exception, LogPriorityID priority = LogPriorityID.High, string className = "NotAvailable", string methodName = "NotAvailable")
        {
            Log(
                exception.ToString(),
                LoggingCategory.Error,
                priority,
                System.Diagnostics.TraceEventType.Error,
                className,
                methodName,
                0);
        }

        /// <summary>
        /// Logs the performance parameter.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        public static void LogPerformanceParam(string message, string className = "NotAvailable", string methodName = "NotAvailable")
        {
            Log(
                message,
                LoggingCategory.PerformanceParam,
                LogPriorityID.Medium,
                System.Diagnostics.TraceEventType.Information,
                className,
                methodName,
                0);
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="loggingCategory">The logging category.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="traceEventType">Type of the trace event.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="networkId">The fire network identifier.</param>
        private static void Log(string message, LoggingCategory loggingCategory, LogPriorityID priority, System.Diagnostics.TraceEventType traceEventType, string className, string methodName, int networkId)
        {
            Logging.DoLog(new LogEntry()
            {
                MessageDetails = message,
                LogCategory = loggingCategory,
                LogPriority = priority,
                LogEventType = traceEventType,
                ClassName = "Class Name = " + className,
                MethodName = "Method Name = " + methodName,
                NetworkId = networkId
            });
        }
    }
}
