using FluentValidation;
using Shopping.API.Extensions;
using Shopping.Domain;
using Shopping.Domain.AggregateModel.ShopAggregate;

namespace Shopping.API.Application.Commands.CreateShop
{
    public class CreateShopValidator : AbstractValidator<CreateShopCommand>
    {
        public CreateShopValidator()
        {
            RuleFor(p => p.CategoryId)
               .GreaterThanOrEqualTo(1).WithMessage(Errors.General.ValueIsLessThanOne().Serialize());

            RuleFor(x => x.Name)
                .MustBeValueObject(ShopName.Create);

            RuleFor(x => x.Description)
                .MustBeValueObject(ShopDescription.Create);

            RuleFor(x => x.Address)
                .MustBeValueObject(Address.Create);

            RuleFor(x => x.Phone)
                .MustBeValueObject(Phone.Create);
        }
    }
}
