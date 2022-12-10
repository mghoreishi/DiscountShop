using CSharpFunctionalExtensions;
using FluentValidation;
using Discounting.Domain;


namespace Discounting.API.Extensions
{
    public static class RuleBuilderExtensions
    {
        /// <summary>
        /// Check that property should not be empty
        /// </summary>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> CNotEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return DefaultValidatorExtensions.NotEmpty(ruleBuilder)
                .WithMessage(Errors.General.ValueIsRequired().Serialize());
        }

        /// <summary>
        ///  Defines a length validator on the current rule builder, but only for string properties.
        ///  Validation will fail if the length of the string is outside of the specified range. The range is inclusive. 
        /// </summary>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> CLength<T>(this IRuleBuilder<T, string> ruleBuilder, int min, int max)
        {
            return DefaultValidatorExtensions.Length(ruleBuilder, min, max)
                .WithMessage(Errors.General.InvalidLength().Serialize());
        }

        /// <summary>
        /// Checks if entry property follows certain rules of entity
        /// </summary>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <param name="factoryMethod">entity's method</param>
        public static IRuleBuilderOptions<T, TElement> MustBeEntity<T, TElement, TValueObject>(
            this IRuleBuilder<T, TElement> ruleBuilder,
            Func<TElement, Result<TValueObject, Error>> factoryMethod)
            where TValueObject : Entity
        {
            return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
            {
                Result<TValueObject, Error> result = factoryMethod(value);

                if (result.IsFailure)
                {
                    context.AddFailure(result.Error.Serialize());
                }
            });
        }

        /// <summary>
        /// Checks if entry property follows certain rules of valueobject
        /// </summary>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <param name="factoryMethod">Valueobject's method</param>
        public static IRuleBuilderOptions<T, string> MustBeValueObject<T, TValueObject>(
            this IRuleBuilder<T, string> ruleBuilder,
            Func<string, Result<TValueObject, Error>> factoryMethod)
            where TValueObject : ValueObject
        {
            return (IRuleBuilderOptions<T, string>)ruleBuilder.Custom((value, context) =>
            {
                Result<TValueObject, Error> result = factoryMethod(value);

                if (result.IsFailure)
                {
                    context.AddFailure(result.Error.Serialize());
                }
            });
        }

        /// <summary>
        /// Helper to apply rules to list of items
        /// </summary>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static IRuleBuilderOptionsConditions<T, IList<TElement>> ListMustContainNumberOfItems<T, TElement>(
            this IRuleBuilder<T, IList<TElement>> ruleBuilder, int? min = null, int? max = null)
        {
            return ruleBuilder.Custom((list, context) =>
            {
                if (min.HasValue && list == null)
                {
                    context.AddFailure(Errors.General.CollectionIsTooSmall(min.Value, 0).Serialize());
                }
                else
                {
                    if (min.HasValue && list.Count < min.Value)
                    {
                        context.AddFailure(Errors.General.CollectionIsTooSmall(min.Value, list.Count).Serialize());
                    }

                    if (max.HasValue && list.Count > max.Value)
                    {
                        context.AddFailure(Errors.General.CollectionIsTooLarge(max.Value, list.Count).Serialize());
                    }
                }
            });
        }
    }
}
