
namespace IC.DTO
{
    public class OperationResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the item is true or not.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the name of the MCode
        /// </summary>
        public MessageCode MCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the Message, it return message string
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the name of the Data, it return data object value.
        /// </summary>
        public object Data { get; set; }
    }
}
