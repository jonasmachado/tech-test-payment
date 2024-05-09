using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechTestPayment.Domain.Entities
{
    /// <summary>
    /// Many-to-Many Relation between products and orders.
    /// Database object model.
    /// </summary>
    [Table("order_product")]
    public class OrderProduct
    {
        /// <summary>
        /// This field identifies the unique key of database object.
        /// </summary>
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// [FK] Reference that identifies the order.
        /// </summary>
        [Required]
        [Column("order_id")]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        /// <summary>
        /// [FK] Reference that identifies the product.
        /// </summary>
        [Required]
        [Column("product_id")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        /// <summary>
        /// The amount of product in an order.
        /// </summary>
        [Required]
        [Column("amount")]
        public double Amount { get; set; }

        /// <summary>
        /// Virtualization of Product object.
        /// </summary>
        public virtual Product? Product { get; set; }

        /// <summary>
        /// Virtualization of order object.
        /// </summary>
        public virtual Order? Order { get; set; }
    }
}
