using Microsoft.EntityFrameworkCore;
using TechTestPayment.Cross.Enums;
using TechTestPayment.Cross.Exceptions;
using TechTestPayment.Domain.Abstractions.Repositories;
using TechTestPayment.Domain.Entities;
using TechTestPayment.Infrastructure.Context;

namespace TechTestPayment.Infrastructure.Repositories
{
    public class OrderRepository(TechTestPaymentContext context) : BaseRepository<Order>(context), IOrderRepository
    {
        public async Task<Order> Get(int orderId)
        {
            return await Context.Set<Order>()
                .Include(o => o.Seller)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId)
                ?? throw new DatabaseException(ErrorCodes.OrderNotFound);
        }
    }
}
