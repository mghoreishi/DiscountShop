using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace Shopping.Domain.AggregateModel.ShopAggregate
{
    public class ShopDescription : ValueObject
    {
        public string Value { get; }

        public ShopDescription(string value)
        {
            Value = value;
        }

        public static Result<ShopDescription, Error> Create(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Errors.General.ValueIsRequired(nameof(ShopDescription));
            }

            input = input.Trim();

            if (input.Length > 300)
            {
                return Errors.General.InvalidLength(nameof(ShopDescription));
            }

            if (Regex.IsMatch(input, @"^[^<>]*$") == false)
            {
                return Errors.General.InvalidCharacters(nameof(ShopDescription));
            }

            return new ShopDescription(input);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}



