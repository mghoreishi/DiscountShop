using CSharpFunctionalExtensions;
using MediatR;
using Shopping.Domain.AggregateModel.ShopAggregate;

namespace Shopping.API.Application.Commands.CreateShop
{
    public class CreateShopCommandHandler : IRequestHandler<CreateShopCommand, Result>
    {
        private readonly IShopRepository _shopRepository;
        private readonly ILogger<CreateShopCommandHandler> _logger;


        public CreateShopCommandHandler(IShopRepository shopRepository,
            ILogger<CreateShopCommandHandler> logger)
        {
            _shopRepository = shopRepository ?? throw new ArgumentNullException(nameof(shopRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result> Handle(CreateShopCommand request, CancellationToken cancellationToken)
        {
            Shop model = new(
             shopName: ShopName.Create(request.Name).Value,
             shopDescription: ShopDescription.Create(request.Name).Value,
             address: Address.Create(request.Name).Value,
             phone: Phone.Create(request.Name).Value,
             categoryId: request.CategoryId
             );

            await _shopRepository.AddAsync(model);
            await _shopRepository.UnitOfWork.SaveChangesAsync();

            _logger.LogInformation($"Shop {model.Id} is successfully created.");

            return Result.Success();
        }
    }
}
