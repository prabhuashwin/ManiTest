using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IC.Framework.Logging
{
    public class Logging
    {
        /// <summary>
        /// Static LogWriter for defaultWriter.
        /// </summary>
        private static LogWriter defaultWriter = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>();

        /// <summary>
        /// Logs a new log entry with a specific category, priority, event Id, and severity title. If the LogCategory is "Event", it logs into Event.log else it logs into Error.log.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        public static void DoLog(LogEntry logEntry)
        {
            if (defaultWriter.IsLoggingEnabled())
            {
                Dictionary<string, object> exproperties = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(logEntry.MethodName) || !string.IsNullOrEmpty(logEntry.ClassName))
                {
                    exproperties.Add(logEntry.MethodName, logEntry.ClassName);
                }

                var logEntity = new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry();
                logEntity.Categories = new string[] { logEntry.LogCategory.ToString() };
                logEntity.Message = logEntry.MessageDetails;
                logEntity.Priority = (int)logEntry.LogPriority;
                logEntity.Severity = logEntry.LogEventType;
                logEntity.TimeStamp = System.DateTime.Now;
                logEntity.ExtendedProperties = exproperties;
                defaultWriter.Write(logEntity);
            }
        }
    }
}
