using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IC.Business.DTO
{
    public class BaseDTO
    {
        /// <summary>
        /// Gets the modified date time string.
        /// </summary>
        /// <value>
        /// The modified date time string.
        /// </value>
        public string ModifiedDateTimeStr
        {
            get
            {
                return this.ModifiedDateTime.ToString("yyyy MM dd HH mm ss", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets the created date time string.
        /// </summary>
        /// <value>
        /// The created date time string.
        /// </value>
        public string CreatedDateTimeStr
        {
            get
            {
                return this.CreatedDateTime.ToString("yyyy MM dd HH mm ss", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets or sets the modified date time.
        /// </summary>
        /// <value>
        /// The modified date time.
        /// </value>
        public DateTime ModifiedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        /// <value>
        /// The created date time.
        /// </value>
        public DateTime CreatedDateTime { get; set; }
    }
}