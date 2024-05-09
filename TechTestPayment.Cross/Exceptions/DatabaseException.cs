using TechTestPayment.Cross.Enums;

namespace TechTestPayment.Cross.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when a database operation fails.
    /// </summary>
    /// <param name="errorCode">A known error</param>
    public class DatabaseException(ErrorCodes errorCode) : TechTestPaymentException(errorCode);
}
