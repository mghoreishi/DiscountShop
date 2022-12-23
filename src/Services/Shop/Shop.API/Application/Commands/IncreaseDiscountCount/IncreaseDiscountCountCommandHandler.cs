using CSharpFunctionalExtensions;
using MediatR;
using Shopping.Domain.AggregateModel.ShopAggregate;

namespace Shopping.API.Application.Commands.IncreaseDiscountCount
{
    public class IncreaseDiscountCountCommandHandler : IRequestHandler<IncreaseDiscountCountCommand, Result>
    {
        private readonly IShopRepository _shopRepository;
        private readonly ILogger<IncreaseDiscountCountCommandHandler> _logger;


        public IncreaseDiscountCountCommandHandler(IShopRepository shopRepository,
            ILogger<IncreaseDiscountCountCommandHandler> logger)
        {
            _shopRepository = shopRepository ?? throw new ArgumentNullException(nameof(shopRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Result> Handle(IncreaseDiscountCountCommand request, CancellationToken cancellationToken)
        {
            var shop = await _shopRepository.GetByIdAsync(request.ShopId);

            if (shop == null)
                return Result.Failure($"There is no shop with shop Id: {request.ShopId}");

            shop.IncreaseDiscountCount();
            _shopRepository.Update(shop);
            await _shopRepository.UnitOfWork.SaveChangesAsync();

            _logger.LogInformation($"Discount Count with shop Id: {shop.Id} successfully increased.");

            return Result.Success();

        }
    }
}
