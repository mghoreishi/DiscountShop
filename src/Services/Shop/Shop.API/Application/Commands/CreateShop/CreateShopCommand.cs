using CSharpFunctionalExtensions;
using MediatR;

namespace Shopping.API.Application.Commands.CreateShop
{
    public record CreateShopCommand : IRequest<Result>
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string Address { get; init; }
        public string Phone { get; init; }
        public long CategoryId { get; init; }
    }
}
