using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IntegrationEventLogEF.Utilities
{
    public class ResilientTransaction
    {
        private readonly DbContext _context;
        private ResilientTransaction(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public static ResilientTransaction New(DbContext context)
        {
            return new(context);
        }

        public async Task ExecuteAsync(Func<Task> action)
        {
            Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _context.Database.BeginTransaction();
                await action();
                transaction.Commit();
            });
        }
    }
}
