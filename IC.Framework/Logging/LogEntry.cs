using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IC.Framework.Logging
{
    public class LogEntry
    {
        /// <summary>
        /// Gets or sets the message details.
        /// </summary>
        /// <value>
        /// The message details.
        /// </value>
        public string MessageDetails { get; set; }

        /// <summary>
        /// Gets or sets the log category.
        /// </summary>
        /// <value>
        /// The log category.
        /// </value>
        public LoggingCategory LogCategory { get; set; }

        /// <summary>
        /// Gets or sets the network identifier.
        /// </summary>
        /// <value>
        /// The network identifier.
        /// </value>
        public int NetworkId { get; set; }

        /// <summary>
        /// Gets or sets the log priority.
        /// </summary>
        /// <value>
        /// The log priority.
        /// </value>
        public LogPriorityID LogPriority { get; set; }

        /// <summary>
        /// Gets or sets the type of the log event.
        /// </summary>
        /// <value>
        /// The type of the log event.
        /// </value>
        public TraceEventType LogEventType { get; set; }

        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        /// <value>
        /// The name of the method.
        /// </value>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        /// <value>
        /// The name of the class.
        /// </value>
        public string ClassName { get; set; }
    }
}
