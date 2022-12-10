using CSharpFunctionalExtensions;
using MediatR;
using Discounting.Domain.AggregateModel.DiscountAggregate;

namespace Discounting.API.Application.Commands.CreateDiscount
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, Result>
    {
        private readonly IDiscountRepository _DiscountRepository;
        private readonly ILogger<CreateDiscountCommandHandler> _logger;


        public CreateDiscountCommandHandler(IDiscountRepository DiscountRepository,
            ILogger<CreateDiscountCommandHandler> logger)
        {
            _DiscountRepository = DiscountRepository ?? throw new ArgumentNullException(nameof(DiscountRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            Discount model = new(
             discountName: DiscountName.Create(request.Name).Value,
             discountDescription: DiscountDescription.Create(request.Name).Value,
             shopId:request.ShopId
             );

            await _DiscountRepository.AddAsync(model);
            await _DiscountRepository.UnitOfWork.SaveChangesAsync();

            _logger.LogInformation($"Discount {model.Id} is successfully created.");

            return Result.Success();
        }
    }
}
