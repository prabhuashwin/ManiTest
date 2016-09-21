using IC.DTO;
using IC.Framework.Constants;
using IC.Framework.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IC.Framework.Utilities
{
    public static class Guard
    {
        /// <summary>
        /// Determines not allowing null value.
        /// </summary>
        /// <param name="value">Determines the specified object instances</param>
        /// <param name="parameterName">Determines name of the parameter</param>
        public static void GuardNull(this object value, string parameterName = "")
        {
            if (null == value)
            {
                throw new SIPException(MessageCode.ObjectNull, string.Format(parameterName));
            }
        }

        /// <summary>
        /// Determines not allowing NullEmpty values.
        /// </summary>
        /// <param name="value">Determines the specified object instances</param>
        /// <param name="parameterName">Determines name of the parameter</param>
        public static void GuardNullEmpty(this string value, string parameterName = "")
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value.Trim()))
            {
                throw new SIPException(MessageCode.StringNullEmpty, string.Format(parameterName));
            }
        }       

        /// <summary>
        /// Rounds up.
        /// </summary>
        /// <param name="datetime">The DATETIME.</param>
        /// <param name="timespan">The timespan.</param>
        /// <returns>DateTime with rounding off to nearest time by the provided timespan</returns>
        public static DateTime RoundUp(DateTime datetime, TimeSpan timespan)
        {
            return new DateTime(((datetime.Ticks + timespan.Ticks - 1) / timespan.Ticks) * timespan.Ticks);
        }

        /// <summary>
        /// Determines not allowing any NonNegative values.
        /// </summary>
        /// <param name="value">Determines the specified object instances</param>
        /// <param name="parameterName">Determines name of the parameter</param>
        public static void GuardNonNegativeInt(this int value, string parameterName = "")
        {
            if (value < 0)
            {
                throw new SIPException(MessageCode.NonNegative, string.Format(ICConstant.GenericGaurdMsg, parameterName));
            }
        }

    }
}
