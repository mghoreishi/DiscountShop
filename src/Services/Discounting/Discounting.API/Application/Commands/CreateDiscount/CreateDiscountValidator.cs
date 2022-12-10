using FluentValidation;
using Discounting.API.Extensions;
using Discounting.Domain;
using Discounting.Domain.AggregateModel.DiscountAggregate;

namespace Discounting.API.Application.Commands.CreateDiscount
{
    public class CreateDiscountValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountValidator()
        {
            RuleFor(p => p.ShopId)
               .GreaterThanOrEqualTo(1).WithMessage(Errors.General.ValueIsLessThanOne().Serialize());

            RuleFor(x => x.Name)
                .MustBeValueObject(DiscountName.Create);

            RuleFor(x => x.Description)
                .MustBeValueObject(DiscountDescription.Create);
        }
    }
}
