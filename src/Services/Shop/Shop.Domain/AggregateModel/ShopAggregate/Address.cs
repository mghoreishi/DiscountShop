using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace Shopping.Domain.AggregateModel.ShopAggregate
{
    public class Address : ValueObject
    {
        public string Value { get; }

        public Address(string value)
        {
            Value = value;
        }

        public static Result<Address, Error> Create(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Errors.General.ValueIsRequired(nameof(Address));
            }

            input = input.Trim();

            if (input.Length > 100)
            {
                return Errors.General.InvalidLength(nameof(Address));
            }

            if (Regex.IsMatch(input, @"^[^<>]*$") == false)
            {
                return Errors.General.InvalidCharacters(nameof(Address));
            }

            return new Address(input);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}



