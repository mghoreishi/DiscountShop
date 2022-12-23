using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog.Context;
using Discounting.API.Extensions;
using Discounting.Infrastructure.Data;
using Discounting.API.Application.IntegrationEvents;

namespace Discounting.API.Application.Behaviors
{
    /// <summary>
    /// Begin a transaction and if the transaction is successfull it will be commited else it would be rolled back
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly DiscountContext _context;
        private readonly IDiscountingIntegrationEventService _discountingIntegrationEventService;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

        public TransactionBehavior(DiscountContext dbContext,
            IDiscountingIntegrationEventService discountingIntegrationEventService,
            ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            _context = dbContext ?? throw new ArgumentException(nameof(DiscountContext));
            _discountingIntegrationEventService = discountingIntegrationEventService ?? throw new ArgumentException(nameof(discountingIntegrationEventService));
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

      
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response = default(TResponse);
            string typeName = request.GetGenericTypeName();

            try
            {
                if (!typeName.EndsWith("Command") || _context.HasActiveTransaction)
                {
                    return await next();
                }

                Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy strategy = _context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;

                    using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await _context.BeginTransactionAsync())
                    using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
                    {
                        _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

                        response = await next();

                        _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                        await _context.CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }

                    await _discountingIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);
                throw;
            }
        }
    }
}
