using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;


namespace Shopping.Domain.AggregateModel.ShopAggregate
{
    public class ShopName : ValueObject
    {
        public string Value { get; }

        public ShopName(string value)
        {
            Value = value;
        }

        public static Result<ShopName, Error> Create(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Errors.General.ValueIsRequired(nameof(ShopName));
            }

            input = input.Trim();

            if (input.Length > 50)
            {
                return Errors.General.InvalidLength(nameof(ShopName));
            }

            if (Regex.IsMatch(input, @"^[^<>]*$") == false)
            {
                return Errors.General.InvalidCharacters(nameof(ShopName));
            }

            return new ShopName(input);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}




