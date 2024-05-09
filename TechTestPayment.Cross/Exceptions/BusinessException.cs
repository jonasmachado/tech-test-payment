using TechTestPayment.Cross.Enums;

namespace TechTestPayment.Cross.Exceptions
{
    /// <summary>
    /// Represents exceptions that are thrown due to business rule violations.
    /// </summary>
    public class BusinessException : TechTestPaymentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class with a specific error code.
        /// </summary>
        /// <param name="errorCode">The error code that represents the specific business rule violation.</param>
        public BusinessException(ErrorCodes errorCode) : base(errorCode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class with a specific error code and additional message arguments.
        /// </summary>
        /// <param name="errorCode">The error code that represents the specific business rule violation.</param>
        /// <param name="msgArgs">An array of strings that contain zero or more objects that will be formatted with the error message.</param>
        public BusinessException(ErrorCodes errorCode, params object?[] msgArgs) : base(errorCode, msgArgs)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class with a specific error code and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="errorCode">The error code that represents the specific business rule violation.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public BusinessException(ErrorCodes errorCode, Exception inner) : base(errorCode, inner)
        {
        }
    }
}
