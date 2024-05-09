using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechTestPayment.Cross.Enums;
using TechTestPayment.Cross.Exceptions;
using TechTestPayment.Cross.Extensions;
using TechTestPayment.Domain.Enums;

namespace TechTestPayment.Domain.Entities
{
    /// <summary>
    /// Represents Order's attributes.
    /// Database object model.
    /// </summary>
    [Table("order")]
    public class Order
    {
        /// <summary>
        /// This field identifies the unique key of database object.
        /// </summary>
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// [FK] Reference that identifies the seller/salesperson.
        /// </summary>
        [Required]
        [ForeignKey("Seller")]
        [Column("seller_id")]
        public int SellerId { get; set; }

        /// <summary>
        /// Sale's date.
        /// </summary>
        [Required]
        [Column("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Sale's status.
        /// </summary>
        [Required]
        [Column("status")]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Virtualization of Seller Object.
        /// </summary>
        public virtual Seller? Seller { get; set; }

        /// <summary>
        /// Virtualization of Order Product Relation Collection.
        /// </summary>
        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = [];

        /// <summary>
        /// Default ctor.
        /// </summary>
        public Order()
        {
            Date = DateTime.Now;
            Status = OrderStatus.WaitingPayment;
        }

        /// <summary>
        /// Update status of the order.
        /// </summary>
        /// <param name="status">New status</param>
        public void UpdateStatus(OrderStatus status)
        {
            ValidateStatusChange(status);
            Status = status;
        }

        private readonly Dictionary<OrderStatus, List<OrderStatus>> _validStatusTransitions = new()
        {
            { OrderStatus.WaitingPayment, [OrderStatus.PaymentApproved, OrderStatus.Cancelled] },
            { OrderStatus.PaymentApproved, [OrderStatus.Sent, OrderStatus.Cancelled] },
            { OrderStatus.Sent, [OrderStatus.Delivered] }
        };

        /// <summary>
        /// Validate status change.
        /// </summary>
        /// <param name="to">Requested status</param>
        /// <exception cref="BusinessException"></exception>
        private void ValidateStatusChange(OrderStatus to)
        {
            if (_validStatusTransitions.TryGetValue(Status, out var validNextStatuses) && validNextStatuses.Contains(to))
            {
                return;
            }

            throw new BusinessException(ErrorCodes.StatusNotAllowed, Status.GetDescription(), to.GetDescription());
        }
    }
}
