using CSharpFunctionalExtensions;
using Discounting.Domain;
using System.Text.RegularExpressions;


namespace Discounting.Domain.AggregateModel.DiscountAggregate
{
    public class DiscountName : ValueObject
    {
        public string Value { get; }

        public DiscountName(string value)
        {
            Value = value;
        }

        public static Result<DiscountName, Error> Create(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Errors.General.ValueIsRequired(nameof(DiscountName));
            }

            input = input.Trim();

            if (input.Length > 50)
            {
                return Errors.General.InvalidLength(nameof(DiscountName));
            }

            if (Regex.IsMatch(input, @"^[^<>]*$") == false)
            {
                return Errors.General.InvalidCharacters(nameof(DiscountName));
            }

            return new DiscountName(input);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}




