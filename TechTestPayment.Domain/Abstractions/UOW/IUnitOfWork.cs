using Microsoft.EntityFrameworkCore.Storage;
using TechTestPayment.Domain.Abstractions.Repositories;
using TechTestPayment.Domain.Entities;

namespace TechTestPayment.Domain.Abstractions.UOW
{
    /// <summary>
    /// Unit of work interface for database operations
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Repository for orders
        /// </summary>
        IOrderRepository OrderRepository { get; }

        /// <summary>
        /// Repository for sellers
        /// </summary>
        IBaseRepository<Seller> SellerRepository { get; }

        /// <summary>
        /// Repository for order products
        /// </summary>
        IBaseRepository<OrderProduct> OrderProductRepository { get; }

        /// <summary>
        /// Repository for products
        /// </summary>
        IBaseRepository<Product> ProductRepository { get; }

        /// <summary>
        /// Begins a transaction asynchronously
        /// </summary>
        /// <returns>Transaction</returns>
        Task<IDbContextTransaction> BeginTransactionAsync();

        /// <summary>
        /// Sets the entity state to modified
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="entity">Provided entity</param>
        public void SetModified<T>(T entity);

        /// <summary>
        /// Sets the entity state to added
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="entity">Provided entity</param>
        public void SetDeleted<T>(T entity);

        /// <summary>
        /// Saves the changes to the database asynchronously
        /// </summary>
        /// <returns>Task completion</returns>
        public Task SaveChangesAsync();
    }
}
