using CSharpFunctionalExtensions;
using MediatR;

namespace Discounting.API.Application.Commands.CreateDiscount
{
    public record CreateDiscountCommand : IRequest<Result>
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public long ShopId { get; init; }

    }
}
