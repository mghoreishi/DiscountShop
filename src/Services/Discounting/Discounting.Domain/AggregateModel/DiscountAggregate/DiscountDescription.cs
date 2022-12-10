using CSharpFunctionalExtensions;
using Discounting.Domain;
using System.Text.RegularExpressions;

namespace Discounting.Domain.AggregateModel.DiscountAggregate
{
    public class DiscountDescription : ValueObject
    {
        public string Value { get; }

        public DiscountDescription(string value)
        {
            Value = value;
        }

        public static Result<DiscountDescription, Error> Create(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Errors.General.ValueIsRequired(nameof(DiscountDescription));
            }

            input = input.Trim();

            if (input.Length > 300)
            {
                return Errors.General.InvalidLength(nameof(DiscountDescription));
            }

            if (Regex.IsMatch(input, @"^[^<>]*$") == false)
            {
                return Errors.General.InvalidCharacters(nameof(DiscountDescription));
            }

            return new DiscountDescription(input);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}



