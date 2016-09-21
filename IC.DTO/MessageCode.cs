
namespace IC.DTO
{
    public enum MessageCode
    {
        /// <summary>
        /// Represent operation failed
        /// </summary>
        OperationFailed = 0,

        /// <summary>
        /// Represent operation successful
        /// </summary>
        OperationSuccessful = 1,

        /// <summary>
        /// Represent UserDoesNotExist
        /// </summary>
        UserDoesNotExist = 2,

        /// <summary>
        /// Represent RequiredFieldsAreBlank
        /// </summary>
        RequiredFeildsAreBlank = 3,

        /// <summary>
        /// Represent NonNegative
        /// </summary>
        NonNegative = 4,

        /// <summary>
        /// Represent InvalidEmailId
        /// </summary>
        InvalidEmailId = 5,

        /// <summary>
        /// Represent ObjectNull
        /// </summary>
        ObjectNull = 6,

        /// <summary>
        /// Represent StringNullEmpty
        /// </summary>
        StringNullEmpty = 7,

        /// <summary>
        /// Represent InvalidCredentials
        /// </summary>
        InvalidCredentials = 8,

        /// <summary>
        /// Represent InvalidSession
        /// </summary>
        InvalidSession = 9
    }
}
