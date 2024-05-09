using System.ComponentModel;

namespace TechTestPayment.Domain.Enums
{
    /// <summary>
    /// - Undefined: 0
    /// - WaitingPayment: 1
    /// - PaymentApproved: 2
    /// - Sent: 3
    /// - Delivered: 4
    /// - Cancelled: 5
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Status not defined
        /// </summary>
        [Description("Undefined")]
        Undefined = 0,

        /// <summary>
        /// Waiting payment.
        /// </summary>
        [Description("Waiting for Payment")]
        WaitingPayment = 1,

        /// <summary>
        /// Approved payment.
        /// </summary>
        [Description("Payment Approved")]
        PaymentApproved = 2,

        /// <summary>
        /// Sent.
        /// </summary>
        [Description("Sent")]
        Sent = 3,

        /// <summary>
        /// Delivered.
        /// </summary>
        [Description("Delivered")]
        Delivered = 4,

        /// <summary>
        /// Cancelled.
        /// </summary>
        [Description("Cancelled")]
        Cancelled = 5
    }
}
