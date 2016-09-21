using IC.DTO;

namespace IC.Framework.Exception
{
    public class SIPException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SIPException" /> class.
        /// </summary>
        public SIPException()
            : base()
        {
            this.Result = new OperationResult();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SIPException" /> class.
        /// </summary>
        /// <param name="code">Error code  will display</param>
        /// <param name="message">Error string will display</param>
        public SIPException(MessageCode code, string message)
            : base(message)
        {
            this.Result = new OperationResult();
            this.Result.Success = false;
            this.Result.MCode = code;
            this.Result.Message = message;
        }

        /// <summary>
        ///  Gets or sets the name of the Result, represent result information.
        /// </summary>
        public OperationResult Result { get; set; }
    }
}
