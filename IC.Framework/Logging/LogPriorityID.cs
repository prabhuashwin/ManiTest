using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IC.Framework.Logging
{
    public enum LogPriorityID
    {
        /// <summary>
        /// Log Priority ID for Least.
        /// </summary>
        Least = 1,

        /// <summary>
        /// Log Priority ID for Very Low.
        /// </summary>
        Neglegible = 2,

        /// <summary>
        /// Log Priority ID for Low.
        /// </summary>
        Low = 3,

        /// <summary>
        /// Log Priority ID for Average.
        /// </summary>
        Average = 4,

        /// <summary>
        /// Log Priority ID for Medium.
        /// </summary>
        Medium = 5,

        /// <summary>
        /// Log Priority ID for Above Medium.
        /// </summary>
        AboveMedium = 6,

        /// <summary>
        /// Log Priority ID for High.
        /// </summary>
        High = 7,

        /// <summary>
        /// Log Priority ID for Extremely High.
        /// </summary>
        ExtremelyHigh = 8,

        /// <summary>
        /// Log Priority ID for Highest.
        /// </summary>
        Highest = 9
    }
}
