using CSharpFunctionalExtensions;
using MediatR;
using Discounting.Domain.AggregateModel.DiscountAggregate;
using Discounting.API.Application.IntegrationEvents;
using Discounting.API.Application.IntegrationEvents.Events;


namespace Discounting.API.Application.Commands.CreateDiscount
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, Result>
    {
        private readonly IDiscountRepository _DiscountRepository;
        private readonly IDiscountingIntegrationEventService _discountingIntegrationEventService;
        private readonly ILogger<CreateDiscountCommandHandler> _logger;


        public CreateDiscountCommandHandler(IDiscountRepository DiscountRepository,
                                            IDiscountingIntegrationEventService discountingIntegrationEventService,
                                            ILogger<CreateDiscountCommandHandler> logger)
        {
            _DiscountRepository = DiscountRepository ?? throw new ArgumentNullException(nameof(DiscountRepository));
            _discountingIntegrationEventService = discountingIntegrationEventService ?? throw new ArgumentNullException(nameof(discountingIntegrationEventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            Discount model = new(
             discountName: DiscountName.Create(request.Name).Value,
             discountDescription: DiscountDescription.Create(request.Description).Value,
             shopId: request.ShopId
             );

            await _DiscountRepository.AddAsync(model);
            await _DiscountRepository.UnitOfWork.SaveChangesAsync();

            
            await _discountingIntegrationEventService.AddAndSaveEventAsync(new IncreaseDiscountCountIntegrationEvent(request.ShopId));

            _logger.LogInformation($"Discount {model.Id} is successfully created.");

            return Result.Success();
        }

     
    }
}
