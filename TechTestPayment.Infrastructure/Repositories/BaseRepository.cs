using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechTestPayment.Cross.Enums;
using TechTestPayment.Cross.Exceptions;
using TechTestPayment.Domain.Abstractions.Repositories;
using TechTestPayment.Infrastructure.Context;

namespace TechTestPayment.Infrastructure.Repositories
{
    public class BaseRepository<TEntity>(TechTestPaymentContext context) : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly TechTestPaymentContext Context = context;

        public async Task InsertAsync(TEntity objEntity)
        {
            await Context.Set<TEntity>().AddAsync(objEntity);
        }

        public IQueryable<TEntity> CollectionAsQueryable()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public virtual async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual List<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        public async Task ThrowIfNotExistsAsync(Expression<Func<TEntity, bool>> predicate, ErrorCodes errorCode)
        {
            if ((await Context.Set<TEntity>().FirstOrDefaultAsync(predicate)) is null)
                throw new DatabaseException(errorCode);
        }

        public async Task ThrowIfExistsAsync(Expression<Func<TEntity, bool>> predicate, ErrorCodes errorCode)
        {
            if ((await Context.Set<TEntity>().FirstOrDefaultAsync(predicate)) is not null)
                throw new DatabaseException(errorCode);
        }
    }
}
