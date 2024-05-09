using TechTestPayment.Cross.Enums;
using TechTestPayment.Cross.Extensions;

namespace TechTestPayment.Cross.Exceptions
{
    /// <summary>
    /// Base class for all exceptions in the TechTestPayment application.
    /// </summary>
    public abstract class TechTestPaymentException : Exception
    {
        /// <summary>
        /// A known error
        /// </summary>
        public ErrorCodes ErrorCode { get; }

        /// <summary>
        /// Additional details about the error.
        /// </summary>
        public string? Details { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TechTestPaymentException"/> class.
        /// </summary>
        /// <param name="errorCode">A known error</param>
        protected TechTestPaymentException(ErrorCodes errorCode) : base(errorCode.GetDescription())
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TechTestPaymentException"/> class.
        /// </summary>
        /// <param name="errorCode">A known error</param>
        /// <param name="msgArgs">Args for the message of ErrorCodes enum</param>
        protected TechTestPaymentException(ErrorCodes errorCode, params object?[] msgArgs) : base(string.Format(errorCode.GetDescription(), msgArgs))
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TechTestPaymentException"/> class.
        /// </summary>
        /// <param name="errorCode">A known error</param>
        /// <param name="inner">Inner exception</param>
        protected TechTestPaymentException(ErrorCodes errorCode, Exception inner) : base(errorCode.GetDescription(), inner)
        {
            ErrorCode = errorCode;
        }
    }
}
