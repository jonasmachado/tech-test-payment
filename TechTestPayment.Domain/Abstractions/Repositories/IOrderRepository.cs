using TechTestPayment.Domain.Entities;

namespace TechTestPayment.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Abstraction for Order repository
    /// </summary>
    public interface IOrderRepository : IBaseRepository<Order>
    {
        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="orderId">order id</param>
        /// <returns>Order entity</returns>
        Task<Order> Get(int orderId);
    }
}
