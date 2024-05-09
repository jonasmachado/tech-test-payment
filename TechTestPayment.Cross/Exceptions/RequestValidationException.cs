using TechTestPayment.Cross.Enums;

namespace TechTestPayment.Cross.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when a request validation fails.
    /// </summary>
    public class RequestValidationException : TechTestPaymentException
    {
        /// <summary>
        /// Details of the exception.
        /// </summary>
        /// <param name="errorCode">A known error</param>
        /// <param name="details">Error details</param>
        public RequestValidationException(ErrorCodes errorCode, string details) : base(errorCode)
        {
            Details = details;
        }
    }
}
