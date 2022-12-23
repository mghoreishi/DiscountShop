using EventBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IntegrationEventLogEF.Services
{
    /// <summary>
    /// When the data changes in the first microservice, the code submits the data to the database and then publishes an IntegrationEvent.
    /// If the service crashes after the database is updated, After the code is executed, but before the integration event is successfully released), the local microservice data has been successfully updated, but the integration event is not released.
    /// This will cause the data defined in the first microservice to be inconsistent with the data in the second microservice.
    /// We save IntegrationEvents to ensure that the event bus can correctly forward events
    /// </summary>
    public class IntegrationEventLogService : IIntegrationEventLogService, IDisposable
    {
        private readonly IntegrationEventLogContext _integrationEventLogContext;
        private readonly DbConnection _dbConnection;
        private readonly List<Type> _eventTypes;
        private volatile bool disposedValue;

        public IntegrationEventLogService(DbConnection dbConnection, string entryAssembly)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _integrationEventLogContext = new IntegrationEventLogContext(
                new DbContextOptionsBuilder<IntegrationEventLogContext>()
                    .UseNpgsql(_dbConnection)
                    .Options);

            _eventTypes = Assembly.Load(entryAssembly)
                .GetTypes()
                .Where(t => t.Name.EndsWith(nameof(IntegrationEvent)))
                .ToList();
        }

        public async Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId)
        {
            string stringTransactionId = transactionId.ToString();

            List<IntegrationEventLogEntry> result = await _integrationEventLogContext.IntegrationEventLogs
                .Where(e => e.TransactionId == stringTransactionId && e.State == EventStateEnum.NotPublished).ToListAsync();

            if (result != null && result.Any())
            {
                return result.OrderBy(o => o.CreationTime)
                    .Select(e => e.DeserializeJsonContent(_eventTypes.Find(t => t.Name == e.EventTypeShortName)));
            }

            return new List<IntegrationEventLogEntry>();
        }

        public Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            IntegrationEventLogEntry eventLogEntry = new(@event, transaction.TransactionId);

            if (_integrationEventLogContext.Database.CurrentTransaction is null)
            {
                _integrationEventLogContext.Database.UseTransaction(transaction.GetDbTransaction());
            }

            _integrationEventLogContext.IntegrationEventLogs.Add(eventLogEntry);

            return _integrationEventLogContext.SaveChangesAsync();
        }

        public Task MarkEventAsPublishedAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, EventStateEnum.Published);
        }

        public Task MarkEventAsInProgressAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, EventStateEnum.InProgress);
        }

        public Task MarkEventAsFailedAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, EventStateEnum.PublishedFailed);
        }

        private Task UpdateEventStatus(Guid eventId, EventStateEnum status)
        {
            IntegrationEventLogEntry eventLogEntry = _integrationEventLogContext.IntegrationEventLogs.Single(ie => ie.EventId == eventId);
            eventLogEntry.State = status;

            if (status == EventStateEnum.InProgress)
            {
                eventLogEntry.TimesSent++;
            }

            _integrationEventLogContext.IntegrationEventLogs.Update(eventLogEntry);

            return _integrationEventLogContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _integrationEventLogContext?.Dispose();
                }


                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
