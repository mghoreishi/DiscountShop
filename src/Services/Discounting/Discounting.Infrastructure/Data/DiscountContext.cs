using Discounting.Domain.AggregateModel.DiscountAggregate;
using Discounting.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Reflection;


namespace Discounting.Infrastructure.Data
{
    public class DiscountContext : DbContext, IUnitOfWork
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
        }

        public DbSet<Discount> Discounts { get; set; }



        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();


            ChangeTracker.AutoDetectChangesEnabled = false;
            Task<int> result = base.SaveChangesAsync(cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;

            return result;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        }



        #region - Transaction - 

        private IDbContextTransaction _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;
        public IDbContextTransaction GetCurrentTransaction()
        {
            return _currentTransaction;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return null;
            }

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (transaction != _currentTransaction)
            {
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
            }

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        #endregion
    }

}


