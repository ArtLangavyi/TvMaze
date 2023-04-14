using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Transactions;

namespace TvMaze.Core.Extensions
{
    public static class EntityFrameworkCoreExtensions
    {
        public static async Task<List<T>> ToListWithNoLockAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default(CancellationToken), Expression<Func<T, bool>> expression = null)
        {
            List<T> result = null;
            using (TransactionScope scope = CreateTransactionScopeReadUncommittedAsync())
            {
                if (expression != null)
                {
                    query = query.Where(expression);
                }

                result = await query.ToListAsync(cancellationToken);
                scope.Complete();
            }

            return result;
        }

        public static async Task<T> FirstOrDefaultWithNoLockAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default(CancellationToken), Expression<Func<T, bool>> expression = null)
        {
            using TransactionScope scope = CreateTransactionScopeReadUncommittedAsync();
            if (expression != null)
            {
                query = query.Where(expression);
            }

            T result = await query.FirstOrDefaultAsync(cancellationToken);
            scope.Complete();
            return result;
        }

        private static TransactionScope CreateTransactionScopeReadUncommittedAsync()
        {
            return new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            }, TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
