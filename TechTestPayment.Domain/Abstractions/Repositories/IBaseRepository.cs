using System.Linq.Expressions;
using TechTestPayment.Cross.Enums;

namespace TechTestPayment.Domain.Abstractions.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Insert.
        /// </summary>
        /// <param name="objEntity"></param>
        Task InsertAsync(TEntity objEntity);

        /// <summary>
        /// Get queryable of collection.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> CollectionAsQueryable();

        /// <summary>
        /// Find Where async.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Checks if a data exists in database.
        /// </summary>
        /// <param name="predicate">Predicate search</param>
        /// <param name="errorCode">Error to be used in case of data does not exist</param>
        /// <returns></returns>
        Task ThrowIfNotExistsAsync(Expression<Func<TEntity, bool>> predicate, ErrorCodes errorCode);

        /// <summary>
        /// Checks if a data exists in database.
        /// </summary>
        /// <param name="predicate">Predicate search</param>
        /// <param name="errorCode">Error to be used in case of data does not exist</param>
        /// <returns></returns>
        Task ThrowIfExistsAsync(Expression<Func<TEntity, bool>> predicate, ErrorCodes errorCode);

        /// <summary>
        /// Find Where.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }

}
