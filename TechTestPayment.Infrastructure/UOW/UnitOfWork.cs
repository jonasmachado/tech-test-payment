using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TechTestPayment.Cross.Enums;
using TechTestPayment.Cross.Exceptions;
using TechTestPayment.Domain.Abstractions.Repositories;
using TechTestPayment.Domain.Abstractions.UOW;
using TechTestPayment.Domain.Entities;
using TechTestPayment.Infrastructure.Context;
using TechTestPayment.Infrastructure.Repositories;

namespace TechTestPayment.Infrastructure.UOW
{
    public class UnitOfWork(TechTestPaymentContext context) : IUnitOfWork
    {
        protected TechTestPaymentContext Context { get; } = context;

        private OrderRepository? _orderRepository;
        private BaseRepository<Seller>? _sellerRepository;
        private BaseRepository<OrderProduct>? _orderProductRepository;
        private BaseRepository<Product>? _productRepository;

        public IOrderRepository OrderRepository
        {
            get
            {
                _orderRepository ??= new OrderRepository(Context);
                return _orderRepository;
            }
        }

        public IBaseRepository<Product> ProductRepository
        {
            get
            {
                _productRepository ??= new BaseRepository<Product>(Context);
                return _productRepository;
            }
        }

        public IBaseRepository<Seller> SellerRepository
        {
            get
            {
                _sellerRepository ??= new BaseRepository<Seller>(Context);
                return _sellerRepository;
            }
        }

        public IBaseRepository<OrderProduct> OrderProductRepository
        {
            get
            {
                _orderProductRepository ??= new BaseRepository<OrderProduct>(Context);
                return _orderProductRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void SetModified<T>(T entity)
        {
            if (entity is null)
                throw new DatabaseException(ErrorCodes.ModifyNullEntityAttempt);

            Context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await Context.Database.BeginTransactionAsync();
        }

        public void SetDeleted<T>(T entity)
        {
            if (entity is null)
                throw new DatabaseException(ErrorCodes.ModifyNullEntityAttempt);

            Context.Entry(entity).State = EntityState.Deleted;
        }
    }
}
