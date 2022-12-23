using CSharpFunctionalExtensions;
using MediatR;

namespace Shopping.API.Application.Commands.IncreaseDiscountCount
{
    public record IncreaseDiscountCountCommand : IRequest<Result>
    {
        public IncreaseDiscountCountCommand(long shopId)
        {
            ShopId = shopId;
        }

        public long ShopId { get; init; }
    }
}
