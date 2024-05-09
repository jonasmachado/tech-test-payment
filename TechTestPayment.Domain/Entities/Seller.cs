using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechTestPayment.Domain.Entities
{
    /// <summary>
    /// Represents Salesperson's attributes.
    /// Database object model
    /// </summary>
    [Table("seller")]
    public class Seller
    {
        /// <summary>
        /// This field identifies the unique key of database object
        /// </summary>
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Document number
        /// </summary>
        [Required]
        [Column("cpf")]
        [MaxLength(11)]
        public string? Cpf { get; set; }

        /// <summary>
        /// Salesperson's name
        /// </summary>
        [Required]
        [Column("name")]
        [MaxLength(100)]
        public string? Name { get; set; }

        /// <summary>
        /// Salesperson's e-mail
        /// </summary>
        [Required]
        [Column("email")]
        [MaxLength(100)]
        public string? Email { get; set; }

        /// <summary>
        /// Salesperson's phone
        /// </summary>
        [Required]
        [Column("phone")]
        [MaxLength(20)]
        public string? Phone { get; set; }

        /// <summary>
        /// Orders collection, representing the one-to-many relationship.
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; } = [];
    }
}
