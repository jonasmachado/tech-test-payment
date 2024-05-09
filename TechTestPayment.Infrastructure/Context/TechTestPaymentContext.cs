using Microsoft.EntityFrameworkCore;
using TechTestPayment.Domain.Entities;

namespace TechTestPayment.Infrastructure.Context
{
    /// <summary>
    /// Database Context.
    /// </summary>
    public class TechTestPaymentContext : DbContext
    {
        /// <summary>
        /// Default ctor, used by EF migrations and Tests 
        /// </summary>
        public TechTestPaymentContext() { }

        /// <summary>
        /// Injected Ctor
        /// </summary>
        public TechTestPaymentContext(DbContextOptions<TechTestPaymentContext> options) : base(options) { }

        /// <summary>
        /// Performs on model creating
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("tech_test_payment");

            modelBuilder.Entity<Order>()
             .HasMany(o => o.OrderProducts)
             .WithOne(op => op.Order)
             .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderProducts)
                .WithOne(op => op.Product)
                .HasForeignKey(op => op.ProductId);

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderProducts)
                .WithOne(op => op.Order)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderProducts)
                .WithOne(op => op.Product)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Seller>()
                .HasMany(s => s.Orders)
                .WithOne(o => o.Seller)
                .HasForeignKey(o => o.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Seller>()
                .HasIndex(x => x.Cpf)
                .IsUnique();

            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18, 2)");

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Product DB set.
        /// </summary>
        DbSet<Product>? Product { get; set; }

        /// <summary>
        /// Sale DB set.
        /// </summary>
        DbSet<Order>? Sale { get; set; }

        /// <summary>
        /// Sale Product DB set.
        /// </summary>
        DbSet<OrderProduct>? SaleProduct { get; set; }

        /// <summary>
        /// Seller DB set.
        /// </summary>
        DbSet<Seller>? Seller { get; set; }
    }
}
