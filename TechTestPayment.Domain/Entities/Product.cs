using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechTestPayment.Cross.Enums;
using TechTestPayment.Cross.Exceptions;

namespace TechTestPayment.Domain.Entities
{
    /// <summary>
    /// Represents Products attributes.
    /// Database object model
    /// </summary>
    [Table("product")]
    public class Product
    {
        /// <summary>
        /// Identifies the unique key of database object.
        /// </summary>
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        [Required]
        [Column("name")]
        [MaxLength(100)]
        public string? Name { get; set; }

        /// <summary>
        /// Items remaining in stock.
        /// </summary>
        [Required]
        [Column("remaining")]
        public double ItemsRemaining { get; set; }

        /// <summary>
        /// Total price of the product.
        /// </summary>
        [Required]
        [Column("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Collection of OrderProduct, representing the many-to-many relationship.
        /// </summary>
        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = [];

        /// <summary>
        /// Withdraws stock from the product.
        /// </summary>
        /// <param name="amount">Amount to be drawn from stock</param>
        /// <exception cref="BusinessException">Thrown when requested amount is higher than actual available number</exception>
        public void WithdrawStock(double amount)
        {
            if (ItemsRemaining < amount)
            {
                throw new BusinessException(ErrorCodes.InsufficientStock);
            }

            ItemsRemaining -= amount;
        }
    }
}
