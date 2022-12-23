using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EventBus;
using IntegrationEventLogEF.Services;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Discounting.Infrastructure.Data;
using IntegrationEventLogEF;

namespace Discounting.API.Application.IntegrationEvents
{
    public class DiscountingIntegrationEventService: IDiscountingIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly DiscountContext _context;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly IEventBus _eventBus;
        private readonly ILogger<DiscountingIntegrationEventService> _logger;

        public DiscountingIntegrationEventService(
            DiscountContext commentContext,
            IEventBus eventBus,
            IntegrationEventLogContext eventLogContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
            ILogger<DiscountingIntegrationEventService> logger)
        {
            _context = commentContext ?? throw new ArgumentNullException(nameof(commentContext));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventLogService = _integrationEventLogServiceFactory(_context.Database.GetDbConnection());
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            System.Collections.Generic.List<IntegrationEventLogEntry> integrationEventLogEntries = (await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId)).ToList();

            foreach (IntegrationEventLogEntry integrationEventLogEntry in integrationEventLogEntries)
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", integrationEventLogEntry.EventId, Program.AppName, integrationEventLogEntry.IntegrationEvent);

                try
                {
                    await _eventLogService.MarkEventAsInProgressAsync(integrationEventLogEntry.EventId);
                    _eventBus.Publish(integrationEventLogEntry.IntegrationEvent);
                    await _eventLogService.MarkEventAsPublishedAsync(integrationEventLogEntry.EventId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}", integrationEventLogEntry.EventId, Program.AppName);

                    await _eventLogService.MarkEventAsFailedAsync(integrationEventLogEntry.EventId);
                }
            }
        }

      

        public async Task AddAndSaveEventAsync(IntegrationEvent integrationEvent)
        {
            _logger.LogInformation("Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", integrationEvent.Id, integrationEvent);

            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _context.GetCurrentTransaction();
            if (transaction == null)
            {
                transaction = await _context.BeginTransactionAsync();
                await _context.SaveChangesAsync();
            }

            await _eventLogService.SaveEventAsync(integrationEvent, transaction);
        }


    }
}
